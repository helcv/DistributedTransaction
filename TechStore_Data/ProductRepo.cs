using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore_Data
{
    public class ProductRepo
    {
        private CloudStorageAccount _storageAccount;
        private CloudTable _table;

        public ProductRepo()
        {
            _storageAccount =
           CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionStringTechProduct"));
            CloudTableClient tableClient = new CloudTableClient(new Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("BankUserTable");
            _table.CreateIfNotExists();
        }

        public IQueryable<Product> RetrieveAllProducts()
        {
            var results = from p in _table.CreateQuery<Product>()
                          where p.PartitionKey == "TechProduct"
                          select p;
            return results;
        }

        public Product RetrieveProduct (string Id)
        {
            var result = (from p in _table.CreateQuery<Product>()
                          where p.Id == Id
                          select p).SingleOrDefault();

            return result;
        }
        public void AddProduct (Product product)
        {
            TableOperation insertOperation = TableOperation.Insert(product);
            _table.Execute(insertOperation);
        }

        public void DeleteProduct(Product product)
        {
            TableOperation deleteOperation = TableOperation.Delete(product);
            _table.Execute(deleteOperation);
        }
    }
}

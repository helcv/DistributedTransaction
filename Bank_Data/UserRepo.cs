using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;

namespace Bank_Data
{
    public class UserRepo
    {
        private CloudStorageAccount _storageAccount;
        private CloudTable _table;

        public UserRepo()
        {
            _storageAccount =
           CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionStringBankUser"));
            CloudTableClient tableClient = new CloudTableClient(new Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("BankUserTable");
            _table.CreateIfNotExists();
        }

        public IQueryable<User> RetrieveAllUsers()
        {
            var results = from u in _table.CreateQuery<User>()
                          where u.PartitionKey == "BankUser"
                          select u;
            return results;
        }

        public User RetrieveUser(string Id)
        {
            var result = (from u in _table.CreateQuery<User>()
                         where u.Id == Id
                         select u).SingleOrDefault();

            return result;
        }
        public void AddUser(User user)
        {
            TableOperation insertOperation = TableOperation.Insert(user);
            _table.Execute(insertOperation);
        }

        public void DeleteUser(User user)
        {
            TableOperation deleteOperation = TableOperation.Delete(user);
            _table.Execute(deleteOperation);
        }
    }
}

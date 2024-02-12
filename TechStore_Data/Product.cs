using Microsoft.WindowsAzure.Storage.Table;
using System;


namespace TechStore_Data
{
    public class Product : TableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public Product(string Id)
        {
            this.Id = Id;
            PartitionKey = "TechProduct";
            RowKey = Id;
        }

        public Product()
        {

        }
    }
}

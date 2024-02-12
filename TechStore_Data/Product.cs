using Microsoft.WindowsAzure.Storage.Table;
using System;


namespace TechStore_Data
{
    public class Product : TableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public Product(int Id)
        {
            this.Id = Id;
            PartitionKey = "TechProduct";
            RowKey = Id.ToString();
        }

        public Product()
        {

        }
    }
}

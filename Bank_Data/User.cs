using Microsoft.WindowsAzure.Storage.Table;
using System;


namespace Bank_Data
{
    public class User : TableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }

        public User(string Id)
        {
            this.Id = Id;
            PartitionKey = "BankUser";
            RowKey = Id.ToString();
        }
        public User()
        {

        }
    }
}

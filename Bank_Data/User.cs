using Microsoft.WindowsAzure.Storage.Table;
using System;


namespace Bank_Data
{
    public class User : TableEntity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }

        public User(string UserId)
        {
            this.UserId = UserId;
            PartitionKey = "BankUser";
            RowKey = UserId;
        }
        public User()
        {

        }
    }
}

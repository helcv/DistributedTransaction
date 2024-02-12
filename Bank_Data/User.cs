using Microsoft.WindowsAzure.Storage.Table;
using System;


namespace Bank_Data
{
    public class User : TableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }

        public User(int Id)
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

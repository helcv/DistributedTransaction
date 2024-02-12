using Common;

namespace Bank
{
    public class BankServerProvider : IBank
    {
        public void Commit()
        {
            throw new System.NotImplementedException();
        }

        public void EnlistMoneyTransfer(string userId, double money)
        {
            throw new System.NotImplementedException();
        }

        public void ListClients()
        {
            throw new System.NotImplementedException();
        }

        public bool Prepare()
        {
            throw new System.NotImplementedException();
        }

        public void Rollback()
        {
            throw new System.NotImplementedException();
        }
    }
}

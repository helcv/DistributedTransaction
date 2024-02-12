using Bank_Data;
using Common;

namespace Bank
{
    public class BankServerProvider : IBank
    {
        private readonly UserRepo _repo;

        public BankServerProvider(UserRepo repo)
        {
            _repo = repo;
        }

        public BankServerProvider()
        {

        }

        public bool EmptyTable()
        {
            if (_repo.RetrieveAllUsers() == null)
                return true;
            return false;
        }

        public void SeedData()
        {    
            _repo.AddUser(new User { Balance = 5000, Id = 1, Name = "Milan" });
            _repo.AddUser(new User { Balance = 10000, Id = 2, Name = "Vlada" });
            _repo.AddUser(new User { Balance = 200, Id = 3, Name = "Stefan" });
            _repo.AddUser(new User { Balance = 5000, Id = 4, Name = "Dragan" });
            _repo.AddUser(new User { Balance = 1500, Id = 5, Name = "Rade" });
        }
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

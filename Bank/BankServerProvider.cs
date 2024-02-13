using Bank_Data;
using Common;
using System.Diagnostics;
using System.Linq;

namespace Bank
{
    public class BankServerProvider : IBank
    {
        private readonly UserRepo _repo = new UserRepo();
        private double amount;
        private string uId;

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
            _repo.AddUser(new User { Balance = 5000, Id = "1", Name = "Milan" });
            _repo.AddUser(new User { Balance = 10000, Id = "2", Name = "Vlada" });
            _repo.AddUser(new User { Balance = 200, Id = "3", Name = "Stefan" });
            _repo.AddUser(new User { Balance = 5000, Id = "4", Name = "Dragan" });
            _repo.AddUser(new User { Balance = 1500, Id = "5", Name = "Rade" });
        }
        public void Commit()
        {
            var users = _repo.RetrieveAllUsers().ToList();

            User tmp_user;

            foreach (User u in users)
            {
                if (u.Id == uId + "_prep")
                {
                    tmp_user = new User(uId);
                    tmp_user.Name = u.Name;
                    tmp_user.Balance = u.Balance;

                    _repo.DeleteUser(u);
                    _repo.DeleteUser(_repo.RetrieveUser(uId));
                    _repo.AddUser(tmp_user);
                }
            }
        }

        public void EnlistMoneyTransfer(string userId, double money)
        {
            uId = userId;
            amount = money;
        }

        public void ListClients()
        {

            var users = _repo.RetrieveAllUsers().ToList();

            foreach (User u in users)
            {
                Trace.WriteLine($"User ID - {u.Id}\nName - {u.Name}\nBalance - {u.Balance}");   //print in compute emulator
            }
        }

        public bool Prepare()
        {
            var users = _repo.RetrieveAllUsers().ToList();

            foreach (User u in users)
            {
                if (u.Id == uId && (u.Balance - amount) >= 0)
                {
                    u.Balance -= amount;
                    User userToAdd = new User(uId + "_prep");
                    userToAdd.Name = u.Name;
                    userToAdd.Balance = u.Balance;

                    _repo.AddUser(userToAdd);
                    return true;
                }
            }
            return false;
        }

        public void Rollback()
        {
            var users = _repo.RetrieveAllUsers().ToList();

            foreach (User u in users)
            {
                if (u.Id == uId + "_prep")
                {
                    _repo.DeleteUser(u);
                }
            }
        }
    }
}

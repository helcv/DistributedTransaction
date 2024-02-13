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
            var users = _repo.RetrieveAllUsers().ToList();

            foreach (User u in users)
            {
                if (u.Balance > -1)
                    return false;
            }
            return true;
        
        }

        public void SeedData()
        {    
            _repo.AddUser(new User("1") { Balance = 500000, Name = "Milan"});
            _repo.AddUser(new User("2") { Balance = 10000, Name = "Vlada" });
            _repo.AddUser(new User("3") { Balance = 200, Name = "Stefan" });
            _repo.AddUser(new User("4") { Balance = 5000, Name = "Dragan" });
            _repo.AddUser(new User("5") { Balance = 1500, Name = "Rade" });
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
                Trace.WriteLine($"User ID - {u.Id}\nName - {u.Name}\nBalance - {u.Balance}\n*****************\n\n");   //print in compute emulator
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

using Common;
using System;
using TechStore_Data;

namespace TechStore
{
    public class TechStoreServerProvider : ITechStore
    {
        private readonly ProductRepo _repo;

        public TechStoreServerProvider(ProductRepo repo)
        {
            _repo = repo;
        }
        public void SeedData()
        {  
            _repo.AddProduct(new Product { Id = 1, Name = "HP Omen", Price = 500, Quantity = 3 });
            _repo.AddProduct(new Product { Id = 2, Name = "Airpods", Price = 200, Quantity = 2 });
            _repo.AddProduct(new Product { Id = 3, Name = "Galaxy S23", Price = 800, Quantity = 1 });
            _repo.AddProduct(new Product { Id = 4, Name = "Beats", Price = 100, Quantity = 7 });
            _repo.AddProduct(new Product { Id = 5, Name = "Macbook", Price = 3500, Quantity = 1 });
        }

        public TechStoreServerProvider()
        {

        }

        public bool EmptyTable()
        {
            if (_repo.RetrieveAllProducts() == null)
                return true;
            return false;
        }
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void EnlistPurchase(string productId, int count)
        {
            throw new NotImplementedException();
        }

        public double GetItemPrice(string productId)
        {
            throw new NotImplementedException();
        }

        public void ListAvailableItems()
        {
            throw new NotImplementedException();
        }

        public bool Prepare()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}

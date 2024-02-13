using Common;
using System;
using System.Diagnostics;
using System.Linq;
using TechStore_Data;

namespace TechStore
{
    public class TechStoreServerProvider : ITechStore
    {
        private readonly ProductRepo _repo = new ProductRepo();
        private string pId;
        private int count;

        public void SeedData()
        {  
            _repo.AddProduct(new Product("1") {Name = "HP Omen", Price = 50000, Quantity = 3 });
            _repo.AddProduct(new Product("2") {Name = "Airpods", Price = 20000, Quantity = 2 });
            _repo.AddProduct(new Product("3") {Name = "Galaxy S23", Price = 800000, Quantity = 1 });
            _repo.AddProduct(new Product("4") {Name = "Adapter", Price = 100, Quantity = 7 });
            _repo.AddProduct(new Product("5") {Name = "Mouse", Price = 3500, Quantity = 1 });
        }

        public TechStoreServerProvider()
        {

        }

        public bool EmptyTable()
        {
            var products = _repo.RetrieveAllProducts().ToList();

            foreach(Product p in products)
            {
                if (p.Price > -1)
                    return false;
            }
            return true;
        }
        public void Commit()
        {
            var products = _repo.RetrieveAllProducts().ToList();
            Product temp_prod;

            foreach (Product p in products)
            {
                if (p.Id == pId + "_prep")
                {
                    temp_prod = new Product(pId);
                    temp_prod.Quantity = p.Quantity;
                    temp_prod.Name = p.Name;
                    temp_prod.Price = p.Price;

                    _repo.DeleteProduct(p);
                    _repo.DeleteProduct(_repo.RetrieveProduct(pId));
                    _repo.AddProduct(temp_prod);
                }
            }
        }

        public void EnlistPurchase(string productId, int count)
        {
            pId = productId;
            this.count = count;
        }

        public double GetProductPrice(string productId)
        {
            var products = _repo.RetrieveAllProducts().ToList();

            foreach (Product p in products)
            {
                if (p.Id == productId)
                    return p.Price;
            }

            return -1;
        }

        public void ListAvailableProducts()
        {
            var products = _repo.RetrieveAllProducts().ToList();

            foreach (Product p in products)
            {
                Trace.WriteLine($"Product ID - {p.Id}\nAmount - {p.Quantity}\nPrice - {p.Price}\nName - {p.Name}\n*****************");     //print in compute emulator
            }
        }

        public bool Prepare()
        {
            var products = _repo.RetrieveAllProducts().ToList();

            foreach (Product p in products)
            {
                if (p.Id == pId && (p.Quantity - count) >= 0)
                {
                    p.Quantity -= count;
                    Product productToAdd = new Product(pId + "_prep");
                    productToAdd.Quantity = p.Quantity;
                    productToAdd.Name = p.Name;
                    productToAdd.Price = p.Price;

                    _repo.AddProduct(productToAdd);
                    return true;
                }
            }
            return false;
        }

        public void Rollback()
        {
            var products = _repo.RetrieveAllProducts().ToList();

            foreach (Product p in products)
            {
                if (p.Id == pId + "_prep")
                {
                    _repo.DeleteProduct(p);
                }
            }
        }
    }
}

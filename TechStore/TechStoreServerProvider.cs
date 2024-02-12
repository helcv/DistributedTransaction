using Common;
using System;


namespace TechStore
{
    public class TechStoreServerProvider : ITechStore
    {
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

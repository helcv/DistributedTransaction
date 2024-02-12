using Common;
using System;


namespace TransactionCoordinator
{
    public class PurchaseServerProvider : IPurchase
    {
        public bool OrderItem(string productId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}

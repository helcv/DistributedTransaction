using Common;
using System;
using System.ServiceModel;

namespace TransactionCoordinator
{
    public class PurchaseServerProvider : IPurchase
    {
        private static IBank bank_proxy;
        private static ITechStore techStore_proxy;

        private readonly string externalBankEnpointName = "BankInput";
        private readonly string externalTechStoreEnpointName = "TechInput";

        public void ConnectToBank()
        {
            string endpoint = String.Format("net.tcp://localhost:10100/{0}", externalBankEnpointName);

            var binding = new NetTcpBinding();
            ChannelFactory<IBank> factory = new ChannelFactory<IBank>(binding, new EndpointAddress(endpoint));
            bank_proxy = factory.CreateChannel();
        }

        public void ConnectToTechStore()
        {
            string endpoint = String.Format("net.tcp://localhost:10101/{0}", externalTechStoreEnpointName);

            var binding = new NetTcpBinding();
            ChannelFactory<ITechStore> factory = new ChannelFactory<ITechStore>(binding, new EndpointAddress(endpoint));
            techStore_proxy = factory.CreateChannel();
        }

        public void ListUsers()
        {
            ConnectToBank();
            bank_proxy.ListClients();
        }

        public void ListProducts()
        {
            ConnectToTechStore();
            techStore_proxy.ListAvailableProducts();
        }

        public bool OrderItem(string productId, int productQuantity, string userId)
        {     

            if (bank_proxy.EmptyTable())    // add data if tables are empty
                bank_proxy.SeedData();

            if (techStore_proxy.EmptyTable())
                techStore_proxy.SeedData();

            techStore_proxy.EnlistPurchase(productId, productQuantity);                           // take needed data
            var productPrice = techStore_proxy.GetProductPrice(productId) * productQuantity;
            bank_proxy.EnlistMoneyTransfer(userId, productPrice);

            var techStore_prepare = techStore_proxy.Prepare();      // prepare for transfer
            var bank_prepare = bank_proxy.Prepare();

            if (!techStore_prepare || !bank_prepare)        // if user balance is too low or there is not enough products, dismiss transaction
            {
                techStore_proxy.Rollback();
                bank_proxy.Rollback();
                return false;
            }

            techStore_proxy.Commit();       // if everything is allright, commit purchase
            bank_proxy.Commit();

            bank_proxy.ListClients();                       // print data in compute emulator
            techStore_proxy.ListAvailableProducts();

            return true;
        }
    }
}

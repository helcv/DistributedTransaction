using Bank;
using Bank_Data;
using Common;
using System;
using System.ServiceModel;
using TechStore;

namespace Client
{
    class Program
    {
        public static IPurchase proxy;
        private string externalEndpointName = "PurchaseInput";

        private void Connect()
        {
            string endpoint = String.Format("net.tcp://localhost:10100/{0}", externalEndpointName);

            var binding = new NetTcpBinding();
            ChannelFactory<IPurchase> factory = new ChannelFactory<IPurchase>(binding, new EndpointAddress(endpoint));
            proxy = factory.CreateChannel();

            //Console.WriteLine("Working");
        }

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Connect();

            BankServerProvider bankProvider = new BankServerProvider();
            TechStoreServerProvider techProvider = new TechStoreServerProvider();

            if (bankProvider.EmptyTable())
                bankProvider.SeedData();

            if (techProvider.EmptyTable())
                techProvider.SeedData();
            

            
            

            
            Console.ReadLine();
            
        }
    }
}

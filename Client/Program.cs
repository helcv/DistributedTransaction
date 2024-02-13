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
            string endpoint = String.Format("net.tcp://localhost:10102/{0}", externalEndpointName);

            var binding = new NetTcpBinding();
            ChannelFactory<IPurchase> factory = new ChannelFactory<IPurchase>(binding, new EndpointAddress(endpoint));
            proxy = factory.CreateChannel();

            //Console.WriteLine("Working");
        }

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Connect();

            while (true)
            {
                proxy.ListProducts();
                proxy.ListUsers();

                Console.WriteLine("Visit Compute Emulator to see test data!\n");
                Console.WriteLine("If you want to exit pres X\n");

                Console.WriteLine("Enter User ID");
                var userId = Console.ReadLine();

                if (userId.ToLower() == "x")
                    break;

                Console.WriteLine("\nEnter Product ID");
                var productID = Console.ReadLine();

                if (productID.ToLower() == "x")
                    break;

                if (proxy.OrderItem(productID, userId))
                {
                    Console.WriteLine("\nUspesan transfer!");
                }
                else
                {
                    Console.WriteLine("\nTransfer neuspesan!");
                }
            }
            
        }
    }
}

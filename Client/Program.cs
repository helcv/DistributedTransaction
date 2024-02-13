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
                if (proxy.OrderItem("4", "3"))
                {
                    Console.WriteLine("Uspesan transfer!");
                    break;
                }
                Console.WriteLine("Transfer neuspesan!");
                break;
            }

            Console.ReadLine();
            
        }
    }
}

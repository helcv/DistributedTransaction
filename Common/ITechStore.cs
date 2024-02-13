using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface ITechStore : ITransaction
    {
        [OperationContract]
        void ListAvailableProducts();
        [OperationContract]
        void EnlistPurchase(string productId, int count);
        [OperationContract]
        double GetProductPrice(string productId);
        [OperationContract]
        bool EmptyTable();
        [OperationContract]
        void SeedData();
    }
}

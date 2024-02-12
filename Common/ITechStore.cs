using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface ITechStore : ITransaction
    {
        [OperationContract]
        void ListAvailableItems();
        [OperationContract]
        void EnlistPurchase(string productId, int count);
        [OperationContract]
        double GetItemPrice(string productId);
    }
}

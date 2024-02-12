using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface  IPurchase
    {
        [OperationContract]
        bool OrderItem(string productId, string userId);
    }
}

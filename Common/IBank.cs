using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface IBank : ITransaction
    {
        [OperationContract]
        void ListClients();
        [OperationContract]
        void EnlistMoneyTransfer(string userId, double money);
    }
}

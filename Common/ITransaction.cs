using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface ITransaction
    {
        [OperationContract]
        bool Prepare();
        [OperationContract]
        void Commit();
        [OperationContract]
        void Rollback();
    }
}

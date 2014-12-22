using System.ServiceModel;
using System.Management;
using System.Collections.Generic;

namespace Common
{
    [ServiceContract(CallbackContract = typeof(IClient))]
    public interface IService
    {
        [OperationContract(IsOneWay = true)]
        void Register(DetaliiClient a);

        [OperationContract(IsOneWay = true)]
        void Unregister(DetaliiClient a);

        [OperationContract(IsOneWay = true)]
        void Send(string a, List<DetaliiClient> b);

        [OperationContract(IsOneWay = true)]
        void SendOutput(string a, DetaliiClient b);
    }
}

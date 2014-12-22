using System.ServiceModel;
using System.Collections.Generic;
using System.Management;

namespace Common
{
    [ServiceContract]
    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void NewRegister(DetaliiClient a);

        [OperationContract(IsOneWay = true)]
        void OnRegister(string a);

        [OperationContract(IsOneWay = true)]
        void OnUnregister(DetaliiClient a);

        [OperationContract(IsOneWay = true)]
        void OnSend(string a, DetaliiClient b);

        [OperationContract(IsOneWay = true)]
        void OnReceiveOutput(string a, string b);
    }
}

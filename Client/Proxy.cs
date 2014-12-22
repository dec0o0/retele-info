using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Common;
using System.Management;


namespace Client
{
    internal class Proxy:DuplexClientBase<IService>, IService
    {

        public void Register(DetaliiClient a)
        {
            Channel.Register(a);
        }

        public void Unregister(DetaliiClient a)
        {
            Channel.Unregister(a);
        }

        public void Send(string a, List<DetaliiClient> b){
            Channel.Send(a, b);
        }

        public void SendOutput(string a, DetaliiClient b)
        {
            Channel.SendOutput(a, b);
        }
       
        public Proxy(IClient client)
            : base(new InstanceContext(client))
        {
        }

    }
}

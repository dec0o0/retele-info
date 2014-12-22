using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using Common;
using System;

namespace Server
{
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    internal class Service : IService
    {
        private static IDictionary<DetaliiClient, IClient> clients = new Dictionary<DetaliiClient, IClient>();
        private IClient client;
        private DetaliiClient me;

        public void Register(DetaliiClient a)
        {
            try
            {
                if (clients.ContainsKey(a))
                {
                    client.OnRegister("Conextiune esuata");
                }
                else
                {
                    client = OperationContext.Current.GetCallbackChannel<IClient>();
                    me = a;
                    lock(clients){
                        foreach (var b in clients)
                        {
                            b.Value.NewRegister(a);
                            client.NewRegister(b.Key);
                        }
                    }
                    clients.Add(a, client);
                    client.OnRegister("Conexiune efectuata cu succes");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Unregister(DetaliiClient a)
        {
            try
            {
                    if (clients.ContainsKey(a))
                    {
                        lock (clients)
                        {
                        clients.Remove(a);
                        foreach (var aa in clients)
                            aa.Value.OnUnregister(me);
                        }
                    }
                    client = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Send(string a, List<DetaliiClient> b)
        {
            try
            {
                lock(clients){
                foreach (DetaliiClient d in b)
                {
                    clients[d].OnSend(a, me);
                }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendOutput(string a, DetaliiClient b)
        {
            try
            {
                clients[b].OnReceiveOutput(a, me.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

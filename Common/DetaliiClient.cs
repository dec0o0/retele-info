using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class DetaliiClient
    {
        string numeStatie;

        public string NumeStatie
        {
            get { return numeStatie; }
            set { numeStatie = value; }
        }
        string ip;

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public DetaliiClient(string a, string b)
        {
            numeStatie = a;
            ip = b;
        }

        public override string ToString()
        {
            return numeStatie + " /// " + ip;
        }

        public override int  GetHashCode()
        {
            return 0;
        }

        public bool Equals(DetaliiClient a)
        {
            if (this.numeStatie == a.numeStatie && this.ip == a.ip)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != this.GetType()) return false;

            return Equals((DetaliiClient)obj);
        }

    }
}

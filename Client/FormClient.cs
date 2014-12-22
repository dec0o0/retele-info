using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using System.Management;

namespace Client
{
    public partial class FormClient : Form, IClient
    {
        private Proxy proxy;
        DetaliiClient me;

        public FormClient()
        {
            InitializeComponent();
            proxy = new Proxy(this);
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            me = (DetaliiClient)this.Tag;
            me.NumeStatie += (new Random()).Next(0, 100).ToString();
            this.Text = me.ToString();
            proxy.Register(me);
            listBox1.Items.Add("Connecting to server ...");
        }

        private bool comanda_valida(string a)
        {
            try
            {
                WqlObjectQuery query =
                    new WqlObjectQuery(a);
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(query);

                ManagementObjectCollection all = searcher.Get();
                if (all.Count > 0)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (listView1.CheckedItems.Count > 0)
                {
                    string comanda = textBox1.Text.Trim();
                    if (comanda_valida(comanda))
                    {
                        List<DetaliiClient> aux = new List<DetaliiClient>();
                        foreach (ListViewItem a in listView1.CheckedItems)
                            aux.Add(new DetaliiClient(a.SubItems[0].Text, a.SubItems[1].Text));
                        proxy.Send(comanda, aux);
                        textBox1.Text = "";
                    }
                    else
                        MessageBox.Show("Comanda invalida");
                    
                }
                else
                    MessageBox.Show("Selectati cel putin un user");
               
            }
        }

        public void NewRegister(DetaliiClient a)
        {
            ListViewItem aux = new ListViewItem(a.NumeStatie);
            aux.SubItems.Add(a.Ip);
            listView1.Items.Add(aux);
            listBox1.Items.Add("Userul " + a.ToString() + " s-a conectat.");
        }

        public void OnRegister(string a)
        {
            listBox1.Items.Add(a);
        }

        public void OnUnregister(DetaliiClient a)
        {
            listBox1.Items.Add("Userul " + a.ToString() + " s-a deconectat");
            ListViewItem aux = new ListViewItem(a.NumeStatie);
            aux.SubItems.Add(a.Ip);
            listView1.Items.Remove(aux);
            listView1.Refresh();
        }

        private void FormClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                proxy.Unregister(me);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OnSend(string a,DetaliiClient b)
        {
            listBox1.Items.Add("Server said : " + a + Environment.NewLine);
            WqlObjectQuery query =
                    new WqlObjectQuery(a);
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(query);

            StringBuilder sb = new StringBuilder();
            foreach (ManagementObject mo in searcher.Get())
            {
                sb.Append(mo.ToString());
                sb.Append(Environment.NewLine);
            }
            proxy.SendOutput(sb.ToString(), b);
        }

        public void OnReceiveOutput(string a, string b)
        {
                addToList("User " + b + " said :" + Environment.NewLine + a);
        }

        private void addToList(string a)
        {
            string[] lines = a.Split(Environment.NewLine.ToCharArray());
            foreach (string l in lines)
                listBox1.Items.Add(l);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem auxx in listView1.Items)
                auxx.Checked = false;
        }
    }
}

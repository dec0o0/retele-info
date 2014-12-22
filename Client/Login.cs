using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Collections;
using Common;

namespace Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            SelectQuery selectQuery = new
            SelectQuery("Win32_OperatingSystem");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(selectQuery);

            foreach (ManagementObject m in searcher.Get())
            {
                textBox1.Text = m["csname"].ToString(); 
            }

            ManagementObjectSearcher NetworkSearcher = new ManagementObjectSearcher("SELECT IPAddress FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");

            string sIPAddress;
            foreach (ManagementObject NetworkObj in NetworkSearcher.Get())
            {
                string[] arrIPAddress = (string[])(NetworkObj["IPAddress"]);

                sIPAddress = arrIPAddress.FirstOrDefault(s => s.Contains('.'));

                if (sIPAddress != null)
                {
                    textBox2.Text = sIPAddress;
                    break;
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DetaliiClient d = new DetaliiClient(textBox1.Text, textBox2.Text);
            FormClient fc = new FormClient();
            fc.Tag = d;
            this.Hide();
            fc.ShowDialog();
            this.Dispose();
        }
    }
}

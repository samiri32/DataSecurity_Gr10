using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Security.Cryptography.Xml;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Client
{
    public partial class Form1 : Form
    {
        X509Certificate2 certifikata = new X509Certificate2();
        RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();
        DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
        Socket klienti;
        byte[] ClientKey;
        byte[] ClientInitialVector;

        Clienti c2;

        public Form1()
        {

            InitializeComponent();
            c2 = new Clienti();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Hide();
            Register signupForm = new Register();
            signupForm.ShowDialog();
            signupForm.Dispose();
            Show();
        }

        
        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //send info to server
            string username = textBox1.Text;
            string password = textBox2.Text;
            string mesazhi = username + "." + password + "." + "1";
            string msg = c2.encrypt(mesazhi);
            c2.udpClient.Send(Encoding.UTF8.GetBytes(msg), Encoding.UTF8.GetBytes(msg).Length);

            //accept info from server
             IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            string response = Encoding.UTF8.GetString(c2.udpClient.Receive(ref remoteIPEndPoint));
            MessageBox.Show(c2.decrypt(response));
            
        }
    }
}
    


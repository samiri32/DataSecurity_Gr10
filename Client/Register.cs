using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Register : Form
    {
        Socket clientSocket;
        RSACryptoServiceProvider objRsa = new RSACryptoServiceProvider();
        DESCryptoServiceProvider objDes = new DESCryptoServiceProvider();
        //X509Certificate2 certifikata = new X509Certificate2();
        string key;
        string IV;


        Socket socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public Register()
        {
            InitializeComponent();
            clientSocket = socket();
            connect();

        }

        private void connect()
        {
            string ipaddress = "127.0.0.1";
            int portNumber = 12000;

            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipaddress), portNumber);
                clientSocket.Connect(ep);

            }
            catch
            {
                MessageBox.Show("Connection Failed");
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                send();

                Dispose();
            }
            else
            {
                this.label3.Text = "Të gjitha fushat duhet të plotësohen!";
            }
        }

        private void send()
        {
            string fullname = textBox1.Text;
            string username = textBox2.Text;
            string password = textBox3.Text;
            string register = "2";

            string msg = fullname + "." + username + "." + password + "." + register;

            //msg = encrypt(msg);
            byte[] data = Encoding.Default.GetBytes(msg);
            clientSocket.Send(data, 0, data.Length, 0);
        }

       
    }
}

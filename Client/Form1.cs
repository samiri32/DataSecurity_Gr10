using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using Server;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Client
{
    public partial class Form1 : Form
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

        public Form1()
        {
            InitializeComponent();
            clientSocket = socket();
            connect();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usrname = textBox1.Text;
            string pw = textBox2.Text;
            
        }
        private void connect()
        {
            string ipaddress = "127.0.0.1";
            int portNumber = 1200;

            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipaddress), portNumber);
                clientSocket.Connect(ep);

                //read();
                byte[] blla=Encoding.ASCII.GetBytes("samir");
                clientSocket.SendTo(blla, ep);
            }
            catch
            {
                MessageBox.Show("Connection Failed");
            }
        }
        void read()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[2048];
                    int rec = clientSocket.Receive(buffer, 0, buffer.Length, 0);
                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }
                    /*
                    Array.Resize(ref buffer, rec);
                    
                    Invoke((MethodInvoker)delegate
                    {
                        if (buffer.Length > 900)
                        {
                           // certifikata.Import(buffer);
                        }
                        else
                        {
                            string data = Encoding.Default.GetString(buffer);
                            //data = decrypt(data);
                            if (data.Substring(0, 5) == "error")
                            {
                                MessageBox.Show("Wrong Credentials");
                            }
                            else
                            {
                                var jo = JObject.Parse(verifyToken(data));
                                new Info(jo["name"].ToString(), jo["surname"].ToString(), jo["department"].ToString(), jo["pozita"].ToString(), jo["paga"].ToString()).Show();
                            }
                        }
                    });
                    
                    */
                }
                catch
                {
                    MessageBox.Show("Disconnected");
                    Application.Exit();
                }
            
        }
        }

        private void send()
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string login = "1";

            string msg = username + "." + password + "." + login;

            //msg = encrypt(msg);
            byte[] data = Encoding.Default.GetBytes(msg);
            clientSocket.Send(data, 0, data.Length, 0);
        }

    }

}

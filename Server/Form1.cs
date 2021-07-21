using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        //X509Certificate2 certifikata;
        //RSACryptoServiceProvider objRsa = new RSACryptoServiceProvider();
        //DESCryptoServiceProvider objDes = new DESCryptoServiceProvider();
        string key;
        string iv;
        Socket serverSocket;
        Socket accept;


        Socket socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public Form1()
        {

            InitializeComponent();
           
           
    }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            serverSocket = socket();
            serverSocket.Bind(new IPEndPoint(0, 1200));
            
            MessageBox.Show("Serveri filloi te degjoj ne portin 3");

            

            while (true)
                {

                    try
                    {

                        byte[] buffer = new byte[2048];
                        int rec = serverSocket.Receive(buffer, 0, buffer.Length, 0);

                        Array.Resize(ref buffer, rec);

                        string data = Encoding.Default.GetString(buffer);
                        MessageBox.Show(data);
                        //data = decrypt(data);

                        //string[] list = data.Split('.');
                    }
                    catch
                    {
                        MessageBox.Show("Connection lost");
                        Application.Exit();
                    }
                }
            
            }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
    }

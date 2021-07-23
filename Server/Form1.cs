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

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 12000);

           //Ruajtja e connection qe e marrim
            serverSocket.Bind(endpoint);   //lidhja e cdo connection ne mberritje

            MessageBox.Show("Serveri po ndegjon ne portin 12000.");

            IPEndPoint derguesi = new IPEndPoint(IPAddress.Any, 12000);   //Lidhje e cdo pajisjeje(klienti) me qfardo IP dhe porti: 12000
            EndPoint tempRemote = derguesi;     //variabla qe e ruan 

       
            while (true)
            {
               
                byte[] buffer = new byte[2048];
                int rec = serverSocket.ReceiveFrom(buffer, ref tempRemote);

                Array.Resize(ref buffer, rec);

                string data = Encoding.Default.GetString(buffer);
                textBox1.Text = data;

            }
        }

      

        private void send()
        {

            string msg = "OK";

            //msg = encrypt(msg);
            byte[] data = Encoding.Default.GetBytes(msg);
            serverSocket.Send(data, 0, data.Length, 0);
        }



    }
    }

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Register : Form
    {
        X509Certificate2 certifikata = new X509Certificate2();
        RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();
        DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
        Socket klienti;
        byte[] ClientKey;
        byte[] ClientInitialVector;
        public static string useri;
        Clienti c1;

        public Register()
        {

            InitializeComponent();
            c1 = new Clienti();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            //send info to server
            string fullname = textBox1.Text;
            string username = textBox2.Text;
            string password = textBox3.Text;
            
            string mesazhi = fullname+"."+username + "." + password + "." + "2";
            
            string msg = c1.encrypt(mesazhi);
            IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            c1.udpClient.Send(Encoding.UTF8.GetBytes(msg), Encoding.UTF8.GetBytes(msg).Length);

            //accept info from server
            Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            form1.Dispose();
            Show();

           

            /* string[] array = pergjigjja.Split('?');
             if (array[0].Equals("OK"))
             {
                 MessageBox.Show("Kredencialet jane ne rregull por duhet te verifikoni nenshkrimin");
             }*/
        }
    }
}

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


namespace Client
{
    public partial class Form1 : Form
    {
        public static string useri;
        Client c1;
        public Form1()
        {
            c1 = new Client();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
            c1.ClientSend("qwerqw");
        }
    }
    }
    


using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
       

        public Form1()
        {

            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Serveri s1 = new Serveri();

            MessageBox.Show("Serveri po degjon ne portin 12000");
        }
    }
}

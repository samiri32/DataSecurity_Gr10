using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            Server s1 = new Server();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("eqr");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalisahaApp
{
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            loginPanel loginPanel = new loginPanel();
            this.Close();
            loginPanel.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SahaSahipleriniGörüntüle sahaSahipleriniGörüntüle = new SahaSahipleriniGörüntüle();
            sahaSahipleriniGörüntüle.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ÜyeleriGörüntüle üyeleriGörüntüle = new ÜyeleriGörüntüle();
            this.Close();
            üyeleriGörüntüle.Show();
        }
    }
}

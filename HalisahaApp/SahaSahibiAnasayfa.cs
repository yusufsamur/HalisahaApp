﻿using System;
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
    public partial class SahaSahibiAnasayfa : Form
    {
        public SahaSahibiAnasayfa()
        {
            InitializeComponent();
        }

        private void SahaSahibiAnasayfa_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SahaRezervasyonlarım rezervasyonlarım = new SahaRezervasyonlarım();
            this.Hide();
            rezervasyonlarım.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SahaBilgileriniGüncelle sahaBilgileriniGüncelle = new SahaBilgileriniGüncelle();
            this.Hide();
            sahaBilgileriniGüncelle.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loginPanel loginPanel = new loginPanel();
            loginPanel.Show();
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pin_Cracker
    {
    public partial class frmAbout : Form
        {
        public frmAbout()
            {
            InitializeComponent();
            }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
            System.Diagnostics.Process.Start("http://gamesguru2.googlepages.com");
            }

        private void button1_Click(object sender, EventArgs e)
            {
            Close();
            }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
            System.Diagnostics.Process.Start("mailto:gamesguru2@gmail.com");
            }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
            System.Diagnostics.Process.Start("");
            }

        private void label3_Click(object sender, EventArgs e)
            {
            MessageBox.Show("Made the official icon.","Icon Maker",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

        private void label2_Click(object sender, EventArgs e)
            {
            MessageBox.Show("Tested and helped with 'Self Correcting'", "Program Helper", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        private void label4_Click(object sender, EventArgs e)
            {
            MessageBox.Show("Verified that my pin cracker was safe.","Program Verifier", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        private void label5_Click(object sender, EventArgs e)
            {
            MessageBox.Show("All others who helped with bugs or made suggestions!","Suggestors", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        private void pictureBox1_Click(object sender, EventArgs e)
            {
            MessageBox.Show("Thanks to spyderbyte for this great icon!","Icon",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
            try
                {
                System.Diagnostics.Process.Start("aim:goim?screenname=gamesgurupwn&message=");
                }
            catch
                {
                MessageBox.Show("AIM is not open!","AIM not open",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }
    }
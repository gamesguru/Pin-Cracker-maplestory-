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

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
           MessageBox.Show("You may use the delivery truck to deliver mesos to the following:\nsc4n14guru      - Scania\nb3r4guru         - Bera;\nbr04guru         - Broa\nw1nd14guru    - Windia\n kh41n1guru    - Khaini\nb4110c4nguru - Bellocan\nm4rd1aguru    - Mardia\nkr41d4guru     - Kradia.");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.gamesgurupwn.myfastforum.org");
        }

        private void label6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What is VF? VF is ''Version Free'' and it means that this PIN Cracker can be made to work with other future version of Maple Story. Go to Settings>Addresses to set the addresses for the current version. Look around to find the addresses.","About VF",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        }
    }
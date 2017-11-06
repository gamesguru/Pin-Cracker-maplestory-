using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Pin_Cracker.Properties;
using System.Diagnostics;

namespace Pin_Cracker
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }
        private void frmSettings_Load(object sender, EventArgs e)
        {
            UpdateSettingsFrm();
        }
        Process mapleProcess = null;
        TrainerFunctions.AllFunctions pKernel = new TrainerFunctions.AllFunctions();

        IntPtr DIALOG_ADDRESS;
        IntPtr LOGGED_IN_ADDRESS;
        IntPtr PIN_TYPE_ADDRESS;
        IntPtr GG_KILL_ADDRESS_EAX;
        IntPtr GG_KILL_ADDRESS_PUSH;

        void RefreshMapleProc()
        {
            Process[] p = Process.GetProcessesByName("MapleStory");
            if (p.Length != 0)
            {
                mapleProcess = p[0];
                pKernel.ReadProcess = mapleProcess;
                pKernel.OpenProcess();
            }
            else
                mapleProcess = null;
        }

        private void UpdateSettingsFrm()
        {
            checkBox1.Checked = Settings.Default.CloseOrNot;
            checkBox2.Checked = Settings.Default.CensoredPass;
            checkBox3.Checked = Settings.Default.CensoredPins;
            checkBox4.Checked = Settings.Default.SaveRAM;
            checkBox5.Checked = Settings.Default.FixLoginBugs;
            checkBox6.Checked = Settings.Default.boolSavePin;
            checkBox7.Checked = Settings.Default.windowed;
            comboBox1.SelectedIndex = 0;
            textBox1.Text = Settings.Default.knownID;
            textBox2.Text = Settings.Default.knownPass;
            textBox3.Text = Settings.Default.knownPin;
            textBox4.Text = Settings.Default.crackingID;
            textBox5.Text = Settings.Default.crackingPass;
            textBox6.Text = Settings.Default.crackingStartPin;
            textBox7.Text = Settings.Default.crackingEndPin;
            textBox8.Text = System.Convert.ToString(Settings.Default.DIALOG_ADDRESS, 16);
            textBox9.Text = System.Convert.ToString(Settings.Default.LOGGED_IN_ADDRESS, 16);
            textBox10.Text = System.Convert.ToString(Settings.Default.PIN_TYPE_ADDRESS, 16);
            textBox11.Text = System.Convert.ToString(Settings.Default.GG_KILL_ADDRESS_EAX, 16);
            textBox12.Text = System.Convert.ToString(Settings.Default.GG_KILL_ADDRESS_PUSH, 16);
            trackBar1.Value = Settings.Default.TrackValue;
            label1.Text = "Value: " + trackBar1.Value.ToString();
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.MainWindowTitle.ToLower() == "maplestory")
                {
                    label9.Text = p.ProcessName + ".exe";
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            UpdateSettings();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void UpdateSettings()
        {
            Settings.Default.CensoredPass = checkBox2.Checked;
            Settings.Default.CensoredPins = checkBox3.Checked;
            Settings.Default.CloseOrNot = checkBox1.Checked;
            Settings.Default.crackingEndPin = textBox7.Text;
            Settings.Default.crackingID = textBox4.Text;
            Settings.Default.crackingPass = textBox5.Text;
            Settings.Default.crackingStartPin = textBox6.Text;
            Settings.Default.knownID = textBox1.Text;
            Settings.Default.knownPass = textBox2.Text;
            Settings.Default.knownPin = textBox3.Text;
            Settings.Default.SaveRAM = checkBox4.Checked;
            Settings.Default.TrackValue = (short)trackBar1.Value;
            Settings.Default.FixLoginBugs = checkBox5.Checked;
            Settings.Default.boolSavePin = checkBox6.Checked;
            Settings.Default.windowed = checkBox7.Checked;
            Settings.Default.Save();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox3.PasswordChar = '*';
                textBox6.PasswordChar = '*';
                textBox7.PasswordChar = '*';
            }
            else
            {
                textBox3.PasswordChar = textBox1.PasswordChar;
                textBox6.PasswordChar = textBox1.PasswordChar;
                textBox7.PasswordChar = textBox1.PasswordChar;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox2.PasswordChar = '*';
                textBox5.PasswordChar = '*';
            }
            else
            {
                textBox2.PasswordChar = textBox1.PasswordChar;
                textBox5.PasswordChar = textBox1.PasswordChar;
            }
        }

        private void SettingsReset()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            checkBox7.Checked = false;
            textBox1.Text = "knownId";
            textBox2.Text = "knownPass";
            textBox3.Text = "0000";
            textBox4.Text = "crackingId";
            textBox5.Text = "crackingPass";
            textBox6.Text = "0000";
            textBox7.Text = "0000";
            trackBar1.Value = 0;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SettingsReset();
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            try
            {
                int Value = Convert.ToInt32(textBox3.Text);
                if (textBox3.Text.Length != 4)
                {
                    if (Value == 0)
                    {
                        textBox3.Text = "0000";
                    }
                    else if (Value > 0 && Value < 10)
                    {
                        textBox3.Text = "000" + textBox3.Text;
                    }
                    else if (Value >= 10 && Value < 100)
                    {
                        textBox3.Text = "00" + textBox3.Text;
                    }
                    else if (Value >= 100 && Value < 1000)
                    {
                        textBox3.Text = "0" + textBox3.Text;
                    }
                }
            }
            catch
            {
                textBox3.Text = "0000";
                MessageBox.Show("Invalid value in text box!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            try
            {
                int Value = Convert.ToInt32(textBox6.Text);
                if (textBox6.Text.Length != 4)
                {
                    if (Value == 0)
                    {
                        textBox6.Text = "0000";
                    }
                    else if (Value > 0 && Value < 10)
                    {
                        textBox6.Text = "000" + textBox6.Text;
                    }
                    else if (Value >= 10 && Value < 100)
                    {
                        textBox6.Text = "00" + textBox6.Text;
                    }
                    else if (Value >= 100 && Value < 1000)
                    {
                        textBox6.Text = "0" + textBox6.Text;
                    }
                }
            }
            catch
            {
                textBox6.Text = "0000";
                MessageBox.Show("Invalid value in text box!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            try
            {
                int Value = Convert.ToInt32(textBox7.Text);
                if (textBox7.Text.Length != 4)
                {
                    if (Value == 0)
                    {
                        textBox7.Text = "0000";
                    }
                    else if (Value > 0 && Value < 10)
                    {
                        textBox7.Text = "000" + textBox7.Text;
                    }
                    else if (Value >= 10 && Value < 100)
                    {
                        textBox7.Text = "00" + textBox7.Text;
                    }
                    else if (Value >= 100 && Value < 1000)
                    {
                        textBox7.Text = "0" + textBox7.Text;
                    }
                }
            }
            catch
            {
                textBox7.Text = "0000";
                MessageBox.Show("Invalid value in text box!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //idk why these are here...i'm lazy so just leave them

        #region ignore
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAbout About = new frmAbout();
            About.ShowDialog();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label10.Text = "Value: " + trackBar1.Value.ToString();
        }

        private void textBox8_MouseEnter(object sender, EventArgs e)
        {
            label15.Text = "This is the address which changes when the current dialog in Maple Story changes.";
        }

        private void textBox9_MouseEnter(object sender, EventArgs e)
        {
            label15.Text = "This is the address which is used to determine if you have successfully logged into an account.";
        }

        private void textBox10_MouseEnter(object sender, EventArgs e)
        {
            label15.Text = "This is the address which makes the Maple Story PIN keypad responsive to virtual keystrokes.";
        }

        private void textBox11_MouseEnter(object sender, EventArgs e)
        {
            label15.Text = "This is the famous EAX address that is used in killing GameGuard.";
        }

        private void textBox12_MouseEnter(object sender, EventArgs e)
        {
            label15.Text = "This is the famous push address that is used in killing Gameguard.";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            RefreshMapleProc();
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            textBox11.Enabled = false;
            textBox12.Enabled = false;
            button4.Enabled = false;
            try
            {
                DIALOG_ADDRESS = (IntPtr)Convert.ToInt32(textBox8.Text, 16);
                LOGGED_IN_ADDRESS = (IntPtr)Convert.ToInt32(textBox9.Text, 16);
                PIN_TYPE_ADDRESS = (IntPtr)Convert.ToInt32(textBox10.Text, 16);
                GG_KILL_ADDRESS_EAX = (IntPtr)Convert.ToInt32(textBox11.Text, 16); 
                GG_KILL_ADDRESS_PUSH = (IntPtr)Convert.ToInt32(textBox12.Text, 16);
                Ex("Addresses are valid, functionallity is undetermined. Test it.");
                button4.Enabled = true;
                textBox8.Enabled = true;
                textBox9.Enabled = true;
                textBox10.Enabled = true;
                textBox11.Enabled = true;
                textBox12.Enabled = true;
            }
            catch
            {
                Ex("One or more invalid addresses...");
                textBox8.Enabled = true;
                textBox9.Enabled = true;
                textBox10.Enabled = true;
                textBox11.Enabled = true;
                textBox12.Enabled = true;
            }

        }
        void Ex(string textToEx)
        {
            richTextBox1.AppendText(textToEx);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.Default.DIALOG_ADDRESS = Convert.ToInt32(textBox8.Text, 16);
                Settings.Default.LOGGED_IN_ADDRESS = Convert.ToInt32(textBox9.Text, 16);
                Settings.Default.PIN_TYPE_ADDRESS = Convert.ToInt32(textBox10.Text, 16);
                Settings.Default.GG_KILL_ADDRESS_EAX = Convert.ToInt32(textBox11.Text, 16);
                Settings.Default.GG_KILL_ADDRESS_PUSH = Convert.ToInt32(textBox12.Text, 16);
                Settings.Default.Save();
                Close();
            }
            catch
            {
                MessageBox.Show("You idiot! You changed the addresses after pressing the ''Test'' button. Fix them now.", "You idiot!");
            }
        }
    }
}
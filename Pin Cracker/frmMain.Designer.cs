namespace Pin_Cracker
    {
    partial class frmMain
        {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
            {
            if (disposing && (components != null))
                {
                components.Dispose();
                }
            base.Dispose(disposing);
            }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
            {
                this.components = new System.ComponentModel.Container();
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
                this.groupBox1 = new System.Windows.Forms.GroupBox();
                this.txtKnownPin = new System.Windows.Forms.TextBox();
                this.label3 = new System.Windows.Forms.Label();
                this.txtKnownPass = new System.Windows.Forms.TextBox();
                this.label2 = new System.Windows.Forms.Label();
                this.txtKnownID = new System.Windows.Forms.TextBox();
                this.label1 = new System.Windows.Forms.Label();
                this.groupBox2 = new System.Windows.Forms.GroupBox();
                this.txtCrackingEndPin = new System.Windows.Forms.TextBox();
                this.label7 = new System.Windows.Forms.Label();
                this.txtCrackingStartPin = new System.Windows.Forms.TextBox();
                this.label6 = new System.Windows.Forms.Label();
                this.txtCrackingPass = new System.Windows.Forms.TextBox();
                this.label5 = new System.Windows.Forms.Label();
                this.txtCrackingID = new System.Windows.Forms.TextBox();
                this.label4 = new System.Windows.Forms.Label();
                this.btnStart = new System.Windows.Forms.Button();
                this.btnSettings = new System.Windows.Forms.Button();
                this.btnClose = new System.Windows.Forms.Button();
                this.timer3 = new System.Windows.Forms.Timer(this.components);
                this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
                this.menuItem1 = new System.Windows.Forms.MenuItem();
                this.menuItem2 = new System.Windows.Forms.MenuItem();
                this.menuItem7 = new System.Windows.Forms.MenuItem();
                this.menuItem10 = new System.Windows.Forms.MenuItem();
                this.menuItem11 = new System.Windows.Forms.MenuItem();
                this.menuItem12 = new System.Windows.Forms.MenuItem();
                this.menuItem3 = new System.Windows.Forms.MenuItem();
                this.menuItem4 = new System.Windows.Forms.MenuItem();
                this.menuItem14 = new System.Windows.Forms.MenuItem();
                this.menuItem15 = new System.Windows.Forms.MenuItem();
                this.menuItem18 = new System.Windows.Forms.MenuItem();
                this.menuItem21 = new System.Windows.Forms.MenuItem();
                this.menuItem20 = new System.Windows.Forms.MenuItem();
                this.menuItem19 = new System.Windows.Forms.MenuItem();
                this.menuItem13 = new System.Windows.Forms.MenuItem();
                this.menuItem16 = new System.Windows.Forms.MenuItem();
                this.menuItem17 = new System.Windows.Forms.MenuItem();
                this.menuItem5 = new System.Windows.Forms.MenuItem();
                this.menuItem6 = new System.Windows.Forms.MenuItem();
                this.menuItem8 = new System.Windows.Forms.MenuItem();
                this.menuItem9 = new System.Windows.Forms.MenuItem();
                this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
                this.statusStrip1 = new System.Windows.Forms.StatusStrip();
                this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
                this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
                this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
                this.tabControl1 = new System.Windows.Forms.TabControl();
                this.tabPage1 = new System.Windows.Forms.TabPage();
                this.tabPage2 = new System.Windows.Forms.TabPage();
                this.timer1 = new System.Windows.Forms.Timer(this.components);
                this.groupBox1.SuspendLayout();
                this.groupBox2.SuspendLayout();
                this.statusStrip1.SuspendLayout();
                this.tabControl1.SuspendLayout();
                this.tabPage1.SuspendLayout();
                this.tabPage2.SuspendLayout();
                this.SuspendLayout();
                // 
                // groupBox1
                // 
                this.groupBox1.Controls.Add(this.txtKnownPin);
                this.groupBox1.Controls.Add(this.label3);
                this.groupBox1.Controls.Add(this.txtKnownPass);
                this.groupBox1.Controls.Add(this.label2);
                this.groupBox1.Controls.Add(this.txtKnownID);
                this.groupBox1.Controls.Add(this.label1);
                this.groupBox1.Location = new System.Drawing.Point(6, 6);
                this.groupBox1.Name = "groupBox1";
                this.groupBox1.Size = new System.Drawing.Size(112, 144);
                this.groupBox1.TabIndex = 0;
                this.groupBox1.TabStop = false;
                this.groupBox1.Text = "Known Info";
                // 
                // txtKnownPin
                // 
                this.txtKnownPin.Location = new System.Drawing.Point(6, 110);
                this.txtKnownPin.MaxLength = 4;
                this.txtKnownPin.Name = "txtKnownPin";
                this.txtKnownPin.Size = new System.Drawing.Size(61, 20);
                this.txtKnownPin.TabIndex = 3;
                this.txtKnownPin.Leave += new System.EventHandler(this.txtKnownPin_Leave);
                // 
                // label3
                // 
                this.label3.AutoSize = true;
                this.label3.Location = new System.Drawing.Point(7, 94);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(64, 13);
                this.label3.TabIndex = 4;
                this.label3.Text = "Known PIN:";
                // 
                // txtKnownPass
                // 
                this.txtKnownPass.Location = new System.Drawing.Point(6, 71);
                this.txtKnownPass.MaxLength = 12;
                this.txtKnownPass.Name = "txtKnownPass";
                this.txtKnownPass.Size = new System.Drawing.Size(100, 20);
                this.txtKnownPass.TabIndex = 2;
                // 
                // label2
                // 
                this.label2.AutoSize = true;
                this.label2.Location = new System.Drawing.Point(7, 55);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(92, 13);
                this.label2.TabIndex = 2;
                this.label2.Text = "Known Password:";
                // 
                // txtKnownID
                // 
                this.txtKnownID.Location = new System.Drawing.Point(6, 32);
                this.txtKnownID.MaxLength = 12;
                this.txtKnownID.Name = "txtKnownID";
                this.txtKnownID.Size = new System.Drawing.Size(100, 20);
                this.txtKnownID.TabIndex = 1;
                // 
                // label1
                // 
                this.label1.AutoSize = true;
                this.label1.Location = new System.Drawing.Point(7, 16);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(57, 13);
                this.label1.TabIndex = 0;
                this.label1.Text = "Known ID:";
                // 
                // groupBox2
                // 
                this.groupBox2.Controls.Add(this.txtCrackingEndPin);
                this.groupBox2.Controls.Add(this.label7);
                this.groupBox2.Controls.Add(this.txtCrackingStartPin);
                this.groupBox2.Controls.Add(this.label6);
                this.groupBox2.Controls.Add(this.txtCrackingPass);
                this.groupBox2.Controls.Add(this.label5);
                this.groupBox2.Controls.Add(this.txtCrackingID);
                this.groupBox2.Controls.Add(this.label4);
                this.groupBox2.Location = new System.Drawing.Point(6, 6);
                this.groupBox2.Name = "groupBox2";
                this.groupBox2.Size = new System.Drawing.Size(132, 144);
                this.groupBox2.TabIndex = 1;
                this.groupBox2.TabStop = false;
                this.groupBox2.Text = "Cracking Info";
                // 
                // txtCrackingEndPin
                // 
                this.txtCrackingEndPin.Location = new System.Drawing.Point(78, 110);
                this.txtCrackingEndPin.MaxLength = 4;
                this.txtCrackingEndPin.Name = "txtCrackingEndPin";
                this.txtCrackingEndPin.Size = new System.Drawing.Size(44, 20);
                this.txtCrackingEndPin.TabIndex = 7;
                this.txtCrackingEndPin.Leave += new System.EventHandler(this.txtCrackingEndPin_Leave);
                // 
                // label7
                // 
                this.label7.AutoSize = true;
                this.label7.Location = new System.Drawing.Point(56, 113);
                this.label7.Name = "label7";
                this.label7.Size = new System.Drawing.Size(16, 13);
                this.label7.TabIndex = 6;
                this.label7.Text = "to";
                // 
                // txtCrackingStartPin
                // 
                this.txtCrackingStartPin.Location = new System.Drawing.Point(6, 110);
                this.txtCrackingStartPin.MaxLength = 4;
                this.txtCrackingStartPin.Name = "txtCrackingStartPin";
                this.txtCrackingStartPin.Size = new System.Drawing.Size(44, 20);
                this.txtCrackingStartPin.TabIndex = 6;
                this.txtCrackingStartPin.Leave += new System.EventHandler(this.txtCrackingStartPin_Leave);
                // 
                // label6
                // 
                this.label6.AutoSize = true;
                this.label6.Location = new System.Drawing.Point(6, 94);
                this.label6.Name = "label6";
                this.label6.Size = new System.Drawing.Size(58, 13);
                this.label6.TabIndex = 4;
                this.label6.Text = "PINS to try";
                // 
                // txtCrackingPass
                // 
                this.txtCrackingPass.Location = new System.Drawing.Point(6, 71);
                this.txtCrackingPass.MaxLength = 12;
                this.txtCrackingPass.Name = "txtCrackingPass";
                this.txtCrackingPass.Size = new System.Drawing.Size(116, 20);
                this.txtCrackingPass.TabIndex = 5;
                // 
                // label5
                // 
                this.label5.AutoSize = true;
                this.label5.Location = new System.Drawing.Point(6, 55);
                this.label5.Name = "label5";
                this.label5.Size = new System.Drawing.Size(78, 13);
                this.label5.TabIndex = 2;
                this.label5.Text = "Cracking Pass:";
                // 
                // txtCrackingID
                // 
                this.txtCrackingID.Location = new System.Drawing.Point(6, 32);
                this.txtCrackingID.MaxLength = 12;
                this.txtCrackingID.Name = "txtCrackingID";
                this.txtCrackingID.Size = new System.Drawing.Size(116, 20);
                this.txtCrackingID.TabIndex = 4;
                // 
                // label4
                // 
                this.label4.AutoSize = true;
                this.label4.Location = new System.Drawing.Point(6, 16);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(66, 13);
                this.label4.TabIndex = 0;
                this.label4.Text = "Cracking ID:";
                // 
                // btnStart
                // 
                this.btnStart.Enabled = false;
                this.btnStart.Location = new System.Drawing.Point(15, 188);
                this.btnStart.Name = "btnStart";
                this.btnStart.Size = new System.Drawing.Size(37, 19);
                this.btnStart.TabIndex = 3;
                this.btnStart.Text = "Start";
                this.toolTip1.SetToolTip(this.btnStart, "Start cracking the account.");
                this.btnStart.UseVisualStyleBackColor = true;
                this.btnStart.Click += new System.EventHandler(this.button1_Click);
                // 
                // btnSettings
                // 
                this.btnSettings.Location = new System.Drawing.Point(63, 188);
                this.btnSettings.Name = "btnSettings";
                this.btnSettings.Size = new System.Drawing.Size(55, 19);
                this.btnSettings.TabIndex = 5;
                this.btnSettings.TabStop = false;
                this.btnSettings.Text = "Settings";
                this.toolTip1.SetToolTip(this.btnSettings, "Change various settings for the pin cracker.");
                this.btnSettings.UseVisualStyleBackColor = true;
                this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
                // 
                // btnClose
                // 
                this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
                this.btnClose.Location = new System.Drawing.Point(128, 188);
                this.btnClose.Name = "btnClose";
                this.btnClose.Size = new System.Drawing.Size(34, 19);
                this.btnClose.TabIndex = 6;
                this.btnClose.Text = "Close";
                this.toolTip1.SetToolTip(this.btnClose, "Close the application.");
                this.btnClose.UseVisualStyleBackColor = true;
                this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
                // 
                // timer3
                // 
                this.timer3.Interval = 30000;
                this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
                // 
                // mainMenu1
                // 
                this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem6,
            this.menuItem8});
                // 
                // menuItem1
                // 
                this.menuItem1.Index = 0;
                this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem7,
            this.menuItem3,
            this.menuItem4,
            this.menuItem18,
            this.menuItem5});
                this.menuItem1.Text = "File";
                this.menuItem1.Popup += new System.EventHandler(this.menuItem1_Popup);
                // 
                // menuItem2
                // 
                this.menuItem2.Index = 0;
                this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.F2;
                this.menuItem2.Text = "Start";
                this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
                // 
                // menuItem7
                // 
                this.menuItem7.Index = 1;
                this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem10,
            this.menuItem11,
            this.menuItem12});
                this.menuItem7.Text = "Settings";
                this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
                // 
                // menuItem10
                // 
                this.menuItem10.Index = 0;
                this.menuItem10.Text = "Censored Pass";
                this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
                // 
                // menuItem11
                // 
                this.menuItem11.Index = 1;
                this.menuItem11.Text = "Censored Pin";
                this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
                // 
                // menuItem12
                // 
                this.menuItem12.Index = 2;
                this.menuItem12.Text = "Save RAM";
                this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
                // 
                // menuItem3
                // 
                this.menuItem3.Index = 2;
                this.menuItem3.Text = "Pause";
                this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
                // 
                // menuItem4
                // 
                this.menuItem4.Index = 3;
                this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem14,
            this.menuItem15});
                this.menuItem4.Text = "Contact Me";
                // 
                // menuItem14
                // 
                this.menuItem14.Index = 0;
                this.menuItem14.Text = "Via Email";
                this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
                // 
                // menuItem15
                // 
                this.menuItem15.Index = 1;
                this.menuItem15.Text = "Via AIM";
                this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
                // 
                // menuItem18
                // 
                this.menuItem18.Index = 4;
                this.menuItem18.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem21,
            this.menuItem20,
            this.menuItem19,
            this.menuItem13,
            this.menuItem16,
            this.menuItem17});
                this.menuItem18.Text = "Maple Proc Related";
                // 
                // menuItem21
                // 
                this.menuItem21.Index = 0;
                this.menuItem21.Text = "Close Maple";
                this.menuItem21.Click += new System.EventHandler(this.menuItem21_Click);
                // 
                // menuItem20
                // 
                this.menuItem20.Index = 1;
                this.menuItem20.Text = "Suspend Maple";
                this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
                // 
                // menuItem19
                // 
                this.menuItem19.Index = 2;
                this.menuItem19.Text = "Inject PIN Type";
                this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
                // 
                // menuItem13
                // 
                this.menuItem13.Index = 3;
                this.menuItem13.Text = "Get Dialog Value";
                this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
                // 
                // menuItem16
                // 
                this.menuItem16.Index = 4;
                this.menuItem16.Text = "Kill GameGuard";
                this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
                // 
                // menuItem17
                // 
                this.menuItem17.Index = 5;
                this.menuItem17.Text = "Set Maple\'s Priority";
                this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
                // 
                // menuItem5
                // 
                this.menuItem5.Index = 5;
                this.menuItem5.Text = "Exit";
                this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
                // 
                // menuItem6
                // 
                this.menuItem6.Index = 1;
                this.menuItem6.Text = "About";
                this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
                // 
                // menuItem8
                // 
                this.menuItem8.Index = 2;
                this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9});
                this.menuItem8.Text = "Help";
                // 
                // menuItem9
                // 
                this.menuItem9.Index = 0;
                this.menuItem9.Text = "Process Problems";
                this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
                // 
                // toolTip1
                // 
                this.toolTip1.AutoPopDelay = 0;
                this.toolTip1.InitialDelay = 25;
                this.toolTip1.ReshowDelay = 10;
                // 
                // statusStrip1
                // 
                this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel2});
                this.statusStrip1.Location = new System.Drawing.Point(0, 214);
                this.statusStrip1.Name = "statusStrip1";
                this.statusStrip1.Size = new System.Drawing.Size(180, 22);
                this.statusStrip1.SizingGrip = false;
                this.statusStrip1.TabIndex = 7;
                this.statusStrip1.Text = "statusStrip1";
                // 
                // toolStripStatusLabel1
                // 
                this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
                this.toolStripStatusLabel1.Size = new System.Drawing.Size(83, 17);
                this.toolStripStatusLabel1.Text = "Last Pin: #NULL";
                // 
                // toolStripProgressBar1
                // 
                this.toolStripProgressBar1.Maximum = 9999;
                this.toolStripProgressBar1.Name = "toolStripProgressBar1";
                this.toolStripProgressBar1.Size = new System.Drawing.Size(90, 16);
                // 
                // toolStripStatusLabel2
                // 
                this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
                this.toolStripStatusLabel2.Size = new System.Drawing.Size(109, 17);
                this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
                // 
                // tabControl1
                // 
                this.tabControl1.Controls.Add(this.tabPage1);
                this.tabControl1.Controls.Add(this.tabPage2);
                this.tabControl1.Location = new System.Drawing.Point(15, 1);
                this.tabControl1.Name = "tabControl1";
                this.tabControl1.SelectedIndex = 0;
                this.tabControl1.Size = new System.Drawing.Size(147, 181);
                this.tabControl1.TabIndex = 8;
                // 
                // tabPage1
                // 
                this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
                this.tabPage1.Controls.Add(this.groupBox2);
                this.tabPage1.Location = new System.Drawing.Point(4, 22);
                this.tabPage1.Name = "tabPage1";
                this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage1.Size = new System.Drawing.Size(139, 155);
                this.tabPage1.TabIndex = 0;
                this.tabPage1.Text = "Cracking Info";
                // 
                // tabPage2
                // 
                this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
                this.tabPage2.Controls.Add(this.groupBox1);
                this.tabPage2.Location = new System.Drawing.Point(4, 22);
                this.tabPage2.Name = "tabPage2";
                this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage2.Size = new System.Drawing.Size(139, 155);
                this.tabPage2.TabIndex = 1;
                this.tabPage2.Text = "Known Info";
                // 
                // timer1
                // 
                this.timer1.Enabled = true;
                this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
                // 
                // frmMain
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(180, 236);
                this.ControlBox = false;
                this.Controls.Add(this.tabControl1);
                this.Controls.Add(this.statusStrip1);
                this.Controls.Add(this.btnClose);
                this.Controls.Add(this.btnSettings);
                this.Controls.Add(this.btnStart);
                this.ForeColor = System.Drawing.SystemColors.ControlText;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.MaximizeBox = false;
                this.Menu = this.mainMenu1;
                this.Name = "frmMain";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "PIN Cracker VF by gamesguru";
                this.Load += new System.EventHandler(this.Form1_Load);
                this.groupBox1.ResumeLayout(false);
                this.groupBox1.PerformLayout();
                this.groupBox2.ResumeLayout(false);
                this.groupBox2.PerformLayout();
                this.statusStrip1.ResumeLayout(false);
                this.statusStrip1.PerformLayout();
                this.tabControl1.ResumeLayout(false);
                this.tabPage1.ResumeLayout(false);
                this.tabPage2.ResumeLayout(false);
                this.ResumeLayout(false);
                this.PerformLayout();

            }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtKnownPin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKnownPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKnownID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCrackingEndPin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCrackingStartPin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCrackingPass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCrackingID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.MenuItem menuItem20;
        private System.Windows.Forms.MenuItem menuItem21;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem menuItem16;
        private System.Windows.Forms.MenuItem menuItem17;
        }
    }


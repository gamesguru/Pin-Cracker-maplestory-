using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Pin_Cracker.Properties;

namespace Pin_Cracker
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        #region Class Declarations

        TrainerFunctions.AllFunctions pKernel = new TrainerFunctions.AllFunctions();
        gma.System.Windows.UserActivityHook actHook = new gma.System.Windows.UserActivityHook();
        MouseFunctions pMouse = new MouseFunctions();
        System.Threading.Thread CrackingThread;
        CaptureScreen.CaptureScreen captureScreen = new CaptureScreen.CaptureScreen();
        Process mapleProcess;

        #endregion

        #region Variable Declarations

        string knownID;
        string knownPass;
        string knownPIN;
        //
        string crackingID;
        string crackingPass;
        string crackingStartPIN;
        string crackingEndPIN;
        //
        string currentPin;
        //string lastPin;
        //
        char CrackingOrKnownChr;
        //
        bool oneTimeOnly; //part of the password function
        //
        string currentDialog; //part of self correcting
        string startUpPath; //also part of it
        //
        bool startedByHelper;
        //
        Keys ee;
        #endregion

        #region Addresses

        IntPtr DIALOG_ADDRESS = (IntPtr)0;//0x8530B0;//0x80F470; these are old
        IntPtr LOGGED_IN_ADDRESS = (IntPtr)0;//0x851079;//0x80D4AD;
        IntPtr PIN_TYPE_ADDRESS = (IntPtr)0;//0x48FFE6;//489692;
        IntPtr GG_KILL_ADDRESS_EAX = (IntPtr)0x79A996;//0x7733D6;//0x739346;
        IntPtr GG_KILL_ADDRESS_PUSH = (IntPtr)0x799661;//0x799661;//0x7380CD;
        //IntPtr GG_KILL_ADDRESS_PUSH2 = (IntPtr)0x799FD4;//new
        #endregion

        #region Mouse Positions
        uint MainPageLoginX;
        uint MainPageLoginY;
        uint IDX;
        uint IDY;
        uint PassWordX;
        uint PassWordY;
        uint PinChangeX;
        uint PinChangeY;
        uint PinCancelX; //also Pin Confirm Cancel
        uint PinCancelY;
        uint GlitchProofX;
        uint GlitchProofY;
        uint loginBackX;
        uint loginBackY;
        uint backConfirmX;
        uint backConfirmY;

        #endregion
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckIfGenuine();
            CheckIfMultipleInstancesOpen();
            Form.CheckForIllegalCrossThreadCalls = false;
            MessageBox.Show("THIS IS VERSION FREE NOW. THAT MEANS YOU NEED TO PUT IN THE ADDRESSES IN SETTINGS>ADDRESSES. IF YOU DON'T, IT WON'T WORK. I DON'T AN EMAIL COMPLAING THE ''START'' BUTTON DOESN'T WORK!1!11!ONE!11ELEVEN!!", "READ THIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (Settings.Default.FirstTimeRun)
            {
                frmAbout About = new frmAbout();
                About.ShowDialog();
                Settings.Default.FirstTimeRun = false;
                Settings.Default.Save();
            }
            if (Settings.Default.boolSavePin)
                txtCrackingStartPin.Text = Settings.Default.LastSavedPin;
            actHook.Start();
            actHook.KeyUp += new KeyEventHandler(actHook_KeyUp);
            RefreshMainForm();
        }

        void actHook_KeyUp(object sender, KeyEventArgs e)
        {
            System.Threading.Thread myT;
            ee = e.KeyCode;
            myT = new System.Threading.Thread(new System.Threading.ThreadStart(actHookProcessing));
            myT.Start();
        }

        void actHookProcessing()
        {
            //just so stuff doesn't lag...it's glitchy
            if (ee == Keys.F9) //for pausing the cracking process
            {
                try
                {
                    if (CrackingThread.ThreadState == System.Threading.ThreadState.Suspended)
                    {
                        CrackingThread.Resume();
                        menuItem3.Checked = false;
                    }
                    else
                    {
                        CrackingThread.Suspend();
                        menuItem3.Checked = true;
                        MessageBox.Show("Thread suspended, the current PIN is: " + currentPin + ". Click OK and then press F9 again to resume.", "Thread Suspended", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch { }
            }
            else if (ee == Keys.F10) //for closing the application
            {
                //FindLastTriedPin();
                //System.IO.File.WriteAllText("Last Pin Tried.txt", lastPin);
                Process.GetCurrentProcess().Kill();
            }
            /*
        else if (ee == Keys.F11) //for pausing maple
            {
            if (menuItem20.Enabled)
                {
                RefreshMapleProc();
                try
                    {
                    if (!menuItem20.Checked)
                        {
                        foreach (System.Diagnostics.ProcessThread pT in mapleProcess.Threads)
                            {
                            IntPtr hThread = TrainerFunctions.UNUSED_APIS.Kernel32Api.OpenThread(TrainerFunctions.UNUSED_APIS.Kernel32Api.ThreadAccess.SUSPEND_RESUME, true, (uint)pT.Id);
                            TrainerFunctions.UNUSED_APIS.Kernel32Api.SuspendThread(hThread);
                            menuItem20.Checked = true;
                            }
                        }
                    else
                        {
                        foreach (System.Diagnostics.ProcessThread pT in mapleProcess.Threads)
                            {
                            IntPtr hThread = TrainerFunctions.UNUSED_APIS.Kernel32Api.OpenThread(TrainerFunctions.UNUSED_APIS.Kernel32Api.ThreadAccess.SUSPEND_RESUME, true, (uint)pT.Id);
                            TrainerFunctions.UNUSED_APIS.Kernel32Api.ResumeThread(hThread);
                            menuItem20.Checked = false;
                            }
                        }
                    }
                catch { }
                }
            }
            */
            else if (ee == Keys.F12 && mapleProcess != null)// for closing maple
            {
                try { mapleProcess.Kill(); }
                catch { }
            }
        }

        void CloseApp()
        {
            DialogResult dRT = MessageBox.Show("Are you sure that you wish to quit?", "Quit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dRT == DialogResult.Yes)
            {
                UpdateSettings();
                //FindLastTriedPin();
                //System.IO.File.WriteAllText("Last Pin Tried.txt", "The last tried PIN was: " + lastPin);
                Process.GetCurrentProcess().Kill();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CrackingThread = new System.Threading.Thread(new System.Threading.ThreadStart(Initiate));
            CrackingThread.Start();
        }

        void Initiate()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowTitle.ToLower().Contains("porn") || p.MainWindowTitle.ToLower().Contains("sex") || p.MainWindowTitle.ToLower().Contains("nude") || p.MainWindowTitle.ToLower().Contains("xxx"))
                {
                    MessageBox.Show("You could be potentially looking at porn.", "You may be in violation of article 3245 subset 48345 quadrant 435", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    //rofl...I HAVE TO ANNOY THOSE N00BS!!
                }
            }
            txtKnownID.Enabled = false; //just disables the controls
            txtKnownPass.Enabled = false;
            txtKnownPin.Enabled = false;
            //
            txtCrackingID.Enabled = false;
            txtCrackingPass.Enabled = false;
            txtCrackingStartPin.Enabled = false;
            txtCrackingEndPin.Enabled = false;
            //
            btnStart.Enabled = false;
            btnSettings.Enabled = false;
            //
            menuItem2.Enabled = false;
            menuItem7.Enabled = false;
            //
            timer1.Enabled = false;
            //
            //
            knownID = txtKnownID.Text; //assigns the variables
            knownPass = txtKnownPass.Text;
            knownPIN = txtKnownPin.Text;
            //
            crackingID = txtCrackingID.Text;
            crackingPass = txtCrackingPass.Text;
            crackingStartPIN = txtCrackingStartPin.Text;
            crackingEndPIN = txtCrackingEndPin.Text;
            //
            currentPin = crackingStartPIN;
            //
            //
            RefreshMapleProc();
            UpdateAddresses();
            if (mapleProcess != null)
            {
                if (BitConverter.ToInt32(pKernel.ReadProcessMemory(DIALOG_ADDRESS, 4), 0) != 3)
                {
                    if (MessageBox.Show("I don't think that Maple is fully loaded, but is open nevertheless. If you choose to go against me, you could be wrong, click OK ONLY if you know exactly what you are doing, and GGLess/GG Killed maple is open.", "Maple might not be fully loaded.", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        txtKnownID.Enabled = true; //re-enables them for reuse
                        txtKnownPass.Enabled = true;
                        txtKnownPin.Enabled = true;
                        //
                        txtCrackingID.Enabled = true;
                        txtCrackingPass.Enabled = true;
                        txtCrackingStartPin.Enabled = true;
                        txtCrackingEndPin.Enabled = true;
                        //
                        btnStart.Enabled = true;
                        btnSettings.Enabled = true;
                        //
                        menuItem2.Enabled = true;
                        menuItem7.Enabled = true;
                        //
                        timer1.Enabled = true;
                        //
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Either Maple Story isn't open or the executable isn't named 'MapleStory.exe'.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtKnownID.Enabled = true; //re-enables them for reuse
                txtKnownPass.Enabled = true;
                txtKnownPin.Enabled = true;
                //
                txtCrackingID.Enabled = true;
                txtCrackingPass.Enabled = true;
                txtCrackingStartPin.Enabled = true;
                txtCrackingEndPin.Enabled = true;
                //
                btnStart.Enabled = true;
                btnSettings.Enabled = true;
                //
                menuItem2.Enabled = true;
                menuItem7.Enabled = true;
                //
                timer1.Enabled = true;
                //
                return;
            }
            if (Convert.ToInt16(crackingStartPIN) > Convert.ToInt16(crackingEndPIN))
            {
                MessageBox.Show("Starting PIN is greater than ending PIN! Make sure the staring it lower than the ending!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtKnownID.Enabled = true; //re-enables them for reuse
                txtKnownPass.Enabled = true;
                txtKnownPin.Enabled = true;
                //
                txtCrackingID.Enabled = true;
                txtCrackingPass.Enabled = true;
                txtCrackingStartPin.Enabled = true;
                txtCrackingEndPin.Enabled = true;
                //
                btnStart.Enabled = true;
                btnSettings.Enabled = true;
                //
                menuItem2.Enabled = true;
                menuItem7.Enabled = true;
                //
                timer1.Enabled = true;
                //
                return;
            }
            toolStripProgressBar1.Maximum = Convert.ToInt16(crackingEndPIN) - Convert.ToInt16(crackingStartPIN);
            toolStripProgressBar1.Maximum++;
            DialogResult dRT;
            if (!startedByHelper)
                dRT = MessageBox.Show("You are about to begin cracking an account.", "Cracking about to begin", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            else
                dRT = DialogResult.OK;

            if (dRT == DialogResult.OK)
            {
                byte[] memory = { 0x0f, 0x83 };
                pKernel.WriteProcessMemory((IntPtr)PIN_TYPE_ADDRESS, memory);
                pMouse.SetActiveWindow(mapleProcess.ProcessName); //yes...i put this under mouse events... i was lazy.
                //timer2.Enabled = true;
                Delay(4000);
                mapleProcess.PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime; //new in 4.02, fixes lag
                //progBarCrackingProgress.Maximum = Convert.ToInt32(crackingEndPIN) - Convert.ToInt32(crackingStartPIN);
                //progBarCrackingProgress.Maximum++;
                //
                //
                //Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.BelowNormal;
                UpdateSettings(); //updates the settings, in case they don't use the settins form
                StartingPinFormat(); //gets the current pin format
                CrackingOrKnownChr = 'C'; //change to K if you want to start off with the known
                CrackingOrKnown(); //why did I do it this way? because I wanted to.
            }
            else
            {
                txtKnownID.Enabled = true; //re-enables them for reuse
                txtKnownPass.Enabled = true;
                txtKnownPin.Enabled = true;
                //
                txtCrackingID.Enabled = true;
                txtCrackingPass.Enabled = true;
                txtCrackingStartPin.Enabled = true;
                txtCrackingEndPin.Enabled = true;
                //
                btnStart.Enabled = true;
                btnSettings.Enabled = true;
                //
                menuItem2.Enabled = true;
                menuItem7.Enabled = true;
                //
                timer1.Enabled = true;
                //
                return;
            }
        }

        void CrackingOrKnown()
        {
            //if you don't understand this, you suck
            if (CrackingOrKnownChr == 'C')
                CrackingLogin();
            else if (CrackingOrKnownChr == 'K')
                KnownLogin();
        }

        void IncreaseCurrentPin()
        {
            currentPin = Convert.ToString(Convert.ToInt16(currentPin) + 1);
            StartingPinFormat();
        }

        void StartingPinFormat()
        {
            if (Convert.ToInt16(currentPin) == 0)
            {
                currentPin = "0000";
            }
            else if (Convert.ToInt16(currentPin) > 0 && Convert.ToInt16(currentPin) < 10)
            {
                currentPin = "000" + currentPin;
            }
            else if (Convert.ToInt16(currentPin) >= 10 && Convert.ToInt16(currentPin) < 100)
            {
                currentPin = "00" + currentPin;
            }
            else if (Convert.ToInt16(currentPin) >= 100 && Convert.ToInt16(currentPin) < 1000)
            {
                currentPin = "0" + currentPin;
            }
        }

        void ClearIDandEnterID()
        {
            for (int i = 0; i < Settings.Default.TrackValue; i++)
            {
                Delay(1);
            }
            Delay(25);
            pMouse.MousePos(IDX, IDY);
            pMouse.LeftClick();
            Delay(25);
            for (int i = 0; i < 20; i++)
            {
                SendKeys.SendWait("{BACKSPACE}");
            }
            for (int i = 0; i < Settings.Default.TrackValue; i++)
            {
                Delay(1);
            }
            Delay(10);
            if (CrackingOrKnownChr == 'C') //changed these because people reported problems
                SendKeys.SendWait(crackingID);
            else if (CrackingOrKnownChr == 'K')
                SendKeys.SendWait(knownID);
            for (int i = 0; i < Settings.Default.TrackValue; i++)
            {
                Delay(1);
            }
            Delay(25);
        }

        void ClearPassAndEnterPass()
        {
            for (int i = 0; i < Settings.Default.TrackValue; i++)
            {
                Delay(1);
            }
            Delay(50);
            for (int i = 0; i < 20; i++)
            {
                SendKeys.SendWait("{BACKSPACE}");
            }
            for (int i = 0; i < Settings.Default.TrackValue; i++)
            {
                Delay(1);
            }
            Delay(10);
            if (CrackingOrKnownChr == 'C')
                SendKeys.SendWait(crackingPass);

            else if (CrackingOrKnownChr == 'K')
                SendKeys.SendWait(knownPass);
            for (int i = 0; i < Settings.Default.TrackValue; i++)
            {
                Delay(1);
            }
            Delay(75);
        }

        void CrackingLogin()
        {
            System.Threading.Thread.Sleep(150);
            mainMenuCorrect(); //goes to the main menu from anywhere
            ClearIDandEnterID();
            Delay(10);
            pMouse.MousePos(PassWordX, PassWordY);
            pMouse.LeftClick();
            Delay(10);
            ClearPassAndEnterPass();
            Delay(10);
            SendKeys.SendWait("{ENTER}");
            Delay(50);
            for (int i = 0; i < Settings.Default.TrackValue; i++)
            {
                Delay(1);
            }
            bool DelayAlready = false;
            for (int i = 0; i < 4; i++)
            {
                CheckIfCracked(); //this won't be true, ever, on the first time but after that it needs to be checked before anything.
                if (!DelayAlready)
                {
                    for (int e = 0; e < Settings.Default.TrackValue; e++)
                    {
                        Delay(1);
                    }
                    Delay(10);
                    CheckIfPinMenuIsOpen();
                    DelayAlready = true;
                    Delay(10);
                    DelayAlready = true;
                    //pin menu is now active
                }
                SendKeys.SendWait(currentPin);
                SendKeys.SendWait("{ENTER}");
                toolStripStatusLabel1.Text = "Last PIN: " + currentPin;
                toolStripProgressBar1.Value++;
                //
                if (i != 4)
                {
                    CheckIfPinMenuIsOpen();
                }
                else
                {
                    Delay(5); //extra delay is needed here
                    CheckIfPinMenuIsOpen();
                }
                CheckIfCracked(); //needs to be here too
                //
                if (Convert.ToInt16(currentPin) - 1 == Convert.ToInt16(crackingEndPIN))
                {
                    FinishedButNotCracked(); //since the current pin and ending pin
                    //are the same...and it's not cracked, go to this method...
                }
                //
                //Delay(10);
                System.IO.File.WriteAllText(crackingID+"'s Last PIN Tried BACKUP.TXT", "The last PIN tried for "+crackingID+" was: " + currentPin);
                if (Settings.Default.boolSavePin)
                {
                    Settings.Default.LastSavedPin = currentPin;
                    Settings.Default.Save();
                }
                if (currentPin == crackingEndPIN)
                    FinishedButNotCracked();
                IncreaseCurrentPin();
            }
            //ends loop
            //
            //return to main menu
            CheckIfPinMenuIsOpen();
            Delay(25);
            pMouse.MousePos(GlitchProofX, GlitchProofY);
            pMouse.LeftClick();
            Delay(10);
            pMouse.MousePos(PinCancelX, PinCancelY);
            pMouse.LeftClick();
            Delay(10);
            pMouse.LeftClick();
            Delay(10);
            CrackingOrKnownChr = 'K';
            CrackingOrKnown();
        }

        void KnownLogin()
        {
            Delay(50);
            mainMenuCorrect();
            Delay(75);
            ClearIDandEnterID();
            Delay(50);
            pMouse.MousePos(PassWordX, PassWordY);
            pMouse.LeftClick();
            Delay(50);
            ClearPassAndEnterPass();
            Delay(50);
            SendKeys.SendWait("{ENTER}");
            Delay(10);
            for (int i = 0; i < Settings.Default.TrackValue; i++)
            {
                Delay(1);
            }
            CheckIfPinMenuIsOpen();
            Delay(10);
            //
            foreach (Char c in knownPIN)
            {
                Delay(1);
                SendKeys.SendWait(c.ToString());
            }
            Delay(50);
            SendKeys.SendWait("{ENTER}");
            Delay(100);
        Retry2:
            Delay(100);
            if (BitConverter.ToInt32(pKernel.ReadProcessMemory(LOGGED_IN_ADDRESS, 4), 0) != 0) //at login screen
            {
                Delay(1000);
                pMouse.MousePos(loginBackX,loginBackY);
                if (Settings.Default.FixLoginBugs) Delay(500); //fix
                pMouse.LeftClick();
                Delay(50);
            }
            else
                goto Retry2;
        Retry3:
            Delay(100);
            getCurrentDialog();
            if (currentDialog == "5")
            {
                Delay(150);
                pMouse.MousePos(backConfirmX, backConfirmY);
                if (Settings.Default.FixLoginBugs) Delay(500); //fix
                pMouse.LeftClick();
                Delay(500);
            }
            else
                goto Retry3;
            Delay(200);
            #region Old
            /*
            pMouse.MousePos(GlitchProofX, GlitchProofY);
            pMouse.LeftClick();
            Delay(20);
            pMouse.MousePos(PinChangeX, PinChangeY);
            pMouse.LeftClick();
            Delay(10);
            CheckIfPinChangeMenuIsOpen();
            Delay(10);
            pMouse.MousePos(GlitchProofX, GlitchProofY);
            pMouse.LeftClick();
            Delay(10);
            pMouse.MousePos(PinCancelX, PinCancelY);
            pMouse.LeftClick();
            Delay(10);
            CheckIfPinChangeCancelConfirmMenuIsOpen();
            Delay(10);
            pMouse.MousePos(GlitchProofX, GlitchProofY);
            pMouse.LeftClick();
            Delay(20);
            pMouse.MousePos(PinCancelX, PinCancelY);
            pMouse.LeftClick();
            Delay(50);
            */
            #endregion
            CrackingOrKnownChr = 'C';
            CrackingOrKnown();
        }

        //here are the dialog checks
        /*
        void CheckIfAtMainLoginMenu()
            {
            getCurrentDialog();
            if (currentDialog != "3") //not main login
                {
                Delay(1);
                getCurrentDialog();
                }
            Delay(10);
            //then returns
            }
        */
        void CheckIfPinMenuIsOpen()
        {
            if (mapleProcess != null)
            {
                Delay(1);
                if (BitConverter.ToInt32(pKernel.ReadProcessMemory(DIALOG_ADDRESS, 4), 0) != 4) //not pin enter
                {
                    CheckIfPinMenuIsOpen();
                }
                //returns
            }
        }

        void CheckIfPinChangeMenuIsOpen()
        {
            for (int i = 0; i < Settings.Default.TrackValue; i++)
            {
                Delay(1);
            }
            if (mapleProcess != null)
            {
                Delay(50);
                if (BitConverter.ToInt32(pKernel.ReadProcessMemory(DIALOG_ADDRESS, 4), 0) != 4) //not pin enter
                {
                    //MessageBox.Show(BitConverter.ToInt32(pKernel.ReadProcessMemory(DialogAddress,4),0).ToString());
                    CheckIfPinChangeMenuIsOpen();
                }
                Delay(100);
            }
        }

        void CheckIfPinChangeCancelConfirmMenuIsOpen()
        {
            for (int i = 0; i < Settings.Default.TrackValue; i++)
            {
                Delay(1);
            }
            if (mapleProcess != null)
            {
                Delay(50);
                if (BitConverter.ToInt32(pKernel.ReadProcessMemory(DIALOG_ADDRESS, 4), 0) != 5)
                {
                    CheckIfPinChangeCancelConfirmMenuIsOpen();
                }
                Delay(100);
            }
        }

        void getCurrentDialog()
        {
            RefreshMapleProc();
            if (mapleProcess != null)
            {
                //returns the value of the dialog address in 4 byte form
                int dialogValue = BitConverter.ToInt32(pKernel.ReadProcessMemory(DIALOG_ADDRESS, 4), 0);
                Delay(50);
                if (dialogValue == 3) //main login
                {
                    currentDialog = "3";
                }
                else if (dialogValue == 4) //either pin change OR regular pin enter
                {
                    //OLD: ok, here's how you find the difference between the regular and the pin change dialog!
                    //pMouse.MousePos(334, 347);
                    Delay(50);
                    currentDialog = "4";
                    /*
                    Bitmap currentPicture = CaptureScreen.CaptureScreen.GetDesktopImage();
                    //
                    if (Convert.ToInt32(currentPicture.GetPixel(302, 347).R - currentPicture.GetPixel(302, 347).G) >= 200) //the thing is red and it's at regular!
                        {
                        currentDialog = "41";
                        }
                    //why didn't i use an else? I LIKE VISUAL CRAP!!!
                    else if (Convert.ToInt32(currentPicture.GetPixel(302, 347).R - currentPicture.GetPixel(302, 347).G) < 200) //the thing ain't red and it's at pin change
                        {
                        currentDialog = "42";
                        }
                     
                    pMouse.MousePos(GlitchProofX, GlitchProofY);
                     */
                }
                else if (dialogValue == 5) //change pin cancel confirm
                {
                    currentDialog = "5";
                }
            }
        }

        void mainMenuCorrect()
        {
            Delay(50);
        Retry:
            getCurrentDialog();
            if (currentDialog == "4") //pin enter dialog or logged in screen
            {
                pMouse.MousePos(GlitchProofX, GlitchProofY);
                pMouse.LeftClick();
                Delay(50);
                pMouse.MousePos(61, 451); //this was the source of problems until 4.03, it was a typo
                pMouse.LeftClick();
                Delay(50);
                /*
                if (BitConverter.ToInt32(pKernel.ReadProcessMemory(LOGGED_IN_ADDRESS, 4), 0) != 0) //not at login screen
                {
                    Delay(50);
                    pMouse.MousePos(61, 651);
                    pMouse.LeftClick();
                    Delay(50);
                }*/
                goto Retry;
                #region omitted
                /*
                if (CrackingOrKnownChr == 'C')
                    {
                    Delay(150);
                    pMouse.MousePos(PinCancelX, PinCancelY);
                    pMouse.LeftClick();
                    Delay(150);
                    CrackingOrKnownChr = 'K';
                    CrackingLogin();
                    }
                else
                    {
                    Delay(150);
                    SendKeys.Send(knownPIN);
                    Delay(50);
                    pMouse.MousePos(PinChangeX, PinChangeY);
                    pMouse.LeftClick();
                    Delay(50);
                    CheckIfPinChangeMenuIsOpen();
                    pMouse.MousePos(GlitchProofX, GlitchProofY);
                    pMouse.LeftClick();
                    Delay(50);
                    pMouse.MousePos(PinCancelX, PinCancelY);
                    pMouse.LeftClick();
                    Delay(150);
                    CheckIfPinChangeCancelConfirmMenuIsOpen();
                    Delay(50);
                    pMouse.MousePos(GlitchProofX, GlitchProofY);
                    pMouse.LeftClick();
                    Delay(50);
                    pMouse.MousePos(PinCancelX, PinCancelY);
                    pMouse.LeftClick();
                    CrackingOrKnownChr = 'C';
                    CrackingOrKnown();
                    }*/
                #endregion
            }
            /*
             * OLD:
        else if (currentDialog == "42") //pin change, could very well happen!
            {
            pMouse.MousePos(GlitchProofX, GlitchProofY);
            pMouse.LeftClick();
            Delay(10);
            pMouse.MousePos(PinCancelX, PinCancelY);
            pMouse.LeftClick();
            Delay(150);
            CheckIfPinChangeCancelConfirmMenuIsOpen();
            Delay(50);
            pMouse.MousePos(GlitchProofX, GlitchProofY);
            pMouse.LeftClick();
            Delay(10);
            pMouse.MousePos(PinCancelX, PinCancelY);
            pMouse.LeftClick();
            Delay(150);
            goto Retry;
            }
             */
            else if (currentDialog == "5") //exit login dialog
            {
                Delay(100);
                pMouse.MousePos(363, 335);
                pMouse.LeftClick();
                Delay(500);
                goto Retry;
            }
            else if (currentDialog == "3") //main login
            {
                Delay(100);
                return;
            }
        }

        #region omitted
        /*
        void SelfCorrecting(int currentDialogValue, int newDialogValue)
            {
            if (currentDialogValue == 3) //main login
                {
                if (newDialogValue == 4) //go from main login to pin dialog
                    {

                    }
                }
            else if (currentDialogValue == 4) //either change pin or normal enter pin
                {
                if (CaptureScreen.CaptureScreen.GetDesktopImage()
                }
            else if (currentDialogValue == 5) //change pin cancel confirm
                {

                }
            #region old
            /*if (CrackingOrKnownChr == 'K')
                {
                RefreshMapleProc();
                if (mapleProcess != null)
                    {
                    int value = BitConverter.ToInt32(pKernel.ReadProcessMemory(DialogAddress, 4), 0);
                    //
                    if (value == 3) //already at main login
                        {
                        //returns
                        }
                    else if (value == 4) //most likely pin change, could be regular pin menu though
                        {
                        //ONLY DIFFERENCE BETWEEN KNOWN AND CRACKING IS THIS!
                        //first we check if it's regular...to be safe
                        Delay(75);
                        pMouse.MousePos(GlitchProofX, GlitchProofY);
                        pMouse.LeftClick();
                        Delay(10);
                        pMouse.MousePos(PinChangeX, PinChangeY);
                        pMouse.LeftClick();
                        Delay(10);
                        pMouse.LeftClick();
                        //now we do if it was at regular (nothing bad could happen)
                        Delay(50);
                        pMouse.MousePos(PinCancelX, PinCancelY);
                        pMouse.LeftClick();
                        Delay(10);
                        pMouse.MousePos(GlitchProofX, GlitchProofY);
                        pMouse.LeftClick();
                        Delay(10);
                        pMouse.MousePos(PinCancelX, PinCancelY);
                        pMouse.LeftClick();
                        Delay(50);
                    //
                    //this is for getting out of the confirm cancel
                    Retry:
                        if (BitConverter.ToInt32(pKernel.ReadProcessMemory(DialogAddress, 4), 0) != 5)
                            {
                            if (BitConverter.ToInt32(pKernel.ReadProcessMemory(DialogAddress, 4), 0) == 4)
                                {
                                Delay(50);
                                goto Retry;
                                }
                            else //it's 3, main menu
                                return;
                            }
                        //this is only done if it at pin change cancel confirm
                        Delay(50);
                        pMouse.MousePos(PinCancelX, PinCancelY);
                        pMouse.LeftClick();
                        Delay(10);
                        pMouse.MousePos(GlitchProofX, GlitchProofY);
                        pMouse.LeftClick();
                        Delay(10);
                        pMouse.MousePos(PinCancelX, PinCancelY);
                        pMouse.LeftClick();
                        Delay(25);
                        CheckIfAtMainLoginMenu();
                        Delay(10);

                        }
                    else if (value == 5) //hopefully it's at pin change...and not pin click menu...
                        {
                        Delay(10);
                        pMouse.MousePos(PinCancelX, PinCancelY);
                        pMouse.LeftClick();
                        pMouse.MousePos(GlitchProofX, GlitchProofY);
                        pMouse.LeftClick();
                        pMouse.MousePos(PinCancelX, PinCancelY);
                        pMouse.LeftClick();
                        Delay(10);
                        CheckIfAtMainLoginMenu();
                        SelfCorrecting();
                        Delay(10);
                        }
                    //else //who the heck knows what happened...
                    }
                }
            else if (CrackingOrKnownChr == 'C')
                {
                    {
                    RefreshMapleProc();
                    if (mapleProcess != null)
                        {
                        int value = BitConverter.ToInt32(pKernel.ReadProcessMemory(DialogAddress, 4), 0);
                        //
                        if (value == 3) //already at main login
                            {
                            //returns
                            }
                        else if (value == 4) //most likely pin change, could be regular pin menu though
                            {

                            Delay(50);
                            pMouse.MousePos(PinCancelX, PinCancelY);
                            pMouse.LeftClick();
                            Delay(10);
                            pMouse.MousePos(GlitchProofX, GlitchProofY);
                            pMouse.LeftClick();
                            Delay(10);
                            pMouse.MousePos(PinCancelX, PinCancelY);
                            pMouse.LeftClick();
                            Delay(50);

                        //
                        //this is for getting out of the confirm cancel
                        Retry2:
                            if (BitConverter.ToInt32(pKernel.ReadProcessMemory(DialogAddress, 4), 0) != 5)
                                {
                                if (BitConverter.ToInt32(pKernel.ReadProcessMemory(DialogAddress, 4), 0) == 4)
                                    {
                                    Delay(50);
                                    goto Retry2;
                                    }
                                else //it's 3, main menu
                                    return;
                                }
                            //this is only done if it at pin change cancel confirm
                            Delay(50);
                            pMouse.MousePos(PinCancelX, PinCancelY);
                            pMouse.LeftClick();
                            Delay(10);
                            pMouse.MousePos(GlitchProofX, GlitchProofY);
                            pMouse.LeftClick();
                            Delay(10);
                            pMouse.MousePos(PinCancelX, PinCancelY);
                            pMouse.LeftClick();
                            Delay(25);
                            CheckIfAtMainLoginMenu();
                            Delay(10);

                            }
                        else if (value == 5) //hopefully it's at pin change...and not pin click menu...
                            {
                            Delay(10);
                            pMouse.MousePos(PinCancelX, PinCancelY);
                            pMouse.LeftClick();
                            pMouse.MousePos(GlitchProofX, GlitchProofY);
                            pMouse.LeftClick();
                            pMouse.MousePos(PinCancelX, PinCancelY);
                            pMouse.LeftClick();
                            Delay(10);
                            CheckIfAtMainLoginMenu();
                            SelfCorrecting();
                            Delay(10);
                            }
                        //else //who the heck knows what happened...
                        }
                    }
                }
            }*/
            #endregion
        //and they end here
        //

        void CheckIfCracked()
        {
            for (int i = 0; i < Settings.Default.TrackValue; i++)
            {
                Delay(1);
            }
            Delay(50);
            RefreshMapleProc();
            if (mapleProcess != null)
            {
                if (BitConverter.ToInt32(pKernel.ReadProcessMemory(LOGGED_IN_ADDRESS, 4), 0) != 0) //cracked
                {
                    Cracked();
                }
            }
        }

        void Cracked()
        {
            MouseFunctions.SwitchToThisWindow(Process.GetCurrentProcess().MainWindowHandle, true);
            Delay(2000);
            if (Settings.Default.CloseOrNot)
                mapleProcess.Kill();
            System.IO.File.WriteAllText(crackingID + "'s PIN.txt", "The following info is correct. ID - " + crackingID + " - Pass - " + crackingPass + " - PIN - " + currentPin);
            MessageBox.Show("The crack was successful! Check the file '" + crackingID + "'s PIN.txt' for the details, but if you don't want to wait, the PIN is: " + currentPin + ".", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CrackingThread.Abort();

        }

        void FinishedButNotCracked()
        {
            if (crackingStartPIN == "0000" && crackingEndPIN == "9999")
                MessageBox.Show("All PIN's were tried but none worked! Somethng went wrong...sucks for you.", "Problem");

            else
                MessageBox.Show("The PIN's " + crackingStartPIN + " to " + crackingEndPIN + " have been tested, but none worked. Perhaps an error occured, or your internet connection lagged in the process!", "Finished but not successful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CrackingThread.Abort();
        }


        void UpdateSettings()
        {
            Settings.Default.knownID = txtKnownID.Text;
            Settings.Default.knownPass = txtKnownPass.Text;
            Settings.Default.knownPin = txtKnownPin.Text;
            //
            Settings.Default.crackingID = txtCrackingID.Text;
            Settings.Default.crackingPass = txtCrackingPass.Text;
            Settings.Default.crackingStartPin = txtCrackingStartPin.Text;
            Settings.Default.crackingEndPin = txtCrackingEndPin.Text;
            Settings.Default.Save();
        }

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
            /*
        foreach (Process p in Process.GetProcesses())
            {
            if (p.MainWindowTitle.ToLower() == "maplestory")
                {
                mapleProcess = p;
                pKernel.ReadProcess = mapleProcess;
                pKernel.OpenProcess();
                break;
                }
            else
                mapleProcess = null;
            }*/
        }

        void UpdateAddresses()
        {
            DIALOG_ADDRESS = (IntPtr)Settings.Default.DIALOG_ADDRESS;
            LOGGED_IN_ADDRESS = (IntPtr)Settings.Default.LOGGED_IN_ADDRESS;
            PIN_TYPE_ADDRESS = (IntPtr)Settings.Default.PIN_TYPE_ADDRESS;
            GG_KILL_ADDRESS_EAX = (IntPtr)Settings.Default.GG_KILL_ADDRESS_EAX;
            GG_KILL_ADDRESS_PUSH = (IntPtr)Settings.Default.GG_KILL_ADDRESS_PUSH;
        }

        private void AssignMouseValues()
        {
            if (Settings.Default.windowed)
            {
                IDX = 566;
                IDY = 274;
                //
                PassWordX = 566;
                PassWordY = 300;
                //
                PinChangeX = 304;
                PinChangeY = 362;
                //
                PinCancelX = 473;
                PinCancelY = 367;
                //
                GlitchProofX = 798;
                GlitchProofY = 624;
                //
                MainPageLoginX = 640;
                MainPageLoginY = 260;
                //
                loginBackX = 65;
                loginBackY = 478;
                //
                //
                backConfirmX = 367;
                backConfirmY = 365;

            }
            else
            {
                MainPageLoginX = 638;
                MainPageLoginY = 254;
                IDX = 550;
                IDY = 249;
                //
                PassWordX = 550;
                PassWordY = 276;
                //
                PinChangeX = 306;
                PinChangeY = 337;
                //
                PinCancelX = 466;
                PinCancelY = 338;
                //
                GlitchProofX = 999;
                GlitchProofY = 999;
                //
                loginBackX = 65;
                loginBackY = 451;
                //
                backConfirmX = 365;
                backConfirmY = 335;
            }

        }

        void RefreshMainForm()
        {
            menuItem10.Checked = Settings.Default.CensoredPass;
            menuItem11.Checked = Settings.Default.CensoredPins;
            menuItem12.Checked = Settings.Default.SaveRAM;
            txtKnownID.Text = Settings.Default.knownID;
            txtKnownPass.Text = Settings.Default.knownPass;
            txtKnownPin.Text = Settings.Default.knownPin;
            //
            txtCrackingID.Text = Settings.Default.crackingID;
            txtCrackingPass.Text = Settings.Default.crackingPass;
            if (!Settings.Default.boolSavePin)
                txtCrackingStartPin.Text = Settings.Default.crackingStartPin;
            txtCrackingEndPin.Text = Settings.Default.crackingEndPin;
            //
            AssignMouseValues();
            //
            DIALOG_ADDRESS = (IntPtr)Settings.Default.DIALOG_ADDRESS;
            LOGGED_IN_ADDRESS = (IntPtr)Settings.Default.LOGGED_IN_ADDRESS;
            PIN_TYPE_ADDRESS = (IntPtr)Settings.Default.PIN_TYPE_ADDRESS;
            GG_KILL_ADDRESS_EAX = (IntPtr)Settings.Default.GG_KILL_ADDRESS_EAX;
            GG_KILL_ADDRESS_PUSH = (IntPtr)Settings.Default.GG_KILL_ADDRESS_PUSH;
            //
            if (Settings.Default.CensoredPass)
            {
                txtKnownPass.PasswordChar = '*';
                txtCrackingPass.PasswordChar = '*';
            }
            else
            {
                txtKnownPass.PasswordChar = txtKnownID.PasswordChar;
                txtCrackingPass.PasswordChar = txtKnownID.PasswordChar;
            }
            if (Settings.Default.CensoredPins)
            {
                txtKnownPin.PasswordChar = '*';
                txtCrackingStartPin.PasswordChar = '*';
                txtCrackingEndPin.PasswordChar = '*';
            }
            else
            {
                txtKnownPin.PasswordChar = txtKnownID.PasswordChar;
                txtCrackingStartPin.PasswordChar = txtKnownID.PasswordChar;
                txtCrackingEndPin.PasswordChar = txtKnownID.PasswordChar;
            }
        }

        void CheckIfMultipleInstancesOpen()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowTitle.ToLower().Contains("pin cracker") && p.MainWindowTitle.ToLower().Contains("by gamesguru"))
                {
                    MessageBox.Show("Only one instance may be opened at once!", "Only one!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        void CheckIfGenuine()
        {
            if (!Application.ExecutablePath.ToLower().EndsWith("pin cracker.exe"))
            {
                MessageBox.Show("Verify that the executable name is 'Pin Cracker.exe', no quotes and try again.", "Invalid excutable name!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.GetCurrentProcess().Kill();
            }
        }

        void Delay(int delayInMilliseconds)
        {
            System.Threading.Thread.Sleep(delayInMilliseconds);
        }

        private void txtKnownPin_Leave(object sender, EventArgs e)
        {
            try
            {
                int Value = Convert.ToInt32(txtKnownPin.Text);
                if (txtKnownPin.Text.Length != 4)
                {
                    if (Value == 0)
                    {
                        txtKnownPin.Text = "0000";
                    }
                    else if (Value > 0 && Value < 10)
                    {
                        txtKnownPin.Text = "000" + txtKnownPin.Text;
                    }
                    else if (Value >= 10 && Value < 100)
                    {
                        txtKnownPin.Text = "00" + txtKnownPin.Text;
                    }
                    else if (Value >= 100 && Value < 1000)
                    {
                        txtKnownPin.Text = "0" + txtKnownPin.Text;
                    }
                }
            }
            catch
            {
                txtKnownPin.Text = "0000";
                MessageBox.Show("Invalid value in text box!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCrackingStartPin_Leave(object sender, EventArgs e)
        {
            try
            {
                int Value = Convert.ToInt32(txtCrackingStartPin.Text);
                if (txtCrackingStartPin.Text.Length != 4)
                {
                    if (Value == 0)
                    {
                        txtCrackingStartPin.Text = "0000";
                    }
                    else if (Value > 0 && Value < 10)
                    {
                        txtCrackingStartPin.Text = "000" + txtCrackingStartPin.Text;
                    }
                    else if (Value >= 10 && Value < 100)
                    {
                        txtCrackingStartPin.Text = "00" + txtCrackingStartPin.Text;
                    }
                    else if (Value >= 100 && Value < 1000)
                    {
                        txtCrackingStartPin.Text = "0" + txtCrackingStartPin.Text;
                    }
                }
            }
            catch
            {
                txtCrackingStartPin.Text = "0000";
                MessageBox.Show("Invalid value in text box!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCrackingEndPin_Leave(object sender, EventArgs e)
        {
            try
            {
                int Value = Convert.ToInt32(txtCrackingEndPin.Text);
                if (txtCrackingEndPin.Text.Length != 4)
                {
                    if (Value == 0)
                    {
                        txtCrackingEndPin.Text = "0000";
                    }
                    else if (Value > 0 && Value < 10)
                    {
                        txtCrackingEndPin.Text = "000" + txtCrackingEndPin.Text;
                    }
                    else if (Value >= 10 && Value < 100)
                    {
                        txtCrackingEndPin.Text = "00" + txtCrackingEndPin.Text;
                    }
                    else if (Value >= 100 && Value < 1000)
                    {
                        txtCrackingEndPin.Text = "0" + txtCrackingEndPin.Text;
                    }
                }
            }
            catch
            {
                txtCrackingEndPin.Text = "0000";
                MessageBox.Show("Invalid value in text box!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.ShowDialog();
            RefreshMainForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseApp();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                if (CrackingThread.ThreadState == System.Threading.ThreadState.Suspended)
                {
                    CrackingThread.Resume();
                    menuItem3.Checked = false;
                }
                else if (CrackingThread.ThreadState == System.Threading.ThreadState.Running || CrackingThread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
                {
                    CrackingThread.Suspend();
                    menuItem3.Checked = true;
                    //MessageBox.Show("Thread suspended, the current pin is: " + lastPin + ". Click OK and then press F9 again to resume.", "Thread Suspended", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowTitle.ToLower().Contains("pin cracker helper"))
                    startedByHelper = true;
            }
            CrackingThread = new System.Threading.Thread(new System.Threading.ThreadStart(Initiate));
            CrackingThread.Start();
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.ShowDialog();
            RefreshMainForm();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            CloseApp();
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            frmAbout About = new frmAbout();
            About.ShowDialog();
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowTitle.ToLower() == "maplestory" || p.MainWindowTitle.ToLower().Contains("maplestory"))
                {
                    if (p.ProcessName.ToLower() == "maplestory")
                        MessageBox.Show("It's open and the process name is correct! But if you still get an error, there's something wrong. Send me a video of everything you did up until this point, if you wish to use this PIN cracker.", "Problem!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("The correct Maple Story process name is PROBABLY: " + p.ProcessName + ". But it could be something else, so rename the Maple client BACK to ''MapleStory''.", "Probable Process Name Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    goto Done1;
                }
            }
            MessageBox.Show("Maple Story PROBABLY not opened, or you failed to Kill GameGuard.", "No Probable Process Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
        Done1:
            return;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Settings.Default.SaveRAM)
                {
                    //Dispose(false); //stupid dispose!
                    CrackingThread.Suspend();
                    System.Threading.Thread.Sleep(1500);
                    CrackingThread.Resume();
                }
            }
            catch { }
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            try { CrackingThread.Suspend(); }
            catch { }
            if (!menuItem10.Checked)
                menuItem10.Checked = true;
            else
                menuItem10.Checked = false;
            Settings.Default.CensoredPass = menuItem10.Checked;
            Settings.Default.crackingID = txtCrackingID.Text;
            Settings.Default.crackingPass = txtCrackingPass.Text;
            Settings.Default.crackingStartPin = txtCrackingStartPin.Text;
            Settings.Default.knownID = txtKnownID.Text;
            Settings.Default.knownPass = txtKnownPass.Text;
            Settings.Default.knownPin = txtKnownPin.Text;
            Settings.Default.Save();
            RefreshMainForm();
            try { CrackingThread.Resume(); }
            catch { }
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            try { CrackingThread.Suspend(); }
            catch { }
            if (!menuItem11.Checked)
                menuItem11.Checked = true;
            else
                menuItem11.Checked = false;
            Settings.Default.CensoredPins = menuItem11.Checked;
            Settings.Default.CensoredPass = menuItem10.Checked;
            Settings.Default.crackingID = txtCrackingID.Text;
            Settings.Default.crackingPass = txtCrackingPass.Text;
            Settings.Default.crackingStartPin = txtCrackingStartPin.Text;
            Settings.Default.knownID = txtKnownID.Text;
            Settings.Default.knownPass = txtKnownPass.Text;
            Settings.Default.knownPin = txtKnownPin.Text;
            Settings.Default.Save();
            RefreshMainForm();
            try { CrackingThread.Resume(); }
            catch { }
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            try { CrackingThread.Suspend(); }
            catch { }
            if (!menuItem12.Checked)
                menuItem12.Checked = true;
            else
                menuItem12.Checked = false;
            Settings.Default.SaveRAM = menuItem12.Checked;
            Settings.Default.Save();
            RefreshMainForm();
            try { CrackingThread.Resume(); }
            catch { }
        }

        private void menuItem1_Popup(object sender, EventArgs e)
        {
            RefreshMapleProc();
            try
            {
                if (CrackingThread.ThreadState == System.Threading.ThreadState.Stopped || CrackingThread.ThreadState == System.Threading.ThreadState.Aborted)
                    menuItem3.Enabled = false;
                else
                    menuItem3.Enabled = true;
            }
            catch { menuItem3.Enabled = false; }

            byte[] memory = pKernel.ReadProcessMemory(PIN_TYPE_ADDRESS, 4);
            if (memory[1] == 0x83) //it's injected
                menuItem19.Text = "DeInject PIN Type";
            else
                menuItem19.Text = "Inject PIN Type";

            if (mapleProcess != null)
            {
                menuItem2.Enabled = true;
                menuItem13.Enabled = true;
                menuItem19.Enabled = true;
                menuItem20.Enabled = true;
                menuItem21.Enabled = true;
                menuItem16.Enabled = true;
                menuItem17.Enabled = true;
            }
            else
            {
                menuItem2.Enabled = false;
                menuItem13.Enabled = false;
                menuItem19.Enabled = false;
                menuItem20.Enabled = false;
                menuItem21.Enabled = false;
                menuItem16.Enabled = false;
                menuItem17.Enabled = false;
            }
            menuItem2.Enabled = btnStart.Enabled;
        }

        private void menuItem14_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:gamesguru2@gmail.com");
        }

        private void menuItem15_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("aim:goim?screenname=gamesgurupwn&message=");
            }
            catch
            {
                MessageBox.Show("AIM is not open!", "AIM not open", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshMapleProc();
            if (mapleProcess != null)
            {
                byte[] bytesToWrite = { 0xB8, 0x55, 0x07 };
                pKernel.WriteProcessMemory(GG_KILL_ADDRESS_EAX, bytesToWrite);
                byte[] bytesToWrite2=BitConverter.GetBytes(2371289194);
                pKernel.WriteProcessMemory(GG_KILL_ADDRESS_PUSH, bytesToWrite2);
                //byte[] bytesToWrite3 = BitConverter.GetBytes(6946922); //{ 0x61, 0x00, 0x6a };
                //pKernel.WriteProcessMemory(GG_KILL_ADDRESS_PUSH2, bytesToWrite3);
                btnStart.Enabled = true;
            }
            else
            {
                btnStart.Enabled = false;
            }
        }

        private void menuItem19_Click(object sender, EventArgs e)
        {
            RefreshMapleProc();
            if (mapleProcess != null)
            {
                if (menuItem19.Text == "Inject PIN Type")
                {
                    byte[] memory = { 0x0f, 0x83 };
                    pKernel.WriteProcessMemory(PIN_TYPE_ADDRESS, memory);
                    menuItem19.Text = "DeInject PIN Type";
                }
                else
                {
                    byte[] memory = { 0x0f, 0x86 }; //original pin type address value
                    pKernel.WriteProcessMemory(PIN_TYPE_ADDRESS, memory);
                    menuItem19.Text = "Inject PIN Type";
                }
            }
        }

        private void menuItem20_Click(object sender, EventArgs e)
        {
            RefreshMapleProc();
            try
            {
                if (!menuItem20.Checked)
                {
                    foreach (System.Diagnostics.ProcessThread pT in mapleProcess.Threads)
                    {
                        IntPtr hThread = TrainerFunctions.UNUSED_APIS.Kernel32Api.OpenThread(TrainerFunctions.UNUSED_APIS.Kernel32Api.ThreadAccess.SUSPEND_RESUME, true, (uint)pT.Id);
                        TrainerFunctions.UNUSED_APIS.Kernel32Api.SuspendThread(hThread);
                        menuItem20.Checked = true;
                    }
                }
                else
                {
                    foreach (System.Diagnostics.ProcessThread pT in mapleProcess.Threads)
                    {
                        IntPtr hThread = TrainerFunctions.UNUSED_APIS.Kernel32Api.OpenThread(TrainerFunctions.UNUSED_APIS.Kernel32Api.ThreadAccess.SUSPEND_RESUME, true, (uint)pT.Id);
                        TrainerFunctions.UNUSED_APIS.Kernel32Api.ResumeThread(hThread);
                        menuItem20.Checked = false;
                    }
                }
            }
            catch { }
        }

        private void menuItem21_Click(object sender, EventArgs e)
        {
            try { mapleProcess.Kill(); }
            catch { }
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            RefreshMapleProc();
            MessageBox.Show(BitConverter.ToInt32(pKernel.ReadProcessMemory(DIALOG_ADDRESS, 4), 0).ToString());
        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            RefreshMapleProc();
            byte[] bytesToWrite = { 0xB8, 0x55, 0x07 };
            pKernel.WriteProcessMemory(GG_KILL_ADDRESS_EAX, bytesToWrite);
            byte[] bytesToWrite2 = BitConverter.GetBytes(2371289194);
            pKernel.WriteProcessMemory(GG_KILL_ADDRESS_PUSH, bytesToWrite2);
            //byte[] bytesToWrite3 = BitConverter.GetBytes(6946922); //{ 0x61, 0x00, 0x6a };
            //pKernel.WriteProcessMemory(GG_KILL_ADDRESS_PUSH2, bytesToWrite3);
        }

        private void menuItem17_Click(object sender, EventArgs e)
        {
            RefreshMapleProc();
            if (mapleProcess != null)
                mapleProcess.PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
        }

    }
}

    
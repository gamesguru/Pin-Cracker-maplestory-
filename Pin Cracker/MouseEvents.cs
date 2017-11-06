using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace Pin_Cracker
    {
    public class MouseFunctions
        {
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        
        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [Flags]
        public enum MouseEventFlags
            {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
            }
        public void LeftClick()
            {
            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            }
        public void LeftMouseDown()
            {
            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            }
        public void LeftMouseUp()
            {
            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            }
        /*public void DragTo(uint x, uint y)
            {
            mouse_event((uint)MouseEventFlags.MOVE, x, y, 0, 0);
            }*/ //doesn't work, so i ommitted it.
        public void MousePos(uint x, uint y)
            {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(Convert.ToInt16(x), Convert.ToInt16(y));
            }
        public void DoubleClick()
            {
            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            }
        public void SetActiveWindow(string procName)
            {
            //i don't know why i put this in the mouse events...there was no where else to put it so it went here...
            System.Diagnostics.Process[] myProcess = System.Diagnostics.Process.GetProcessesByName(procName);
            IntPtr hWnd = myProcess[0].MainWindowHandle;
            SetActiveWindow(hWnd);
            SwitchToThisWindow(hWnd, true);
            }
        }
    }

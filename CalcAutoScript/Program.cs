using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String sClassName, String sAppName);
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetForegroundWindow(IntPtr hwnd);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, out int wParam, out int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, StringBuilder lParam);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wparam, IntPtr lparam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public const int KEYEVENTF_KEYUP = 0x0002;
        public const int KEVENTF_EXTENDEDKEY = 0x0001;
        public const int VK_RETURN = 0x0D;
        public const int VK_0 = 0x30;
        public const int VK_3 = 0x33;
        public const int VK_6 = 0x36;
        public const int VK_TAB = 0x09;
        public const int VK_DOWN = 0x28;
        public const int VK_NEXT = 0x22;
        public const int VK_LSHIFT = 0xA0;
        public const int VK_BACK = 0x08;
        public const int VK_MENU = 0x12;
        public const int VK_F4 = 0x73;
        public const int WM_GETTEXT = 0x0D;
        public const uint WM_GETTEXTLENGTH = 0x000E;
        public const uint WM_SETTEXT = 0x000C;
        public const uint CB_SETCURSEL = 0x014E;
        public const uint BM_CLICK = 0x00F5;
        public const uint WM_CLOSE = 0x0010;

        [STAThread]
        public static void Main(string[] args)
        {

            //Application.Run(new Calculator.Form1());
            var proc = new Process();
            proc.StartInfo.FileName = "calculator.exe";
            proc.Start();

            IntPtr myCalculator = FindWindow(null, "Simple calculator");
            while (myCalculator.ToInt32() == 0)
            {
                myCalculator = FindWindow(null, "Simple calculator");
            }
            //SetActiveWindow(myCalculator);
            SetForegroundWindow(myCalculator);

            //Set textboxes to 6 and 0, select Divide, and hit equals button
            /*keybd_event(VK_6, 0, 0, 0);
            keybd_event(VK_TAB, 0, 0, 0);
            keybd_event(VK_0, 0, 0, 0);
            keybd_event(VK_TAB, 0, 0, 0);
            keybd_event(VK_DOWN, 0, 0, 0);
            keybd_event(VK_NEXT, 0, 0, 0);
            keybd_event(VK_TAB, 0, 0, 0);
            keybd_event(VK_RETURN, 0, 0, 0);

            //Switch second textbox to 3 and hit equals button
            keybd_event(VK_LSHIFT, 0, KEVENTF_EXTENDEDKEY | 0, 0);
            keybd_event(VK_TAB, 0, KEVENTF_EXTENDEDKEY | 0, 0);
            keybd_event(VK_TAB, 0, KEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            keybd_event(VK_TAB, 0, KEVENTF_EXTENDEDKEY | 0, 0);
            keybd_event(VK_TAB, 0, KEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            keybd_event(VK_LSHIFT, 0, KEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            keybd_event(VK_BACK, 0, 0, 0);
            keybd_event(VK_3, 0, 0, 0);
            keybd_event(VK_TAB, 0, 0, 0);
            keybd_event(VK_TAB, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_TAB, 0, 0, 0);
            keybd_event(VK_RETURN, 0, 0, 0);
            */
            Thread.Sleep(200);

            IntPtr child = IntPtr.Zero;
            IntPtr labelHwnd = FindWindowEx(myCalculator, child, null, null);
            IntPtr buttonHwnd = FindWindowEx(myCalculator, labelHwnd, null, null);
            IntPtr comboHwnd = FindWindowEx(myCalculator, buttonHwnd, null, null);
            IntPtr textHwnd1 = FindWindowEx(myCalculator, comboHwnd, null, null);
            IntPtr textHwnd2 = FindWindowEx(myCalculator, textHwnd1, null, null);

            //Send Text
            SendMessage(textHwnd2, WM_SETTEXT, IntPtr.Zero, "6");
            SendMessage(textHwnd1, WM_SETTEXT, IntPtr.Zero, "0");
            SendMessage(comboHwnd, CB_SETCURSEL, 3, "");
            SendMessage(buttonHwnd, BM_CLICK, IntPtr.Zero, IntPtr.Zero);

            //Try again
            SendMessage(textHwnd1, WM_SETTEXT, IntPtr.Zero, "3");
            SendMessage(buttonHwnd, BM_CLICK, IntPtr.Zero, IntPtr.Zero);

            //Grab text
            int textlength = (int)SendMessage(labelHwnd, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero) + 1;

            //Console.WriteLine(textlength);

            StringBuilder sb = new StringBuilder(textlength);
            SendMessage(labelHwnd, WM_GETTEXT, (IntPtr)textlength, sb);

            string returnText = sb.ToString();

            //Close the application
            SendMessage(myCalculator, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

            /*keybd_event(VK_MENU, 0, 0, 0);
            keybd_event(VK_F4, 0, 0, 0);
            keybd_event(VK_MENU, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_F4, 0, KEYEVENTF_KEYUP, 0);*/



            Console.WriteLine(returnText);
            Console.ReadLine();
        }

    }
}

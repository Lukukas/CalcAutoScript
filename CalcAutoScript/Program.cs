using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

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
            keybd_event(VK_6, 0, 0, 0);
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

            //Close the application
            keybd_event(VK_MENU, 0, 0, 0);
            keybd_event(VK_F4, 0, 0, 0);
            keybd_event(VK_MENU, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_F4, 0, KEYEVENTF_KEYUP, 0);

            Console.WriteLine(2);
            Console.ReadLine();
        }
      
    }
}

using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;

namespace SC
{
    public static class Win32Interface
    {
        public delegate bool CallBack(IntPtr hwnd, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("User32.dll", EntryPoint = "GetWindowLong")]
        public static extern UInt32 GetWindowLong(IntPtr hwnd, int nindex);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessageA(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        //public static extern IntPtr PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        public static extern IntPtr PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, uint lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr PostMessageA(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        public static extern int GetWindowText(
                    IntPtr hwnd,
                    StringBuilder lpString,
                    int cch
                );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "EnumChildWindows")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

        [DllImport("user32.dll", EntryPoint = "EnumWindows")]
        public static extern int EnumWindows(CallBack lpfn, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        //static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);
        //[DllImport("user32.dll", EntryPoint = "GetWindowTextW")]
        //public static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetClassNameW")]
        public static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);

        [DllImport("gdi32.dll")]
        public static extern System.UInt32 GetPixel(IntPtr hdc, int xPos, int yPos);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hc, IntPtr hDest);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point p);

        public const string VB_EDIT_CLASS = "ThunderRT6TextBox";
        public const string VB_FRAME_CLASS = "ThunderRT6Frame";
        public const string VB_CHECKBOX_CLASS = "ThunderRT6CheckBox";
        public const string VB_USER_CONTROL_CLASS = "ThunderRT6UserControlDC";
        public const string VB_USER_WINDOW = "ThunderRT6FormDC";
        public const UInt32 UID_EDIT_STYLE = 0x540100C0;
        public const UInt32 PWD_EDIT_STYLE = 0x540100E0;


        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_CHAR = 0x102;
        public const int WM_SETTEXT = 0x000C;
        public const int WM_CLOSE = 0x0010;
        public const int BST_UNCHECKED = 0x0000;
        public const int BM_SETCHECK = 0x00F1;
        public const int WM_SETFOCUS = 0x0007;
        //按下鼠标左键
        public const int WM_LBUTTONDOWN = 0x201;
        //释放鼠标左键
        public const int WM_LBUTTONUP = 0x202;


        public const string ORIGINAL_STRING = "TD34Y1UNSC75WVOMB8HLG9I6A0QXRKZFJE2P";
        public const string SECRET_STRING = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    }
}
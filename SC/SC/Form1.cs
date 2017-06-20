using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace SC
{
    public partial class Form1 : Form
    {
        private int _curr_page = 0;
        private int _total_page = 10;
        Bitmap bmp0 = new Bitmap(@"0.jpg");     //0% image, avoid the 0% during the webpage refreshing
        private Color _color = Color.Transparent;
        private Point _location;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(X.Text);
            int y = Convert.ToInt32(Y.Text);
            int w = Convert.ToInt32(W.Text);
            int h = Convert.ToInt32(H.Text);

            Bitmap bmpScreen = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            Graphics gfxScreen = Graphics.FromImage(bmpScreen);
            gfxScreen.CopyFromScreen(x, y, 0, 0, new Size(w, h), CopyPixelOperation.SourceCopy);
            p.Image = bmpScreen;
            bmpScreen.Save(@"X.jpg", ImageFormat.Jpeg);
        }

        private void CaptureScreen(int page)
        {
            int x = Convert.ToInt32(X.Text);
            int y = Convert.ToInt32(Y.Text);
            int w = Convert.ToInt32(W.Text);
            int h = Convert.ToInt32(H.Text);
            Thread.Sleep(1000);
            Bitmap bmpScreen = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            Graphics gfxScreen = Graphics.FromImage(bmpScreen);
            gfxScreen.CopyFromScreen(x, y, 0, 0, new Size(w, h), CopyPixelOperation.SourceCopy);

            bmpScreen.Save(@"X.jpg", ImageFormat.Jpeg);
            Bitmap bmp1 = new Bitmap(@"X.jpg");
            if (!CompareImage(bmp0, bmp1, 75, 40, 135, 80))
            {
                p.Image = bmpScreen;
                Color color = bmpScreen.GetPixel(_location.X, _location.Y);
                if (_color.R != color.R || _color.G != color.G || _color.B != color.B)
                    goto Exit_CaptureScreen;
                //bmpScreen.Save("\\\\huaweb03\\c$\\inetpub\\wwwroot\\OEEDashboard\\Web" + page.ToString() + ".jpg", ImageFormat.Jpeg);
                bmpScreen.Save("\\\\huanb1419\\Web\\" + "Page" + page.ToString() + ".jpg", ImageFormat.Jpeg);
            }
            Exit_CaptureScreen:
            bmp1.Dispose();
        }

        private bool CompareImage(Bitmap img1, Bitmap img2, int x1, int y1, int x2, int y2)
        {
            bool flg = true;
            for (int x = x1; x < x2 && flg; x++)
                for (int y = y1; y < y2 && flg; y++)
                    flg = (img1.GetPixel(x, y) == img2.GetPixel(x, y));

            return flg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bp = new Bitmap(p.Image);
            Color color = bp.GetPixel(_location.X, _location.Y);
            if (_color.R != color.R || _color.G != color.G || _color.B != color.B)
            {
                MessageBox.Show("The color point can not be matched;");
                bp.Dispose();
                return;
            }
            bp.Dispose();

            TimerCapture.Enabled = false;
            if (MessageBox.Show("打开"+ _total_page.ToString()+ "页面，设定好工作参数，并选择好第一个TAB， 是否正确?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _curr_page = 0;
                TimerCapture.Enabled = true;
                button1.Enabled = false;
                button2.Enabled = true;
                btnCapture.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                TimerCapture.Enabled = false;
                CaptureScreen(_curr_page + 1);
            }
            catch
            {

            }

            IntPtr w = Win32Interface.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Chrome_WidgetWin_1", "CAMXCS3 - Google Chrome");
            if (w == IntPtr.Zero)
                return;
            Win32Interface.SetForegroundWindow(w);
            SendKeys.SendWait("^{PGDN}");
            _curr_page++;
            if (_curr_page == _total_page)
                _curr_page = 0;

            TimerCapture.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TimerCapture.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = false;
            btnCapture.Enabled = true;
        }

        private void btnColor_Click(object sender, EventArgs e)
        {

        }

        public System.Drawing.Color GetPixelColor(int x, int y)
        {
            IntPtr hdc = Win32Interface.GetDC(IntPtr.Zero);
            uint pixel = Win32Interface.GetPixel(hdc, x, y);
            Win32Interface.ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF),
            (int)(pixel & 0x0000FF00) >> 8,
            (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }



        private void p_MouseDown(object sender, MouseEventArgs e)
        {
            if (TimerCapture.Enabled == true)
                return;
            Point point;
            Win32Interface.GetCursorPos(out point);

            _color = GetPixelColor(point.X, point.Y);

            panColor.BackColor = _color;

            _location = e.Location;
        }
    }
}

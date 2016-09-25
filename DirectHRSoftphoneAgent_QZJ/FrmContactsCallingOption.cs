using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DHRSoftphone.DSUserControls;
using DHRSoftphone.Eyebeam;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public partial class FrmContactsCallingOption : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        UserContactInfo info;

        public FrmContactsCallingOption()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            //Draw green board
            base.WndProc(ref m);
            if (m.Msg == 0xf || m.Msg == 0x133)
            {
                IntPtr hDC = GetWindowDC(m.HWnd);
                if (hDC.ToInt32() == 0)
                {
                    return;
                }
                System.Drawing.Pen pen = new Pen(Color.Green, 1);
                System.Drawing.Graphics g = Graphics.FromHdc(hDC);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
                pen.Dispose();
            }
        }

        public FrmContactsCallingOption(UserContactInfo info)
        {
            InitializeComponent();
            this.info = info;
            var location = new Point(Cursor.Position.X - this.Size.Width / 8, Cursor.Position.Y - 20);
            this.Location = location;
            lab_name.Text = info.Username;
            lab_name.Left = this.Width / 2 - lab_name.Width / 2;
            if (string.IsNullOrEmpty(info.ExtensionNumber))
            {
                pic_Tel.Enabled = false;
                pic_Tel.Image = Properties.Resources.icon_phone_nocolor;
            }



        }

        private void pic_Tel_Click(object sender, EventArgs e)
        {
            if (!Eyebeam.Dialer.Dial(info.ExtensionNumber))
                MessageBox.Show("Eyebeam failed to startup!");
        }

        private void pic_Cellphone_Click(object sender, EventArgs e)
        {
            var cellphone = new TelephoneNumber(info.Cellphone).MakeFinallyNumber(AgentDatabaseProvider.CurrentAreaCode);
            if (!Eyebeam.Dialer.Dial(cellphone))
                MessageBox.Show("Eyebeam failed to startup!");
        }

        private void pic_Email_Click(object sender, EventArgs e)
        {
            var email = info.UserEmail;
            try
            {
                System.Diagnostics.Process.Start("chrome", "https://mail.google.com/mail/?view=cm&fs=1&to=" + email + "&su=Direct%20HR:%20");
            }
            catch (Exception ex)
            {
                AgentLogs.WriteLog("Can not start the Chrome or other issues: " + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace, AgentLogEventLevel.Errors);
                MessageBox.Show("Can not start Google Chrome browser!");
            }
        }

        private void pic_Tel_MouseHover(object sender, EventArgs e)
        {
            pic_Tel.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pic_Tel_MouseLeave(object sender, EventArgs e)
        {
            pic_Tel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pic_Cellphone_MouseHover(object sender, EventArgs e)
        {
            pic_Cellphone.BorderStyle = BorderStyle.Fixed3D;

        }

        private void pic_Cellphone_MouseLeave(object sender, EventArgs e)
        {
            pic_Cellphone.BorderStyle = BorderStyle.FixedSingle;

        }

        private void pic_Email_MouseHover(object sender, EventArgs e)
        {
            pic_Email.BorderStyle = BorderStyle.Fixed3D;

        }

        private void pic_Email_MouseLeave(object sender, EventArgs e)
        {
            pic_Email.BorderStyle = BorderStyle.FixedSingle;

        }

        private void FrmContactsCallingOption_MouseMove(object sender, MouseEventArgs e)
        {
            CheckClose();
        }

        private void FrmContactsCallingOption_MouseLeave(object sender, EventArgs e)
        {
            CheckClose();
        }

        private void FrmContactsCallingOption_Deactivate(object sender, EventArgs e)
        {
            CheckClose();
        }

        private void FrmContactsCallingOption_Leave(object sender, EventArgs e)
        {
            CheckClose();
        }

        protected void CheckClose()
        {
            if (Cursor.Position.X < this.Location.X || Cursor.Position.X > this.Location.X + this.Width ||
    Cursor.Position.Y < this.Location.Y || Cursor.Position.Y > this.Location.Y + this.Height)
                this.Close();
        }

        protected Bitmap GetNoColorImage(Image image)
        {
            int Height = image.Height;
            int Width = image.Width;
            Bitmap bitmap = new Bitmap(Width, Height);
            Bitmap MyBitmap = (Bitmap)image;
            Color pixel;
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    pixel = MyBitmap.GetPixel(x, y);
                    int r, g, b, Result = 0;
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    //实例程序以加权平均值法产生黑白图像
                    int iType = 2;
                    switch (iType)
                    {
                        case 0://平均值法
                            Result = ((r + g + b) / 3);
                            break;
                        case 1://最大值法
                            Result = r > g ? r : g;
                            Result = Result > b ? Result : b;
                            break;
                        case 2://加权平均值法
                            Result = ((int)(0.7 * r) + (int)(0.2 * g) + (int)(0.1 * b));
                            break;
                    }
                    bitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                }
            return bitmap;
        }
    }
}

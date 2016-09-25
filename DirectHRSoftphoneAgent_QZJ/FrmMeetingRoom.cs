using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DHRSoftphone.AgentExceptions;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public partial class FrmMeetingRoom : Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        protected PictureBox[] listPictureBox = new PictureBox[10];
        protected string[] roomName = new string[10];
        protected string[] loginUrl = new string[10];
        protected List<string>[] members = new List<string>[10];
        private int isloaded = 0;
        private Thread checkingProcess;
        private MeetingXml meetingData;

        private delegate void AdjustWindowCallBack(int height);
        public delegate void ProcessDelegate();

        private FormMeetingMembers frmMembers = new FormMeetingMembers();

        #region 窗体边框暗影成效变量声明

        private const int CS_DropSHADOW = 0x20000;

        private const int GCL_STYLE = (-26);

        //声明Win32 API

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetClassLong(IntPtr hwnd, int nIndex);

        #endregion 窗体边框暗影成效变量声明

        public void OnPaint(Object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black) { Width = 2.0F };
            e.Graphics.DrawRectangle(pen, e.ClipRectangle);
        }

        private void AdjustWindow(int height)
        {
            this.Size = new Size(25, ++height * 21 + 30);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            AgentLogs.WriteLog("Users trying to close the meeting room window!", AgentLogEventLevel.Errors);
            e.Cancel = true;

            //new AgentException("Please do not close Meeting Room window!");
            //Application.Exit();
            //System.Environment.Exit(-1);

            //}

            //if (e.CloseReason == CloseReason.TaskManagerClosing)
            //    MessageBox.Show("You just try to close me via TASK MANAGER, too nude! If the program has any problem you may need to email me, thank you!", "Direct HR Softphone Agent", MessageBoxButtons.OK);
        }

        public int Isloaded
        {
            get { return isloaded; }
            set { isloaded = value; }
        }

        public FrmMeetingRoom()
        {
            InitializeComponent();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW); //API函数加载，完成窗体边框暗影成效

            //this.Paint += new PaintEventHandler(this.OnPaint);
            //SetWindowSizeByUsersNumber(0);

            #region Init all pics

            for (int i = 0; i < 10; i++)
            {
                var pic = new PictureBox();

                //pic.Location = new Point(6, 124 * (i + 1));
                var meeting = new MeetingImagePic();
                pic.Image = meeting.MakePicByColor(Brushes.Blue, "0", Color.White);
                pic.Name = string.Format("room{0}", i);
                listPictureBox[i] = pic;
                listPictureBox[i].Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
                listPictureBox[i].Size = new System.Drawing.Size(20, 20);
                listPictureBox[i].Click += pics_Click;
                listPictureBox[i].Cursor = Cursors.Hand;
                listPictureBox[i].MouseHover += pics_MouseHover;
                listPictureBox[i].MouseLeave += pics_MouseLeave;
                members[i] = new List<string>();
                flowLayoutPanel1.Controls.Add(pic);
            }

            #endregion Init all pics

            //flowLayoutPanel1.Controls.AddRange(listPictureBox.ToArray());
            this.ResumeLayout(false);
        }

        public void SetWindowSizeByUsersNumber(int usersCount)
        {
            //this.Size = new Size(25, ++usersCount * 21);
        }

        private void FrmMeetingRoom_Load(object sender, EventArgs e)
        {
            AdjustWindow(0);
            meetingData = new MeetingXml();
            meetingData.MeetingRoomDataUpdated += this.DoShow;
            checkingProcess = new Thread(new ThreadStart(meetingData.DoChecking));
            checkingProcess.Start();
            this.TopLevel = true;
            this.TopMost = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left & this.WindowState == FormWindowState.Normal)
            {
                this.Capture = false;
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void picCamera_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void timer_loadxml_Tick(object sender, EventArgs e)
        {
            //AgentLogs.WriteLog("Now reload xml from meeting server...", AgentLogEventLevel.Details);
            //Thread thread = new Thread(new ThreadStart(DoShow));
        }

        public void DoShow(object sender, List<Meeting> meetings)
        {
            //AgentLogs.WriteLog("DoShow function started...", AgentLogEventLevel.Details);

            if (meetings == null)
            {
                SetWindowSizeByUsersNumber(0);
                return;
            }

            try
            {
                int pics = 0;

                while (meetings.Count < 5)
                {
                    if (meetings.All(m => m.Color.ToUpper() != "BLACK"))
                        meetings.Add(new Meeting() { BrushColor = Brushes.Black, Title = "BLACK Room", Color = "BLACK", NumOfUsers = "0" });
                    else if (meetings.All(m => m.Color.ToUpper() != "BLUE"))
                        meetings.Add(new Meeting() { BrushColor = Brushes.Blue, Title = "BLUE Room", Color = "BLUE", NumOfUsers = "0" });
                    else if (meetings.All(m => m.Color.ToUpper() != "GREEN"))
                        meetings.Add(new Meeting() { BrushColor = Brushes.Green, Title = "GREEN Room", Color = "GREEN", NumOfUsers = "0" });
                    else if (meetings.All(m => m.Color.ToUpper() != "ORANGE"))
                        meetings.Add(new Meeting() { BrushColor = Brushes.Orange, Title = "ORANGE Room", Color = "ORANGE", NumOfUsers = "0" });
                    else if (meetings.All(m => m.Color.ToUpper() != "RED"))
                        meetings.Add(new Meeting() { BrushColor = Brushes.Red, Title = "RED Room", Color = "RED", NumOfUsers = "0" });
                }

                foreach (var meeting in meetings)
                {
                    //(flowLayoutPanel1.Controls.Find(
                    // string.Format("room{0}", pics), false).Single()
                    var meetingPic = new MeetingImagePic();
                    ((PictureBox)flowLayoutPanel1.Controls[pics]).Image = meetingPic.MakePicByColor(meeting.BrushColor, meeting.NumOfUsers);
                    int num = Convert.ToInt32(((PictureBox)flowLayoutPanel1.Controls[pics]).Name.Replace("room", string.Empty));
                    roomName[num] = meeting.Title;
                    loginUrl[num] = meeting.JoinUrl;
                    members[num].Clear();
                    if (meeting.Users != null && meeting.Users.Any())
                        members[num].AddRange(meeting.Users);
                    pics++;
                }
                AdjustWindowCallBack alicb = new AdjustWindowCallBack(AdjustWindow);
                this.Invoke(alicb, new object[] { meetings.Count });

                SetWindowSizeByUsersNumber(meetings.Count);
                isloaded = 1;
            }
            catch (Exception ex)
            {
                isloaded = -1;
                new AgentException("MeetingRoom form timer error", ex);
            }

            //AgentLogs.WriteLog("DoShow function ended...", AgentLogEventLevel.Details);
        }

        private void picCamera_Click(object sender, EventArgs e)
        {
        }

        private void pics_Click(object sender, EventArgs e)
        {
            string username = new AgentSettings().GetFullname();
            int num = Convert.ToInt32(((PictureBox)sender).Name.Replace("room", string.Empty));

            //if (string.IsNullOrEmpty(loginUrl[num]))
            //    return;
            try
            {
                AgentLogs.WriteLog("Trying to start Chrome to goto meeting room");
                System.Diagnostics.Process.Start("chrome", MeetingXml.MakeLoginUrl(username, roomName[num]));
            }
            catch (Exception ex)
            {
                AgentLogs.WriteLog("Can not start the Chrome or other issues: " + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace, AgentLogEventLevel.Errors);
                MessageBox.Show("Can not start Google Chrome browser!");
            }
        }

        private void FrmMeetingRoom_Move(object sender, EventArgs e)
        {
            bool isMoveOutOfWindow = false;
            System.Drawing.Rectangle window = Screen.GetWorkingArea(this);
            if (Left < window.Left)
            {
                isMoveOutOfWindow = true;
                Left = window.Left;
            }
            if (Right > window.Right)
            {
                isMoveOutOfWindow = true;
                Left = window.Right - 40;
            }
            if (Top < window.Top)
            {
                isMoveOutOfWindow = true;
                Top = window.Top;
            }
            if (Bottom > window.Bottom - 50)
            {
                isMoveOutOfWindow = true;
                Top = window.Bottom - 50 - Height;
            }

            //if (isMoveOutOfWindow)
            //{
            //    FormMessage frmMsg = new FormMessage();
            //    frmMsg.Messages = "How can you see the meeting window if it was moved out of your desktop?";
            //    frmMsg.Show();
            //}
            //if (Convert.ToInt32(Left) > Screen.)
            //MessageBox.Show(Left.ToString(CultureInfo.InvariantCulture));
            //System.Drawing.Rectangle pictureBox1Rect = Screen.GetWorkingArea(this);
            //MessageBox.Show(SystemInformation.WorkingArea.X.ToString());
        }

        private void picCamera_MouseHover(object sender, EventArgs e)
        {
        }

        private void pics_MouseHover(object sender, EventArgs e)
        {
            string username = new AgentSettings().GetFullname();
            int num = Convert.ToInt32(((PictureBox)sender).Name.Replace("room", string.Empty));
            if (members[num].Any())
            {
                frmMembers.SetMembers(members[num]);
                frmMembers.Left = this.Left - frmMembers.Width;
                frmMembers.Top = this.Top;
                frmMembers.Show();
            }
        }

        private void pics_MouseLeave(object sender, EventArgs e)
        {
            frmMembers.Hide();
        }

        private void picCamera_MouseLeave(object sender, EventArgs e)
        {
        }

        private void pic_tel_Click(object sender, EventArgs e)
        {
            if (FrmContacts.IsError)
            {
                if (MessageBox.Show("Contacts feature was disabled because an error occurred, would you like to re-try?", "DHR Contacts", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    new ProcessDelegate(FrmContacts.Reload).BeginInvoke(null, null);
                return;
            }
            if (FrmContacts.IsLoaded)
            {
                FrmContacts.frmContacts.Hide();
                FrmContacts.frmContacts.Show();
            }
            else
                MessageBox.Show("Contacts interface still loading [" + FrmContacts.LoadingPercent.ToString("d") + "%], please wait a while and try again.");
        }
    }
}
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public partial class FrmLoading : Form
    {
        public FrmLoading()
        {
            InitializeComponent();
            this.Height = lab_status.Bottom + 50 - this.Top;
            this.Width = 581;
            //pic_loading.Height = 290;
        }

        public void SetName(string username)
        {
            //pic_loading.Height = 290;
            lab_loginuser.Visible = true;
            lab_loginuserstatic.Visible = true;
            btn_changeuser.Visible = true;
            lab_loginuser.Text = username;
        }

        public void SetStatus(string status)
        {
            lab_status.Text = status;
        }

        private void btn_changeuser_Click(object sender, System.EventArgs e)
        {
            var settingsFrm = new FrmSettings();
            settingsFrm.ShowDialog();
        }

        private void FrmLoading_Load(object sender, System.EventArgs e)
        {
            bool isLoaded = false;
            var picFolder = @"http://softphone.directhr.cn/Loading_Pic/";
            var picPath = picFolder + DateTime.Today.ToString("MMdd") + ".jpg";
            var picNLPath = picFolder + "nl" + DateTime.Today.ToString("MMdd") + ".jpg";
            QzjIoCtl.SendHeartbeat();
            Thread.Sleep(1000);
            lab_loginuser.Visible = false;
            lab_loginuserstatic.Visible = false;
            btn_changeuser.Visible = false;
            lab_version.Text = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            try
            {
                pic_loading.Load(picPath);
                isLoaded = true;
            }
            catch
            {

            }
            if (isLoaded) return;

            try
            {
                pic_loading.Load(picNLPath);
                isLoaded = true;
            }
            catch
            {

            }
            if (isLoaded) return;

            try
            {
                pic_loading.Load("http://softphone.directhr.cn/loading.gif");
            }
            catch
            {

            }
            try
            {
                pic_loading.Load("http://softphone.directhr.cn/loading.jpg");
            }
            catch
            {

            }
        }

    }
}
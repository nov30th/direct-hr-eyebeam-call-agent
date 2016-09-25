using System;
using System.IO;
using System.Windows.Forms;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public partial class FormWelcomeScreen : Form
    {
        public FormWelcomeScreen()
        {
            InitializeComponent();
        }

        private void btn_enter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormWelcomeScreen_Load(object sender, EventArgs e)
        {
            richTextBox1.LoadFile(Path.Combine(Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName), "welcome.rtf"));
        }
    }
}
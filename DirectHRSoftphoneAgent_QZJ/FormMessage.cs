using System;
using System.Windows.Forms;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public partial class FormMessage : Form
    {
        private string _messages;

        public FormMessage()
        {
            InitializeComponent(); ;
        }

        public string Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMessage_Load(object sender, EventArgs e)
        {
            lab_message.Text = _messages;
        }
    }
}
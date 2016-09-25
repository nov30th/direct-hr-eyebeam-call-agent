using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public partial class FormMeetingMembers : Form
    {
        public FormMeetingMembers()
        {
            InitializeComponent();
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoSize = true;
        }

        public void SetMembers(List<string> members)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var member in members)
            {
                sb.AppendLine(member);
            }
            lab_members.Text = sb.ToString();
        }

        public override sealed bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        private void lab_members_TextChanged(object sender, EventArgs e)
        {
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoSize = true;
        }
    }
}
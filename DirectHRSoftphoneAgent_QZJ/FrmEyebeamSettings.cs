using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DHRSoftphone.Eyebeam;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public partial class FrmEyebeamSettings : Form
    {
        public FrmEyebeamSettings()
        {
            InitializeComponent();
        }

        public void SetLabelText(string text)
        {
            _labInformation.Text = text;
        }

        private void _btnOneKeySet_Click(object sender, EventArgs e)
        {
            AgentSettings set = new AgentSettings();
            var eyebeam = new Eyebeam.ConfigManager();
            var sipUser = new SIPUser()
            {
                DisplayName = set.GetFullname(),
                Domain = eyebeam.GetIPPBXIPAddress(set.GetCurrentExtension()),
                Enabled = true,
                Extension = set.GetCurrentExtension(),
                Password = string.Empty,
                Username = set.GetCurrentExtension()
            };
            eyebeam.RewriteConfigurationWithSimplyConfiguration(sipUser, true);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

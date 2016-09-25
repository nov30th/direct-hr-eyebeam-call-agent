using System;
using System.Windows.Forms;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public partial class FrmExtensionSettings : Form
    {
        public FrmExtensionSettings()
        {
            InitializeComponent();
        }

        private void FrmExtensionSteeings_Load(object sender, EventArgs e)
        {
            AgentSettings set = new AgentSettings();
            textbox_extension.Text = set.GetCurrentExtension();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            AgentSettings settings = new AgentSettings();
            settings.SetCurrentExtension(textbox_extension.Text);
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using System.Reflection;
using System.Windows.Forms;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public partial class FrmAboutcs : Form
    {
        public FrmAboutcs()
        {
            InitializeComponent();
            label3.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public void SetPluginText(string about)
        {
            txtAbout.Text = about;
        }

    }
}
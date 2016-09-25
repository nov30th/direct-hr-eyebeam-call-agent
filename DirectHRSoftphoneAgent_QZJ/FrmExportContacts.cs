using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public partial class FrmExportContacts : Form
    {
        public FrmExportContacts()
        {
            InitializeComponent();
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "vcf";
            saveDialog.Filter = "vCard files (*.vcf)|*.vcf|All files (*.*)|*.*";
            saveDialog.Title = "Save DHR HR Contacts vCards File";
            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var file = File.CreateText(saveDialog.FileName);
                if (radioNamewithoutDHR.Checked)
                    file.Write(FrmContacts.csvFileContent.CsvContactsContent);
                else
                    file.Write(FrmContacts.csvFileContent.CsvContactsContentWithNameStyle);

                file.Close();
            }
            MessageBox.Show("File was exported, please upload this file to your Google Contacts or your mobile device");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AgentLogs.WriteLog("Trying to start Chrome to goto the contacts page");
                System.Diagnostics.Process.Start("chrome", "https://contacts.google.com");
            }
            catch (Exception ex)
            {
                AgentLogs.WriteLog("Can not start the Chrome or other issues: " + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace, AgentLogEventLevel.Errors);
                MessageBox.Show("Can not start Google Chrome browser!");
            }
        }

        private void btn_done_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DHRSoftphone.DSUserControls;
using DHRSoftphone.AgentExceptions;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public partial class FrmContacts : Form
    {
        public static FrmContacts frmContacts { get; set; }
        public static ContactsCSVBuilder csvFileContent { get; set; }
        protected readonly int tableLayoutPanelAddHeight = 60;
        protected readonly int tableLayoutPanelAddWidth = 5;
        static bool isBeginLoaded = false;
        static bool isLoaded = false;
        static bool isError = false;
        static int loadingPercent = 0;

        public static int LoadingPercent
        {
            get { return FrmContacts.loadingPercent; }
            set { FrmContacts.loadingPercent = value; }
        }

        public static bool IsError
        {
            get { return FrmContacts.isError; }
            set { FrmContacts.isError = value; }
        }

        static public bool IsLoaded
        {
            get { return isLoaded; }
            set { isLoaded = value; }
        }

        public static void Init()
        {
            if (frmContacts == null || frmContacts.IsDisposed)
                frmContacts = new FrmContacts();
            isBeginLoaded = false;
            isLoaded = false;
            isError = false;
            loadingPercent = 0;
            frmContacts.FrmContacts_Load(null, null);
        }

        public static void Reload()
        {
            frmContacts.Close();
            frmContacts.Dispose();
            Init();
        }


        public FrmContacts()
        {
            InitializeComponent();

        }

        private void FrmContacts_Load(object sender, EventArgs e)
        {
            if (isBeginLoaded)
                return;
            csvFileContent = new ContactsCSVBuilder();
            isBeginLoaded = true;
            int officeCount = 0;
            ContactsXml contacts = new ContactsXml();
            List<UserContact> xmlData;
            try
            {
                xmlData = contacts.GetData().OrderBy(x => x.Office).ToList();
            }
            catch (Exception ex)
            {
                AgentLogs.WriteLog("Load DHR Contacts Failed", AgentLogEventLevel.Errors);
                new AgentException("Load DHR Contacts Failed", ex);
                MessageBox.Show("Load DHR Contacts failed! Please check your Internet connection to NEWTOOLS server, contacts feature was disabled now!", "DHR Contacts Disabled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isError = true;
                return;
            }
            var lastTabName = string.Empty;
            TabPage tab;
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            int total = xmlData.Count;
            int currentLoaded = 0;
            foreach (var contact in xmlData)
            {
                if (contact.Office != lastTabName)
                {
                    officeCount++;
                    //build tab
                    tab = new TabPage(contact.Office);
                    tabContacts.TabPages.Add(tab);
                    tab.Name = contact.Office;
                    tab.Text = contact.Office;
                    lastTabName = contact.Office;
                    //build layout
                    tableLayoutPanel = new TableLayoutPanel();
                    tableLayoutPanel.AutoSize = true;
                    tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                    tableLayoutPanel.ColumnCount = 4;
                    tableLayoutPanel.SizeChanged += (sender1, e1) =>
                    {
                        if (tabContacts.Height <= tableLayoutPanel.Height)
                            tabContacts.Height = tableLayoutPanel.Height + tableLayoutPanelAddHeight;
                        if (tabContacts.Width <= tableLayoutPanel.Width)
                            tabContacts.Width = tableLayoutPanel.Width + tableLayoutPanelAddWidth;
                    };
                    tab.Controls.Add(tableLayoutPanel);
                }

                //add contacts
                DSUserControls.DHRContactsControl dhrContactsControl1 = new DHRContactsControl();
                dhrContactsControl1.UserPhotoUrl = contact.Photo;
                dhrContactsControl1.UserName = contact.Name;
                dhrContactsControl1.ExtensionNumber = contact.Extension;
                dhrContactsControl1.Cellphone = contact.Cellphone;
                dhrContactsControl1.UserEmail = contact.Email;
                dhrContactsControl1.MasterEmail = contact.MasterEmail;
                tableLayoutPanel.Controls.Add(dhrContactsControl1);
                dhrContactsControl1.OnUserMouseClicked += new DHRSoftphone.DSUserControls.DHRContactsControl.UserMouseClickEventHandler(UserMouseClicked);
                currentLoaded++;
                loadingPercent = currentLoaded * 100 / total;
                
                //add contacts to csv
                csvFileContent.AddPerson(contact);
            }
            tabContacts.SizeMode = TabSizeMode.Fixed;
            tabContacts.ItemSize = new System.Drawing.Size((tabContacts.Width - 5) / officeCount, 40);


            //Test1();
            //Test2();


            /* Default process for load the tab, do not modify */
            AgentSettings settings = new AgentSettings();
            var selectedTabName = settings.ReadString("Contacts", "SelectedTab", string.Empty);
            if (!string.IsNullOrEmpty(selectedTabName))
            {
                foreach (TabPage tabSelected in tabContacts.TabPages)
                {
                    if (tabSelected.Text == selectedTabName)
                    {
                        tabContacts.SelectedTab = tabSelected;
                        break;
                    }
                }
            }
            isLoaded = true;
        }

        //private void Test1()
        //{
        //    var tab = new TabPage("Ningbo");
        //    tabContacts.TabPages.Add(tab);
        //    //var tab = tabContacts.TabPages["Ningbo"];
        //    var tableLayoutPanel = new TableLayoutPanel();
        //    tableLayoutPanel.AutoSize = true;
        //    tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        //    tableLayoutPanel.ColumnCount = 4;
        //    tableLayoutPanel.SizeChanged += (sender1, e1) =>
        //    {
        //        if (tabContacts.Height <= tableLayoutPanel.Height)
        //            tabContacts.Height = tableLayoutPanel.Height + tableLayoutPanelAddHeight;
        //        if (tabContacts.Width <= tableLayoutPanel.Width)
        //            tabContacts.Width = tableLayoutPanel.Width + tableLayoutPanelAddWidth;
        //    };
        //    for (int i = 1; i <= 21; i++)
        //    {

        //        DSUserControls.DHRContactsControl dhrContactsControl1 = new DHRContactsControl();
        //        dhrContactsControl1.UserPhotoUrl = "http://newtools.directhr.net/public/images/team_member/dhr_it_ah_60px_x_60px_72dpi_website.jpg";
        //        dhrContactsControl1.UserName = "Test User Name";
        //        dhrContactsControl1.ExtensionNumber = "123";
        //        dhrContactsControl1.Cellphone = "13500000000";
        //        dhrContactsControl1.UserEmail = "v.qiu@directhr.cn";
        //        tableLayoutPanel.Controls.Add(dhrContactsControl1);
        //    }
        //    tab.Controls.Add(tableLayoutPanel);
        //}

        //private void Test2()
        //{
        //    var tab = new TabPage("Shenzhen");
        //    tabContacts.TabPages.Add(tab);
        //    //var tab = tabContacts.TabPages["Ningbo"];
        //    var tableLayoutPanel = new TableLayoutPanel();
        //    tableLayoutPanel.AutoSize = true;
        //    tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        //    tableLayoutPanel.ColumnCount = 4;
        //    tableLayoutPanel.SizeChanged += (sender1, e1) =>
        //    {
        //        if (tabContacts.Height <= tableLayoutPanel.Height)
        //            tabContacts.Height = tableLayoutPanel.Height + tableLayoutPanelAddHeight;
        //        if (tabContacts.Width <= tableLayoutPanel.Width)
        //            tabContacts.Width = tableLayoutPanel.Width + tableLayoutPanelAddWidth;
        //    };
        //    for (int i = 1; i <= 12; i++)
        //    {

        //        DSUserControls.DHRContactsControl dhrContactsControl1 = new DHRContactsControl();
        //        dhrContactsControl1.UserPhotoUrl = "http://newtools.directhr.net/public/images/team_member/dhr_it_ah_60px_x_60px_72dpi_website.jpg";
        //        dhrContactsControl1.UserName = "Test User Name";
        //        dhrContactsControl1.ExtensionNumber = "123";
        //        dhrContactsControl1.Cellphone = "13500000000";
        //        dhrContactsControl1.UserEmail = "v.qiu@directhr.cn";

        //        tableLayoutPanel.Controls.Add(dhrContactsControl1);

        //    }
        //    tableLayoutPanel.BackColor = Color.White;
        //    tab.Controls.Add(tableLayoutPanel);
        //    tab.BackColor = Color.White;

        //}

        //private void tabContacts_TabIndexChanged(object sender, EventArgs e)
        //{

        //}

        private void tabContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTab = tabContacts.SelectedTab;
            var selectedTabName = selectedTab.Text;
            AgentSettings settings = new AgentSettings();
            settings.WriteString("Contacts", "SelectedTab", selectedTabName);
            settings.UpdateFile();
        }

        public void UserMouseClicked(object sender, UserContactInfo info)
        {
            //var cellphone = new TelephoneNumber(info.Cellphone).MakeFinallyNumber(AgentDatabaseProvider.CurrentAreaCode);
            if (info.Username == "John He")
            {
                MessageBox.Show("John He may not understand you on the phone, please write email instead. Thank you!");
            }
            FrmContactsCallingOption option = new FrmContactsCallingOption(info);
            option.ShowDialog();
            //if (!Eyebeam.Dialer.Dial(info.ExtensionNumber))
            //{
            //    MessageBox.Show("Startup Eyebeam failed!");
            //}
        }

        private void FrmContacts_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void FrmContacts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Hide();
        }

        //private void FrmContacts_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == 'v')
        //        Hide();
        //}

    }
}

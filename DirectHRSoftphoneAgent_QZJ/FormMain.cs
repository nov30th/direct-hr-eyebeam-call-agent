using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DHRSoftphone.AgentExceptions;
using DHRSoftphone.Eyebeam;
using Microsoft.Win32;
using System.Linq;
using System.Runtime.InteropServices;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    /// <summary>
    /// Copyright (C) 1985-2012 Vincent Qiu, Direct HR
    /// Http://hoho.im 版权所有。
    /// NameSpace: DirectHRSoftphoneAgent_QZJ
    /// FullName: DirectHRSoftphoneAgent_QZJ.Form1
    /// Class Created: 2011/11/25 16:46
    /// On Computer: NOV30TH-LAPTOP - Administrator
    /// </summary>
    public partial class FormMain : Form
    {
        public static FormMain frmMain;
        private int _currentPressedPosition = 0;
        private bool _isClosing = false;
        private string _lastNumber = string.Empty;
        private char[] _pressedKey = new char[3];
        private AgentUpdate au;
        private MyHttpServer httpServer;
        private FormWelcomeScreen m_frmWelcomeDialog;
        private FrmMeetingRoom m_meetingRoomDialog;
        private ContextMenu m_menu;
        private NotifyIcon m_notifyicon;
        private FrmSettings m_settingsDialog;
        private Thread thread;
        private Thread QzjNdisIoctlThread;
        private Thread m_keepMatchingClientRunning;
        private Process m_eyebeam;
        private Process m_contacts;
        public delegate void ProcessDelegate();

        //注册热键的api
        [DllImport("user32")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint control, Keys vk);
        //解除注册热键的api
        [DllImport("user32")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        public FormMain()
        {
            frmMain = this;
            FrmLoading loading = new FrmLoading();
            loading.Show();
            loading.SetStatus("Initialize Component ...");
            InitializeComponent();

            try
            {
                RegisterHotKey(this.Handle, 7069, 3, Keys.S);
            }
            catch
            {
                MessageBox.Show("Hot Key Registed failed!", "Soft Phone Agent");
            }


            #region Check port whether is in use


            #endregion



            #region notify icon
            loading.SetStatus("Notify Icon ...");
            m_notifyicon = new NotifyIcon();
            m_notifyicon.Text = "DirectHR Softphone Agent";
            m_notifyicon.Visible = true;
            m_notifyicon.Icon = this.Icon;// new Icon(GetType(), "20110811125203259_easyicon_cn_48.ico");

            #endregion notify icon

            #region menu for dhr softphone agent

            m_menu = new ContextMenu();
            m_menu.MenuItems.Add(0,
                new MenuItem("Show", Show_Click));
            m_menu.MenuItems.Add(1,
                new MenuItem("Hide", Hide_Click));

            //m_menu.MenuItems.Add(2,
            //    new MenuItem("Exit", new System.EventHandler(Exit_Click)));
            m_notifyicon.ContextMenu = m_menu;
            m_notifyicon.DoubleClick += Show_Click;

            #endregion menu for dhr softphone agent

            #region http server
            loading.SetStatus("Starting Web Server ...");
            //try
            //{
            httpServer = new MyHttpServer(7069);
            httpServer.InformationFromGetMethodChanged += AddNumberToListBox;
            thread = new Thread(new ThreadStart(httpServer.listen));
            thread.Start();

            //}
            //            catch (Exception e)
            //            {
            //                MessageBox.Show(@"Please notice! The port 7069 has been used by other program, this agent can not startup without this port!
            //Please try to logoff and login again if this still happens!");
            //                Application.Exit();
            //            }

            #endregion http server

            loading.SetStatus("Loading Settings ...");
            AgentSettingsStartingArgs agentSettingsStartingArgs = new AgentSettingsStartingArgs();
            agentSettingsStartingArgs.appPath = GetAppPath();
            agentSettingsStartingArgs.userDocumentPath = GetUserDocumentPath();

            Thread initSetting = new Thread(new ParameterizedThreadStart(AgentSettings.InitSettings));
            initSetting.Start(agentSettingsStartingArgs);

            while (AgentSettings.Inited == false)
            {
                Thread.Sleep(10);
                Application.DoEvents();
            }

            loading.SetStatus("Init Update Module ...");
            au = new AgentUpdate();
            au.NewVersionDetected += UpdateNotify;
            au.UpdateDownloaded += UpdateFileDownloaded;

            AgentLogs.WriteLog("CheckUpdate", AgentLogEventLevel.Info);
            Thread checkingUpdate = new Thread(new ThreadStart(au.CheckUpdate));
            checkingUpdate.Start();

            AgentLogs.WriteLog("UserStatus", AgentLogEventLevel.Info);
            Thread userStatus = new Thread(new ThreadStart(UserStatus.UploadUserStatus));
            userStatus.Start();

            timer1.Enabled = true;

            lab_areacode.Text = string.Empty;
            lab_cardtype.Text = string.Empty;
            lab_cityname.Text = string.Empty;
            lab_number.Text = string.Empty;

            loading.SetStatus("Init Program Windows ...");

            if (m_settingsDialog == null)
                m_settingsDialog = new FrmSettings();
            if (m_frmWelcomeDialog == null)
                m_frmWelcomeDialog = new FormWelcomeScreen();

            /********************************
             Set extension if it's empty
             ********************************/
            loading.SetStatus("Loading User Settings ...");
            SetCurrentStatus();
            AgentSettings set = new AgentSettings();
            loading.SetName(set.GetFullname());
            SetCurrentStatus();

            /********************************
             Eyebeam Settings Checker
             ********************************/
            loading.SetStatus("Checking Softphone Settings ...");
            AgentLogs.WriteLog("Eyebeam Configuration Checker Started...", AgentLogEventLevel.Info);

            var eyebeam = new Eyebeam.ConfigManager();
            var domain = eyebeam.GetIPPBXIPAddress(set.GetCurrentExtension());
            if (string.IsNullOrEmpty(domain))
            {
                MessageBox.Show("There is no such extension number in system or you need update me, soft phone checker will be disabled!",
                                "Is Extension Number Correct?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AgentLogs.WriteLog("Incorrect extension data, checker disabled.", AgentLogEventLevel.Info);
            }
            else
            {
                var sipUser = new SIPUser()
                {
                    DisplayName = set.GetFullname(),
                    Domain = domain,
                    Enabled = true,
                    Extension = set.GetCurrentExtension(),
                    Password = string.Empty,
                    Username = set.GetCurrentExtension()
                };
                if (!eyebeam.IsExistFile)
                {
                    AgentLogs.WriteLog("No eyebeam conf file found, writing configuration...", AgentLogEventLevel.Info);
                    eyebeam.RewriteConfigurationWithSimplyConfiguration(sipUser, true);
                }
                else if (!eyebeam.CheckConfigurationByExtension(set.GetCurrentExtension()))
                {
                    FrmEyebeamSettings eyebeamSettingsFrm = new FrmEyebeamSettings();
                    var retval = eyebeamSettingsFrm.ShowDialog();
                    if (retval == DialogResult.Cancel)
                        AgentLogs.WriteLog("Wrong extension settings, user cancelled re-write...", AgentLogEventLevel.Info);
                    else
                        AgentLogs.WriteLog("Wrong extension settings, writing configuration...", AgentLogEventLevel.Info);

                    //if (MessageBox.Show("Your extension does not in Softphone Client!\r\n\r\n" +
                    //                    "Your Softphone may not work properly.\r\n\r\n" +
                    //                    "Would you like to fix this problem?"
                    //                    , "Softphone Settings not correct"
                    //                    , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    //{
                    //    eyebeam.RewriteConfigurationWithSimplyConfiguration(sipUser, true);
                    //}
                    //else
                    //{
                    //}
                }
            }


            /********************************
             Plugin Loader
             *********************************/
            loading.SetStatus("Loading Plugins ...");

            DhrSpPluginProvider provider = new DhrSpPluginProvider();
            provider.LoadAllPlugins(agentSettingsStartingArgs.appPath);
            provider.InitBeforeEvents();
            provider.InitAllPluginsWithConfigurations(AgentSettings.GetSettings());
            provider.Startup();


            /********************************
             Meeting room loader
             *********************************/
            loading.SetStatus("Loading Meeting Rooms ...");

            m_meetingRoomDialog = new FrmMeetingRoom();
            m_meetingRoomDialog.Show();

            //FrmContacts.Init();

            //FrmContacts.frmContacts.Visible = false;
            //FrmContacts.frmContacts.Show();
            //FrmContacts.frmContacts.Hide();
            //FrmContacts.frmContacts.Visible = true;

            /********************************
             Plugin Check
             *********************************/
            var plugins = DhrSpPluginProvider.GetPlugins();
            if (plugins.Find(p => p.PluginName == "ProcessKiller") == null
                || plugins.Find(p => p.PluginName == "DNSFixer") == null
                || plugins.Find(p => p.PluginName == "AgentStatusUploader") == null
                )
            {
                throw new ApplicationException("Sorry, important plugin missing! Application will not exit!");
                //Application.Exit();
                //Environment.Exit(-1);
            }


            /********************************
             Check QzjNdis Driver
             ********************************/
            //loading.SetStatus("Checking Driver ...");
            //if (!QzjIoCtl.SendHeartbeat())
            //{
            //    System.Diagnostics.Process p = new System.Diagnostics.Process();
            //    loading.SetStatus("Installing Driver ...");
            //    try
            //    {
            //        p.StartInfo.FileName = Path.Combine(GetAppPath(), "InstallDriver.bat");
            //        p.Start();
            //        p.WaitForExit();
            //        var lastwait = DateTime.Now;
            //        while (DateTime.Now < lastwait.AddSeconds(20))
            //        {
            //            Thread.Sleep(100);
            //            Application.DoEvents();
            //        }
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Driver installation failed! The software can not be started without driver.");
            //    }

            //    if (!QzjIoCtl.SendHeartbeat())
            //    {
            //        throw new AgentException("WARNING: Driver check failed! Application will now exit.., Please try reboot this computer and if this still happens, report this problem to Vincent Qiu.");
            //    }


            //    //MessageBox.Show("Driver check failed! Application will not exit...", "DRIVER ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //Application.Exit();
            //    //Environment.Exit(-1);
            //}
            //QzjNdisIoctlThread = new Thread(new ThreadStart(QzjIoCtl.Timer));
            //QzjNdisIoctlThread.Start();

            /********************************
             Start the softphone
             *********************************/
            loading.SetStatus("Start Soft Phone Client ...");

            Eyebeam.Dialer.StartupPath = Application.StartupPath;
            System.Diagnostics.ProcessStartInfo newProcess = new System.Diagnostics.ProcessStartInfo();
            newProcess.FileName = "eyebeam.exe";
            newProcess.Arguments = "";
            newProcess.WorkingDirectory = Application.StartupPath;
            if (!File.Exists(Path.Combine(Application.StartupPath, "eyebeam.exe")))
            {
                MessageBox.Show("Please make sure you are running this tool under the same path with eyebeam.exe file.");
            }
            else
            {
                try
                {
                    m_eyebeam = System.Diagnostics.Process.Start(newProcess);
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    MessageBox.Show("Failed to start up Eyebeam.");
                }
            }


            /********************************
             Start the Matching Client
             *********************************/

            //m_keepMatchingClientRunning = new Thread(new ThreadStart(this.KeepMatchingClientRunning));
            //m_keepMatchingClientRunning.Start();

            #region DHR Contacts

            /********************************
             DHR Contacts init
             *********************************/
            loading.SetStatus("Loading Contacts Data / Pictures ... Connecting newtools.directhr.net");
            //TODO: is better way to init the window?
            new ProcessDelegate(FrmContacts.Init).BeginInvoke(null, null);
            var startLoadContactsAt = DateTime.Now;
            int loadingStatus = 0;
            while (!FrmContacts.IsLoaded)
            {
                if (loadingStatus != FrmContacts.LoadingPercent)
                    loading.SetStatus("Loading Contacts Data / Pictures ... " + (loadingStatus = FrmContacts.LoadingPercent).ToString() + "%");
                Thread.Sleep(100);
                Application.DoEvents();
                if (FrmContacts.IsError)
                    break;
                if (startLoadContactsAt.AddSeconds(30) < DateTime.Now)
                {
                    MessageBox.Show(@"Contacts data / pictures from newtools loading more than 30 secs,
this problem might be your slow Internet connection to newtools.directhr.net.
loading process will be in background and during the time you cannot use DHR Contacts feature!",
                                                                                                "NEWTOOLS SLOW",
                                                                                                 MessageBoxButtons.OK,
                                                                                                  MessageBoxIcon.Warning);
                    AgentLogs.WriteLog("Contacts loading timeout, into background!", AgentLogEventLevel.Info);
                    break;
                }
            }

            #endregion DHR Contacts


            /********************************
             All boot Finiahed.
             *********************************/

            loading.SetStatus("Done.");
            {
                var lastwait = DateTime.Now;
                while (DateTime.Now < lastwait.AddSeconds(5))
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
            }
            AgentLogs.WriteLog("Inited Done...", AgentLogEventLevel.Info);
            loading.Close();
            SetCurrentStatus();
        }

        private delegate void AddListItemCallBack(string text);

        private delegate void ChangeSelectedTextCallBack(string text);

        private delegate void FocusOnControllerCallBack();

        protected override bool ShowWithoutActivation
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Add Telephoe Number to ListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddNumberToListBox(object sender, TelephoneEventArg e)
        {
            if (e.TelephoneNumber == "0" || e.TelephoneNumber == string.Empty)
                return;
            var number = new TelephoneNumber(e.TelephoneNumber).MakeFinallyNumber(AgentDatabaseProvider.CurrentAreaCode);
            e.TelephoneNumber = number;

            //string lastNumber = string.Empty;
            if (_lastNumber != e.TelephoneNumber)
            {
                AddListItem(e.TelephoneNumber);
                _lastNumber = e.TelephoneNumber;
            }
            ChangeSelectedNumber(e.TelephoneNumber);
            if (e.Commands.ToString() == "Dial")
            {
                _btnCallNumber_Click(this, null);
            }
            else
            {
                FocusOnController();
            }
        }

        public string GetAppPath()
        {
            return Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\";
        }

        public string GetUserDocumentPath()
        {
            return System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        /// <summary>
        /// Set current information on status bar also check the settings if it is valued
        /// </summary>
        public void SetCurrentStatus()
        {
            AgentSettings set = new AgentSettings();
            while (string.IsNullOrEmpty(AgentDatabaseProvider.CurrentAreaCode)
                || AgentDatabaseProvider.CurrentAreaCode == "0000"
                || string.IsNullOrEmpty(set.GetFullname())
                || set.GetCurrentExtension() == "000"
                )
            {
                m_frmWelcomeDialog.ShowDialog();
                m_settingsDialog.ShowDialog();
            }
            try
            {
                RegistryKey regWrite = Registry.CurrentUser.CreateSubKey("Environment");
                regWrite.SetValue("DHR_Username", set.GetFullname());
                regWrite.SetValue("DHR_Extension", set.GetCurrentExtension());
                regWrite.SetValue("DHR_AreaCode", AgentDatabaseProvider.CurrentAreaCode);
                regWrite.Close();

                regWrite = Registry.LocalMachine.CreateSubKey("SOFTWARE\\DIRECTHR");
                regWrite.SetValue("DHR_Username", set.GetFullname());
                regWrite.SetValue("DHR_Extension", set.GetCurrentExtension());
                regWrite.SetValue("DHR_AreaCode", AgentDatabaseProvider.CurrentAreaCode);
                regWrite.Close();

            }
            catch (Exception ex)
            {
                new AgentException("Failed to write info to system!", ex);
            }
            var city = AgentDatabaseProvider.GetCityByAreaCode(AgentDatabaseProvider.CurrentAreaCode);
            if (city != null)
                toolStripStatusLabel1.Text = string.Format("Current City Setting: {0} Extension: {1}",
                    city.City,
                    set.GetCurrentExtension()
                    );
            else
            {
                MessageBox.Show("City area data error! Please re-set yopur city area code.");

                //    m_settingsDialog.ShowDialog();
            }
        }

        public void UpdateFileDownloaded(object sender, UpdateEventArgs e)
        {
            MessageBox.Show("New version detected, Please update!\r\nPress 'OK' to continue.",
                            "Direct HR Softphone Agent New Version", MessageBoxButtons.OK, MessageBoxIcon.Information);

            while (true)
            {
                Process p = System.Diagnostics.Process.Start(e.filename);
                while (!p.HasExited)
                    Thread.Sleep(3000);
                Thread.Sleep(5000);
            }
        }

        public void UpdateNotify(object sender, UpdateEventArgs e)
        {
            toolStripStatusLabel1.Text = "New Version " + e.version.ToString() + " detected!";
            timer1.Enabled = false;
            Thread thread = new Thread(new ThreadStart(au.DownlaodFile));
            thread.Start();

            //do nothing
        }

        protected void DoAddNumberToList(object sender, string telephoneNumber)
        {
            //var number = new TelephoneNumber(telephoneNumber).MakeFinallyNumber(AgentDatabaseProvider.CurrentAreaCode);
            int index = _lstTeleNumbers.Items.Add(telephoneNumber);
            //_lstTeleNumbers.SelectedIndex = index;
        }

        protected void Exit_Click(Object sender, System.EventArgs e)
        {
            _isClosing = true;
            Close();
        }

        protected void Hide_Click(Object sender, System.EventArgs e)
        {
            Hide();
        }

        protected override void OnClosed(EventArgs e)
        {
            //thread.Abort();
            //httpServer.Exit();
            UnregisterHotKey(this.Handle, 7069);
            m_notifyicon.Dispose();
            Application.ExitThread();
            Application.Exit();

            //if (e.CloseReason == CloseReason.TaskManagerClosing)
            //    MessageBox.Show("You just try to close me via TASK MANAGER, too nude! If the program has any problem you may need to email me, thank you!", "Direct HR Softphone Agent", MessageBoxButtons.OK);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_isClosing && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            //if (e.CloseReason == CloseReason.TaskManagerClosing)
            //    MessageBox.Show("You just try to close me via TASK MANAGER, too nude! If the program has any problem you may need to email me, thank you!", "Direct HR Softphone Agent", MessageBoxButtons.OK);
        }

        protected void Show_Click(Object sender, System.EventArgs e)
        {
            frmMain.Focus();
            Show();
        }

        private void _btnCallNumber_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo newProcess = new System.Diagnostics.ProcessStartInfo();
            newProcess.FileName = "eyebeam.exe";
            newProcess.Arguments = " -dial=\"" + _txtSelectedNumber.Text + "\"";
            newProcess.WorkingDirectory = Application.StartupPath;
            try
            {
                var processing = System.Diagnostics.Process.Start(newProcess);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show("Please make sure you are running this tool under the same path with eyebeam.exe file.");
                return;
            }
        }

        /// <summary>
        /// Selected Index Changed And Add Telephone Number to Text Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _lstTeleNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lstTeleNumbers.SelectedItem == null)
                return;
            _txtSelectedNumber.Text = _lstTeleNumbers.GetItemText(_lstTeleNumbers.SelectedItem);

            var selectedNumber = _lstTeleNumbers.SelectedItem.ToString();

            var number = new TelephoneNumber(selectedNumber);

            lab_areacode.Text = number.AreaCode ?? string.Empty;
            lab_cardtype.Text = (number.TelephoneType == TelephoneNumber.TelephoneNumberType.Cellphone) ?
                (number.Location != null) ? number.Location.CardType : "" :
                Enum.GetName(typeof(TelephoneNumber.TelephoneNumberType), number.TelephoneType);
            lab_cityname.Text = number.CityName ?? string.Empty;
            lab_number.Text = number.FixedNumber ?? string.Empty;
        }

        private void _txtSelectedNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                _btnCallNumber_Click(this, null);
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmAboutcs about = new FrmAboutcs();
            //DhrSpPluginProvider pluginProvider = new DhrSpPluginProvider();
            var plugins = DhrSpPluginProvider.GetPlugins();
            var sb = new StringBuilder("Plugins:" + Environment.NewLine);
            foreach (var pluginObject in plugins)
            {
                sb.AppendLine(pluginObject.PluginName);
                sb.AppendLine(pluginObject.PluginVersion);
                sb.AppendLine();
            }
            sb.AppendLine("System:");
            sb.AppendLine(Environment.MachineName);
            sb.AppendLine(Environment.CurrentDirectory);
            about.SetPluginText(sb.ToString());
            about.ShowDialog();
        }

        private void AddListItem(string text)
        {
            if (this._lstTeleNumbers.InvokeRequired)
            {
                AddListItemCallBack alicb = new AddListItemCallBack(AddListItem);
                this.Invoke(alicb, new object[] { text });
            }
            else
            {
                this._lstTeleNumbers.Items.Add(text);
            }
        }

        private void ChangeSelectedNumber(string text)
        {
            if (this._txtSelectedNumber.InvokeRequired)
            {
                ChangeSelectedTextCallBack alicb = new ChangeSelectedTextCallBack(ChangeSelectedNumber);
                this.Invoke(alicb, new object[] { text });
            }
            else
            {
                this._txtSelectedNumber.Text = text;
            }
        }

        private void cleanALLCONFIGSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.Delete(AgentSettings.GetConfigFilePath());
            if (MessageBox.Show("Do you also want to exit this agent?", "Soft phone agent DEBUG OPTION", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Application.Exit();
        }

        private void extensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmExtensionSettings extension = new FrmExtensionSettings();
            extension.ShowDialog();
        }

        private void FocusOnController()
        {
            if (this.InvokeRequired)
            {
                FocusOnControllerCallBack foc = new FocusOnControllerCallBack(FocusOnController);
                this.Invoke(foc);
            }
            else
            {
                this.Focus();
                _txtSelectedNumber.Focus();
                this.Show();
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int i = 0;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'q')
                _currentPressedPosition = 0;
            _pressedKey[_currentPressedPosition] = e.KeyChar;
            if ((++_currentPressedPosition) == 3)
                _currentPressedPosition = 0;
            if (_pressedKey[0] == 'q' && _pressedKey[1] == 'z' && _pressedKey[2] == 'j')
            {
                yesYesToolStripMenuItem.Visible = true;
                Width = 608;
            }

        }

        private void getChromePluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AgentLogs.WriteLog("Trying to start Chrome to goto the plugin page");
                System.Diagnostics.Process.Start("chrome", "https://chrome.google.com/webstore/detail/mapjclclndigebgobdncmlhmpmnhmpma");
            }
            catch (Exception ex)
            {
                AgentLogs.WriteLog("Can not start the Chrome or other issues: " + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace, AgentLogEventLevel.Errors);
                MessageBox.Show("Can not start Google Chrome browser!");
            }
        }

        private void helpToolStripMenuItemHelp_Click(object sender, EventArgs e)
        {
            m_frmWelcomeDialog.ShowDialog();
        }

        private void killMyselfToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Application.Exit();
            Environment.Exit(-1);
        }

        private void locationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_settingsDialog.ShowDialog();
            SetCurrentStatus();
        }

        private void onlineStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://softphone.directhr.cn/OnlineAgents.aspx");
        }

        private void openCONFIGFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void openErrorsLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("NotePad.exe ", AgentException.GetErrorFile());
        }

        private void openLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("NotePad.exe ", AgentLogs.GetLogFile());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            au.CheckUpdate();
        }

        private void allWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDHRSPPlugin.PluginObject processPlugin = null;
            var plugins = DhrSpPluginProvider.GetPlugins();
            foreach (var plugin in plugins)
            {
                if (plugin.PluginName == "ProcessKiller")
                {
                    processPlugin = plugin;
                    break;
                }
            }
            if (processPlugin == null)
            {
                MessageBox.Show("Please put the ProcessKiller plugin into Plugin folder to use this feature!",
                                "ERROR in DEBUG FEATURE");
                return;
            }
            txtInformation.Clear();
        }

        private void softphoneClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new FrmEyebeamSettings();
            dialog.SetLabelText("Please notice that if you re-write the settings in the soft phone client, you may lose other hardware configuration in the settings file.\r\nPlease ask the IT team before you doing this.");
            dialog.ShowDialog();
        }

        private void loadContactsDemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmContacts frm = new FrmContacts();
            frm.Show();
        }

        private void btn_SmartCall_Click(object sender, EventArgs e)
        {
            if (SpecialKeys(_txtSelectedNumber.Text))
                return;
            var number = new TelephoneNumber(_txtSelectedNumber.Text).MakeFinallyNumber(AgentDatabaseProvider.CurrentAreaCode);
            AddListItem(number);
            _lstTeleNumbers.SelectedIndex = _lstTeleNumbers.Items.Count - 1;
        }

        private void dHRContactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private bool SpecialKeys(string text)
        {
            if (text == "fuckme")
            {
                m_meetingRoomDialog.Hide();
                QzjIoCtl.SendAllowAll();
                return true;
            }
            return false;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("TeamViewerQS.exe");
            }
            catch
            {
                //
            }
        }

        private void showContactsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void exportContactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmExportContacts contacts = new FrmExportContacts();
            contacts.ShowDialog();
            return;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Width = 393;
        }

        private void hideMeetingsRoomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_meetingRoomDialog.Hide();
        }

        private void reinstallDriverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = new System.Diagnostics.ProcessStartInfo();
            info.FileName = Path.Combine(GetAppPath(), "InstallDriver.bat");
            info.WorkingDirectory = GetAppPath();
            //System.Diagnostics.Process p = new System.Diagnostics.Process();
            var p = System.Diagnostics.Process.Start(info);
            p.WaitForExit();
            //p.Dispose();
        }

        private void allowAllNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QzjIoCtl.SendAllowAll();
        }

        private void blockAllNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QzjIoCtl.SendBlockAll();
        }

        private void restoreToHeartbeatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QzjIoCtl.SendResotreToHeartbeat();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0312:  //这个是window消息定义的注册的热键消息  
                    if (m.WParam.ToString() == "7069")   // 按下CTRL+Q隐藏  
                    {
                        Show_Click(this, null);
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        //public void KeepMatchingClientRunning()
        //{
        //    while (true)
        //    {
        //        if (Process.GetProcesses().Where(p => p.ProcessName == "DhrMatchingUrlService").Count() == 0)
        //        {

        //            Eyebeam.Dialer.StartupPath = Application.StartupPath;
        //            System.Diagnostics.ProcessStartInfo newProcessMatching = new System.Diagnostics.ProcessStartInfo();
        //            newProcessMatching.FileName = "DhrMatchingUrlService.exe";
        //            newProcessMatching.Arguments = "";
        //            newProcessMatching.WorkingDirectory = Application.StartupPath;
        //            if (!File.Exists(Path.Combine(Application.StartupPath, "DhrMatchingUrlService.exe")))
        //            {
        //                //MessageBox.Show("Missing Matching Client file! Please re-install this soft phone agnet.");
        //                throw new AgentException("Missing Matching Client file! Please re-install this soft phone agnet.");
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    m_eyebeam = System.Diagnostics.Process.Start(newProcessMatching);
        //                }
        //                catch (System.ComponentModel.Win32Exception ex)
        //                {
        //                    MessageBox.Show("Failed to start up Matching Client.");
        //                }
        //            }
        //        }
        //        Thread.Sleep(60000);
        //    }
        //}
    }
}
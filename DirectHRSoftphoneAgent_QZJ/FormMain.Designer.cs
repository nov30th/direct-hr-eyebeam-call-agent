namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {

                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._lstTeleNumbers = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_SmartCall = new System.Windows.Forms.Button();
            this._txtSelectedNumber = new System.Windows.Forms.TextBox();
            this._btnCallNumber = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getChromePluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.softphoneClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extensionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dHRContactsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showContactsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportContactsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openErrorsLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.yesYesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neverUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorLogToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsLogToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testUnhandleExceptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCONFIGFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killMyselfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanALLCONFIGSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideMeetingsRoomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusUploadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frmTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadContactsDemoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.driverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reinstallDriverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allowAllNetworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blockAllNetworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToHeartbeatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lab_cardtype = new System.Windows.Forms.Label();
            this.lab_areacode = new System.Windows.Forms.Label();
            this.lab_cityname = new System.Windows.Forms.Label();
            this.lab_number = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtInformation = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._lstTeleNumbers);
            this.groupBox1.Location = new System.Drawing.Point(8, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 182);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Telephone Numbers";
            // 
            // _lstTeleNumbers
            // 
            this._lstTeleNumbers.FormattingEnabled = true;
            this._lstTeleNumbers.Location = new System.Drawing.Point(12, 23);
            this._lstTeleNumbers.Name = "_lstTeleNumbers";
            this._lstTeleNumbers.Size = new System.Drawing.Size(177, 147);
            this._lstTeleNumbers.TabIndex = 0;
            this._lstTeleNumbers.SelectedIndexChanged += new System.EventHandler(this._lstTeleNumbers_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_SmartCall);
            this.groupBox2.Controls.Add(this._txtSelectedNumber);
            this.groupBox2.Controls.Add(this._btnCallNumber);
            this.groupBox2.Location = new System.Drawing.Point(8, 222);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 60);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected Telephone Number";
            // 
            // btn_SmartCall
            // 
            this.btn_SmartCall.Location = new System.Drawing.Point(196, 21);
            this.btn_SmartCall.Name = "btn_SmartCall";
            this.btn_SmartCall.Size = new System.Drawing.Size(155, 23);
            this.btn_SmartCall.TabIndex = 3;
            this.btn_SmartCall.Text = "Format Number";
            this.btn_SmartCall.UseVisualStyleBackColor = true;
            this.btn_SmartCall.Click += new System.EventHandler(this.btn_SmartCall_Click);
            // 
            // _txtSelectedNumber
            // 
            this._txtSelectedNumber.Location = new System.Drawing.Point(12, 23);
            this._txtSelectedNumber.Name = "_txtSelectedNumber";
            this._txtSelectedNumber.Size = new System.Drawing.Size(101, 20);
            this._txtSelectedNumber.TabIndex = 0;
            this._txtSelectedNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._txtSelectedNumber_KeyPress);
            // 
            // _btnCallNumber
            // 
            this._btnCallNumber.Location = new System.Drawing.Point(125, 21);
            this._btnCallNumber.Name = "_btnCallNumber";
            this._btnCallNumber.Size = new System.Drawing.Size(64, 23);
            this._btnCallNumber.TabIndex = 2;
            this._btnCallNumber.Text = "Call";
            this._btnCallNumber.UseVisualStyleBackColor = true;
            this._btnCallNumber.Click += new System.EventHandler(this._btnCallNumber_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.dHRContactsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.yesYesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(603, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getChromePluginToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // getChromePluginToolStripMenuItem
            // 
            this.getChromePluginToolStripMenuItem.Name = "getChromePluginToolStripMenuItem";
            this.getChromePluginToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.getChromePluginToolStripMenuItem.Text = "Get Chrome Plugin";
            this.getChromePluginToolStripMenuItem.Click += new System.EventHandler(this.getChromePluginToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locationToolStripMenuItem,
            this.softphoneClientToolStripMenuItem,
            this.extensionToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Settings";
            // 
            // locationToolStripMenuItem
            // 
            this.locationToolStripMenuItem.Name = "locationToolStripMenuItem";
            this.locationToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.locationToolStripMenuItem.Text = "Basic";
            this.locationToolStripMenuItem.Click += new System.EventHandler(this.locationToolStripMenuItem_Click);
            // 
            // softphoneClientToolStripMenuItem
            // 
            this.softphoneClientToolStripMenuItem.Name = "softphoneClientToolStripMenuItem";
            this.softphoneClientToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.softphoneClientToolStripMenuItem.Text = "Softphone Client";
            this.softphoneClientToolStripMenuItem.Click += new System.EventHandler(this.softphoneClientToolStripMenuItem_Click);
            // 
            // extensionToolStripMenuItem
            // 
            this.extensionToolStripMenuItem.Enabled = false;
            this.extensionToolStripMenuItem.Name = "extensionToolStripMenuItem";
            this.extensionToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.extensionToolStripMenuItem.Text = "Extension";
            this.extensionToolStripMenuItem.Visible = false;
            this.extensionToolStripMenuItem.Click += new System.EventHandler(this.extensionToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(84, 20);
            this.toolStripMenuItem1.Text = "TeamViewer";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // dHRContactsToolStripMenuItem
            // 
            this.dHRContactsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showContactsToolStripMenuItem,
            this.exportContactsToolStripMenuItem});
            this.dHRContactsToolStripMenuItem.Name = "dHRContactsToolStripMenuItem";
            this.dHRContactsToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.dHRContactsToolStripMenuItem.Text = "DHR Contacts";
            this.dHRContactsToolStripMenuItem.Click += new System.EventHandler(this.dHRContactsToolStripMenuItem_Click);
            // 
            // showContactsToolStripMenuItem
            // 
            this.showContactsToolStripMenuItem.Name = "showContactsToolStripMenuItem";
            this.showContactsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.showContactsToolStripMenuItem.Text = "Show Contacts";
            this.showContactsToolStripMenuItem.Click += new System.EventHandler(this.showContactsToolStripMenuItem_Click);
            // 
            // exportContactsToolStripMenuItem
            // 
            this.exportContactsToolStripMenuItem.Name = "exportContactsToolStripMenuItem";
            this.exportContactsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exportContactsToolStripMenuItem.Text = "Export Contacts";
            this.exportContactsToolStripMenuItem.Click += new System.EventHandler(this.exportContactsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLogFileToolStripMenuItem,
            this.openErrorsLogToolStripMenuItem,
            this.aboutToolStripMenuItem1,
            this.helpToolStripMenuItemHelp});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // openLogFileToolStripMenuItem
            // 
            this.openLogFileToolStripMenuItem.Name = "openLogFileToolStripMenuItem";
            this.openLogFileToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openLogFileToolStripMenuItem.Text = "Open Details Log";
            this.openLogFileToolStripMenuItem.Visible = false;
            this.openLogFileToolStripMenuItem.Click += new System.EventHandler(this.openLogFileToolStripMenuItem_Click);
            // 
            // openErrorsLogToolStripMenuItem
            // 
            this.openErrorsLogToolStripMenuItem.Name = "openErrorsLogToolStripMenuItem";
            this.openErrorsLogToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openErrorsLogToolStripMenuItem.Text = "Open Errors Log";
            this.openErrorsLogToolStripMenuItem.Visible = false;
            this.openErrorsLogToolStripMenuItem.Click += new System.EventHandler(this.openErrorsLogToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItemHelp
            // 
            this.helpToolStripMenuItemHelp.Name = "helpToolStripMenuItemHelp";
            this.helpToolStripMenuItemHelp.Size = new System.Drawing.Size(164, 22);
            this.helpToolStripMenuItemHelp.Text = "Audio Settings";
            this.helpToolStripMenuItemHelp.Click += new System.EventHandler(this.helpToolStripMenuItemHelp_Click);
            // 
            // yesYesToolStripMenuItem
            // 
            this.yesYesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.logsToolStripMenuItem,
            this.programToolStripMenuItem,
            this.statusUploadingToolStripMenuItem,
            this.frmTestToolStripMenuItem,
            this.driverToolStripMenuItem});
            this.yesYesToolStripMenuItem.Name = "yesYesToolStripMenuItem";
            this.yesYesToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.yesYesToolStripMenuItem.Text = "Yes!!Yes!!";
            this.yesYesToolStripMenuItem.Visible = false;
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysUpdateToolStripMenuItem,
            this.neverUpdateToolStripMenuItem});
            this.settingsToolStripMenuItem.Enabled = false;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.settingsToolStripMenuItem.Text = "Update Option";
            this.settingsToolStripMenuItem.Visible = false;
            // 
            // alwaysUpdateToolStripMenuItem
            // 
            this.alwaysUpdateToolStripMenuItem.Name = "alwaysUpdateToolStripMenuItem";
            this.alwaysUpdateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.alwaysUpdateToolStripMenuItem.Text = "Always Update";
            // 
            // neverUpdateToolStripMenuItem
            // 
            this.neverUpdateToolStripMenuItem.Name = "neverUpdateToolStripMenuItem";
            this.neverUpdateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.neverUpdateToolStripMenuItem.Text = "Never Update";
            // 
            // logsToolStripMenuItem
            // 
            this.logsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.errorLogToolStripMenuItem1,
            this.detailsLogToolStripMenuItem1,
            this.onlineStatusToolStripMenuItem});
            this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
            this.logsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.logsToolStripMenuItem.Text = "Logs";
            // 
            // errorLogToolStripMenuItem1
            // 
            this.errorLogToolStripMenuItem1.Name = "errorLogToolStripMenuItem1";
            this.errorLogToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.errorLogToolStripMenuItem1.Text = "Errors Log";
            this.errorLogToolStripMenuItem1.Click += new System.EventHandler(this.openErrorsLogToolStripMenuItem_Click);
            // 
            // detailsLogToolStripMenuItem1
            // 
            this.detailsLogToolStripMenuItem1.Name = "detailsLogToolStripMenuItem1";
            this.detailsLogToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.detailsLogToolStripMenuItem1.Text = "Details Log";
            this.detailsLogToolStripMenuItem1.Click += new System.EventHandler(this.openLogFileToolStripMenuItem_Click);
            // 
            // onlineStatusToolStripMenuItem
            // 
            this.onlineStatusToolStripMenuItem.Name = "onlineStatusToolStripMenuItem";
            this.onlineStatusToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.onlineStatusToolStripMenuItem.Text = "Online Status";
            this.onlineStatusToolStripMenuItem.Click += new System.EventHandler(this.onlineStatusToolStripMenuItem_Click);
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testUnhandleExceptionToolStripMenuItem,
            this.openCONFIGFileToolStripMenuItem,
            this.killMyselfToolStripMenuItem,
            this.cleanALLCONFIGSToolStripMenuItem,
            this.allWindowsToolStripMenuItem,
            this.hideMeetingsRoomsToolStripMenuItem});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // testUnhandleExceptionToolStripMenuItem
            // 
            this.testUnhandleExceptionToolStripMenuItem.Enabled = false;
            this.testUnhandleExceptionToolStripMenuItem.Name = "testUnhandleExceptionToolStripMenuItem";
            this.testUnhandleExceptionToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.testUnhandleExceptionToolStripMenuItem.Text = "Test Unhandle Exception";
            // 
            // openCONFIGFileToolStripMenuItem
            // 
            this.openCONFIGFileToolStripMenuItem.Enabled = false;
            this.openCONFIGFileToolStripMenuItem.Name = "openCONFIGFileToolStripMenuItem";
            this.openCONFIGFileToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.openCONFIGFileToolStripMenuItem.Text = "Edit CONFIG file";
            this.openCONFIGFileToolStripMenuItem.Click += new System.EventHandler(this.openCONFIGFileToolStripMenuItem_Click);
            // 
            // killMyselfToolStripMenuItem
            // 
            this.killMyselfToolStripMenuItem.Name = "killMyselfToolStripMenuItem";
            this.killMyselfToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.killMyselfToolStripMenuItem.Text = "Kill Myself";
            this.killMyselfToolStripMenuItem.Click += new System.EventHandler(this.killMyselfToolStripMenuItem_Click);
            // 
            // cleanALLCONFIGSToolStripMenuItem
            // 
            this.cleanALLCONFIGSToolStripMenuItem.Name = "cleanALLCONFIGSToolStripMenuItem";
            this.cleanALLCONFIGSToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.cleanALLCONFIGSToolStripMenuItem.Text = "Clean ALL CONFIGS";
            this.cleanALLCONFIGSToolStripMenuItem.Click += new System.EventHandler(this.cleanALLCONFIGSToolStripMenuItem_Click);
            // 
            // allWindowsToolStripMenuItem
            // 
            this.allWindowsToolStripMenuItem.Name = "allWindowsToolStripMenuItem";
            this.allWindowsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.allWindowsToolStripMenuItem.Text = "All Windows";
            this.allWindowsToolStripMenuItem.Click += new System.EventHandler(this.allWindowsToolStripMenuItem_Click);
            // 
            // hideMeetingsRoomsToolStripMenuItem
            // 
            this.hideMeetingsRoomsToolStripMenuItem.Name = "hideMeetingsRoomsToolStripMenuItem";
            this.hideMeetingsRoomsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.hideMeetingsRoomsToolStripMenuItem.Text = "Hide Meetings Rooms";
            this.hideMeetingsRoomsToolStripMenuItem.Click += new System.EventHandler(this.hideMeetingsRoomsToolStripMenuItem_Click);
            // 
            // statusUploadingToolStripMenuItem
            // 
            this.statusUploadingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableToolStripMenuItem,
            this.disableToolStripMenuItem});
            this.statusUploadingToolStripMenuItem.Enabled = false;
            this.statusUploadingToolStripMenuItem.Name = "statusUploadingToolStripMenuItem";
            this.statusUploadingToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.statusUploadingToolStripMenuItem.Text = "Status Uploading";
            this.statusUploadingToolStripMenuItem.Visible = false;
            // 
            // enableToolStripMenuItem
            // 
            this.enableToolStripMenuItem.Name = "enableToolStripMenuItem";
            this.enableToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.enableToolStripMenuItem.Text = "Enable";
            // 
            // disableToolStripMenuItem
            // 
            this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
            this.disableToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.disableToolStripMenuItem.Text = "Disable";
            // 
            // frmTestToolStripMenuItem
            // 
            this.frmTestToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadContactsDemoToolStripMenuItem});
            this.frmTestToolStripMenuItem.Name = "frmTestToolStripMenuItem";
            this.frmTestToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.frmTestToolStripMenuItem.Text = "FrmTest";
            // 
            // loadContactsDemoToolStripMenuItem
            // 
            this.loadContactsDemoToolStripMenuItem.Name = "loadContactsDemoToolStripMenuItem";
            this.loadContactsDemoToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.loadContactsDemoToolStripMenuItem.Text = "LoadContactsDemo";
            this.loadContactsDemoToolStripMenuItem.Click += new System.EventHandler(this.loadContactsDemoToolStripMenuItem_Click);
            // 
            // driverToolStripMenuItem
            // 
            this.driverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reinstallDriverToolStripMenuItem,
            this.allowAllNetworkToolStripMenuItem,
            this.blockAllNetworkToolStripMenuItem,
            this.restoreToHeartbeatToolStripMenuItem});
            this.driverToolStripMenuItem.Name = "driverToolStripMenuItem";
            this.driverToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.driverToolStripMenuItem.Text = "Driver";
            // 
            // reinstallDriverToolStripMenuItem
            // 
            this.reinstallDriverToolStripMenuItem.Name = "reinstallDriverToolStripMenuItem";
            this.reinstallDriverToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.reinstallDriverToolStripMenuItem.Text = "Reinstall Driver";
            this.reinstallDriverToolStripMenuItem.Click += new System.EventHandler(this.reinstallDriverToolStripMenuItem_Click);
            // 
            // allowAllNetworkToolStripMenuItem
            // 
            this.allowAllNetworkToolStripMenuItem.Name = "allowAllNetworkToolStripMenuItem";
            this.allowAllNetworkToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.allowAllNetworkToolStripMenuItem.Text = "Allow All Network";
            this.allowAllNetworkToolStripMenuItem.Click += new System.EventHandler(this.allowAllNetworkToolStripMenuItem_Click);
            // 
            // blockAllNetworkToolStripMenuItem
            // 
            this.blockAllNetworkToolStripMenuItem.Name = "blockAllNetworkToolStripMenuItem";
            this.blockAllNetworkToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.blockAllNetworkToolStripMenuItem.Text = "Block All Network";
            this.blockAllNetworkToolStripMenuItem.Click += new System.EventHandler(this.blockAllNetworkToolStripMenuItem_Click);
            // 
            // restoreToHeartbeatToolStripMenuItem
            // 
            this.restoreToHeartbeatToolStripMenuItem.Name = "restoreToHeartbeatToolStripMenuItem";
            this.restoreToHeartbeatToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.restoreToHeartbeatToolStripMenuItem.Text = "Restore to Heartbeat";
            this.restoreToHeartbeatToolStripMenuItem.Click += new System.EventHandler(this.restoreToHeartbeatToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 292);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(603, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lab_cardtype);
            this.groupBox3.Controls.Add(this.lab_areacode);
            this.groupBox3.Controls.Add(this.lab_cityname);
            this.groupBox3.Controls.Add(this.lab_number);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(220, 34);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(148, 182);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Number Information";
            // 
            // lab_cardtype
            // 
            this.lab_cardtype.AutoSize = true;
            this.lab_cardtype.Location = new System.Drawing.Point(9, 155);
            this.lab_cardtype.Name = "lab_cardtype";
            this.lab_cardtype.Size = new System.Drawing.Size(13, 13);
            this.lab_cardtype.TabIndex = 16;
            this.lab_cardtype.Text = "0";
            // 
            // lab_areacode
            // 
            this.lab_areacode.AutoSize = true;
            this.lab_areacode.Location = new System.Drawing.Point(9, 112);
            this.lab_areacode.Name = "lab_areacode";
            this.lab_areacode.Size = new System.Drawing.Size(52, 13);
            this.lab_areacode.TabIndex = 15;
            this.lab_areacode.Text = "areacode";
            // 
            // lab_cityname
            // 
            this.lab_cityname.AutoSize = true;
            this.lab_cityname.Location = new System.Drawing.Point(9, 74);
            this.lab_cityname.Name = "lab_cityname";
            this.lab_cityname.Size = new System.Drawing.Size(13, 13);
            this.lab_cityname.TabIndex = 14;
            this.lab_cityname.Text = "0";
            // 
            // lab_number
            // 
            this.lab_number.AutoSize = true;
            this.lab_number.Location = new System.Drawing.Point(9, 39);
            this.lab_number.Name = "lab_number";
            this.lab_number.Size = new System.Drawing.Size(13, 13);
            this.lab_number.TabIndex = 13;
            this.lab_number.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Number Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Area Code:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Area Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Number: ";
            // 
            // timer1
            // 
            this.timer1.Interval = 600000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtInformation);
            this.groupBox4.Location = new System.Drawing.Point(390, 34);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 248);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Information";
            // 
            // txtInformation
            // 
            this.txtInformation.Location = new System.Drawing.Point(6, 17);
            this.txtInformation.Multiline = true;
            this.txtInformation.Name = "txtInformation";
            this.txtInformation.Size = new System.Drawing.Size(188, 216);
            this.txtInformation.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 314);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Direct HR Soft Phone Agent";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox _lstTeleNumbers;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox _txtSelectedNumber;
        private System.Windows.Forms.Button _btnCallNumber;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lab_cardtype;
        private System.Windows.Forms.Label lab_areacode;
        private System.Windows.Forms.Label lab_cityname;
        private System.Windows.Forms.Label lab_number;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openErrorsLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem getChromePluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extensionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem yesYesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neverUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorLogToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem detailsLogToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testUnhandleExceptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCONFIGFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem killMyselfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusUploadingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtInformation;
        private System.Windows.Forms.ToolStripMenuItem cleanALLCONFIGSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allWindowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem softphoneClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frmTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadContactsDemoToolStripMenuItem;
        private System.Windows.Forms.Button btn_SmartCall;
        private System.Windows.Forms.ToolStripMenuItem dHRContactsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showContactsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportContactsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideMeetingsRoomsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem driverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reinstallDriverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allowAllNetworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blockAllNetworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToHeartbeatToolStripMenuItem;
    }
}


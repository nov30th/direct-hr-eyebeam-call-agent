namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    partial class FrmContacts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmContacts));
            this.tabContacts = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabContacts
            // 
            this.tabContacts.Location = new System.Drawing.Point(12, 12);
            this.tabContacts.Name = "tabContacts";
            this.tabContacts.SelectedIndex = 0;
            this.tabContacts.Size = new System.Drawing.Size(42, 29);
            this.tabContacts.TabIndex = 0;
            this.tabContacts.SelectedIndexChanged += new System.EventHandler(this.tabContacts_SelectedIndexChanged);
            // 
            // FrmContacts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(63, 51);
            this.Controls.Add(this.tabContacts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmContacts";
            this.Text = "Direct HR Contacts";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmContacts_FormClosing);
            this.Load += new System.EventHandler(this.FrmContacts_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmContacts_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabContacts;
    }
}
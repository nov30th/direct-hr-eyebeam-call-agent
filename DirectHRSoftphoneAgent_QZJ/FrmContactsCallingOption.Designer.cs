namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    partial class FrmContactsCallingOption
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
            this.lab_name = new System.Windows.Forms.Label();
            this.pic_Email = new System.Windows.Forms.PictureBox();
            this.pic_Cellphone = new System.Windows.Forms.PictureBox();
            this.pic_Tel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Email)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Cellphone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Tel)).BeginInit();
            this.SuspendLayout();
            // 
            // lab_name
            // 
            this.lab_name.AutoSize = true;
            this.lab_name.Location = new System.Drawing.Point(95, 9);
            this.lab_name.Name = "lab_name";
            this.lab_name.Size = new System.Drawing.Size(35, 13);
            this.lab_name.TabIndex = 3;
            this.lab_name.Text = "label1";
            // 
            // pic_Email
            // 
            this.pic_Email.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_Email.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_Email.Image = global::DHRSoftphone.SoftphoneAgent_QZJ.Properties.Resources.icon_mail;
            this.pic_Email.InitialImage = null;
            this.pic_Email.Location = new System.Drawing.Point(185, 33);
            this.pic_Email.Name = "pic_Email";
            this.pic_Email.Size = new System.Drawing.Size(54, 54);
            this.pic_Email.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Email.TabIndex = 2;
            this.pic_Email.TabStop = false;
            this.pic_Email.Click += new System.EventHandler(this.pic_Email_Click);
            this.pic_Email.MouseLeave += new System.EventHandler(this.pic_Email_MouseLeave);
            this.pic_Email.MouseHover += new System.EventHandler(this.pic_Email_MouseHover);
            // 
            // pic_Cellphone
            // 
            this.pic_Cellphone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_Cellphone.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_Cellphone.Image = global::DHRSoftphone.SoftphoneAgent_QZJ.Properties.Resources.icon_mobile;
            this.pic_Cellphone.InitialImage = null;
            this.pic_Cellphone.Location = new System.Drawing.Point(98, 33);
            this.pic_Cellphone.Name = "pic_Cellphone";
            this.pic_Cellphone.Size = new System.Drawing.Size(54, 54);
            this.pic_Cellphone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Cellphone.TabIndex = 1;
            this.pic_Cellphone.TabStop = false;
            this.pic_Cellphone.Click += new System.EventHandler(this.pic_Cellphone_Click);
            this.pic_Cellphone.MouseLeave += new System.EventHandler(this.pic_Cellphone_MouseLeave);
            this.pic_Cellphone.MouseHover += new System.EventHandler(this.pic_Cellphone_MouseHover);
            // 
            // pic_Tel
            // 
            this.pic_Tel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_Tel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_Tel.Image = global::DHRSoftphone.SoftphoneAgent_QZJ.Properties.Resources.icon_phone;
            this.pic_Tel.InitialImage = null;
            this.pic_Tel.Location = new System.Drawing.Point(13, 33);
            this.pic_Tel.Name = "pic_Tel";
            this.pic_Tel.Size = new System.Drawing.Size(54, 54);
            this.pic_Tel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Tel.TabIndex = 0;
            this.pic_Tel.TabStop = false;
            this.pic_Tel.Click += new System.EventHandler(this.pic_Tel_Click);
            this.pic_Tel.MouseLeave += new System.EventHandler(this.pic_Tel_MouseLeave);
            this.pic_Tel.MouseHover += new System.EventHandler(this.pic_Tel_MouseHover);
            // 
            // FrmContactsCallingOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(251, 100);
            this.Controls.Add(this.lab_name);
            this.Controls.Add(this.pic_Email);
            this.Controls.Add(this.pic_Cellphone);
            this.Controls.Add(this.pic_Tel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmContactsCallingOption";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Call via";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.FrmContactsCallingOption_Deactivate);
            this.Leave += new System.EventHandler(this.FrmContactsCallingOption_Leave);
            this.MouseLeave += new System.EventHandler(this.FrmContactsCallingOption_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmContactsCallingOption_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Email)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Cellphone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Tel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_Tel;
        private System.Windows.Forms.PictureBox pic_Cellphone;
        private System.Windows.Forms.PictureBox pic_Email;
        private System.Windows.Forms.Label lab_name;
    }
}
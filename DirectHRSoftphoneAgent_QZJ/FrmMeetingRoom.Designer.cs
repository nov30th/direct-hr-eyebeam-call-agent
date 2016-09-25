namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    partial class FrmMeetingRoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMeetingRoom));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.timer_loadxml = new System.Windows.Forms.Timer(this.components);
            this.pic_tel = new System.Windows.Forms.PictureBox();
            this.picCamera = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_tel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 56);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(28, 219);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // timer_loadxml
            // 
            this.timer_loadxml.Enabled = true;
            this.timer_loadxml.Interval = 2000;
            this.timer_loadxml.Tick += new System.EventHandler(this.timer_loadxml_Tick);
            // 
            // pic_tel
            // 
            this.pic_tel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_tel.Image = global::DHRSoftphone.SoftphoneAgent_QZJ.Properties.Resources.icon_phone;
            this.pic_tel.Location = new System.Drawing.Point(2, 28);
            this.pic_tel.Name = "pic_tel";
            this.pic_tel.Size = new System.Drawing.Size(22, 21);
            this.pic_tel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_tel.TabIndex = 2;
            this.pic_tel.TabStop = false;
            this.pic_tel.Click += new System.EventHandler(this.pic_tel_Click);
            // 
            // picCamera
            // 
            this.picCamera.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.picCamera.Image = global::DHRSoftphone.SoftphoneAgent_QZJ.Properties.Resources.icon_webcam;
            this.picCamera.Location = new System.Drawing.Point(6, 6);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new System.Drawing.Size(19, 20);
            this.picCamera.TabIndex = 0;
            this.picCamera.TabStop = false;
            this.picCamera.Click += new System.EventHandler(this.picCamera_Click);
            this.picCamera.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCamera_MouseDown);
            this.picCamera.MouseLeave += new System.EventHandler(this.picCamera_MouseLeave);
            this.picCamera.MouseHover += new System.EventHandler(this.picCamera_MouseHover);
            // 
            // FrmMeetingRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(25, 283);
            this.Controls.Add(this.pic_tel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.picCamera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMeetingRoom";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Meeting Room";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmMeetingRoom_Load);
            this.Move += new System.EventHandler(this.FrmMeetingRoom_Move);
            ((System.ComponentModel.ISupportInitialize)(this.pic_tel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCamera;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Timer timer_loadxml;
        private System.Windows.Forms.PictureBox pic_tel;
    }
}
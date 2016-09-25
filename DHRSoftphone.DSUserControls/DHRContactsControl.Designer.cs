namespace DHRSoftphone.DSUserControls
{
    partial class DHRContactsControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._userPhoto = new System.Windows.Forms.PictureBox();
            this._username = new System.Windows.Forms.Label();
            this._extensionNumber = new System.Windows.Forms.Label();
            this._cellphoneNumber = new System.Windows.Forms.Label();
            this._userEmail = new System.Windows.Forms.Label();
            this._masterEmail = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._userPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // _userPhoto
            // 
            this._userPhoto.Location = new System.Drawing.Point(4, 4);
            this._userPhoto.Name = "_userPhoto";
            this._userPhoto.Size = new System.Drawing.Size(56, 56);
            this._userPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._userPhoto.TabIndex = 0;
            this._userPhoto.TabStop = false;
            this._userPhoto.Click += new System.EventHandler(this._userPhoto_Click);
            // 
            // _username
            // 
            this._username.AutoSize = true;
            this._username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._username.Location = new System.Drawing.Point(66, 5);
            this._username.Name = "_username";
            this._username.Size = new System.Drawing.Size(37, 15);
            this._username.TabIndex = 1;
            this._username.Text = "label1";
            this._username.Click += new System.EventHandler(this._username_Click);
            // 
            // _extensionNumber
            // 
            this._extensionNumber.AutoSize = true;
            this._extensionNumber.Location = new System.Drawing.Point(66, 25);
            this._extensionNumber.Name = "_extensionNumber";
            this._extensionNumber.Size = new System.Drawing.Size(35, 13);
            this._extensionNumber.TabIndex = 2;
            this._extensionNumber.Text = "label1";
            this._extensionNumber.Click += new System.EventHandler(this._extensionNumber_Click);
            // 
            // _cellphoneNumber
            // 
            this._cellphoneNumber.AutoSize = true;
            this._cellphoneNumber.Location = new System.Drawing.Point(66, 43);
            this._cellphoneNumber.Name = "_cellphoneNumber";
            this._cellphoneNumber.Size = new System.Drawing.Size(35, 13);
            this._cellphoneNumber.TabIndex = 3;
            this._cellphoneNumber.Text = "label2";
            this._cellphoneNumber.Click += new System.EventHandler(this._cellphoneNumber_Click);
            // 
            // _userEmail
            // 
            this._userEmail.AutoSize = true;
            this._userEmail.Location = new System.Drawing.Point(4, 62);
            this._userEmail.Name = "_userEmail";
            this._userEmail.Size = new System.Drawing.Size(35, 13);
            this._userEmail.TabIndex = 4;
            this._userEmail.Text = "label1";
            this._userEmail.Click += new System.EventHandler(this._userEmail_Click);
            // 
            // _masterEmail
            // 
            this._masterEmail.AutoSize = true;
            this._masterEmail.Location = new System.Drawing.Point(4, 77);
            this._masterEmail.Name = "_masterEmail";
            this._masterEmail.Size = new System.Drawing.Size(35, 13);
            this._masterEmail.TabIndex = 5;
            this._masterEmail.Text = "label1";
            // 
            // DHRContactsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._masterEmail);
            this.Controls.Add(this._userEmail);
            this.Controls.Add(this._cellphoneNumber);
            this.Controls.Add(this._extensionNumber);
            this.Controls.Add(this._username);
            this.Controls.Add(this._userPhoto);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "DHRContactsControl";
            this.Size = new System.Drawing.Size(188, 95);
            this.Load += new System.EventHandler(this.DHRContactsControl_Load);
            this.MouseEnter += new System.EventHandler(this.DHRContactsControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.DHRContactsControl_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this._userPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _userPhoto;
        private System.Windows.Forms.Label _username;
        private System.Windows.Forms.Label _extensionNumber;
        private System.Windows.Forms.Label _cellphoneNumber;
        private System.Windows.Forms.Label _userEmail;
        private System.Windows.Forms.Label _masterEmail;
    }
}

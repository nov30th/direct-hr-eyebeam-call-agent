namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    partial class FrmLoading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLoading));
            this.pic_loading = new System.Windows.Forms.PictureBox();
            this.lab_status = new System.Windows.Forms.Label();
            this.lab_loginuserstatic = new System.Windows.Forms.Label();
            this.lab_loginuser = new System.Windows.Forms.Label();
            this.btn_changeuser = new System.Windows.Forms.Button();
            this.lab_version = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic_loading)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_loading
            // 
            this.pic_loading.Image = global::DHRSoftphone.SoftphoneAgent_QZJ.Properties.Resources.loading__1_;
            this.pic_loading.Location = new System.Drawing.Point(15, 12);
            this.pic_loading.Name = "pic_loading";
            this.pic_loading.Size = new System.Drawing.Size(538, 290);
            this.pic_loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_loading.TabIndex = 0;
            this.pic_loading.TabStop = false;
            // 
            // lab_status
            // 
            this.lab_status.AutoSize = true;
            this.lab_status.Location = new System.Drawing.Point(12, 355);
            this.lab_status.Name = "lab_status";
            this.lab_status.Size = new System.Drawing.Size(30, 13);
            this.lab_status.TabIndex = 1;
            this.lab_status.Text = "Init...";
            // 
            // lab_loginuserstatic
            // 
            this.lab_loginuserstatic.AutoSize = true;
            this.lab_loginuserstatic.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_loginuserstatic.Location = new System.Drawing.Point(16, 311);
            this.lab_loginuserstatic.Name = "lab_loginuserstatic";
            this.lab_loginuserstatic.Size = new System.Drawing.Size(95, 25);
            this.lab_loginuserstatic.TabIndex = 2;
            this.lab_loginuserstatic.Text = "You are";
            // 
            // lab_loginuser
            // 
            this.lab_loginuser.Font = new System.Drawing.Font("Mistral", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_loginuser.ForeColor = System.Drawing.Color.Black;
            this.lab_loginuser.Location = new System.Drawing.Point(108, 309);
            this.lab_loginuser.Name = "lab_loginuser";
            this.lab_loginuser.Size = new System.Drawing.Size(211, 37);
            this.lab_loginuser.TabIndex = 3;
            this.lab_loginuser.Text = "Vincent Qiu";
            // 
            // btn_changeuser
            // 
            this.btn_changeuser.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_changeuser.Location = new System.Drawing.Point(334, 308);
            this.btn_changeuser.Name = "btn_changeuser";
            this.btn_changeuser.Size = new System.Drawing.Size(219, 32);
            this.btn_changeuser.TabIndex = 4;
            this.btn_changeuser.Text = "Not You? Click Here.";
            this.btn_changeuser.UseVisualStyleBackColor = true;
            this.btn_changeuser.Click += new System.EventHandler(this.btn_changeuser_Click);
            // 
            // lab_version
            // 
            this.lab_version.AutoSize = true;
            this.lab_version.Location = new System.Drawing.Point(477, 355);
            this.lab_version.Name = "lab_version";
            this.lab_version.Size = new System.Drawing.Size(41, 13);
            this.lab_version.TabIndex = 5;
            this.lab_version.Text = "version";
            // 
            // FrmLoading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 309);
            this.ControlBox = false;
            this.Controls.Add(this.lab_version);
            this.Controls.Add(this.pic_loading);
            this.Controls.Add(this.btn_changeuser);
            this.Controls.Add(this.lab_loginuser);
            this.Controls.Add(this.lab_loginuserstatic);
            this.Controls.Add(this.lab_status);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLoading";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Starting Direct HR Softphone Agent";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmLoading_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_loading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_loading;
        private System.Windows.Forms.Label lab_status;
        private System.Windows.Forms.Label lab_loginuserstatic;
        private System.Windows.Forms.Label lab_loginuser;
        private System.Windows.Forms.Button btn_changeuser;
        private System.Windows.Forms.Label lab_version;
    }
}
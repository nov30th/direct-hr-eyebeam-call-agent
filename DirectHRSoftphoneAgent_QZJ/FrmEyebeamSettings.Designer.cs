namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    partial class FrmEyebeamSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEyebeamSettings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._btnAdvencedSet = new System.Windows.Forms.Button();
            this._btnOneKeySet = new System.Windows.Forms.Button();
            this._labInformation = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._btnAdvencedSet);
            this.groupBox1.Controls.Add(this._btnOneKeySet);
            this.groupBox1.Location = new System.Drawing.Point(12, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Direct HR Softphone Extension Settings";
            // 
            // _btnAdvencedSet
            // 
            this._btnAdvencedSet.Enabled = false;
            this._btnAdvencedSet.Location = new System.Drawing.Point(227, 22);
            this._btnAdvencedSet.Name = "_btnAdvencedSet";
            this._btnAdvencedSet.Size = new System.Drawing.Size(115, 51);
            this._btnAdvencedSet.TabIndex = 1;
            this._btnAdvencedSet.Text = "Professional (Advenced)";
            this._btnAdvencedSet.UseVisualStyleBackColor = true;
            // 
            // _btnOneKeySet
            // 
            this._btnOneKeySet.Location = new System.Drawing.Point(15, 22);
            this._btnOneKeySet.Name = "_btnOneKeySet";
            this._btnOneKeySet.Size = new System.Drawing.Size(206, 52);
            this._btnOneKeySet.TabIndex = 0;
            this._btnOneKeySet.Text = "Smart Click (Simple)";
            this._btnOneKeySet.UseVisualStyleBackColor = true;
            this._btnOneKeySet.Click += new System.EventHandler(this._btnOneKeySet_Click);
            // 
            // _labInformation
            // 
            this._labInformation.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labInformation.Location = new System.Drawing.Point(12, 22);
            this._labInformation.Name = "_labInformation";
            this._labInformation.Size = new System.Drawing.Size(360, 111);
            this._labInformation.TabIndex = 1;
            this._labInformation.Text = "Your configuration in soft phone client seems not correct, would you like this to" +
    "ol to help you to fix this problem?\r\nIf you are on a business trip, just skip an" +
    "d close this window.";
            // 
            // FrmEyebeamSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 236);
            this.Controls.Add(this._labInformation);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEyebeamSettings";
            this.Text = "Softphone Client Smart Tool";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _btnOneKeySet;
        private System.Windows.Forms.Button _btnAdvencedSet;
        private System.Windows.Forms.Label _labInformation;
    }
}
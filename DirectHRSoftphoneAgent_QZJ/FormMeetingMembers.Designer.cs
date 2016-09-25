namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    partial class FormMeetingMembers
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
            this.label1 = new System.Windows.Forms.Label();
            this.lab_members = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Members:";
            // 
            // lab_members
            // 
            this.lab_members.AutoSize = true;
            this.lab_members.Font = new System.Drawing.Font("Calibri", 10F);
            this.lab_members.Location = new System.Drawing.Point(9, 27);
            this.lab_members.Name = "lab_members";
            this.lab_members.Size = new System.Drawing.Size(42, 17);
            this.lab_members.TabIndex = 1;
            this.lab_members.Text = "label2";
            this.lab_members.TextChanged += new System.EventHandler(this.lab_members_TextChanged);
            // 
            // FormMeetingMembers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(113, 323);
            this.Controls.Add(this.lab_members);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMeetingMembers";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FormMeetingMembers";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_members;
    }
}
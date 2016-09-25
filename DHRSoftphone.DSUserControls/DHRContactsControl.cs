using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DHRSoftphone.DSUserControls
{



    public partial class DHRContactsControl : UserControl
    {
        public delegate void UserMouseClickEventHandler(object serder, UserContactInfo e);
        public event UserMouseClickEventHandler OnUserMouseClicked;



        public DHRContactsControl()
        {
            InitializeComponent();
        }

        public string UserEmail
        {
            get { return _userEmail.Text.Replace("E: ",string.Empty); }
            set { _userEmail.Text = "E: " + value; }
        }

        public string MasterEmail
        {
            get { return _masterEmail.Text.Replace("ME: ", string.Empty); }
            set { _masterEmail.Text = "ME: " + value; }
        }


        public string UserName
        {
            get { return _username.Text; }
            set { _username.Text = value; }
        }

        public string Cellphone
        {
            get { return _cellphoneNumber.Text.Replace("T: ", string.Empty); }
            set { _cellphoneNumber.Text = "T: " + value; }
        }

        public string ExtensionNumber
        {
            get { return _extensionNumber.Text.Replace("Ext: ", string.Empty); }
            set { _extensionNumber.Text = "Ext: " + value; }
        }

        public Image UserPhoto
        {
            get { return _userPhoto.Image; }
            set { _userPhoto.Image = UserPhoto; }
        }

        public string UserPhotoUrl
        {
            set
            {
                try
                {
                    _userPhoto.Load(value);
                }
                catch (Exception ex)
                {
                    //do nothing
                }
            }
        }

        public DHRContactsControl(string name, string extension, string cellphone, string email, string masterEmail, Image image)
        {
            _username.Text = name;
            _extensionNumber.Text = extension;
            _userPhoto.Image = image;
            _cellphoneNumber.Text = cellphone;
            _userEmail.Text = email;
            _masterEmail.Text = masterEmail;
        }

        private void DHRContactsControl_Load(object sender, EventArgs e)
        {

        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            UserMouseClick(this);
        }

        public void UserMouseClick(object sender)
        {
            if (OnUserMouseClicked != null)
                OnUserMouseClicked(sender, new UserContactInfo()
                {
                    Cellphone = this.Cellphone,
                    ExtensionNumber = this.ExtensionNumber,
                    Username = this.UserName,
                    UserEmail = this.UserEmail
                });
        }

        private void _userPhoto_Click(object sender, EventArgs e)
        {
            UserMouseClick(sender);
        }

        private void _username_Click(object sender, EventArgs e)
        {
            UserMouseClick(sender);
        }

        private void _extensionNumber_Click(object sender, EventArgs e)
        {
            UserMouseClick(sender);
        }

        private void _cellphoneNumber_Click(object sender, EventArgs e)
        {
            UserMouseClick(sender);
        }

        private void _userEmail_Click(object sender, EventArgs e)
        {
            UserMouseClick(sender);
        }

        private void DHRContactsControl_MouseEnter(object sender, EventArgs e)
        {
            //this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        }

        private void DHRContactsControl_MouseLeave(object sender, EventArgs e)
        {
            //this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }



    }
}

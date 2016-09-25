using System;
using System.Linq;
using System.Windows.Forms;
using DHRSoftphone.AgentExceptions;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    /// <summary>
    /// Copyright (C) 1985-2012 Vincent Qiu, Direct HR
    /// Http://hoho.im 版权所有。
    /// NameSpace: DirectHRSoftphoneAgent_QZJ
    /// FullName: DirectHRSoftphoneAgent_QZJ.FrmSettings
    /// Class Created: 2011/11/25 17:14
    /// On Computer: NOV30TH-LAPTOP - Administrator
    ///
    /// </summary>
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
            if (AgentDatabaseProvider.CityItems.Count <= 0)
                throw new AgentException("Sorry! Can not load the city list!");
            foreach (AgentDatabaseProvider.AgentCityItem item in AgentDatabaseProvider.CityItems)
            {
                //if (!dropdown_city.Items.Contains(item.City))
                    dropdown_city.Items.Add(item.City);

                //dropdown_city.SelectedIndex = 0;
                //dropdown_city.SelectedText = item.City;
                //dropdown_city.SelectedValue = item.AreaCode;
            }
            AgentSettings settings = new AgentSettings();
            if (settings.GetCurrentCityInConf() != null)
                SetCurrentArea(settings.GetCurrentCityInConf().AreaCode);
            txt_fullname.Text = settings.GetFullname();
            txt_phoneExt.Text = settings.GetCurrentExtension();
        }

        private void txt_areacode_TextChanged(object sender, EventArgs e)
        {
            SetCurrentArea(txt_areacode.Text);
        }

        private void dropdown_city_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_areacode.Text =
                AgentDatabaseProvider.CityItems.Single(c => c.City == dropdown_city.Items[dropdown_city.SelectedIndex].ToString()).AreaCode;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (!CheckRequiredInformation())
            {
                MessageBox.Show("Please fill all the information this program needed!");
                return;
            }
            this.Close();
        }

        public void SetCurrentArea(string areaCode)
        {
            if (AgentDatabaseProvider.CityItems.Any(c => c.AreaCode == areaCode))
            {
                string cityname = AgentDatabaseProvider.CityItems.First(c => c.AreaCode == areaCode).City;
                foreach (object item in dropdown_city.Items)
                {
                    if (item.ToString() == cityname)
                    {
                        dropdown_city.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        public bool CheckRequiredInformation()
        {
            if (string.IsNullOrEmpty(txt_fullname.Text))
                return false;
            if (string.IsNullOrEmpty(txt_phoneExt.Text) || txt_phoneExt.Text == "000")
                return false;
            if (String.IsNullOrEmpty(txt_areacode.Text))
                return false;
            return true;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (!CheckRequiredInformation())
            {
                MessageBox.Show("Please fill all the information");
                return;
            }
            var city = AgentDatabaseProvider.GetCityByAreaCode(txt_areacode.Text);
            if (city == null)
            {
                MessageBox.Show("Please check your telephone area code! Is it USA area code?");
                return;
            }
            AgentSettings settings = new AgentSettings();
            settings.SetCurrentAreaCode(txt_areacode.Text);
            settings.SetCurrentExtension(txt_phoneExt.Text);
            settings.SetFullname(txt_fullname.Text);
            settings.ReleaseSettings();
            this.Close();
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
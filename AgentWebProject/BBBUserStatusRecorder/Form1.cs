using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DhrAgentDatabaseUtils;

namespace BBBUserStatusRecorder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RecordMeetings target = new RecordMeetings();
           

            File.AppendAllText("debug.log",DateTime.Now + ": " +  target.RecordMeetingUserStatus().ToString(CultureInfo.InvariantCulture));
           
            File.AppendAllText("debug.log", DateTime.Now + ": " +  target.FlagExpiredUserStatus().ToString(CultureInfo.InvariantCulture));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}

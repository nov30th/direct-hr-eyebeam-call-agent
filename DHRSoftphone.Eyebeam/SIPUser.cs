using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHRSoftphone.Eyebeam
{
    public class SIPUser
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Extension { get; set; }
        public string Domain { get; set; }
        public bool Enabled { get; set; }
    }
}

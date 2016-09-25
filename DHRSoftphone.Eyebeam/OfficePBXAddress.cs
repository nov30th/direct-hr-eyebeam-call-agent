using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHRSoftphone.Eyebeam
{
    public static class OfficePBXAddress
    {
        private static Dictionary<string, string> _pbxAddress = new Dictionary<string, string>();

        public static Dictionary<string, string> PbxAddress
        {
            get { return _pbxAddress; }
            set { _pbxAddress = value; }
        }
    }
}

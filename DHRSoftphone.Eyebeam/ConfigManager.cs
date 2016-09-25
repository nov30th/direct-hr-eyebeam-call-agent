using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace DHRSoftphone.Eyebeam
{
    public class ConfigManager
    {
        protected readonly string CONFIGFILEPATH;

        protected readonly string CONFIGFILENAME;

        protected readonly string CONFIGFILE;

        private bool _isExistFile = false;

        private List<SIPUser> _sipUsers; 

        public ConfigManager()
        {
            CONFIGFILEPATH = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"CounterPath\RegNow Enhanced\default_user");
            CONFIGFILENAME = "settings.cps";
            CONFIGFILE = Path.Combine(CONFIGFILEPATH, CONFIGFILENAME);
            _isExistFile = LoadConfigurations();
        }

        public bool IsExistFile
        {
            get { return _isExistFile; }
            set { _isExistFile = value; }
        }

        public List<SIPUser> SipUsers
        {
            get { return _sipUsers; }
            set { _sipUsers = value; }
        }


        public bool CheckConfigurationByExtension(string extension)
        {
            if (!_isExistFile)
                return false;
            if (_sipUsers.Any(s => s.Extension == extension))
                return true;
            return false;
        }

        public bool LoadConfigurations()
        {
            if (!File.Exists(CONFIGFILE))
                return false;
            _sipUsers = ReadConfigFile();
            return true;
        }

        public bool RewriteConfigurationWithSimplyConfiguration(SIPUser sipuser, bool forceCloseProgram)
        {
            if (forceCloseProgram)
                KillEyebeamProcess();
            var proxy = SimplyUserConfiguration.Proxy.Replace("{username}", sipuser.Username)
                .Replace("{displayname}", sipuser.DisplayName)
                .Replace("{extension}", sipuser.Extension)
                .Replace("{domain}", sipuser.Domain);
            Directory.CreateDirectory(CONFIGFILEPATH);
            File.WriteAllText(CONFIGFILE, proxy);
            return true;
        }

        public List<SIPUser> ReadConfigFile()
        {
            TextReader txt = new StreamReader(CONFIGFILE);
            var fileContent = txt.ReadToEnd();
            txt.Close();
            var proxyManager = new ProxyManager();
            return proxyManager.LoadUsersFromXml(fileContent);
        }

        public int KillEyebeamProcess()
        {
            var retval = 0;
            var process = Process.GetProcessesByName("eyebeam");
            if (process.Any())
            {
                foreach (var process1 in process)
                {
                    process1.Kill();
                    retval++;
                }
            }
            return retval;
        }

        public string GetIPPBXIPAddress(string extension)
        {
            string domain;
            switch (extension.Substring(0, 1))
            {
                case "1":
                    domain = "172.31.1.2";
                    break;
                case "2":
                    domain = "192.168.1.133";
                    break;
                case "3":
                    domain = "172.27.1.2";
                    break;
                case "5":
                    domain = "172.26.1.2";
                    break;
                case "6":
                    domain = "172.28.1.2";
                    break;
                case "7":
                    domain = "172.30.1.3";
                    break;
                default:
                    domain = string.Empty;
                    break;
            }
            return domain;
        }
    }
}

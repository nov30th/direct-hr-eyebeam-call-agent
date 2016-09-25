using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHRSoftphone.IDHRSPPlugin;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace DHRSoftphone.Plugins.DNSFixer
{
    public class SoftphoneLoader : PluginObject, IDHRSPLoad
    {
        private readonly string updateURI = "http://softphone.directhr.cn/AgentHosts.asmx/Upload";
        protected Dictionary<string, string> settings = new Dictionary<string, string>();
        private readonly Dictionary<string, string> hosts;
        private readonly string _additionSign = @"#DIRECTHR SOFTPHONE AGENT [Version {0}] Modified at {1}";

        private string GetAdditionSign()
        {
            return string.Format(_additionSign, settings["VERSION"], DateTime.Now.ToString());
        }

        public SoftphoneLoader()
        {
            PluginName = "DNSFixer";
        }

        public void InitBeforeEvents()
        {
            OnWriteLogToFile(new AgentPluginLogArgs() { LogLevel = AgentPluginLogEventLevel.Info, Messages = "Inited." });
        }

        public void StartUp()
        {
            string hosts = string.Empty;
            while (true)
            {
                try
                {
                    hosts = base.Upload(settings, updateURI);
                    hosts = Regex.Replace(hosts, @"<[\/\!]*?[^<>]*?>", "");
                    //hosts.Replace(@"<?xml version=""1.0"" encoding=""utf-8""?>", string.Empty)
                    //    .Replace(@"<string xmlns=""http://tempuri.org/"">", string.Empty)
                    //    .Replace(@"</string>", string.Empty);
                    LocalHostsController.ReplaceContext(hosts, GetAdditionSign());
                }
                catch (Exception exception)
                {
                    LocalHostsController.SetRights(LocalHostsController.HostPath);
                    OnWriteLogToFile(new AgentPluginLogArgs() { LogLevel = AgentPluginLogEventLevel.Errors, Messages = exception.Message });
                }
                Thread.Sleep(120000);
            }
        }

        public void InitConfiguration(Dictionary<string, string> items)
        {
            this.settings = items;
            OnWriteLogToFile(new AgentPluginLogArgs() { LogLevel = AgentPluginLogEventLevel.Info, Messages = "InitConfigurationed." });
        }

        public void End()
        {
            OnWriteLogToFile(new AgentPluginLogArgs() { LogLevel = AgentPluginLogEventLevel.Info, Messages = "Ended." });
        }

        public void OnSettingsChanged(object sender, Dictionary<string, string> settings)
        {
            this.settings = settings;
            OnWriteLogToFile(new AgentPluginLogArgs() { LogLevel = AgentPluginLogEventLevel.Info, Messages = "OnSettingsChanged." });
        }

        public string Test()
        {
            //Test whether the hosts file can be write.
            try
            {
                LocalHostsController.SetRights(LocalHostsController.HostPath);
                //LocalHostsController.ProtectFile(false);
                if (!LocalHostsController.WritableCheck())
                    OnShowMessagesWindow(@"Please notice that the there are some errors with the DNSFixer which you may not using Google Mail or other related service normally!
If you got Anti-Virus or other software on your computer, please allow the changes from Soft Phone Agent Program! If you are not using DHR comptuers, you may need to turn UAC off.");
                //LocalHostsController.ProtectFile(true);
            }
            catch (Exception ex)
            {
                OnWriteLogToFile(new AgentPluginLogArgs()
                {
                    LogLevel = AgentPluginLogEventLevel.Errors,
                    Messages = ex.Message + Environment.NewLine + ex.StackTrace
                });
                OnShowMessagesWindow(@"Please notice that the there are some errors with the DNSFixer, you may need to contact DHR IT group.");
                //return ex.Message + Environment.NewLine + ex.StackTrace;
            }
            return "OK";
        }
    }
}

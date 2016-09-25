using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using DHRSoftphone.IDHRSPPlugin;

namespace DHRSoftphone.Plugins.AgentStatusUploader
{
    public class SoftphoneLoader : PluginObject, IDHRSPLoad
    {
        //private const string updateURI = "http://localhost:3443/UploadStatus.asmx/Upload";
        private const string updateURI = "http://softphone.directhr.cn/UploadStatus.asmx/Upload";
        private const Int32 uploadInv = 5000;
        protected Dictionary<string, string> settings = new Dictionary<string, string>();

        public SoftphoneLoader()
        {
            PluginName = "AgentStatusUploader";
        }

        public void InitBeforeEvents()
        {
            OnWriteLogToFile(new AgentPluginLogArgs() { LogLevel = AgentPluginLogEventLevel.Info, Messages = "Inited." });
        }

        public void StartUp()
        {

            while (true)
            {
                try
                {
                    base.Upload(settings, updateURI);
                }
                catch (Exception exception)
                {
                    OnWriteLogToFile(new AgentPluginLogArgs() { LogLevel = AgentPluginLogEventLevel.Errors, Messages = exception.Message });
                }

                //WebClient wc = new WebClient();
                //wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                //var uri = string.Format("{0}?fullname={1}&extensionnumber={2}&verison={3}&computername={4}&rnd={5}",
                //                        updateURI,
                //                        isFullname ? fullname :s string.Empty,
                //                        isExtensionnumber ? extensionnumber : string.Empty,
                //                        isVersion ? version : string.Empty,
                //                        System.Environment.MachineName,
                //                        (new Random()).NextDouble().ToString(CultureInfo.InvariantCulture)
                //    );
                //try
                //{
                //    var result = wc.UploadString(updateURI, postdata);
                //    //WriteLogToFile(new AgentPluginLogArgs() { LogLevel = AgentPluginLogEventLevel.Details, Messages = result });

                //}
                //catch (Exception e)
                //{
                //    OnWriteLogToFile(new AgentPluginLogArgs() { LogLevel = AgentPluginLogEventLevel.Errors, Messages = string.Format("{0}:{1}", e.Message, e.StackTrace) });
                //}
                //finally
                //{
                //    wc.Dispose();
                //}
                Thread.Sleep(60000);
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
            return "OK";
        }
    }
}
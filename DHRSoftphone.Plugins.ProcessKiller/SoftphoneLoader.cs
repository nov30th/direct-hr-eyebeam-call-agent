using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using DHRSoftphone.IDHRSPPlugin;

namespace DHRSoftphone.Plugins.ProcessKiller
{
    public class SoftphoneLoader : PluginObject, IDHRSPLoad
    {
        //private const string WEBSERVICEURI = "http://127.0.0.1:3443/BlackList.asmx";
        protected ProcessController ProcessController;

        private const string WEBSERVICEURI = "http://softphone.directhr.cn/BlackList.asmx";
        private Dictionary<string, string> _settings = new Dictionary<string, string>();

        private BlackWS.ProgramBlackList[] blackList;

        public SoftphoneLoader()
        {
            PluginName = "ProcessKiller";
            ProcessController = new ProcessController();
        }

        public void End()
        {

        }

        public void InitBeforeEvents()
        {
            ProcessController.ProcessClosed += OnProcessClosed;
        }

        public void InitConfiguration(Dictionary<string, string> items)
        {
            _settings = items;
        }

        public void OnProcessClosed(object sender, ClosedArgs args)
        {
            Binding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            var endpointAddress = new EndpointAddress(new Uri(WEBSERVICEURI));
            var webService = new BlackWS.BlackListSoapClient(binding, endpointAddress);
            try
            {
                webService.UploadProcessLog(
                    System.Environment.MachineName,
                    _settings["FULLNAME"],
                    args.SearchKey,
                    args.WindowName??string.Empty,
                    args.Process.ProcessName,
                    args.Process.MainModule.FileName
                    );
            }
            catch (Exception exception)
            {
                OnWriteLogToFile(new AgentPluginLogArgs()
                                     {
                                         LogLevel = AgentPluginLogEventLevel.Errors,
                                         Messages = exception.Message + Environment.NewLine + exception.StackTrace
                                     });
            }
            OnWriteLogToFile(new AgentPluginLogArgs()
                                 {
                                     LogLevel = AgentPluginLogEventLevel.Info,
                                     Messages = string.Format("Process {0} was closed by search keywords: {1}",
                                                              args.Process.ProcessName,
                                                              args.SearchKey)
                                 });
        }

        public void OnSettingsChanged(object sender, Dictionary<string, string> settings)
        {
            _settings = settings;
        }

        public void StartUp()
        {
            Binding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            var endpointAddress = new EndpointAddress(new Uri(WEBSERVICEURI));
            var webService = new BlackWS.BlackListSoapClient(binding, endpointAddress);
            while (true)
            {
                //For debugging, Vincent Qiu will be not enabled
                if (_settings["FULLNAME"] == "Vincent Qiu")
                    return;

                try
                {
                    blackList = webService.GetBlackList(_settings["FULLNAME"]);
                }
                catch (Exception ex)
                {
                    OnWriteLogToFile(
                        new AgentPluginLogArgs()
                        {
                            Messages = ex.Message + Environment.NewLine + ex.StackTrace,
                            LogLevel = AgentPluginLogEventLevel.Errors
                        });
                }
                if (blackList != null && blackList.Length > 0)
                {
                    foreach (var programBlackList in blackList)
                    {
                        ProcessController.Refresh();
                        if (programBlackList.MatchFileName)
                        {
                            ProcessController.KillProcess(programBlackList.FileName, programBlackList.IsMustBeSame);
                        }
                        if (programBlackList.MatchWindowName)
                        {
                            ProcessController.KillWindow(programBlackList.WindowName, programBlackList.IsMustBeSame);
                        }
                    }
                }
                Thread.Sleep(60000);
            }
        }

        public string Test()
        {
            //OnShowMessagesWindow("test");
            return "OK";
        }
    }
}
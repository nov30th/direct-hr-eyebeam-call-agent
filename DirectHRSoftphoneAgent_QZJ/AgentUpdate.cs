using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Xml;
using DHRSoftphone.AgentExceptions;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public class UpdateEventArgs
    {
        public string filename { get; set; }

        public Version version { get; set; }
    }

    /// <summary>
    /// 程序更新
    /// </summary>
    public class AgentUpdate
    {
        public delegate void HasNewVersion(object sender, UpdateEventArgs e);

        public delegate void Update(object sender, UpdateEventArgs e);

        public event HasNewVersion NewVersionDetected;

        public event Update UpdateDownloaded;

        private bool _hasNewVersion = false;
        private Version _version;
        private string _downloadUri = string.Empty;
        public static string UpdateUrl = string.Empty;
        private string _updateProjectName = @"DirectHR_Softphone_Agent";

        public AgentUpdate()
        {
            UpdateUrl = new AgentSettings().GetUpdateUri();
            _hasNewVersion = false;
        }

        public bool IsAdminRights()
        {
            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
            WindowsPrincipal currentPrincipal = new WindowsPrincipal(currentIdentity);
            return currentPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// 更新完成时触发的事件
        /// </summary>
        protected void OnUpdateDownloaded(object sender, UpdateEventArgs e)
        {
            if (UpdateDownloaded != null)
            {
                UpdateDownloaded(sender, e);
            }
        }

        private void OnNewVersionDetected(object sender, UpdateEventArgs e)
        {
            if (_hasNewVersion == true && NewVersionDetected != null)
                NewVersionDetected(sender, e);
        }

        /// <summary>
        /// 下载更新
        /// </summary>
        public void DownlaodFile()
        {
            if (!_hasNewVersion)
                return;
            WebClient wc = new WebClient();
            string filename = "";
            string exten = _downloadUri.Substring(_downloadUri.LastIndexOf("."));
            filename = "Update_" + Path.GetFileNameWithoutExtension(_downloadUri) + exten;
            try
            {
                AgentLogs.WriteLog("UPDATE: Download new version from " + _downloadUri + " to " + filename, AgentLogEventLevel.Warnings);
                if (File.Exists(filename))
                {
                    try
                    {
                        File.Delete(filename);
                    }
                    catch (Exception)
                    {
                        throw new AgentException("Please try to close the update installation setup before you startup this program!");
                    }
                }
                wc.DownloadFile(_downloadUri, filename);
                AgentLogs.WriteLog("UPDATE: New version has been downloaded.", AgentLogEventLevel.Warnings);
                wc.Dispose();
                UpdateDownloaded(this, new UpdateEventArgs() { filename = filename, version = _version });
            }
            catch (Exception e)
            {
                AgentLogs.WriteLog("UPDATE: Failed to download the update", AgentLogEventLevel.Warnings);
                AgentLogs.WriteLog(e.Message, AgentLogEventLevel.Errors);
                AgentLogs.WriteLog(e.StackTrace.ToString(), AgentLogEventLevel.Errors);
                //throw new AgentException("Failed to download the update", e);
            }
            return;
        }

        /// <summary>
        /// 检查是否需要更新
        /// </summary>
        public void CheckUpdate()
        {
            Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            try
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(string.Format("{0}?t={1}", UpdateUrl, new Random().NextDouble().ToString()));
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);
                XmlNode list = xmlDoc.SelectSingleNode("Update");
                foreach (XmlNode node in list)
                {
                    if (node.Name.ToUpper() == "PROJECT" && node.Attributes["Name"].Value.ToUpper() == _updateProjectName.ToUpper())
                    {
                        foreach (XmlNode xml in node)
                        {
                            switch (xml.Name.ToUpper())
                            {
                                case "VERSION":
                                    _version = new Version(xml.InnerText);
                                    break;

                                case "UPDATEURI":
                                    _downloadUri = xml.InnerText;
                                    break;
                            }
                        }
                    }
                }

                if (_version == null)
                {
                    AgentLogs.WriteLog("UPDATE: can not get the version value from remote server!", AgentLogEventLevel.Warnings);
                    _hasNewVersion = false;
                    return;
                }

                if (currentVersion.CompareTo(_version) < 0)
                    _hasNewVersion = true;
                else
                    _hasNewVersion = false;
                AgentLogs.WriteLog("UPDATE: Current Version: " + currentVersion.ToString() + " Remote Version: " + _version.ToString(), AgentLogEventLevel.Details);
                OnNewVersionDetected(this, new UpdateEventArgs() { filename = string.Empty, version = _version });
            }
            catch (Exception ex)
            {
                AgentLogs.WriteLog("UPDATE: AgentUpdate check version failed" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace, AgentLogEventLevel.Errors);

                //new AgentException("AgentUpdate check version failed", ex);
                //do nothing
            }
        }
    }
}
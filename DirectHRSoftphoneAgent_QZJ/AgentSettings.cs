using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using DHRSoftphone.AgentExceptions;
using DHRSoftphone.IDHRSPPlugin;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public class AgentSettingsStartingArgs
    {
        public string appPath { get; set; }

        public string userDocumentPath { get; set; }
    }

    /// <summary>
    /// Copyright (C) 1985-2012 Vincent Qiu, Direct HR
    /// Http://hoho.im 版权所有。
    /// NameSpace: DirectHRSoftphoneAgent_QZJ
    /// FullName: DirectHRSoftphoneAgent_QZJ.AgentSettings
    /// Class Created: 2011/11/25 16:45
    /// On Computer: NOV30TH-LAPTOP - Administrator
    ///
    /// </summary>
    public class AgentSettings
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        public delegate void SettingsChange(object sender, Dictionary<string, string> settings);

        static public event SettingsChange SettingsChanged;

        public static void OnSettingsChanged(Dictionary<string, string> settings)
        {
            SettingsChange handler = SettingsChanged;
            if (handler != null) handler(null, settings);
        }

        static public bool Inited = false;

        public static string IniFileName { get; set; }

        private static AgentDatabaseProvider.AgentCityItem m_currentCity;
        private static string _currentExtension = string.Empty;
        private static string _fullname = string.Empty;
        static private Dictionary<string, string> settings = new Dictionary<string, string>();
        private bool isSettingReleased = false;

        static public void WriteConfigurationFromPlugin(object sender, AgentPluginConfArgs e)
        {
            new AgentSettings().
            WriteString(string.IsNullOrEmpty(e.Section) ? ((PluginObject)sender).PluginName : e.Section, e.Name,
                        e.Value);
        }

        public string GetFullname()
        {
            _fullname = ReadString("Account", "Fullname", "");
            if (string.IsNullOrEmpty(_fullname))
                AgentLogs.WriteLog("Full name not set!", AgentLogEventLevel.Warnings);
            return _fullname;
        }

        public void SetFullname(string fullname)
        {
            if (string.IsNullOrEmpty(fullname))
                return;
            _fullname = fullname;
            AgentLogs.WriteLog("Saving fullname " + fullname);
            WriteString("Account", "Fullname", _fullname);
            ChangeSetting("FullName", _fullname);
        }

        public string GetCurrentExtension()
        {
            if (string.IsNullOrEmpty(_currentExtension))
                _currentExtension = new AgentSettings().ReadString("Account", "Extension", "000");
            ChangeSetting("ExtensionNumber", _currentExtension);
            return _currentExtension;
        }

        public void SetCurrentExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return;
            AgentLogs.WriteLog("Set Current Extension To " + extension);
            WriteString("Account", "Extension", extension);
            ChangeSetting("ExtensionNumber", _currentExtension);
            _currentExtension = extension;
        }

        public AgentDatabaseProvider.AgentCityItem GetCurrentCityInConf()
        {
            string areacode = ReadString("Location", "AreaCode", string.Empty);
            m_currentCity = AgentDatabaseProvider.GetCityByAreaCode(areacode);
            AgentDatabaseProvider.CurrentAreaCode = areacode;
            ChangeSetting("AreaCode", areacode);
            if (m_currentCity != null)
                ChangeSetting("CityName", m_currentCity.City);
            return m_currentCity;
        }

        public void SetCurrentAreaCode(string areacode)
        {
            WriteString("Location", "AreaCode", areacode);
            ChangeSetting("AreaCode", areacode);
            m_currentCity = AgentDatabaseProvider.GetCityByAreaCode(areacode);
            if (m_currentCity != null)
                ChangeSetting("CityName", m_currentCity.City);
            AgentDatabaseProvider.CurrentAreaCode = areacode;
        }

        public void SetUpdateUri(string uri)
        {
            WriteString("Update", "Uri", uri);
            ChangeSetting("UpdateUri", uri);
        }

        public string GetUpdateUri()
        {
            return "http://softphone.directhr.cn/Update.xml?" + (new Random().NextDouble()).ToString();
            //return ReadString("Update", "Uri", "http://softphone.directhr.cn/Update.xml?");
        }

        static public string GetConfigFilePath()
        {
            if (string.IsNullOrEmpty(IniFileName))
                IniFileName = "AgentOptions.ini";
            return IniFileName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentSettings"/> class.
        /// </summary>
        public AgentSettings()
        {
            if (string.IsNullOrEmpty(IniFileName))
                IniFileName = "AgentOptions.ini";
            FileInfo fileInfo = new FileInfo(GetConfigFilePath());
            if ((!fileInfo.Exists))
            { //|| (FileAttributes.Directory in fileInfo.Attributes))
                System.IO.StreamWriter sw = new System.IO.StreamWriter(IniFileName, false, System.Text.Encoding.Default);
                try
                {
                    sw.Write("#Direct HR Softphone Agent Version 1.x Configuration File");
                    sw.Close();
                }

                catch
                {
                    throw (new ApplicationException("Can not create INI file!"));
                }
            }

            //必须是完全路径，不能是相对路径
            IniFileName = fileInfo.FullName;
        }

        /// <summary>
        /// Writes the string.
        /// </summary>
        /// <param name="Section">The section.</param>
        /// <param name="Ident">The ident.</param>
        /// <param name="Value">The value.</param>
        public void WriteString(string Section, string Ident, string Value)
        {
            AgentLogs.WriteLog("Write data to Ini file: " + "section:" + Section + " Ident:" + Ident + " Value:" + Value);
            if (WritePrivateProfileString(Section, Ident, Value, IniFileName) == 0)
            {
                throw (new AgentException("Failed at writing/saving agent confuration file!"));
            }
            ChangeSetting(string.Format("{0}-{1}", Section, Ident), Value);
        }

        /// <summary>
        /// Reads the string.
        /// </summary>
        /// <param name="Section">The section.</param>
        /// <param name="Ident">The ident.</param>
        /// <param name="Default">The default.</param>
        /// <returns></returns>
        public string ReadString(string Section, string Ident, string Default)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), IniFileName);

            //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);
            s = s.Trim();
            ChangeSetting(string.Format("{0}-{1}", Section, Ident), s);

            return s;
        }

        /// <summary>
        /// Reads the integer.
        /// </summary>
        /// <param name="Section">The section.</param>
        /// <param name="Ident">The ident.</param>
        /// <param name="Default">The default.</param>
        /// <returns></returns>
        public int ReadInteger(string Section, string Ident, int Default)
        {
            string intStr = ReadString(Section, Ident, Convert.ToString(Default));
            try
            {
                return Convert.ToInt32(intStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Default;
            }
        }

        /// <summary>
        /// Writes the integer.
        /// </summary>
        /// <param name="Section">The section.</param>
        /// <param name="Ident">The ident.</param>
        /// <param name="Value">The value.</param>
        public void WriteInteger(string Section, string Ident, int Value)
        {
            WriteString(Section, Ident, Value.ToString());
        }

        /// <summary>
        /// Reads the bool.
        /// </summary>
        /// <param name="Section">The section.</param>
        /// <param name="Ident">The ident.</param>
        /// <param name="Default">if set to <c>true</c> [default].</param>
        /// <returns></returns>
        public bool ReadBool(string Section, string Ident, bool Default)
        {
            try
            {
                return Convert.ToBoolean(ReadString(Section, Ident, Convert.ToString(Default)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Default;
            }
        }

        /// <summary>
        /// Writes the bool.
        /// </summary>
        /// <param name="Section">The section.</param>
        /// <param name="Ident">The ident.</param>
        /// <param name="Value">if set to <c>true</c> [value].</param>
        public void WriteBool(string Section, string Ident, bool Value)
        {
            WriteString(Section, Ident, Convert.ToString(Value));
        }

        //从Ini文件中，将指定的Section名称中的所有Ident添加到列表中
        public void ReadSection(string Section, StringCollection Idents)
        {
            Byte[] Buffer = new Byte[16384];

            //Idents.Clear();

            int bufLen = GetPrivateProfileString(Section, null, null, Buffer, Buffer.GetUpperBound(0),
             IniFileName);

            //对Section进行解析
            GetStringsFromBuffer(Buffer, bufLen, Idents);
        }

        private void GetStringsFromBuffer(Byte[] Buffer, int bufLen, StringCollection Strings)
        {
            Strings.Clear();
            if (bufLen != 0)
            {
                int start = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if ((Buffer[i] == 0) && ((i - start) > 0))
                    {
                        String s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
                        Strings.Add(s);
                        start = i + 1;
                    }
                }
            }
        }

        //从Ini文件中，读取所有的Sections的名称
        public void ReadSections(StringCollection SectionList)
        {
            //Note:必须得用Bytes来实现，StringBuilder只能取到第一个Section
            byte[] Buffer = new byte[65535];
            int bufLen = 0;
            bufLen = GetPrivateProfileString(null, null, null, Buffer,
             Buffer.GetUpperBound(0), IniFileName);
            GetStringsFromBuffer(Buffer, bufLen, SectionList);
        }

        //读取指定的Section的所有Value到列表中
        public void ReadSectionValues(string Section, NameValueCollection Values)
        {
            StringCollection KeyList = new StringCollection();
            ReadSection(Section, KeyList);
            Values.Clear();
            foreach (string key in KeyList)
            {
                Values.Add(key, ReadString(Section, key, ""));
            }
        }

        public void EraseSection(string Section)
        {
            if (WritePrivateProfileString(Section, null, null, IniFileName) == 0)
            {
                throw (new ApplicationException("无法清除Ini文件中的Section"));
            }
        }

        public void DeleteKey(string Section, string Ident)
        {
            WritePrivateProfileString(Section, Ident, null, IniFileName);
        }

        //Note:对于Win9X，来说需要实现UpdateFile方法将缓冲中的数据写入文件
        //在Win NT, 2000和XP上，都是直接写文件，没有缓冲，所以，无须实现UpdateFile
        //执行完对Ini文件的修改之后，应该调用本方法更新缓冲区。
        public void UpdateFile()
        {
            WritePrivateProfileString(null, null, null, IniFileName);
        }

        public bool ValueExists(string Section, string Ident)
        {
            //
            StringCollection Idents = new StringCollection();
            ReadSection(Section, Idents);
            return Idents.IndexOf(Ident) > -1;
        }

        /// <summary>
        /// Inits the settings.
        /// </summary>
        /// <param name="args">The args.</param>
        static public void InitSettings(object args)
        {
            AgentSettingsStartingArgs agentSettingsStartingArgs = (AgentSettingsStartingArgs)args;
            string userDocumentPath = agentSettingsStartingArgs.userDocumentPath;
            ChangeSetting("UserDocumentPath", userDocumentPath);
            string appPath = agentSettingsStartingArgs.appPath;
            ChangeSetting("AppPath", appPath);

            //Init the filename with absolute path.
            AgentLogs.SetLogFile(Path.Combine(userDocumentPath, "dhr_softphone_logs.log"));
            ChangeSetting("LogFilePath", AgentLogs.GetLogFile());
            AgentLogs.WriteLog("log file path inited!", AgentLogEventLevel.Info);
            AgentSettings.IniFileName = Path.Combine(userDocumentPath, "AgentOptions.ini");
            ChangeSetting("IniFilePath", IniFileName);
            AgentLogs.WriteLog("Ini file path inited: " + AgentSettings.IniFileName, AgentLogEventLevel.Info);
            AgentException.SetErrorFile(Path.Combine(userDocumentPath, "error.log"));
            ChangeSetting("ErrorLogFilePath", AgentException.GetErrorFile());

            AgentLogs.WriteLog("Error log file path inited: " + AgentException.GetErrorFile(), AgentLogEventLevel.Info);

            //Init the city database to list

            AgentLogs.WriteLog("Init datasource of cellphone database...", AgentLogEventLevel.Info);
            AgentDatabaseProvider.DataSource = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + appPath + "cellphone_agentdatabase.dat";
            AgentLogs.WriteLog("datasource inited: " + AgentDatabaseProvider.DataSource, AgentLogEventLevel.Info);
            ChangeSetting("DataSource", AgentDatabaseProvider.DataSource);

            AgentLogs.WriteLog("Getting city list...", AgentLogEventLevel.Info);
            AgentDatabaseProvider.GetCityList();
            AgentLogs.WriteLog("City list got.", AgentLogEventLevel.Info);
            AgentLogs.WriteLog("Getting data list...", AgentLogEventLevel.Info);
            AgentDatabaseProvider.GetDataList();
            AgentLogs.WriteLog("Data list got.", AgentLogEventLevel.Info);

            AgentSettings settings = new AgentSettings();

            //THE NEXT CODE MAKE STATIC UPDATE ADDRESS.
            AgentUpdate.UpdateUrl = settings.GetUpdateUri();

            settings.SetUpdateUri(AgentUpdate.UpdateUrl);
            ChangeSetting("UpdateUri", AgentUpdate.UpdateUrl);

            AgentLogs.WriteLog("SetUpdateUri: " + AgentUpdate.UpdateUrl, AgentLogEventLevel.Info);
            AgentLogs.WriteLog("GetCurrentCityInConf", AgentLogEventLevel.Info);
            AgentDatabaseProvider.AgentCityItem city = settings.GetCurrentCityInConf();
            AgentLogs.WriteLog("Get fullname...", AgentLogEventLevel.Info);
            settings.GetFullname();

            if (city != null)
            {
                ChangeSetting("AreaCode", city.AreaCode);
                ChangeSetting("CityName", city.City);
                ChangeSetting("FullName", settings.GetFullname());
            }

            ChangeSetting("Version", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            Inited = true;
        }

        public void ReleaseSettings()
        {
            isSettingReleased = true;
            OnSettingsChanged(settings);
        }

        static public void ChangeSetting(string key, string value)
        {
            settings[key.ToUpper()] = value;
        }

        static public Dictionary<string, string> GetSettings()
        {
            return settings;
        }

        ~AgentSettings()
        {
            UpdateFile();
            if (!isSettingReleased)
                OnSettingsChanged(settings);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using DHRSoftphone.IDHRSPPlugin;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    /// <summary>
    /// Copyright (C) 1985-2012 Vincent Qiu, Direct HR
    /// Http://hoho.im 版权所有。
    /// NameSpace: DHRSoftphone.SoftphoneAgent_QZJ
    /// FullName: DHRSoftphone.SoftphoneAgent_QZJ.DhrSpPlugin
    /// Class Created: 2012/8/24 0:59
    /// On Computer: NOV30TH-LAPTOP - Administrator
    ///
    /// </summary>
    public class DhrSpPlugin
    {
        public string FileLocation { get; set; }

        public PluginObject Plugin { get; set; }
    }

    /// <summary>
    /// Copyright (C) 1985-2012 Vincent Qiu, Direct HR
    /// Http://hoho.im 版权所有。
    /// NameSpace: DHRSoftphone.SoftphoneAgent_QZJ
    /// FullName: DHRSoftphone.SoftphoneAgent_QZJ.DhrSpPluginProvider
    /// Class Created: 2012/8/24 0:59
    /// On Computer: NOV30TH-LAPTOP - Administrator
    ///
    /// </summary>
    public class DhrSpPluginProvider
    {
        protected static List<PluginObject> plugins = new List<PluginObject>();
        protected static List<Thread> pluginThreads = new List<Thread>();

        public static List<PluginObject> GetPlugins()
        {
            return plugins;
        }

        public void ShowMessages(object sender, string e)
        {
            FormMessage fm = new FormMessage();
            fm.Messages = e;
            fm.Show();
        }

        public void InitAllPluginsWithConfigurations(Dictionary<string, string> confs)
        {
            foreach (var plugin in plugins)
            {
                ((IDHRSPLoad)plugin).InitConfiguration(confs);
                AgentSettings.SettingsChanged += ((IDHRSPLoad)plugin).OnSettingsChanged;
            }
        }

        public void InitBeforeEvents()
        {
            foreach (PluginObject plugin in plugins)
            {
                ((IDHRSPLoad)plugin).InitBeforeEvents();
                plugin.WriteConfToFile += AgentSettings.WriteConfigurationFromPlugin;
                plugin.WriteLogToFile += AgentLogs.WriteLogToFileFromPlugin;
                //plugin.ShowMessagesWindow
            }
        }

        public void LoadAllPlugins(string appPath)
        {
            DirectoryInfo df = new System.IO.DirectoryInfo(Path.Combine(appPath, "Plugins"));
            AgentLogs.WriteLog("Plugin Path: " + Path.Combine(appPath, "Plugins").ToString(CultureInfo.InvariantCulture), AgentLogEventLevel.Details);
            FileInfo[] fr = df.GetFiles();
            foreach (FileInfo f in fr.Where(f => f.Name.EndsWith(".dll")))
            {
                try
                {
                    System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFile(f.FullName);
                    Type[] types = asm.GetExportedTypes();

                    //if (types.Count() > 1)
                    //    continue;
                    var type = types.FirstOrDefault(t => t.Name == "SoftphoneLoader");
                    if (type == null)
                        continue;
                    ;

                    //Type type = types[0];
                    object obj = Activator.CreateInstance(type);
                    PluginObject pw = obj as PluginObject;
                    pw.PluginVersion = asm.FullName;
                    pw.ShowMessagesWindow += ShowMessages;
                    string result = ((IDHRSPLoad)pw).Test();
                    if (result.Equals("OK"))
                    {
                        //var plugArgs = new DhrSpPlugin();
                        //plugArgs.FileLocation = f.FullName;
                        //plugArgs.Plugin = pw;
                        plugins.Add(pw);
                        AgentLogs.WriteLog("Plugin [" + type.FullName + "]: " + pw.PluginName + " loaded!", AgentLogEventLevel.Info);
                    }
                    else
                    {
                        AgentLogs.WriteLog("Plugin [" + type.FullName + "]: " + pw.PluginName + " testing not return [OK], failed to load!" + Environment.NewLine + "Details: " + result, AgentLogEventLevel.Info);
                    }
                }
                catch (Exception ex)
                {
                    AgentLogs.WriteLog("Can not load plugins from" + f.FullName + Environment.NewLine + ex.StackTrace,
                                       AgentLogEventLevel.Details);

                    //new AgentException("Can not load plugins from" + f.FullName, ex);
                }
            }
        }




        public void Startup()
        {
            Startup(plugins);
            //foreach (IDHRSPLoad pluginObject in plugins)
            //{
            //    var thread = new Thread(new ThreadStart(pluginObject.StartUp));
            //    pluginThreads.Add(thread);
            //    thread.Start();
            //}
        }

        public void Startup<T>(List<T> t) where T : IDHRSPLoad
        {
            foreach (dynamic pluginObject in t)
            {
                //pluginObject.StartUp();
                var thread = new Thread(new ThreadStart(pluginObject.StartUp));
                pluginThreads.Add(thread);
                thread.Start();
            }
        }
    }
}
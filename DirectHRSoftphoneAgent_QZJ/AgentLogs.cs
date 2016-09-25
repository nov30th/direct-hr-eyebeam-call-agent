using System;
using System.IO;
using DHRSoftphone.IDHRSPPlugin;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    /// <summary>
    /// Log level
    /// </summary>
    [Flags]
    public enum AgentLogEventLevel
    {
        All,
        Details,
        Info,
        Warnings,
        Errors
    }

    /// <summary>
    /// Copyright (C) 1985-2012 Vincent Qiu, Direct HR
    /// Http://hoho.im 版权所有。
    /// NameSpace: DirectHRSoftphoneAgent_QZJ
    /// FullName: DirectHRSoftphoneAgent_QZJ.AgentLogs
    /// Class Created: 2012/03/15
    /// On Computer: I7
    /// </summary>
    public class AgentLogs
    {
        private static string _logFileName = "dhr_softphone_logs.log";
        private static AgentLogEventLevel _logLevel = AgentLogEventLevel.All;

        static public string GetLogFile()
        {
            return _logFileName;
        }

        static public void SetLogFile(string filename)
        {
            _logFileName = filename;
            //if (File.Exists(filename))
            //{
            //    if ((File.GetAttributes(filename) & FileAttributes.Compressed) != FileAttributes.Compressed)
            //        File.SetAttributes(filename, FileAttributes.Compressed);
            //}
        }

        static public void SetLogLevel(AgentLogEventLevel logLevel)
        {
            _logLevel = logLevel;
        }

        static public void WriteLogToFileFromPlugin(object sender, AgentPluginLogArgs e)
        {
            var pluginName = ((PluginObject)sender).PluginName;
            if (string.IsNullOrEmpty(pluginName))
                pluginName = "UNKNOWN PLUGIN!";
            FileStream fs = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(
                string.Format("{0}{1} {2}: {3} - {4}",
            Environment.NewLine,
            DateTime.UtcNow,
            pluginName,
            Enum.GetName(typeof(AgentPluginLogEventLevel), e.LogLevel),
            e.Messages
            ));
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// Format message to with date time
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stack"></param>
        /// <returns></returns>
        static public string FormatMessage(string message, AgentLogEventLevel logLevel)
        {
            return string.Format("{0}{1}: {2} - {3}",
                Environment.NewLine,
                DateTime.UtcNow,
                Enum.GetName(typeof(AgentLogEventLevel), logLevel),
                message
                );
        }

        /// <summary>
        /// Write message to log file
        /// </summary>
        /// <param name="message">the message will be saved to log file</param>
        /// <param name="logLevel">log level of the message</param>
        /// <returns></returns>
        static public string WriteLog(string message, AgentLogEventLevel logLevel = AgentLogEventLevel.Info)
        {
            if (_logLevel <= logLevel)
            {
                FileStream fs = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(FormatMessage(message, logLevel));
                sw.Close();
                fs.Close();
                return message;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
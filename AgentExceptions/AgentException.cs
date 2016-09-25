using System;
using System.IO;

namespace DHRSoftphone.AgentExceptions
{
    /// <summary>
    /// Copyright (C) 1985-2012 Vincent Qiu, Direct HR
    /// Http://hoho.im 版权所有。
    /// NameSpace: DirectHRSoftphoneAgent_QZJ
    /// FullName: DirectHRSoftphoneAgent_QZJ.AgentException
    /// Class Created: 2011/11/25 16:28
    /// On Computer: NOV30TH-LAPTOP - Administrator
    ///
    /// </summary>
    public class AgentException : Exception
    {
        private static string _errorFileName = "error.log";

        static public string GetErrorFile()
        {
            return _errorFileName;
        }

        static public void SetErrorFile(string filename)
        {
            _errorFileName = filename;
        }

        public AgentException(string message)
            : base(message)
        {
            WriteText(FormatErrorMessage(message));
            //File.AppendAllText(_errorFileName, FormatErrorMessage(message));
        }

        public AgentException()
            : base()
        { }

        public AgentException(string message, Exception innerException)
            : base(message, innerException)
        {
            WriteText(FormatErrorMessage(message));
            WriteText(FormatErrorMessage(innerException.Message, innerException.StackTrace));
            //File.AppendAllText(_errorFileName, FormatErrorMessage(message));
            //File.AppendAllText(_errorFileName, FormatErrorMessage(innerException.Message, innerException.StackTrace));
        }

        /// <summary>
        /// Formats the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public string FormatErrorMessage(string message)
        {
            return string.Format("Error Message At {0}: {1}\r\n",
                DateTime.UtcNow,
                message
                );
        }

        /// <summary>
        /// Formats the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="stack">The stack.</param>
        /// <returns></returns>
        public string FormatErrorMessage(string message, string stack)
        {
            return string.Format("Error Message At {0}: {1}\r\n Stack Trace: {2}\r\n",
                DateTime.UtcNow,
                message,
                stack
                );
        }

        public Exception DataException(Exception e)
        {
            WriteText(FormatErrorMessage("DATA PROCESS ERROR:" + e.Message, e.StackTrace));
            //File.AppendAllText(_errorFileName, FormatErrorMessage("DATA PROCESS ERROR:" + e.Message, e.StackTrace));
            return e;
        }

        public void WriteText(string text)
        {

            FileStream fs = new FileStream(_errorFileName, FileMode.Append, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(text);
            sw.Close();
            fs.Close();
        }
    }
}
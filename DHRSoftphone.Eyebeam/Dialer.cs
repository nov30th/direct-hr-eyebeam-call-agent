using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHRSoftphone.Eyebeam
{
    public static class Dialer
    {
        static public string StartupPath { get; set; }

        public static bool Dial(string number)
        {

            if (string.IsNullOrEmpty(StartupPath))
                throw new ApplicationException("Program Startup Path Not Set!");
            System.Diagnostics.ProcessStartInfo newProcess = new System.Diagnostics.ProcessStartInfo();
            newProcess.FileName = "eyebeam.exe";
            newProcess.Arguments = " -dial=\"" + number + "\"";
            newProcess.WorkingDirectory = StartupPath;
            try
            {
                var processing = System.Diagnostics.Process.Start(newProcess);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}

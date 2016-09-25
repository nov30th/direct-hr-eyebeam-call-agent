using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DHRSoftphone.Plugins.DNSFixer
{
    public static class LocalHostsController
    {
        private static readonly string _hostPath = Path.Combine(Environment.SystemDirectory, "drivers\\etc\\hosts");

        public static string HostPath
        {
            get { return LocalHostsController._hostPath; }
        } 

        private static string _hostContent;
        private static string _oldhostContent;
        public static Exception LastError { get; set; }

        private static readonly string _beginSign = @"######DHR CONTEXT BEGIN######";
        private static readonly string _endSign = @"######DHR CONTEXT END######";
        private static readonly string _additionSign = string.Empty;

        public static bool WritableCheck()
        {
            string testString = string.Format("#Created By DHR Softphone Agent At {0}", DateTime.Now);
            try
            {
                ProtectFile(false);
                if (!File.Exists(_hostPath))
                    using (var sw = File.CreateText(_hostPath))
                        sw.WriteLine(testString);
                using (StreamReader sr = new StreamReader(_hostPath))
                {
                    _hostContent = sr.ReadToEnd();
                }
                using (StreamWriter sw = new StreamWriter(_hostPath))
                {
                    sw.Write(_hostContent);
                }
                ProtectFile(true);
            }
            catch (UnauthorizedAccessException ex)
            {
                LastError = ex;
                return false;
            }
            return true;
        }


        public static void RemoveAddedHostsContentWithoutWrite()
        {
            if (_hostContent.IndexOf(_beginSign) >= 0)
            {
                var begin = _hostContent.IndexOf(_beginSign);
                //Please notice that +2 means the \r\n, otherwise you may got \r\n everytime you modify the hosts context
                int count = 0;
                if (_hostContent.IndexOf(_endSign) > 0)
                    count = _hostContent.IndexOf(_endSign) + _endSign.Length - begin + 2;
                else
                    count = _hostContent.Length - begin + 2;
                if (count > _hostContent.Length - begin)
                    count = _hostContent.Length - begin; 
                _hostContent = _hostContent.Remove(begin, count);
            }
        }

        public static void MakeHostsContextWithoutWrite(string newHosts, string addationSign)
        {
            StringBuilder sb = new StringBuilder(_beginSign).AppendLine();
            sb.AppendLine(addationSign).AppendLine();
            sb.AppendLine(newHosts).AppendLine(_endSign).Append(_hostContent);
            _hostContent = sb.ToString();
        }

        public static void MakeHostsContextWithoutWrite(string newHosts)
        {
            MakeHostsContextWithoutWrite(newHosts, string.Empty);
        }


        public static bool ReplaceContext(string hostsContext, string additionSign)
        {
            ProtectFile(false);
            using (StreamReader sr = new StreamReader(_hostPath))
            {
                _hostContent = sr.ReadToEnd();
            }
            RemoveAddedHostsContentWithoutWrite();
            MakeHostsContextWithoutWrite(hostsContext, additionSign);
            using (StreamWriter sw = new StreamWriter(_hostPath))
            {
                sw.Write(_hostContent);
            }
            ProtectFile(true);
            if (_oldhostContent != hostsContext)
            {
                _oldhostContent = hostsContext;
                FlushDNS();
            }
            return true;
        }

        public static bool FlushDNS()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine("ipconfig /flushdns");
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            return true;
        }

        public static bool ProtectFile(bool protect)
        {
            if (protect)
                File.SetAttributes(_hostPath, FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReadOnly);
            else
                File.SetAttributes(_hostPath, FileAttributes.Normal);
            return true;
        }

        public static bool SetRights(string filePath)
        {
            try
            {
                if (!File.Exists(_hostPath))
                {
                    using (StreamWriter sw = new StreamWriter(_hostPath))
                    {
                        sw.Write("#Hosts");
                    }
                }
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.WriteLine("icacls " + _hostPath + " /grant Users:(F)");
                p.StandardInput.WriteLine("exit");
                string strRst = p.StandardOutput.ReadToEnd();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
   //     //TEST with failed, can not process with UAC enabled, put the command to DHR Softphone Installer
   //     //cacls hosts /t /e /c /g Users:f
   //     public static bool AddRights()
   //     {
   //         return SetFolderACL(_hostPath, "Users", FileSystemRights.FullControl, AccessControlType.Allow);
   //     }


   //     public static bool SetFolderACL(String FolderPath, String UserName, FileSystemRights Rights, AccessControlType AllowOrDeny
   //, InheritanceFlags Inherits, PropagationFlags PropagateToChildren, AccessControlModification AddResetOrRemove)
   //     {
   //         bool ret;
   //         DirectoryInfo folder = new DirectoryInfo(FolderPath);
   //         DirectorySecurity dSecurity = folder.GetAccessControl(AccessControlSections.All);
   //         FileSystemAccessRule accRule = new FileSystemAccessRule(UserName, Rights, Inherits, PropagateToChildren, AllowOrDeny);
   //         dSecurity.ModifyAccessRule(AddResetOrRemove, accRule, out ret);
   //         folder.SetAccessControl(dSecurity);
   //         return ret;
   //     }

   //     public static bool SetFolderACL(String FolderPath, String UserName, FileSystemRights Rights, AccessControlType AllowOrDeny)
   //     {
   //         InheritanceFlags inherits = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
   //         return SetFolderACL(FolderPath, UserName, Rights, AllowOrDeny, inherits, PropagationFlags.None, AccessControlModification.Add);
   //     }
    }
}



using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DHRSoftphone.Plugins.ProcessKiller
{
    /// <summary>
    /// Copyright (C) 1985-2012 Vincent Qiu, Direct HR
    /// Http://hoho.im 版权所有。
    /// NameSpace: DHRSoftphone.Plugins.ProcessKiller
    /// FullName: DHRSoftphone.Plugins.ProcessKiller.ClosedArgs
    /// Class Created: 2012/8/24 0:44
    /// On Computer: NOV30TH-LAPTOP - Administrator
    ///
    /// </summary>
    public class ClosedArgs
    {
        public string AddationInfo { get; set; }

        public bool IsProcessSearch { get; set; }

        public Process Process { get; set; }

        public string SearchKey { get; set; }

        public string WindowName { get; set; }
    }

    /// <summary>
    /// Copyright (C) 1985-2012 Vincent Qiu, Direct HR
    /// Http://hoho.im 版权所有。
    /// NameSpace: DHRSoftphone.Plugins.ProcessKiller
    /// FullName: DHRSoftphone.Plugins.ProcessKiller.ProcessController
    /// Class Created: 2012/8/24 0:45
    /// On Computer: NOV30TH-LAPTOP - Administrator
    ///
    /// </summary>
    public class ProcessController
    {
        private List<Exception> _lastErrors = new List<Exception>();

        private System.Diagnostics.Process[] _processOnComputer = System.Diagnostics.Process.GetProcesses();

        private Dictionary<IntPtr, string> _windowHwnds = new Dictionary<IntPtr, string>();

        public delegate bool CallBack(IntPtr hwnd, int y);

        public delegate void OnProcessClosedCallBack(object sender, ClosedArgs args);

        public event OnProcessClosedCallBack ProcessClosed;

        public List<Exception> LastErrors
        {
            get { return _lastErrors; }
            set { _lastErrors = value; }
        }

        public Process[] ProcessOnComputer
        {
            get { return _processOnComputer; }
        }

        public Dictionary<IntPtr, string> WindowHwnds
        {
            get { return _windowHwnds; }
        }

        [DllImport("user32.dll")]
        public static extern IntPtr EnumWindows(CallBack x, int y);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")] //找子窗体
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32")]
        public static extern IntPtr GetParent(IntPtr hwnd);

        [DllImport("user32")]
        public static extern IntPtr GetWindowText(IntPtr hwnd, StringBuilder lptrString, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint lpdwProcessId);

        [DllImport("user32")]
        public static extern IntPtr IsWindowVisible(IntPtr hwnd);
        [DllImport("user32.dll", EntryPoint = "SendMessage")] //用于发送信息给窗体
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        public void GetAllProcess()
        {
            _processOnComputer = Process.GetProcesses();
        }

        public void GetAllWindows()
        {
            //items.Clear();
            _windowHwnds.Clear();
            EnumWindows(Report, 0);
        }

        public uint GetWindowThreadProcessId(IntPtr hWnd)
        {
            uint procId = 0;
            GetWindowThreadProcessId(hWnd, ref procId);
            return procId;
        }

        public int KillProcess(string processFileName, bool isFullMatch)
        {
            _lastErrors.Clear();
            int killedCount = 0;
            if (_processOnComputer.Any(p => p.ProcessName.ToUpper().Contains(processFileName.ToUpper())))
            {
                var processes = _processOnComputer.Where(p => p.ProcessName.ToUpper().Contains(processFileName.ToUpper()));
                foreach (var process in processes)
                {
                    if (!isFullMatch || processFileName.ToUpper() == process.ProcessName.ToUpper())
                    {
                        try
                        {
                            if (process != null && process.HasExited == false)
                            {
                                process.Kill();
                                OnClosedProgram(new ClosedArgs()
                                                    {
                                                        AddationInfo = string.Empty,
                                                        IsProcessSearch = true,
                                                        Process = process,
                                                        SearchKey = processFileName
                                                    });
                                killedCount++;
                            }
                        }
                        catch (Exception ex)
                        {
                            _lastErrors.Add(ex);
                        }
                    }
                }
            }
            return killedCount;
        }


        public int HasProcess(string processFileName, bool isFullMatch)
        {
            _lastErrors.Clear();
            int Count = 0;
            if (_processOnComputer.Any(p => p.ProcessName.ToUpper().Contains(processFileName.ToUpper())))
            {
                var processes = _processOnComputer.Where(p => p.ProcessName.ToUpper().Contains(processFileName.ToUpper()));
                foreach (var process in processes)
                {
                    if (!isFullMatch || processFileName.ToUpper() == process.ProcessName.ToUpper())
                    {
                        try
                        {
                            if (process != null && process.HasExited == false)
                            {
                                //process.Kill();
                                Count++;
                            }
                        }
                        catch (Exception ex)
                        {
                            _lastErrors.Add(ex);
                        }
                    }
                }
            }
            return Count;
        }


        public int KillWindow(string windowName, bool isFullMatch)
        {
            int killedCount = 0;
            if (!isFullMatch)
            {
                if (_windowHwnds.Any(w => w.Value.ToUpper().Contains(windowName.ToUpper())))
                {
                    foreach (var windowHwnd in _windowHwnds.Where(w => w.Value.ToUpper().Contains(windowName.ToUpper())))
                    {
                        try
                        {
                            var searchedWindowName = windowHwnd.Value;
                            var process = Process.GetProcessById(
                                Convert.ToInt32(GetWindowThreadProcessId(windowHwnd.Key)));
                            if (process.HasExited == false)
                            {
                                process.Kill();
                                OnClosedProgram(new ClosedArgs()
                                {
                                    AddationInfo = string.Empty,
                                    IsProcessSearch = false,
                                    Process = process,
                                    SearchKey = windowName,
                                    WindowName = searchedWindowName
                                });
                                killedCount++;
                            }
                        }
                        catch (Exception ex)
                        {
                            _lastErrors.Add(ex);
                        }
                    }
                }
            }
            else
            {
                if (_windowHwnds.Any(w => w.Value.ToUpper() == windowName.ToUpper()))
                {
                    foreach (var windowHwnd in _windowHwnds.Where(w => w.Value.ToUpper() == windowName.ToUpper()))
                    {
                        try
                        {
                            var searchedWindowName = windowHwnd.Value;
                            var process = Process.GetProcessById(
                                                          Convert.ToInt32(GetWindowThreadProcessId(windowHwnd.Key)));
                            if (process.HasExited == false)
                            {
                                process.Kill();
                                OnClosedProgram(new ClosedArgs()
                                                    {
                                                        AddationInfo = string.Empty,
                                                        IsProcessSearch = false,
                                                        Process = process,
                                                        SearchKey = windowName,
                                                        WindowName = searchedWindowName
                                                    });
                                killedCount++;
                            }
                        }
                        catch (Exception ex)
                        {
                            _lastErrors.Add(ex);
                        }
                    }
                }
            }
            return killedCount;
        }

        public void Refresh()
        { GetAllProcess(); GetAllWindows(); }

        //List<string> items = new List<string>();
        public bool Report(IntPtr hwnd, int lParam)
        {
            //IntPtr pHwnd;
            //pHwnd = GetParent(hwnd);
            //if (!_windowHwnds.ContainsKey(hwnd) && (int) hwnd != 0)// (pHwnd == 0 )//&& IsWindowVisible(hwnd) == 1)
            //{
            var sb = new StringBuilder(512);
            GetWindowText(hwnd, sb, sb.Capacity);
            if (sb.Length > 0)
            {
                //items.Add(sb.ToString());
                _windowHwnds.Add(hwnd, sb.ToString().ToUpper());
            }

            //}
            return true;
        }

        protected void OnClosedProgram(ClosedArgs args)
        {
            OnProcessClosedCallBack handler = ProcessClosed;
            if (handler != null) handler(this, args);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using DHRSoftphone.AgentExceptions;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public static class QzjIoCtl
    {
        public static int _error_code = 0;
        public static bool _isDriverInstalled = false;
        public static int _failedTimes = 0;
        //IntPtr buffer = Marshal.AllocHGlobal(sizeof(int));
        //result = DeviceIoControl(_hdev, CTL_CODE(0x00000033, 0x0400, 0, 1), IntPtr.Zero, 0, buffer, sizeof(int), out bytesReturned, IntPtr.Zero);
        //int sessionId = Marshal.ReadInt32(buffer);
        //Marshal.FreeHGlobal(buffer);

        public static void Timer()
        {
            while (true)
            {
                if (SendHeartbeat())
                {
                    _failedTimes = 0;
                }
                else
                {
                    _failedTimes++;
                    if (_failedTimes > 10)
                    {
                        throw new ApplicationException("Driver missing, application will not exit!");
                    }
                }

                Thread.Sleep(10000);
            }
        }

        public static bool SendAllowAll()
        {
            return SendHeartbeat(0xFF03);
        }


        public static bool SendBlockAll()
        {
            return SendHeartbeat(0xFF02);
        }

        public static bool SendResotreToHeartbeat()
        {
            return SendHeartbeat(0xFF04);
        }

        public static bool SendHeartbeat()
        {
            return SendHeartbeat(0xFF01);
        }


        public static bool SendHeartbeat(int QzjNdisStatus)
        {
            string driveLetter = "QzjNdis";
            SafeFileHandle _hdev = CreateFileR(driveLetter);
            if (_hdev.IsClosed | _hdev.IsInvalid)
            {
                int error_code = Marshal.GetLastWin32Error();
                new AgentException("QzjIoCtl: Error opening device. " + error_code);
                return false;
            }
            else
            {
                _isDriverInstalled = true;
            }

            bool result = false;
            int bytesReturned = 0;

            IntPtr buffer = Marshal.AllocHGlobal(sizeof(int));
            result = DeviceIoControl(_hdev, CTL_CODE(0x00000017, QzjNdisStatus, 0, 0), IntPtr.Zero, 0, buffer, sizeof(int), out bytesReturned, IntPtr.Zero);
            int sessionId = Marshal.ReadInt32(buffer);

            //result = DeviceIoControl(_hdev, CTL_CODE(0x00000033, 0x0400, 0, 1), IntPtr.Zero, 0, (IntPtr)sessionId, Marshal.SizeOf(sessionId), out bytesReturned, IntPtr.Zero);
            //result = DeviceIoControl(_hdev, CTL_CODE(0x00000033, 0x0400, 0, 1), IntPtr.Zero, 0, (IntPtr)sessionId, Marshal.SizeOf(sessionId), out bytesReturned, IntPtr.Zero);

            if (result == false)
            {
                int error_code = Marshal.GetLastWin32Error();
                new AgentException("SendHeartbeat failed with GetLastWin32Error: " + error_code);
                //new AgentException("error code: " + error_code);
            }
            else
            {
                //new AgentException("Result: " + result);
                //new AgentException("BytesReturned: " + bytesReturned);
                //new AgentException("SessionId: " + sessionId);
                //new AgentException("sizeof(SessionId): " + Marshal.SizeOf(sessionId));
            }
            Marshal.FreeHGlobal(buffer);
            return result;
        }


        public static int CTL_CODE(int DeviceType, int Function, int Method, int Access)
        {
            return (((DeviceType) << 16) | ((Access) << 14) | ((Function) << 2)
              | (Method));
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);
        public static SafeFileHandle CreateFileR(string device)
        {
            string str = device.EndsWith(@"\") ? device.Substring(0, device.Length - 1) : device;
            var retval = new SafeFileHandle(CreateFile(@"\\.\" + str,
                WinntConst.GENERIC_READ | WinntConst.GENERIC_WRITE,
                WinntConst.FILE_SHARE_READ | WinntConst.FILE_SHARE_WRITE,
                IntPtr.Zero,
                WinntConst.OPEN_EXISTING,
                0,
                IntPtr.Zero
                ),
                true);
            //int error_code = Marshal.GetLastWin32Error();
            //new AgentException("DEBUG: SendHeartbeat failed with GetLastWin32Error: " + error_code);
            return retval;
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool DeviceIoControl([In] SafeFileHandle hDevice, [In] int dwIoControlCode, [In] IntPtr lpInBuffer, [In] int nInBufferSize, [Out] IntPtr lpOutBuffer, [In] int nOutBufferSize, out int lpBytesReturned, [In] IntPtr lpOverlapped);

        internal class WinntConst
        {
            // Fields
            internal static uint FILE_ATTRIBUTE_NORMAL = 0x80;
            internal static uint FILE_SHARE_READ = 1;
            internal static uint FILE_SHARE_WRITE = 0x00000002;
            internal static uint GENERIC_READ = 0x80000000;
            internal static uint GENERIC_WRITE = 0x40000000;
            internal static uint OPEN_EXISTING = 3;
            internal static uint FILE_ANY_ACCESS = 0;
        }

        [Flags]
        public enum EIOControlCode : uint
        {
            // QzjNDIS
            IOCTL_FILTER_SET_NETWORK_STATUS_CONFIG = 0xFF01,
            IOCTL_FILTER_SET_NETWORK_BLOCKALL = 0xFF02,
            IOCTL_FILTER_SET_NETWORK_ALLOWALL = 0xFF03,
            IOCTL_FILTER_SET_NETWORK_DEFAULT = 0xFF04
        };

        [Flags]
        public enum EFileDevice : uint
        {
            FILE_DEVICE_PHYSICAL_NETCARD = 0x00000017,
        }

        [Flags]
        public enum EMethod : uint
        {
            Buffered = 0,
            InDirect = 1,
            OutDirect = 2,
            Neither = 3
        }

        [DllImport("Kernel32.dll", EntryPoint = "DeviceIoControl", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool DeviceIoControlAlt(
            Microsoft.Win32.SafeHandles.SafeFileHandle hDevice,
            EIOControlCode IoControlCode,
            [MarshalAs(UnmanagedType.AsAny)][In] object InBuffer,
            uint nInBufferSize,
            [MarshalAs(UnmanagedType.AsAny)][Out] object OutBuffer,
            uint nOutBufferSize,
            ref uint pBytesReturned,
            [In] ref System.Threading.NativeOverlapped Overlapped
        );
    }
}

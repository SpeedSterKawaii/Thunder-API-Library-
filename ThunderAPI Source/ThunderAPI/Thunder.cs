using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThunderAPI
{
    public class Thunder
    {
        public void Inject() 
        {
            if (NamedPipeExist(PipeName)) 
            {
                MessageBox.Show("Thunder is Already Injected into Roblox.", "Already Injected.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else 
            {
                if (File.Exists(DLLName)) 
                {
                    if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 1) 
                    {
                        try 
                        {
                            this.InitThunderDLL();
                        }
                        catch (Exception x) 
                        {
                            MessageBox.Show("Caught Thread Error While Injecting: " + x.Message.ToString(), "DLL Injection Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else 
                    {
                        Update();
                    }
                }
                else 
                {
                    Update();
                }
            }
        }

        public void Execute(string script) 
        {
            if (NamedPipeExist(PipeName)) 
            {
                new Thread(delegate ()
                {
                    try
                    {
                        using (NamedPipeClientStream VanillaPipeStream = new NamedPipeClientStream(".", "h0kuPBYmCIWP7P6jjq", PipeDirection.Out))
                        {
                            VanillaPipeStream.Connect();
                            using (StreamWriter streamWriter = new StreamWriter(VanillaPipeStream, Encoding.Default, 999999))
                            {
                                streamWriter.Write(script);
                                streamWriter.Dispose();
                            }
                            VanillaPipeStream.Dispose();
                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("A error occurred while connecting to the pipe.", "Problem accessing pipe.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("There was a problem while accessing the pipe.", "Problem accessing pipe.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }).Start();
            }
            else 
            {
                if (File.Exists(DLLName)) 
                {
                    MessageBox.Show("Please Inject before Executing Scripts.", "Please Inject.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else 
                {
                    MessageBox.Show("The DLL could not be found.", "DLL not Found.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Update() 
        {
            if (File.Exists(DLLName))
            {
                if (NamedPipeExist(PipeName))
                {
                    MessageBox.Show("Please Close Roblox before Updating the DLL.", "Close Roblox.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    File.Delete(DLLName);
                    new WebClient().DownloadFile(new WebClient().DownloadString(LinkName), DLLName);
                }
            }
            else
            {
                new WebClient().DownloadFile(new WebClient().DownloadString(LinkName), DLLName);
            }
        }

        public bool IsAPIInjected() 
        {
            return NamedPipeExist(PipeName);
        }

        public bool IsRBXLaunched()
        {
            return Process.GetProcessesByName("RobloxPlayerBeta").Length == 1;
        }

        public bool DoesDLLExists()
        {
            return File.Exists(DLLName);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WaitNamedPipe(string name, int timeout);

        public static string PipeName = "h0kuPBYmCIWP7P6jjq";

        public static string LinkName = "https://pastebin.com/raw/KAvw43pq";

        public static string DLLName = "ThunderInjector.dll";

        public static bool NamedPipeExist(string pipe)
        {
            bool result;
            try
            {
                int timeout = 0;
                if (!WaitNamedPipe(Path.GetFullPath(string.Format("\\\\.\\pipe\\{0}", pipe)), timeout))
                {
                    int lastWin32Error = Marshal.GetLastWin32Error();
                    if (lastWin32Error == 0)
                    {
                        return false;
                    }
                    if (lastWin32Error == 2)
                    {
                        return false;
                    }
                }
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
        internal static extern IntPtr LoadLibraryA(string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern UIntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr OpenProcess(ProcessAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, UIntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        public bool InitThunderDLL()
        {
            if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
            {
                return false;
            }
            Process process = Process.GetProcessesByName("RobloxPlayerBeta")[0];
            byte[] bytes = new ASCIIEncoding().GetBytes(AppDomain.CurrentDomain.BaseDirectory + DLLName);
            IntPtr hModule = LoadLibraryA("kernel32.dll");
            UIntPtr procAddress = GetProcAddress(hModule, "LoadLibraryA");
            FreeLibrary(hModule);
            if (procAddress == UIntPtr.Zero)
            {
                return false;
            }
            IntPtr intPtr = OpenProcess(ProcessAccess.AllAccess, false, process.Id);
            if (intPtr == IntPtr.Zero)
            {
                return false;
            }
            IntPtr intPtr2 = VirtualAllocEx(intPtr, (IntPtr)0, (uint)bytes.Length, 12288u, 4u);
            UIntPtr uintPtr;
            IntPtr intPtr3;
            return !(intPtr2 == IntPtr.Zero) && WriteProcessMemory(intPtr, intPtr2, bytes, (uint)bytes.Length, out uintPtr) && !(CreateRemoteThread(intPtr, (IntPtr)0, 0u, procAddress, intPtr2, 0u, out intPtr3) == IntPtr.Zero);
        }

        [Flags]
        public enum ProcessAccess
        {
            AllAccess = 1050235,
            CreateThread = 2,
            DuplicateHandle = 64,
            QueryInformation = 1024,
            SetInformation = 512,
            Terminate = 1,
            VMOperation = 8,
            VMRead = 16,
            VMWrite = 32,
            Synchronize = 1048576
        }
    }
}
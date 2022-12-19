using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace self_updater
{
    static class Program
    {
        static string TargetName = "";
        static string SourceName = "";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Args.Length == 2)
            {
                SourceName = Args[0];
                TargetName = Args[1];


                Console.WriteLine("Killing process " + TargetName);
                KillProcess(TargetName);
                System.Threading.Thread.Sleep(1000);
                try
                {
                    System.IO.File.Copy(SourceName, TargetName, true);
                    Console.WriteLine("Target file " + TargetName + " was replaced by " + SourceName);
                }
                catch (Exception ee)
                {
                    Console.WriteLine("Failed to copy file after 1s");
                    Console.WriteLine(ee.Message);
                    Console.WriteLine(ee.StackTrace);
                    System.Threading.Thread.Sleep(10000);
                    try
                    {
                        System.IO.File.Copy(SourceName, TargetName, true);
                        Console.WriteLine("Target file " + TargetName + " was replaced by " + SourceName);
                    }
                    catch (Exception eee)
                    {
                        Console.WriteLine("Failed to copy file after 10s");
                        Console.WriteLine(eee.Message);
                        Console.WriteLine(eee.StackTrace);
                    }

                }
                Application.Run(new Form1());
            }else
            {
                Console.WriteLine("Usage: app_updater.exe [sourcefile] [targetfile]");
            }

        }
        public static void KillProcess(string filepath)
        {
            System.Diagnostics.Process[] myprocesses;
            myprocesses = System.Diagnostics.Process.GetProcesses(); //获取当前启动的所有进程
            for (int i = 0; i < myprocesses.Length; i++)
            {
                Process p = myprocesses[i];
            }
            var wmiQueryString = "SELECT ProcessId, ExecutablePath, CommandLine FROM Win32_Process";
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            using (var results = searcher.Get())
            {
                var query = from p in Process.GetProcesses()
                            join mo in results.Cast<ManagementObject>()
                            on p.Id equals (int)(uint)mo["ProcessId"]
                            select new
                            {
                                Process = p,
                                Path = (string)mo["ExecutablePath"],
                                CommandLine = (string)mo["CommandLine"],
                            };
                foreach (var item in query)
                {
                    //执行路径不区分大小写
                    if (item.Path != null && item.Path.ToLower() == filepath.ToLower())
                    {
                        //Log("Shutdowning running app");
                        item.Process.Kill();
                    }
                }
            }
        }
    }
}

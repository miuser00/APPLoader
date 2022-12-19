using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;       //进程
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoUpdater.lib
{
    /// <summary>
    /// 通过程序名称、版本号和路径检查程序是否已运行
    /// </summary>
    class ClassCheckProIsRun
    {
        /// 该函数设置由不同线程产生的窗口的显示状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary>
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private const int SW_SHOWNOMAL = 1;

        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWNOMAL);   //show已运行的更新程序
            SetForegroundWindow(instance.MainWindowHandle);     //窗口置最前
        }

        /// <summary>
        /// 检测进程中程序是否已运行,返回运行状态
        /// </summary>
        /// <param name="strPath">程序完整路径</param>
        /// <returns>运行状态</returns>
        public bool checkProcess(string strPath)
        {
            ////获取版本号
            string strVersionNumber = Application.ProductVersion;

            string strProName = strPath.Substring(strPath.LastIndexOf('\\') + 1, (strPath.LastIndexOf('.') - 1) - strPath.LastIndexOf('\\'));  //获取不带扩展名的程序名称

            bool isRun = false;      //更新程序是否已运行，默认未运行
            Process[] processes = Process.GetProcessesByName(strProName);   //同程序名的所有进程
            foreach (Process p in processes)//判断当前进程中是否已有该程序
            {
                if (p.MainModule.FileName.ToString() == strPath)//通过程序路径判断，而不能通过程序名判断
                {
                    string strVN = p.MainModule.FileVersionInfo.FileVersion;    //获取进程中正在运行的这个程序的版本号，留作判断版本时用
                    isRun = true;   //更新程序已运行
                    HandleRunningInstance(p);   //show已运行的更新程序，并置顶
                    break;
                }
            }
            return isRun;
        }

        /// <summary>
        /// 按程序名检查进程
        /// </summary>
        /// <param name="strProName">带扩展名的程序名</param>
        /// <returns>是否存在</returns>
        public bool checkProcessForProName(string strProName)
        {
            bool isRun = false;      //更新程序是否已运行，默认未运行
            Process[] processes = Process.GetProcessesByName(strProName.Substring(0, strProName.LastIndexOf('.')));   //同程序名的所有进程
            foreach (Process p in processes)//判断当前进程中是否已有该程序
            {
                if (p.MainModule.ModuleName == strProName)//通过程序路径判断，而不能通过程序名判断
                {
                    string strVN = p.MainModule.FileVersionInfo.FileVersion;    //获取进程中正在运行的这个程序的版本号
                    isRun = true;   //更新程序已运行
                    break;
                }
            }
            return isRun;
        }
    }
}

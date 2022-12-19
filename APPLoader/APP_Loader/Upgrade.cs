using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AppLoader
{
    public partial class UpgradeForm : Form
    {

        public UpgradeForm()
        {
            InitializeComponent();
        }
        private void UpgradeForm_Shown(object sender, EventArgs e)
        {

        }
        private void UpgradeForm_Load(object sender, EventArgs e)
        {
            panel1.Dock = DockStyle.Fill;

            if (G.loader_cfg.Debug == false)
            {
                this.Height = 65;
                for (int i = 0; i < 100; i++)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                }
                btn_run_Click(sender, e);
            }

        }
        private void btn_UpgradeAppFolder_Click(object sender, EventArgs e)
        {

            if (File.Exists(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\zipped\\app\\AppLoader.exe"))
            {
                Log("Detected loader binary file.");
                Log("Move loader file to Bin32 Directory");
                if (!Directory.Exists(Application.StartupPath + "\\Bin32")) Directory.CreateDirectory(Application.StartupPath + "\\Bin32");
                File.Copy(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\zipped\\app\\AppLoader.exe", Application.StartupPath + "\\Bin32\\AppLoader.bin",true);
                File.Delete(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\zipped\\app\\AppLoader.exe");
            }

            Log("Upgrading app folder.");
            G.CopyDirectory(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\zipped\\app", Application.StartupPath, true);
            Log("Upgrading done");
            Log("Restore user setting.");
            try 
            {
                G.CopyDirectory(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\skipped", Application.StartupPath, true);
                Log("User setting restored");
            }catch
            {
                Log("No user setting need to be restored.");
            }
        } 

        private void btn_ClearUp_Click(object sender, EventArgs e)
        {
            //清理安装
            Log("Removing folder \"skipped\"");
            if (Directory.Exists(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\skipped"))
                Directory.Delete(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\skipped", true);
            else Log("Folder \"skipped\" wasn't existing");
            Log("Removing folder \"zipped\"");
            if (Directory.Exists(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\zipped"))
                Directory.Delete(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\zipped", true);
            else Log("Folder \"zipped\" wasn't existing");
            Log("All was cleaned");
        }
        //启动更新程序
        private void btn_run_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
        private void btn_RunMain_Click(object sender, EventArgs e)
        {
            Log("Startup app");
            if (File.Exists(Application.StartupPath + "\\" + G.loader_cfg.AppFile))
            {
                string s_args = "";
                foreach (string arg in G.args)
                {
                    s_args = s_args + " " + arg;
                }
                ProcessStartInfo info = new ProcessStartInfo(Application.StartupPath + "\\" + G.loader_cfg.AppFile, s_args);
                Process.Start(info);

            }
            else
            {
                //删除标志文件跟配置文件，重启重新下载完整程序
                if (File.Exists(Application.StartupPath + "\\uppack\\upgrade.xml")) File.Delete(Application.StartupPath + "\\uppack\\upgrade.xml");
                if (File.Exists(Application.StartupPath + "\\uppack\\ready.flag")) File.Delete(Application.StartupPath + "\\uppack\\ready.flag");
                Log("升级失败，请检查网络，程序将重新尝试下载更新");
                //重启
                Application.Restart();
            }
        }


        private void btn_upself_Click(object sender, EventArgs e)
        {
            string app = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            //MessageBox.Show(app, "路径");

            //运行此函数后，程序必须马上退出让self_updater回写
            ProcessStartInfo pro = new ProcessStartInfo();
            pro.WindowStyle = ProcessWindowStyle.Hidden;
            pro.FileName = Application.StartupPath + "\\bin32\\self_updater.exe";
            pro.Arguments ="\"" +Application.StartupPath + "\\bin32\\AppLoader.bin"+ "\"" + " "+ "\""+app+ "\"";
            //MessageBox.Show(pro.Arguments, "参数");
            try
            {
                Process.Start(pro);
            }catch(Exception ee)
            {
                Log_Local(ee.StackTrace);
            }

        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            //退出
            Environment.Exit(0);
        }

        private void btn_KissProcess_Click(object sender, EventArgs e)
        {
            KillProcess(Application.StartupPath + "\\" + G.loader_cfg.AppFile);
        }

        private void btn_Clearflag_Click(object sender, EventArgs e)
        {
            //调试模式下，旗标不会被删除，这里补漏
            if (File.Exists(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\ready.flag")) File.Delete(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\ready.flag");
        }
        /// <summary>
        /// 终止指定路径运行中的程序
        /// </summary>
        /// <param name="filepath">要终止的程序的运行路径</param>
        public void KillProcess(string filepath)
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
                        Log("Shutdowning running app");
                        item.Process.Kill();
                    }
                }
            }
        }
        /// <summary>
        /// 在窗口中写Log
        /// </summary>
        /// <param name="log"></param>
        /// <param name="AddingNewLine"></param>
        public void Log(string log, bool AddingNewLine = true)
        {
            this.Invoke(new Action(delegate
            {
                if (AddingNewLine)
                {
                    rtb_log.AppendText(DateTime.Now.ToString("[HH:mm:ss.fff]") + log + "\r\n");
                }
                else
                {
                    int currlineNo = rtb_log.GetLineFromCharIndex(rtb_log.SelectionStart - 1);
                    rtb_log.SelectionStart = rtb_log.GetFirstCharIndexFromLine(currlineNo);
                    rtb_log.SelectionLength = this.rtb_log.Lines[currlineNo].Length + 1;
                    rtb_log.SelectedText = String.Empty;
                    rtb_log.AppendText(DateTime.Now.ToString("[HH:mm:ss.fff]") + log + "\r\n");
                }
                rtb_log.SelectionStart = rtb_log.TextLength;
                rtb_log.ScrollToCaret();
            }));
        }
        /// <summary>
        /// 在本地文件中记录Log
        /// </summary>
        /// <param name="log"></param>
        public void Log_Local(string log,bool isError=true)
        {
            try
            {
                Directory.CreateDirectory(Application.StartupPath + "\\log");
                if (isError)
                {
                    StreamWriter sw = new StreamWriter(Application.StartupPath + "\\log\\error.log", true);
                    sw.Write(DateTime.Now.ToString("[HH:mm:ss.fff]") + log + "\r\n");
                    sw.Flush();
                    sw.Close();
                }else
                {
                    StreamWriter sw = new StreamWriter(Application.StartupPath + "\\log\\run.log", true);
                    sw.Write(DateTime.Now.ToString("[HH:mm:ss.fff]") + log + "\r\n");
                    sw.Flush();
                    sw.Close();
                }
            }
            catch { }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //清除旗标
                btn_Clearflag_Click(sender, e);
                //杀进程
                //清除旗标
                btn_KissProcess_Click(sender, e);
                //更新APP
                btn_UpgradeAppFolder_Click(sender, e);
                //删除解压缩的临时目录
                btn_ClearUp_Click(sender, e);
                //启动主程序
                btn_RunMain_Click(sender, e);
                //更新Loader程序
                btn_upself_Click(sender, e);
                //如果非调试模式更新后退出
                if (!G.loader_cfg.Debug) btn_Exit_Click(sender, e);
            }
            catch (Exception ee)
            {
                //记录错误
                Log_Local(ee.StackTrace);
                //删除原有版本信息，这样重新启动后会自动启动下载
                if (File.Exists(Application.StartupPath + "\\uppack\\upgrade.xml"))
                    File.Delete(Application.StartupPath + "\\uppack\\upgrade.xml");

                Log("Removed upgrade setting.");
                Log("Will restart download while next startup");
                //如果非调试模式出错后退出
                if (!G.loader_cfg.Debug) btn_Exit_Click(sender, e);
            }
        }
    }
}



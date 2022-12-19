using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AppLoader
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            //保存Loader加载时使用的参数，用来传递到实际的外部主程序
            G.args = Args;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //从资源中释放程序运行需要的外部程序
            G.ExtractResFiles();
            //加载配置
            if (File.Exists(Application.StartupPath + "\\config.xml"))
            {
                G.loader_cfg = BaseCfg.LoadExternalCfg(Application.StartupPath + "\\config.xml");

            }else
            {
                G.loader_cfg = BaseCfg.LoadBuildinCfg();
            }
            //如果还没有下载安装目录，则尝试从自身资源文件中解压相应文件
            if (G.CheckNewInstallation())
            {
                //解压缩所有程序文件
                try
                {
                    G.ExtractProgramFiles();
                    //建立要求软件立即更新的旗标文件
                    if (!Directory.Exists(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder)) Directory.CreateDirectory(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder);
                    StreamWriter sw = new StreamWriter(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\" + "ready.flag");
                    sw.WriteLine("");
                    sw.Flush();
                    sw.Close();
                }
                catch 
                {
                    Console.WriteLine("程序中不包含程序压缩包");
                }


            }
            //检查是否有准备好待更新的软件包
            if (G.CheckNewUpdateAvailable() == true)
            {
                //启动更新窗体
                Application.Run(new UpgradeForm());
            }else
            {
                //启动下载窗体
                Application.Run(new DownloadForm());
            }
        }


    }

}

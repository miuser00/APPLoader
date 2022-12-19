using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OTAPackageGenerate
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    //本Loader应用的运行设置
    public class BaseCfg
    {
        //服务端应用配置的Url地址
        public string AppVerXmlUrl { get; set; }
        //本地升级文件使用的工作目录
        public string UpgradeFolder { get; set; }
        //启动后要调用的外部主程序
        public string AppFile { get; set; }
        //是否启动调试模式
        public bool Debug { get; set; }
        //下载窗口的标题
        public string AppTitle { get; set; }

        //从外部文件读取loader的配置
        public static BaseCfg LoadExternalCfg(string filename)
        {
            StreamReader sr = new StreamReader(filename);

            XmlSerializer serializer = new XmlSerializer(typeof(BaseCfg));
            BaseCfg cfg = (BaseCfg)(serializer.Deserialize(sr));
            sr.Close();
            return cfg;
        }
    }
}

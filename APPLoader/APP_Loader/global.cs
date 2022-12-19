using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AppLoader
{
    /// <summary>
    /// 全局变量和全局函数保存在这个对象里
    /// </summary>
    public class G
    {
        public static string[] args;
        //app资源文件中的设置
        public static BaseCfg loader_cfg;
        //检查是否有等待更新的固件
        public static bool CheckNewUpdateAvailable()
        {
            //检查升级目录是否存在标识文件"ready_upgrade.txt",如果是则立即升级
            if (File.Exists(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\ready.flag"))
            {
                if (G.loader_cfg.Debug != true)
                {
                    File.Delete(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\ready.flag");
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 如果是新安装则不存在下载目录，返回真
        /// 如果已经下载安装过了，就返回假
        /// </summary>
        /// <returns></returns>
        public static bool CheckNewInstallation()
        {
            return (!Directory.Exists(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder));
        }

        //从资源中释放程序运行需要的外部程序
        public static void ExtractResFiles()
        {
            Stream s1 = G.GetResource("BinRes.7za.exe");
            if (!Directory.Exists(Application.StartupPath + "\\Bin32")) Directory.CreateDirectory(Application.StartupPath + "\\Bin32");
            G.StreamToFile(s1, Application.StartupPath + "\\Bin32\\7za.exe");
            s1.Close();

            Stream s2 = G.GetResource("BinRes.self_updater.exe");
            if (!Directory.Exists(Application.StartupPath + "\\Bin32")) Directory.CreateDirectory(Application.StartupPath + "\\Bin32");
            G.StreamToFile(s2, Application.StartupPath + "\\Bin32\\self_updater.exe");
            s1.Close();
        }
        //从资源文件中释放打包的源程序
        public static void ExtractProgramFiles()
        {
            //建立下载目录
            if (!Directory.Exists(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\download"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\download");
            }
            List<string> fileres = G.GetSubResources("BinApp");
            foreach (string rs in fileres)
            {
                string filename = rs.Replace(G.GetNameSpace() + "." + "BinApp" + ".", "");
                Stream s = G.GetResource(rs.Replace(G.GetNameSpace() + ".", ""));
                G.StreamToFile(s, Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\download\\" + filename);
                s.Close();
            }
            //解压缩文件
            XmlSerializer serializer = new XmlSerializer(typeof(Upgrade_XML_Info));
            StreamReader sr = new StreamReader(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\download\\" + System.IO.Path.GetFileName(G.loader_cfg.AppVerXmlUrl));
            Upgrade_XML_Info server_info = (Upgrade_XML_Info)(serializer.Deserialize(sr));
            sr.Close();
            ExtractFile(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\download\\" + System.IO.Path.GetFileName(server_info.PackageUrl), Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\zipped");

        }        /// <summary>
                 /// 解压缩文件
                 /// </summary>
                 /// <param name="source">源文件</param>
                 /// <param name="destination">目标目录</param>
        public static void ExtractFile(string source, string destination)
        {
            try
            {
                ProcessStartInfo pro = new ProcessStartInfo();
                pro.WindowStyle = ProcessWindowStyle.Hidden;
                pro.FileName = Application.StartupPath + "\\bin32\\7za.exe";
                pro.Arguments = "x \"" + source + "\" -o" + "\""+ destination + "\"" + " -aoa";
                Process x = Process.Start(pro);
                x.WaitForExit();
            }
            catch (System.Exception Ex)
            {
                if (G.loader_cfg.Debug == false)
                {
                    Environment.Exit(0);
                }
            }
        }
        /// <summary>
        /// 从项目嵌入的资源中读取指定的资源文件。
        /// </summary>
        /// <param name="name">指定的资源文件名称</param>
        /// <returns>返回的资源文件流</returns>
        public static Stream GetResource(string name)
        {
            //获得正在运行类所在的名称空间          
            string _namespace = MethodBase.GetCurrentMethod().DeclaringType.Namespace;

            //获得当前运行的Assembly 方法1  
            Assembly _assembly = Assembly.GetExecutingAssembly();
            //获得当前运行的Assembly 方法2
            //Assembly _assembly1 = this.GetType().Assembly;

            //根据名称空间和文件名生成资源名称
            var curNamespace = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType;
            string resourceName = curNamespace.Namespace + "." + name;
            Debug.Print(curNamespace.Namespace + ":" + resourceName);
            //根据资源名称从Assembly中获取此资源的Stream
            Stream istr = _assembly.GetManifestResourceStream(resourceName);
            return istr;
        }

        public static string GetNameSpace()
        {
            var curNamespace = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType;
            return curNamespace.Namespace;
        }
        /// <summary>
        /// 获取制定资源路径下的包含的资源文件列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<string> GetSubResources(string name)
        {
            List<string> resouces = new List<string>();
            //获得当前运行的Assembly 方法1  
            Assembly _assembly = Assembly.GetExecutingAssembly();
            //根据名称空间和文件名生成资源名称
            var curNamespace = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType;
            // Enumerate the assembly's manifest resources
            foreach (string rsName in _assembly.GetManifestResourceNames())
            {
                if (rsName.StartsWith(curNamespace.Namespace + "." + name))
                { 
                    resouces.Add(rsName); 
                }


            }
            return resouces;
        }
        /// <summary>
        /// 将 Stream 写入文件
        /// </summary>
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);

            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }
        /// <summary>
        /// 读取软件资源文件内容
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>文件内容字符串</returns>
        public static string GetAssetsFileContent(string path)
        {
            path = path.Replace("/", ".");
            var source = G.GetResource(path);
            byte[] f = new byte[source.Length];
            source.Read(f, 0, (int)source.Length);
            return Encoding.UTF8.GetString(f);
        }
        /// <summary>
        /// 复制目录，recursive决定是否复制下面的子目录
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="destinationDir"></param>
        /// <param name="recursive"></param>
        /// <returns>拷贝文件过程是否完全成功，true完全成功，false发生了至少一处错误</returns>
        public static bool CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            bool fullsuccess = true;
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            try
            {
                // Create the destination directory if not existed
                if (!Directory.Exists(destinationDir)) Directory.CreateDirectory(destinationDir);
            }
            catch 
            {
                fullsuccess = false;
            }

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                try
                {
                    string targetFilePath = Path.Combine(destinationDir, file.Name);
                    file.CopyTo(targetFilePath, true);
                }
                catch
                {
                    fullsuccess = false;
                }
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    try
                    {
                        string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                        CopyDirectory(subDir.FullName, newDestinationDir, true);
                    }
                    catch
                    {
                        fullsuccess = false;
                    }
                }
            }
            return fullsuccess;
        }
        /// <summary>
        /// 计算文件的MD5哈希值
        /// </summary>

        public static string GetMD5HashFromFile(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();
            String hex = BytesToHex(retVal);
            return hex;
        }
        /// <summary>
        /// 字节数组装HEX字符串
        /// </summary>
        private static string BytesToHex(byte[] bytes)
        {
            string result = "";
            foreach (char c in bytes)
            {
                result += Convert.ToString(c, 16);
            }
            return result.ToUpper();
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
        //从本低内置的配置文件读取loader的配置
        public static BaseCfg LoadBuildinCfg()
        {
            //读取配置信息
            try
            {

                XmlSerializer serializer = new XmlSerializer(typeof(BaseCfg));
                return (BaseCfg)(serializer.Deserialize(G.GetResource("config.config.xml")));
            }
            catch
            {
                return null;
            }
        }
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
    //App版本配置信息
    public class Upgrade_XML_Info
    {
        //主版本号
        public float Version { get; set; }
        //版本号尾缀
        public string Version_tail { get; set; }
        //升级压缩包的Url
        public string PackageUrl { get; set; }
        //升级说明的Url
        public string UpgradeMDFileUrl { get; set; }
        //覆盖升级文件时要跳过的目录，使用相对路径比如 config
        public List<string> SkipFolder { get; set; }
        //覆盖升级文件时要跳过的目录，使用相对路径比如 config/config.toml
        public List<string> SkipFile { get; set; }
        //升级文件压缩包的MD5校验值(HEX String)
        public string MD5_Of_Package_File { get; set; }
    }
}

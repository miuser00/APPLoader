using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AppLoader
{
    public partial class DownloadForm : Form
    {
        private WebClient downWebClient;
        //本地当前的升级配置文档
        Upgrade_XML_Info local_info;
        //服务器上刚下载的配置文档
        Upgrade_XML_Info server_info;
        //是否隐藏窗体
        public bool isHide=true;
        //连续执行的标识
        public bool isRunning=false;
        //下载完成后重启本程序，用于主程序启动失败时
        public bool RestartAfterDown = false;
        //定义的一个虚拟文件，用文件名显示下载进度
        string progressFilename = "empty";
        public DownloadForm()
        {
            InitializeComponent();

            //配置程序启动状态
            this.Text = G.loader_cfg.AppTitle + " downloader";
            if (G.loader_cfg.Debug == true)
            {
                isHide = false;
                this.ShowInTaskbar = true;
            }
            //判断是否要静默下载,并设置isHide全局变量
            Determine_HideRunDownload();

        }
        /// <summary>
        /// 设置窗口隐含属性
        /// </summary>
        protected override void SetVisibleCore(bool e)
        {
            if (isHide == true)
            {
                base.SetVisibleCore(false);
                DownloadForm_Load(null, null);
            }
            else
            {
                base.SetVisibleCore(true);
                this.ShowInTaskbar = true;
            }
        }
        private void DownloadForm_Load(object sender, EventArgs e)
        {
            downWebClient = new WebClient();
            //运行Download主程序，是否为调试模式在内置的config文件中描述，如果是则不立即运行，等待手工方式执行
            if (!G.loader_cfg.Debug)
            {
                //执行下载更新流程
                btn_RunAll_Click(null, null);
            }
        }
        /// <summary>
        /// 执行全部下载流程
        /// </summary>
        private void btn_RunAll_Click(object sender, EventArgs e)
        {
            //执行App
            btn_RunExternalApp_Click(null, null);
            //做下载准备
            downWebClient = new WebClient();

            //启动升级流程
            if (!isRunning)
            {
                if (File.Exists(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\ready.flag")) File.Delete(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\ready.flag");
                //检查新版本
                btn_CheckLatestVersion_Click(sender, e);
                isRunning = true;
            }
        }
        /// <summary>
        /// 检查服务器最新版本的固件，与本地对比
        /// </summary>
        private void btn_CheckLatestVersion_Click(object sender, EventArgs e)
        {   
            //读取本地当前APP配置
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Upgrade_XML_Info));
                StreamReader sr = new StreamReader(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\" + System.IO.Path.GetFileName(G.loader_cfg.AppVerXmlUrl));
                local_info = (Upgrade_XML_Info)(serializer.Deserialize(sr));
                sr.Close();
            }
            catch
            {
                //本地不存在配置文件
                local_info = null;
                Log("Local app version file doesn't exist.");
            }
            Log("Checking upgrade server...");
            Log(" ");
            string strUpdateURL = G.loader_cfg.AppVerXmlUrl;
            //委托下载数据时事件 
            this.downWebClient.DownloadProgressChanged += delegate (object wcsender, DownloadProgressChangedEventArgs ex)
            {
                Log("Downloading progress:" + convertSize(ex.BytesReceived) + "/" + convertSize(ex.TotalBytesToReceive), false);

            };
            //委托下载完成时事件 
            this.downWebClient.DownloadFileCompleted += delegate (object wcsender, AsyncCompletedEventArgs ex)
            {
                if (ex.Error != null)
                {
                    ClearAllEvents(this, "DownloadFileCompleted");
                    ClearAllEvents(this, "DownloadProgressChanged");
                    isRunning = false;
                    Log(ex.Error.Message + "\n\r" + ex.Error.StackTrace);
                    //if (G.loader_cfg.Debug == false) Environment.Exit(0);

                }
                else
                {
                    Log("File "+System.IO.Path.GetFileName(G.loader_cfg.AppVerXmlUrl) +" download was completed.");
                    //读取刚下载的配置文件
                    XmlSerializer serializer = new XmlSerializer(typeof(Upgrade_XML_Info));
                    StreamReader sr = new StreamReader(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\download\\" + System.IO.Path.GetFileName(G.loader_cfg.AppVerXmlUrl));
                    server_info = (Upgrade_XML_Info)(serializer.Deserialize(sr));
                    sr.Close();

                    if (server_info != null)
                    {
                        Log("Server side app version is " + server_info.Version.ToString() + server_info.Version_tail);
                    }else
                    {
                        Log("Server side app version is nil");
                    }
                    if (local_info != null)
                    {
                        Log("Locally existed version is " + local_info.Version.ToString() + local_info.Version_tail);
                    }
                    else
                    {
                        Log("Local existed app version is nil");
                    }
                    if ((server_info == null)||(local_info == null) || (server_info.Version>local_info.Version))
                    {
                        Log("New upgrade package is detected");
                        if (File.Exists(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\ready.flag"))
                        {
                            File.Delete(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\ready.flag");
                            Log("Clear ready.flag");
                        }
                        ClearAllEvents(downWebClient, "DownloadFileCompleted");
                        ClearAllEvents(downWebClient, "DownloadProgressChanged");
                        btn_CheckLatestVersion.BackColor = Color.Green;
                        if (isRunning) 
                        {
                            btn_DownloadPackage_Click(sender, e); 
                        }
                    }else
                    {
                        btn_CheckLatestVersion.BackColor = Color.Green;
                        Log("It's already the latest app.");
                        isRunning = false;
                        if (G.loader_cfg.Debug == false)
                        {
                            
                            Environment.Exit(0);
                        }

                    }
                }
            };
            if (!Directory.Exists(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\download")) Directory.CreateDirectory(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\download");
            downWebClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
            downWebClient.DownloadFileAsync(new Uri(strUpdateURL), Application.StartupPath+"\\"+G.loader_cfg.UpgradeFolder+"\\download\\"+System.IO.Path.GetFileName(G.loader_cfg.AppVerXmlUrl));
        }

        /// <summary>
        /// 下载程序的安装文件包
        /// </summary>
        private void btn_DownloadPackage_Click(object sender, EventArgs e)
        {
                Log("Downloading upgrade package");
                Log(" ");
            //委托下载数据时事件 
            //this.downWebClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            this.downWebClient.DownloadProgressChanged += delegate (object wcsender, DownloadProgressChangedEventArgs ex)
                {
                    Log("Downloading progress:" + convertSize(ex.BytesReceived) + "/" + convertSize(ex.TotalBytesToReceive),false);
                    try
                    {
                        if (File.Exists(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\" + progressFilename)) File.Delete(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\" + progressFilename);
                        progressFilename = "downloadprogress." + convertSize(ex.BytesReceived) + "$" + convertSize(ex.TotalBytesToReceive);
                        StreamWriter sw = new StreamWriter(Application.StartupPath + "\\" + G.loader_cfg.UpgradeFolder + "\\" + progressFilename);
                        sw.WriteLine("");
                        sw.Flush();
                        sw.Close();
                    }
                    catch { }
                };
                //委托下载完成时事件 
                this.downWebClient.DownloadFileCompleted += delegate (object wcsender, AsyncCompletedEventArgs ex)
                {
                    if (ex.Error != null)
                    {
                        ClearAllEvents(downWebClient, "DownloadFileCompleted");
                        ClearAllEvents(downWebClient, "DownloadProgressChanged");
                        Log(ex.Error.Message+"\n\r"+ ex.Error.StackTrace);
                        isRunning = false;
                        //if (G.loader_cfg.Debug == false ) Environment.Exit(0);
                    }
                    else
                    {
                        Upgrade_XML_Info server_info = new Upgrade_XML_Info();
                        XmlSerializer serializer = new XmlSerializer(typeof(Upgrade_XML_Info));
                        server_info = (Upgrade_XML_Info)(serializer.Deserialize(new StreamReader(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\download\\" + System.IO.Path.GetFileName(G.loader_cfg.AppVerXmlUrl))));

                        Log("File " + System.IO.Path.GetFileName(server_info.PackageUrl) + " download was completed.");
                        ClearAllEvents(downWebClient, "DownloadFileCompleted");
                        ClearAllEvents(downWebClient, "DownloadProgressChanged");
                        btn_DownloadPackage.BackColor = Color.Green;
                        if (isRunning)
                        {

                            btn_VerifyFile_Click(sender, e);
                        }
                    }
                };
            downWebClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
                downWebClient.DownloadFileAsync(new Uri(server_info.PackageUrl), Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\download\\" + System.IO.Path.GetFileName(server_info.PackageUrl));
        }
        /// <summary>
        /// 校验文件是否OK
        /// </summary>
        private void btn_VerifyFile_Click(object sender, EventArgs e)
        {
            String md5_hash = G.GetMD5HashFromFile( Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\download\\" + System.IO.Path.GetFileName(server_info.PackageUrl));
            String md5_value_in_xml = server_info.MD5_Of_Package_File;
            if (md5_hash == md5_value_in_xml)
            {
                Log("Package is verified.");
                btn_VerifyFile.BackColor = Color.Green;
                if (isRunning)
                {
                    btn_Extract_Click(sender, e);
                }

            }
            else
            {
                Log("Package was damaged.");
                Log("Will restart download procedure while next bootup.");
                isRunning = false;
            }
        }
        /// <summary>
        /// 解压缩所有文件
        /// </summary>
        private void btn_Extract_Click(object sender, EventArgs e)
        {
            ExtractFile(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\download\\" + System.IO.Path.GetFileName(server_info.PackageUrl), Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\zipped");
            btn_Extract.BackColor = Color.Green;
            if (isRunning)
            {
                btn_SaveSkippedFile_Click(sender, e);
            }
        }
        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="source">源文件</param>
        /// <param name="destination">目标目录</param>
        public void ExtractFile(string source, string destination)
        {
            try
            {
                ProcessStartInfo pro = new ProcessStartInfo();
                pro.WindowStyle = ProcessWindowStyle.Hidden;
                pro.FileName = Application.StartupPath+"\\bin32\\7za.exe";
                pro.Arguments = "x \"" + source + "\" -o" + "\""+destination+"\"" + " -aoa";
                Process x = Process.Start(pro);
                x.WaitForExit();
                Log("Extracted all files");
            }
            catch (System.Exception Ex)
            {
                Log("Error occured while extrating files");
                isRunning = false;
                if (G.loader_cfg.Debug == false)
                {
                    Environment.Exit(0);
                }
            }
        }
        /// <summary>
        /// 保存需要跳过的文件目录
        /// </summary>
        private void btn_SaveSkippedFile_Click(object sender, EventArgs e)
        {
            try
            {
                //保存跳过的目录
                foreach (string sourcePath in server_info.SkipFolder)
                {
                    string fullpath = Application.StartupPath + "\\" + sourcePath;
                    if (System.IO.Directory.Exists(fullpath))
                    {
                        string sourceFolder = fullpath;
                        string destFolder = Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\skipped\\" + sourcePath;
                        G.CopyDirectory(sourceFolder, destFolder, true);
                        Log("Directory:\"" + fullpath + "\"" + " was saved.");
                    }
                }
                //保存跳过的文件
                foreach (string skipfile in server_info.SkipFile)
                {
                    System.IO.Directory.CreateDirectory(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\skipped\\" + Path.GetDirectoryName(skipfile));
                    if (System.IO.File.Exists(Application.StartupPath + "\\"+skipfile))
                    {
                        string file = Application.StartupPath + "\\" + skipfile;
                        string destFile = Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\skipped\\" + skipfile;
                        System.IO.File.Copy(file, destFile, true);
                        Log("File:\"" + file + "\"" + " was saved.");
                    }
                }
            }
            catch
            {
                Log("Error while save skipping file");
                isRunning = false;
                if (G.loader_cfg.Debug == false)
                {
                    Environment.Exit(0);
                }
            }
            btn_SaveSkippedFile.BackColor = Color.Green;
            if (isRunning)
            {
                btn_CreateOKFlag_Click(sender, e);
            }
        }
        /// <summary>
        /// 创建名为ready.flag的升级准备好的旗标文件
        /// </summary>
        private void btn_CreateOKFlag_Click(object sender, EventArgs e)
        {
            Log("Creating upgrade ready flag for upgrading");
            try
            {
                System.IO.File.Copy(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\download\\" + System.IO.Path.GetFileName(G.loader_cfg.AppVerXmlUrl), Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\" + System.IO.Path.GetFileName(G.loader_cfg.AppVerXmlUrl), true);
                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\" + "ready.flag");
                sw.WriteLine("");
                sw.Flush();
                sw.Close();
            }
            catch
            {
                Log("Error while creating ok flag");
                isRunning = false;
                if (G.loader_cfg.Debug == false)
                {
                    Environment.Exit(0);
                }
            }
            btn_CreateOKFlag.BackColor = Color.Green;
            if (isRunning)
            {
                btn_UpdateXML_Click(sender, e);
            }
        }
        /// <summary>
        /// 更新本地的版本文件
        /// </summary>
        private void btn_UpdateXML_Click(object sender, EventArgs e)
        {
            Log("Updating local app version file");
            //更新当前软件版本配置信息文件
            System.IO.File.Copy(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\download\\" + System.IO.Path.GetFileName(G.loader_cfg.AppVerXmlUrl), Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\" + System.IO.Path.GetFileName(G.loader_cfg.AppVerXmlUrl), true);
            btn_UpdateXML.BackColor = Color.Green;
            Log("All done");
            isRunning = false;
            //删除进度显示文件
            if (File.Exists(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\" + progressFilename)) File.Delete(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\" + progressFilename);
            //下载后重启，当外部主程序无法启动，需要重新下载完整的程序时发生
            if (G.loader_cfg.Debug == false && RestartAfterDown == true)
            {
                Application.Restart();
            }
            if (G.loader_cfg.Debug==false)
            {
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// 运行外部主程序
        /// </summary>
        private void btn_RunExternalApp_Click(object sender, EventArgs e)
        {
            Log("Startup app @ "+ Application.StartupPath + "\\" + G.loader_cfg.AppFile);
            if (File.Exists(Application.StartupPath + "\\" + G.loader_cfg.AppFile))
            {
                string s_args = "";
                foreach (string arg in G.args)
                {
                    s_args = s_args+ " "+ arg ;
                }
                ProcessStartInfo info = new ProcessStartInfo(Application.StartupPath + "\\" + G.loader_cfg.AppFile, s_args);
                Process.Start(info);
            }
            else
            {   
                //删除标志文件跟配置文件，重启重新下载完整程序
                if (File.Exists(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\upgrade.xml")) File.Delete(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\upgrade.xml");
                if (File.Exists(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\ready.flag")) File.Delete(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\ready.flag");
                Log("Detected incomplished program files, will download full app package to recover now.");
                if (G.loader_cfg.Debug == false)
                {
                    isHide = false;
                    panel_buttons.Visible = false;
                    this.ShowInTaskbar = true;
                }
                RestartAfterDown = true;
            }
        }
        //判断外部要调用的主程序是否存在
        public bool AppExisted()
        {
            if (File.Exists(Application.StartupPath + "\\" + G.loader_cfg.AppFile)) return true; else return false;
        }
        //根据程序是否已经正确安装，判断是否要显示下载窗体
        //如果是正常升级，是不显示下载窗体的
        //如果主程序损坏或者首次下载，则显示下载进度
        public bool Determine_HideRunDownload()
        {
            if (!AppExisted())
            {
                //删除标志文件跟配置文件，重启重新下载完整程序
                if (File.Exists(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\upgrade.xml")) File.Delete(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\upgrade.xml");
                if (File.Exists(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\ready.flag")) File.Delete(Application.StartupPath + "\\"+G.loader_cfg.UpgradeFolder+"\\ready.flag");
                Log("Detected incomplished program files, will download full app package to recover now.");
                if (G.loader_cfg.Debug == false)
                {
                    panel_buttons.Visible = false;
                }
                isHide = false;
                this.ShowInTaskbar = true;
                RestartAfterDown = true;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 清除事件的回调事件
        /// </summary>
        public static void ClearAllEvents(object objectHasEvents, string eventName)
        {
            if (objectHasEvents == null)
            {
                return;
            }
            try
            {
                EventInfo[] events = objectHasEvents.GetType().GetEvents(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (events == null || events.Length < 1)
                {
                    return;
                }
                for (int i = 0; i < events.Length; i++)
                {
                    EventInfo ei = events[i];
                    if (ei.Name == eventName)
                    {
                        FieldInfo fi = ei.DeclaringType.GetField(eventName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        if (fi != null)
                        {
                            fi.SetValue(objectHasEvents, null);
                        }
                        break;
                    }
                }
            }
            catch
            {
            }
        }
        /// <summary> 
        /// 转换字节大小 
        /// </summary> 
        private static string convertSize(long byteSize)
        {
            string str = "";
            float tempf = (float)byteSize;
            if (tempf / 1024 > 1)
            {
                if ((tempf / 1024) / 1024 > 1)
                {
                    str = ((tempf / 1024) / 1024).ToString("##0.00", CultureInfo.InvariantCulture) + "MB";
                }
                else
                {
                    str = (tempf / 1024).ToString("##0.00", CultureInfo.InvariantCulture) + "KB";
                }
            }
            else
            {
                str = tempf.ToString(CultureInfo.InvariantCulture) + "B";
            }
            return str;
        }
        /// <summary>
        /// 打印Log
        /// </summary>
        /// <param name="log"></param>
        /// <param name="AddingNewLine">是否换行</param>
        public void Log(string log, bool AddingNewLine = true)
        {
            if (this.Created)
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
                        if (currlineNo < this.rtb_log.Lines.Length)
                        {
                            rtb_log.SelectionLength = this.rtb_log.Lines[currlineNo].Length + 1;
                        }
                        else
                        {
                            rtb_log.SelectionLength = this.rtb_log.Lines[this.rtb_log.Lines.Length - 1].Length + 1;
                        }
                        rtb_log.SelectedText = DateTime.Now.ToString("[HH:mm:ss.fff]") + log + "\r\n";

                    }
                    rtb_log.SelectionStart = rtb_log.TextLength;
                    rtb_log.ScrollToCaret();
                }));
            }
            else
            {
                //try
                //{
                //    Directory.CreateDirectory(Application.StartupPath + "\\log");
                //    StreamWriter sw = new StreamWriter(Application.StartupPath+"\\log\\system.log", true);
                //    sw.Write(DateTime.Now.ToString("[HH:mm:ss.fff]") + log + "\r\n");
                //    sw.Flush();
                //    sw.Close();
                //}
                //catch { }
            }
        }

    }

}

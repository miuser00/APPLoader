using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Globalization;
using System.Xml;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoUpdater
{
    public partial class FormUpdate : Form
    {
        private WebClient downWebClient = new WebClient();

        /// <summary>
        /// 消息处理类申明
        /// </summary>
        private static AutoUpdater.lib.ClassMessage classMsg = new AutoUpdater.lib.ClassMessage();

        /// <summary>
        /// 进程检查类申明
        /// </summary>
        private static AutoUpdater.lib.ClassCheckProIsRun classCPIR = new AutoUpdater.lib.ClassCheckProIsRun();

        /// <summary>
        /// 日志类申明
        /// </summary>
        private static AutoUpdater.lib.ClassLog classLog = new AutoUpdater.lib.ClassLog();

        /// <summary>
        /// 是否有更新，默认没有更新
        /// </summary>
        private static bool boolUpdateFalg = false;

        /// <summary>
        /// 更新服务器的URL
        /// </summary>
        private static string strUpdateURL;
        /// <summary>
        /// 所有文件大小 
        /// </summary>
        private static long SIZE;
        /// <summary>
        /// 文件总数 
        /// </summary>
        private static int COUNT;
        /// <summary>
        /// 文件名数组
        /// </summary>
        private static string[] strArrayFileNames;
        /// <summary>
        /// 已更新文件数 
        /// </summary>
        private static int NUM;
        /// <summary>
        /// 已更新文件大小 
        /// </summary>
        private static long UPSIZE;
        /// <summary>
        /// 当前更新文件名 
        /// </summary>
        private static string strCurFileName;

        /// <summary>
        /// 当前更新的目录
        /// </summary>
        private static string strCurPath;

        /// <summary>
        /// 当前文件大小 
        /// </summary>
        private static long lngCurFileSize;
        /// <summary>
        /// 本地update.xml的路径 
        /// </summary>
        private static string strUpdateXmlPath = Application.StartupPath + @"\conf\update.xml";

        /// <summary>
        /// 服务端updatelist.xml的URL地址 
        /// </summary>
        private static string strUpdateListXmlPath = "UpdateServer/UpdateList.xml";
                
        /// <summary>
        /// 服务端更新文件的URL地址 
        /// </summary>
        private static string strUpdateFilesUrl = "UpdateServer/UpdateFiles";

        /// <summary>
        /// 服务端updatelist.xml的更新日期 
        /// </summary>
        private static string strTheUpdateDate;

        /// <summary>
        /// 更新文件临时保存的路径
        /// </summary>
        private static string strUpdateFilesTmpPath = Application.StartupPath + @"\UpdateFiles\";

        /// <summary>
        /// 主程序的文件名
        /// </summary>
        private static string strMainProFileName = "K3SP.exe";

        /// <summary>
        /// 主程序目录
        /// </summary>
        private static string strMainProDir = new DirectoryInfo(Application.StartupPath).Parent.FullName;

        /// <summary>
        /// 备份目录
        /// </summary>
        private static string strBackupPath = strMainProDir + @"\backup\";

        /// <summary>
        /// 主程序的路径
        /// </summary>
        private static string strMainProPath = strMainProDir + "\\" + strMainProFileName;

        public FormUpdate()
        {
            InitializeComponent();
            //430, 333
        }

        private void FormUpdate_Load(object sender, EventArgs e)
        {
            //提示关掉所有主程序
            if(classCPIR.checkProcessForProName(strMainProFileName))
            {
                classMsg.messageInfoBox("进程中检测到主程序正在运行，请先关闭才可更新。" + strMainProFileName);
                return;
            }
            checkUpdate();
        }

        /// <summary>
        /// 检测是否有更新
        /// </summary>
        private void checkUpdate()
        {
            strUpdateURL = getConfigValue(strUpdateXmlPath, "Url");     //读取本地xml中配置的更新服务器的URL
            string strLastUpdateDate = getConfigValue(strUpdateXmlPath, "UpDate");   //读取本地xml中配置的最近一次更新日期

            if (strUpdateURL.Substring(strUpdateURL.Length - 1) != "/")       //如果配置的xml中URL没带最后一个反斜杠，则加一下，防止出错
                strUpdateURL += "/";

            strTheUpdateDate = getTheLastUpdateTime(strUpdateURL);        //获得更新服务器端的此次更新日期
            if (!String.IsNullOrEmpty(strTheUpdateDate) && !String.IsNullOrEmpty(strLastUpdateDate))      //日期都不为空
            {
                if (DateTime.Compare(
                    Convert.ToDateTime(strTheUpdateDate, CultureInfo.InvariantCulture),
                    Convert.ToDateTime(strLastUpdateDate, CultureInfo.InvariantCulture)) > 0)     //字符转日期，并比较日期大小
                {
                    boolUpdateFalg = true;     //本次更新日期 大于 最近一次更新日期，有更新，修改更新标记
                }
            }
        }

        /// <summary> 
        /// 开始更新 
        /// </summary> 
        private void updaterStart()
        {
            float tempf;
            //委托下载数据时事件 
            this.downWebClient.DownloadProgressChanged += delegate(object wcsender, DownloadProgressChangedEventArgs ex)
            {
                this.labelDownload.Text = String.Format(
                    CultureInfo.InvariantCulture,
                    "正在下载:{0}  [ {1}/{2} ]",
                    strCurFileName,
                    convertSize(ex.BytesReceived),
                    convertSize(ex.TotalBytesToReceive));

                lngCurFileSize = ex.TotalBytesToReceive;
                tempf = ((float)(UPSIZE + ex.BytesReceived) / SIZE);
                this.progressBarState.Value = Convert.ToInt32(tempf * 100);
                this.progressBarDownFile.Value = ex.ProgressPercentage;
            };
            //委托下载完成时事件 
            this.downWebClient.DownloadFileCompleted += delegate(object wcsender, AsyncCompletedEventArgs ex)
            {
                if (ex.Error != null)
                {
                    classMsg.messageInfoBox(ex.Error.Message);
                    classLog.WriteLog(AutoUpdater.lib.ClassLog.LogType.Error, "Source:{" + ex.Error.Source + "}" + " StackTrace:{" + ex.Error.StackTrace + "}" + " Message:{" + ex.Error.Message + "}");
                }
                else
                {
                    //创建备份日期+更新文件目录
                    if (!Directory.Exists(strBackupPath + strTheUpdateDate + strCurPath))     //路径是否存在
                    {
                        createMultipleFolders(strBackupPath, strTheUpdateDate + strCurPath);      //创建多层目录
                    }
                    try
                    {
                        //备份
                        File.Move(strMainProDir + strCurFileName, strBackupPath + strTheUpdateDate + strCurFileName);
                        //将更新文件从临时存放路径移动到正式路径中
                        File.Move(Application.StartupPath + @"\UpdateFiles\" + strCurFileName, strMainProDir + strCurFileName);
                    }
                    catch (IOException ioex)
                    {
                        classMsg.messageInfoBox(ioex.Message + strCurFileName);      //已更新过，再更新报这个文件已存在；或者未备份成功的话更新会出故障
                        classLog.WriteLog(AutoUpdater.lib.ClassLog.LogType.Error, "Source:{" + ioex.Source + "}" + " StackTrace:{" + ioex.StackTrace + "}" + " Message:{" + ioex.Message + "}");
                    }
                    UPSIZE += lngCurFileSize;
                    if (strArrayFileNames.Length > NUM)
                    {
                        downloadFile(NUM);
                    }
                    else
                    {
                        setConfigValue(strUpdateXmlPath, "UpDate", strTheUpdateDate);
                        classMsg.messageInfoBox("更新完成！");
                        updaterClose();
                    }
                }
            };

            SIZE = getUpdateSize(strUpdateURL + strUpdateListXmlPath);
            if (SIZE == 0)  //判断要更新的文件是否有大小，如果为0字节则结束更新
                updaterClose();
            NUM = 0;
            UPSIZE = 0;
            updateList();
            if (strArrayFileNames != null)
                downloadFile(0);
        }

        /// <summary> 
        /// 获取更新文件大小统计 
        /// </summary> 
        /// <param name="filePath">更新文件数据XML</param> 
        /// <returns>返回值</returns> 
        private static long getUpdateSize(string filePath)
        {
            long len = 0;
            try
            {
                WebClient wc = new WebClient();
                Stream sm = wc.OpenRead(filePath);
                XmlTextReader xr = new XmlTextReader(sm);
                while (xr.Read())
                {
                    if (xr.Name == "UpdateSize")
                    {
                        len = Convert.ToInt64(xr.GetAttribute("Size"), CultureInfo.InvariantCulture);
                        break;
                    }
                }
                xr.Close();
                sm.Close();
            }
            catch (WebException ex)
            {
                classMsg.messageInfoBox(ex.Message);
                classLog.WriteLog(AutoUpdater.lib.ClassLog.LogType.Error, "Source:{" + ex.Source + "}" + " StackTrace:{" + ex.StackTrace + "}" + " Message:{" + ex.Message + "}");
            }
            return len;
        }

        /// <summary> 
        /// 获取文件列表并赋给全局变量
        /// </summary> 
        private static void updateList()
        {
            string xmlPath = strUpdateURL + strUpdateListXmlPath;
            WebClient wc = new WebClient();
            DataSet ds = new DataSet();
            ds.Locale = CultureInfo.InvariantCulture;

            try
            {
                Stream sm = wc.OpenRead(xmlPath);
                ds.ReadXml(sm);
                DataTable dt = ds.Tables["UpdateFile"];     //直接取UpdateFile的table
                StringBuilder sb = new StringBuilder();
                COUNT = dt.Rows.Count;                      //UpdateFile的节点数
                for (int i = 0; i < dt.Rows.Count; i++)     //遍历所有叫UpdateFile的节点
                {
                    if (i == 0)
                    {
                        sb.Append(dt.Rows[i][0].ToString());            //每行第一列
                    }
                    else
                    {
                        sb.Append("," + dt.Rows[i][0].ToString());
                    }
                }
                strArrayFileNames = sb.ToString().Split(',');       //赋给全局变量
                sm.Close();
            }
            catch (WebException ex)
            {
                classMsg.messageInfoBox(ex.Message);
                classLog.WriteLog(AutoUpdater.lib.ClassLog.LogType.Error, "Source:{" + ex.Source + "}" + " StackTrace:{" + ex.StackTrace + "}" + " Message:{" + ex.Message + "}");
            }
        }

        /// <summary> 
        /// 下载文件 
        /// </summary> 
        /// <param name="arry">下载序号</param> 
        private void downloadFile(int arry)
        {
            try
            {
                NUM++;      //全局变量来记录下载的文件序号
                strCurFileName = strArrayFileNames[arry];
                string strFileName = strCurFileName.Substring(strCurFileName.LastIndexOf("\\") + 1);        //截取文件名
                strCurPath = strCurFileName.Substring(0, strCurFileName.LastIndexOf("\\") + 1);        //截取文件路径
                if (!Directory.Exists(strUpdateFilesTmpPath + strCurPath))     //路径是否存在
                {
                    createMultipleFolders(strUpdateFilesTmpPath, strCurPath);      //创建多层目录
                }

                this.labelState.Text = String.Format(
                    CultureInfo.InvariantCulture,
                    "更新进度 {0}/{1}  [ {2} ]",
                    NUM,
                    COUNT,
                    convertSize(SIZE));

                this.progressBarDownFile.Value = 0;
                this.downWebClient.DownloadFileAsync(new Uri(strUpdateURL + strUpdateFilesUrl + strCurFileName.Replace('\\', '/')), strUpdateFilesTmpPath + strCurFileName);
            }
            catch (WebException ex)
            {
                classMsg.messageInfoBox(ex.Message);
                classLog.WriteLog(AutoUpdater.lib.ClassLog.LogType.Error, "Source:{" + ex.Source + "}" + " StackTrace:{" + ex.StackTrace + "}" + " Message:{" + ex.Message + "}");
            }
        }
        
        /// <summary>
        /// 创建多层文件夹
        /// </summary>
        /// <param name="strDialogPath">弹出文件浏览窗口选中的符文件路径</param>
        /// <param name="strPath">需要创建的多层文件夹路径</param>
        private void createMultipleFolders(string strParentPath, string strPath)
        {
            try
            {
                if (strParentPath != "")
                {
                    Directory.CreateDirectory(strParentPath + @strPath);
                }
            }
            catch (IOException ex)
            {
                classMsg.messageInfoBox("路径存在非法字符" + ex.Message);
                classLog.WriteLog(AutoUpdater.lib.ClassLog.LogType.Error, "Source:{" + ex.Source + "}" + " StackTrace:{" + ex.StackTrace + "}" + " Message:{" + ex.Message + "}");
            }
        }

        /// <summary> 
        /// 转换字节大小 
        /// </summary> 
        /// <param name="byteSize">输入字节数</param> 
        /// <returns>返回值</returns> 
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
        /// 关闭程序 
        /// </summary> 
        private static void updaterClose()
        {
            try
            {
                if (classCPIR.checkProcess(strMainProPath))
                {
                    classMsg.messageInfoBox("更新程序" + strMainProFileName + "已打开！");
                }
                else
                {
                    Process.Start(strMainProPath);
                }
            }
            catch (Win32Exception ex)
            {
                classMsg.messageInfoBox(ex.Message);      //主程序未更新成功或者被误删掉，再更新一遍
                classLog.WriteLog(AutoUpdater.lib.ClassLog.LogType.Error, "Source:{" + ex.Source + "}" + " StackTrace:{" + ex.StackTrace + "}" + " Message:{" + ex.Message + "}");
            }
            Application.Exit();         //退出程序
        }

        /// <summary> 
        /// 读取update.xml 
        /// </summary> 
        /// <param name="path">update.xml文件的路径</param> 
        /// <param name="appKey">"key"的值</param> 
        /// <returns>返回"value"的值</returns> 
        internal static string getConfigValue(string path, string appKey)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xNode;
            XmlElement xElem = null;
            try
            {
                xDoc.Load(path);

                xNode = xDoc.SelectSingleNode("//appSettings");

                xElem = (XmlElement)xNode.SelectSingleNode("//add[@key=\"" + appKey + "\"]");

            }
            catch (XmlException ex)
            {
                classMsg.messageInfoBox(ex.Message);
                classLog.WriteLog(AutoUpdater.lib.ClassLog.LogType.Error, "Source:{" + ex.Source + "}" + " StackTrace:{" + ex.StackTrace + "}" + " Message:{" + ex.Message + "}");
            }
            if (xElem != null)
                return xElem.GetAttribute("value");
            else
                return "";
        }

        /// <summary> 
        /// 程序更新完后，要更新update.xml
        /// </summary> 
        /// <param name="path">update.xml文件的路径</param> 
        /// <param name="appKey">"key"的值</param> 
        /// <param name="appValue">"value"的值</param> 
        internal static void setConfigValue(string strXmlPath, string appKey, string appValue)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(strXmlPath);

                XmlNode xNode;
                XmlElement xElem1;
                XmlElement xElem2;

                xNode = xDoc.SelectSingleNode("//appSettings");

                xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key=\"" + appKey + "\"]");
                if (xElem1 != null) xElem1.SetAttribute("value", appValue);
                else
                {
                    xElem2 = xDoc.CreateElement("add");
                    xElem2.SetAttribute("key", appKey);
                    xElem2.SetAttribute("value", appValue);
                    xNode.AppendChild(xElem2);
                }
                xDoc.Save(strXmlPath);
            }
            catch (XmlException ex)
            {
                classMsg.messageInfoBox(ex.Message);
                classLog.WriteLog(AutoUpdater.lib.ClassLog.LogType.Error, "Source:{" + ex.Source + "}" + " StackTrace:{" + ex.StackTrace + "}" + " Message:{" + ex.Message + "}");
            }
        }

        /// <summary> 
        /// 获取服务器端软件的更新日期 
        /// </summary> 
        /// <param name="Dir">服务器地址</param> 
        /// <returns>返回日期</returns> 
        private static string getTheLastUpdateTime(string Dir)
        {
            string LastUpdateTime = "";
            string AutoUpdaterFileName = Dir + strUpdateListXmlPath;
            try
            {
                WebClient wc = new WebClient();
                Stream sm = wc.OpenRead(AutoUpdaterFileName);
                XmlTextReader xml = new XmlTextReader(sm);
                while (xml.Read())
                {
                    if (xml.Name == "UpdateTime")
                    {
                        LastUpdateTime = xml.GetAttribute("Date");
                        break;
                    }
                }
                xml.Close();
                sm.Close();
            }
            catch (WebException ex)
            {
                classMsg.messageInfoBox(ex.Message);
                classLog.WriteLog(AutoUpdater.lib.ClassLog.LogType.Error, "Source:{" + ex.Source + "}" + " StackTrace:{" + ex.StackTrace + "}" + " Message:{" + ex.Message + "}");
            }
            return LastUpdateTime;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                updaterClose();
            }
            catch
            {

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //提示关掉所有主程序
            if (classCPIR.checkProcessForProName(strMainProFileName))
            {
                classMsg.messageInfoBox("进程中检测到主程序正在运行，请先关闭才可更新。" + strMainProFileName);
                return;
            }

            if (boolUpdateFalg)      //是否有更新
            {
                updaterStart();     //有更新
            }
            else
            {
                classMsg.messageInfoBox("暂无更新！");
                updaterClose();     //默认无更新
            }
        }
    }
}

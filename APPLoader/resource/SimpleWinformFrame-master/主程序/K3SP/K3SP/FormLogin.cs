using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Globalization;     //本地化单位
using System.Net;               //网络
using System.IO;                //文件
using System.Xml;               //xml
using System.Diagnostics;       //进程
using System.Runtime.InteropServices;

namespace K3SP
{
    public partial class FormLogin : Form
    {
        /// <summary>
        /// 消息处理类申明
        /// </summary>
        private static K3SP.lib.ClassMessage classMsg = new K3SP.lib.ClassMessage();

        /// <summary>
        /// 日志类申明
        /// </summary>
        private static K3SP.lib.ClassLog classLog = new K3SP.lib.ClassLog();

        /// <summary>
        /// 全局变量，用于存储更新服务器的URL
        /// </summary>
        private static string strUpdateURL;

        /// <summary>
        /// 本地update.xml的路径
        /// </summary>
        private static string strUpdateXmlPath = Application.StartupPath + @"\update\conf\update.xml";

        /// <summary>
        /// 服务端updatelist.xml的URL地址 
        /// </summary>
        private static string strUpdateListXmlPath = "UpdateServer/UpdateList.xml";

        /// <summary>
        /// 全局变量，用于存储服务端updatelist.xml的更新日期 
        /// </summary>
        private static string strTheUpdateDate;

        /// <summary>
        /// 更新程序的文件名
        /// </summary>
        private static string strUpdaterProFileName = "AutoUpdater";

        /// <summary>
        /// 更新程序的路径
        /// </summary>
        private static string strUpdaterProPath = Application.StartupPath + @"\update\AutoUpdater.exe";

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            checkUpdate();  //检测更新
        }

        /// <summary>
        /// 进入程序
        /// </summary>
        private void button_Login_Click(object sender, EventArgs e)
        {
            FormMain form_Main = new FormMain();
            form_Main.Show();
            this.Hide();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        private void buttonXGMM_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 检测更新
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
                    //本次更新日期 大于 最近一次更新日期，开始更新
                    try
                    {
                        if (new K3SP.lib.ClassCheckProIsRun().checkProcess(strUpdaterProFileName, strUpdaterProPath))
                        {
                            classMsg.messageInfoBox("更新程序" + strUpdaterProFileName + "已打开！");
                        }
                        else
                        {
                            Process.Start(strUpdaterProPath);
                        }
                    }
                    catch (Win32Exception ex)
                    {
                        classMsg.messageInfoBox(ex.Message);      //主程序未更新成功或者被误删掉，再更新一遍
                    }
                    Application.Exit();         //退出主程序
                }
            }
        }

        /// <summary> 
        /// 读取本地update.xml 
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
            }
            if (xElem != null)
                return xElem.GetAttribute("value");
            else
                return "";
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
            }
            return LastUpdateTime;
        }
    }
}

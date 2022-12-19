using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;    //文件操作
using System.Globalization; //定义区域性相关信息的类
using System.Xml;
using System.Diagnostics;

namespace UpdateListBuilder
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 消息处理类申明
        /// </summary>
        private static UpdateListBuilder.lib.ClassMessage classMsg = new UpdateListBuilder.lib.ClassMessage();

        /// <summary>
        /// 下载文件目录
        /// </summary>
        public static string strUpdateFilesPath = Application.StartupPath + @"\UpdateFiles";

        /// <summary>
        /// 下载文件xml列表
        /// </summary>
        public static string strUpdateListXmlPath = Application.StartupPath + @"\UpdateList.xml";

        /// <summary>
        /// 主程序的文件名
        /// </summary>
        private static string strMainProFileName = "K3SP.exe";

        /// <summary>
        /// 刷新前 主程序的版本
        /// </summary>
        private static string Global_strProVer;

        /// <summary>
        /// 刷新前 所有更新文件字节大小
        /// </summary>
        private static long Global_longFilesSize;

        /// <summary>
        /// 刷新前 所有更新文件总数
        /// </summary>
        private static int Global_intFilesCount;

        /// <summary>
        /// 遍历所有文件和子目录的方法来获得某目录下的文件个数
        /// </summary>
        /// <param name="dirInfo"></param>
        /// <returns></returns>
        public static int getFilesCount(DirectoryInfo dirInfo)
        {
            int totalFile = 0;
            try
            {
                totalFile += dirInfo.GetFiles().Length;
                string s = dirInfo.FullName;

                foreach (DirectoryInfo subdir in dirInfo.GetDirectories())
                {
                    totalFile += getFilesCount(subdir);
                }
            }
            catch (Exception ex)
            {
                classMsg.messageInfoBox(ex.Message);
            }
            return totalFile;
        }

        /// <summary>
        /// 返回指定目录下的所有文件信息
        /// </summary>
        /// <param name="strDirectory"></param>
        /// <returns></returns>
        public List<FileInfo> GetAllFilesInDirectory(string strDirectory)
        {
            //判断文件夹是否存在，不存在则退出
            if (!Directory.Exists(strDirectory))
                return null;
            
            List<FileInfo> listFiles = new List<FileInfo>(); //保存所有的文件信息  
            DirectoryInfo directory = new DirectoryInfo(strDirectory);
            DirectoryInfo[] directoryArray = directory.GetDirectories();
            FileInfo[] fileInfoArray = directory.GetFiles();
            if (fileInfoArray.Length > 0) listFiles.AddRange(fileInfoArray);
            foreach (DirectoryInfo _directoryInfo in directoryArray)
            {
                DirectoryInfo directoryA = new DirectoryInfo(_directoryInfo.FullName);
                DirectoryInfo[] directoryArrayA = directoryA.GetDirectories();
                FileInfo[] fileInfoArrayA = directoryA.GetFiles();
                if (fileInfoArrayA.Length > 0) listFiles.AddRange(fileInfoArrayA);
                GetAllFilesInDirectory(_directoryInfo.FullName);//递归遍历  
            }
            return listFiles;
        }

        /// <summary>
        /// 获取所有目录即子目录下的文件总字节大小
        /// </summary>
        /// <param name="listFileInfo">返回指定目录下的所有文件信息</param>
        /// <returns>返回值</returns>
        private static long getUpdateSize(List<FileInfo> listFileInfo)
        {
            long len = 0;
            if (listFileInfo != null)
            {
                foreach (FileInfo fileInfo in listFileInfo)
                {
                    len += fileInfo.Length;
                }
            }
            return len;
        }

        /// <summary>
        /// 获取所有目录即子目录下的文件完整路径列表
        /// </summary>
        /// <param name="listFileInfo">返回指定目录下的所有文件信息</param>
        /// <returns>返回值</returns>
        private static string[] getUpdateList(List<FileInfo> listFileInfo)
        {
            string strRelativePath = null;
            string[] strArrayRelativePaths = null;
            if (listFileInfo != null)
            {
                foreach (FileInfo fileInfo in listFileInfo)
                {
                    strRelativePath += fileInfo.FullName.Replace(strUpdateFilesPath, "") + ',';
                }
                if (strRelativePath != null)
                {
                    strArrayRelativePaths = strRelativePath.TrimEnd(',').Split(',');
                }
            }
            return strArrayRelativePaths;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //判断下载目录不存在则新建一个目录
            if (!Directory.Exists(strUpdateFilesPath))
            {
                Directory.CreateDirectory(strUpdateFilesPath);
            }

            createHeadersAndFillListView();     //设置列名
            getUpdateInfo();        //更新列表头信息
            populateListViewWithArray(getUpdateList(GetAllFilesInDirectory(strUpdateFilesPath)));       //填充数据

        }

        /// <summary>
        /// 更新信息头
        /// </summary>
        private void getUpdateInfo()
        {
            labelUpDate.Text = "更新日期：" + DateTime.Today.ToShortDateString();
            labelVer.Text = "主程序版本：" + getMainProVer();
            labelSize.Text = "更新文件总大小：" + ConvertSize(getUpdateSize(GetAllFilesInDirectory(strUpdateFilesPath)));

            Global_strProVer = getMainProVer();
            Global_longFilesSize = getUpdateSize(GetAllFilesInDirectory(strUpdateFilesPath));
            DirectoryInfo dirInfoUpdate = new DirectoryInfo(strUpdateFilesPath);        //这个不会报异常，因为没有这个目录就已经在窗口加载的时候被创建了
            Global_intFilesCount = getFilesCount(dirInfoUpdate);
        }

        /// <summary>
        /// 获取主程序文件版本
        /// </summary>
        /// <returns></returns>
        private string getMainProVer()
        {
            string strProVer = "";
            if (File.Exists(strUpdateFilesPath + "\\" + strMainProFileName))        //如果更新中有主程序
            {
                FileVersionInfo fviMainPro = FileVersionInfo.GetVersionInfo(strUpdateFilesPath + "\\" + strMainProFileName);
                strProVer = fviMainPro.FileVersion;
            }
            return strProVer;
        }

        /// <summary>
        /// 给listview添加列名
        /// </summary>
        private void createHeadersAndFillListView()
        {
            int lvWithd = this.listViewUpdateList.Width;
            ColumnHeader colHead;

            // First header
            colHead = new ColumnHeader();
            colHead.Text = "#";
            colHead.Width = lvWithd / 16;
            this.listViewUpdateList.Columns.Add(colHead); // Insert the header

            // Second header
            colHead = new ColumnHeader();
            colHead.Text = "文件名";
            colHead.Width = lvWithd / 4;
            this.listViewUpdateList.Columns.Add(colHead); // Insert the header

            // Third header
            colHead = new ColumnHeader();
            colHead.Text = "更新路径";
            colHead.Width = lvWithd * 11 / 16;
            this.listViewUpdateList.Columns.Add(colHead); // Insert the header
        }

        /// <summary>
        /// 使用路径字符数组填充列表
        /// </summary>
        /// <param name="strArray"></param>
        private void populateListViewWithArray(string[] strArray)
        {
            this.listViewUpdateList.Items.Clear();
            if (strArray != null)
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = (i + 1).ToString();    //添加序号，从1开始，这样好统计
                    int intStart = strArray[i].LastIndexOf('\\') + 1;   //截取文件名起始位置
                    lvi.SubItems.Add(strArray[i].Substring(intStart, strArray[i].Length - intStart));   //添加文件名（从路径中截取出来的）
                    lvi.SubItems.Add(strArray[i]);      //添加路径
                    this.listViewUpdateList.Items.Add(lvi);
                }
            }
        }

        /// <summary> 
        /// 转换字节大小 
        /// </summary> 
        /// <param name="byteSize">输入字节数</param> 
        /// <returns>返回值</returns> 
        private static string ConvertSize(long byteSize)
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
        /// 获取xml元素的值
        /// </summary>
        /// <param name="strEml">元素名称</param>
        /// <param name="strAtr">元素值</param>
        /// <returns></returns>
        private static string getUpdateXmlValue(string strEml,string strAtr)
        {
            string LastUpdateTime = "";
            try
            {
                XmlTextReader xml = new XmlTextReader(strUpdateListXmlPath);
                while (xml.Read())
                {
                    if (xml.Name == strEml)
                    {
                        LastUpdateTime = xml.GetAttribute(strAtr);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                classMsg.messageInfoBox(ex.Message);
            }
            return LastUpdateTime;
        }

        //刷新
        private void buttonRead_Click(object sender, EventArgs e)
        {
            DirectoryInfo dirInfoUpdate = new DirectoryInfo(strUpdateFilesPath);
            //判断 主程序的版本号、所有更新文件的字节大小、文件总数
            if (Global_strProVer == getMainProVer() &&
                Global_longFilesSize == getUpdateSize(GetAllFilesInDirectory(strUpdateFilesPath)) &&
                Global_intFilesCount == getFilesCount(dirInfoUpdate))
            {
                classMsg.messageInfoBox("更新文件无变更！");
                return;
            }
            getUpdateInfo();        //更新列表头信息
            populateListViewWithArray(getUpdateList(GetAllFilesInDirectory(strUpdateFilesPath)));       //填充数据
        }

        //生成
        private void buttonBuild_Click(object sender, EventArgs e)
        {            
            buildUpdateListXml(getUpdateList(GetAllFilesInDirectory(strUpdateFilesPath)));
        }

        /// <summary>
        /// 生成更新配置文件
        /// </summary>
        /// <param name="strArray"></param>
        private void buildUpdateListXml(string[] strArray)
        {
            if (strArray != null)
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    // 创建根节点AutoUpdater
                    XmlElement xeRoot = xmlDoc.CreateElement("AutoUpdater");
                    xmlDoc.AppendChild(xeRoot);    // 加入到上层节点

                    // 创建UpdateInfo元素
                    XmlElement xeUI = xmlDoc.CreateElement("UpdateInfo");
                    xeRoot.AppendChild(xeUI);

                    // 创建UpdateTime元素
                    XmlElement xeUT = xmlDoc.CreateElement("UpdateTime");
                    xeUT.SetAttribute("Date", DateTime.Today.ToShortDateString());
                    xeUI.AppendChild(xeUT);
                    // 创建Version元素
                    XmlElement xeUV = xmlDoc.CreateElement("Version");
                    xeUV.SetAttribute("Num", getMainProVer());
                    xeUI.AppendChild(xeUV);
                    // 创建UpdateSize元素
                    XmlElement xeUS = xmlDoc.CreateElement("UpdateSize");
                    xeUS.SetAttribute("Size", getUpdateSize(GetAllFilesInDirectory(strUpdateFilesPath)).ToString());
                    xeUI.AppendChild(xeUS);

                    // 创建UpdateFileList元素
                    XmlElement xeUFL = xmlDoc.CreateElement("UpdateFileList");
                    xeRoot.AppendChild(xeUFL);

                    for (int i = 0; i < strArray.Length; i++)
                    {
                        // 循环创建UpdateFile元素
                        XmlElement xeUF = xmlDoc.CreateElement("UpdateFile");
                        xeUF.InnerText = strArray[i];
                        xeUFL.AppendChild(xeUF);        // 加入到上层节点
                    }

                    xmlDoc.Save("UpdateList.xml");      // 保存文件

                    classMsg.messageInfoBox("配置文件创建完成！");
                }
                catch(Exception ex)
                {
                    classMsg.messageInfoBox(ex.Message);
                }
                return;
            }
            classMsg.messageInfoBox("UpdateFiles目录中无更新文件！");
        }
    }
}

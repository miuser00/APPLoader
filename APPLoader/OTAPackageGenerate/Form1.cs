using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OTAPackageGenerate
{
    public partial class Form1 : Form
    {
        String currentProjectPath;
        //设置窗体
        SetupForm frm_setup = new SetupForm();
        //上传文件表
        public DataTable dt_UploadFiles = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Log("使用说明：", true, true);
            Log("------------------------------------------------------------------", true, true);
            Log("请先选择要打包的应用程序的根目录",true,true);
            Log("1 在左侧第一个文件浏览窗选择要压缩的文件", true, true);
            Log("2 在第二个文件浏览窗选中升级过程中不作更新的用户目录和文件", true, true);
            Log("3 生成安装配置", true, true);
            Log("4 可以点击按钮编辑说明文档，利用内置MD文件编辑器修改markdown说明文档", true, true);
            Log("5 点击生成安装文件，生成安装文件，并点击上传服务器!", true, true);

            Log("------------------------------------------------------------------", true, true);

            //读取上次打开时的控件信息
            MSaveControl.Load_All_SupportedControls(this.Controls);
        }
        private void PopulateTreeView(TreeView tv)
        {
            tv.Nodes.Clear();
            //TreeNode是TreeView中的节点
            TreeNode rootNode;
            string path = currentProjectPath;
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists)
            {
                // TreeNode(string text)
                rootNode = new TreeNode(info.Name, 0, 0);
                //TreeNode.Tag是objec型变量 能接受任何数据
                rootNode.Name = info.FullName;
                rootNode.Tag = "根目录";
                //将目录树的信息写入rootnode
                GetDirectories(info.GetDirectories(), rootNode);
                GetFiles(info.GetFiles(), rootNode);
                tv.Nodes.Add(rootNode);
            }
        }
        // 模拟生成树节点的过程-层层递推
        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subsubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Name =subDir.FullName;
                aNode.Tag = "目录";
                subsubDirs = subDir.GetDirectories();
                // DirectoryInfo.Length表示子目录的个数
                if (subsubDirs.Length != 0)
                    GetDirectories(subsubDirs, aNode);
                nodeToAddTo.Nodes.Add(aNode);
                GetFiles(subDir.GetFiles(), aNode);
            }
        }
        // 模拟生成树节点的过程-层层递推
        private void GetFiles(FileInfo[] files, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            foreach (FileInfo file in files)
            {
                aNode = new TreeNode(file.Name, 0, 0);
                aNode.Name = file.FullName;
                aNode.Tag = "文件";
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        private void btn_SelectRootFolder_Click(object sender, EventArgs e)
        {
            var odd = new CommonOpenFileDialog();
            odd.IsFolderPicker = true; //设置为true为选择文件夹，设置为false为选择文件
            odd.Title = "选择文件夹";
            var result = odd.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                currentProjectPath = odd.FileName;
                PopulateTreeView(tv_PacakItems);
                PopulateTreeView(tv_ExcludeItems);
                tv_ExcludeItems.Nodes[0].Expand();
                tv_PacakItems.Nodes[0].Expand();
                Log("当前根目录为:" + currentProjectPath);
                btn_GenerateFiles.Enabled = true;
                btn_BrowserFile.Enabled = true;
                //生成前禁止其他功能
                btn_GeneratePackage.Enabled = false;
                btn_Publish.Enabled = false;
                btn_EditMDFile.Enabled = false;

                tv_PacakItems.Nodes[0].Checked = true;
                SetChildNodeCheckedState(tv_PacakItems.Nodes[0], true);


            }
        }
        public void Log(string log, bool AddingNewLine = true,bool NoTimeLine=false)
        {
            this.Invoke(new Action(delegate
            {
                if (AddingNewLine)
                {
                    if (NoTimeLine) rtb_log.AppendText(log + "\r\n"); else rtb_log.AppendText(DateTime.Now.ToString("[HH:mm:ss.fff]") + log + "\r\n");
                }
                else
                {
                    int currlineNo = rtb_log.GetLineFromCharIndex(rtb_log.SelectionStart - 1);
                    rtb_log.SelectionStart = rtb_log.GetFirstCharIndexFromLine(currlineNo);
                    rtb_log.SelectionLength = this.rtb_log.Lines[currlineNo].Length + 1;
                    rtb_log.SelectedText = String.Empty;
                    if (NoTimeLine) rtb_log.AppendText(log + "\r\n");else rtb_log.AppendText(DateTime.Now.ToString("[HH:mm:ss.fff]") + log + "\r\n");
                }
                rtb_log.SelectionStart = rtb_log.TextLength;
                rtb_log.ScrollToCaret();
            }));
        }

        private void tv_PacakItems_AfterCheck(object sender, TreeViewEventArgs e)
        {            
            //通过鼠标或者键盘触发事件，防止修改节点的Checked状态时候再次进入
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                string relative = GetRelativePath(currentProjectPath, e.Node.Name.ToString());
                SetChildNodeCheckedState(e.Node, e.Node.Checked);
                SetParentNodeCheckedState(e.Node, e.Node.Checked);
                if (e.Node.Checked)
                {
                    Log("添加" + e.Node.Tag + " " + relative);
                }else
                {
                    Log("取消添加" + e.Node.Tag + " " + relative);
                }
            }
            //生成前禁止其他功能
            btn_GeneratePackage.Enabled = false;
            btn_Publish.Enabled = false;
            btn_EditMDFile.Enabled = false;
        }
        //关于获取相对路径的计算
        private static string GetRelativePath(string pathA, string pathB)
        {
            string[] pathAArray = pathA.Split('\\');
            string[] pathBArray = pathB.Split('\\');
            //返回2者之间的最小长度
            int s = pathAArray.Length >= pathBArray.Length ? pathBArray.Length : pathAArray.Length;
            //两个目录最底层的共用目录的索引
            int closestRootIndex = -1;
            for (int i = 0; i < s; i++)
            {
                if (pathAArray[i] == pathBArray[i])
                {
                    closestRootIndex = i;
                }
                else
                {
                    break;
                }
            }
            //由pathA计算 ‘../’部分
            string pathADepth = "";
            for (int i = 0; i < pathAArray.Length; i++)
            {
                if (i > closestRootIndex + 1)
                {
                    pathADepth += "../";
                }
            }
            //由pathB计算‘../’后面的目录
            string pathBdepth = "";
            for (int i = closestRootIndex + 1; i < pathBArray.Length; i++)
            {
                pathBdepth += "/" + pathBArray[i];
            }
            if (pathBdepth.Length != 0) pathBdepth = pathBdepth.Substring(1);//去掉重复的斜杠 “ / ”
            return pathADepth + pathBdepth;//pathB相对于pathA的相对路径
        }

        //设置子节点状态
        private void SetChildNodeCheckedState(TreeNode currNode, bool isCheckedOrNot)
        {
            if (currNode.Nodes == null) return; //没有子节点返回
            foreach (TreeNode tmpNode in currNode.Nodes)
            {
                tmpNode.Checked = isCheckedOrNot;
                SetChildNodeCheckedState(tmpNode, isCheckedOrNot);
            }
        }        //设置父节点状态
        private void SetParentNodeCheckedState(TreeNode currNode, bool isCheckedOrNot)
        {
            if (currNode.Parent == null) return; //没有父节点返回
            if (isCheckedOrNot) //如果当前节点被选中，则设置所有父节点都被选中
            {
                currNode.Parent.Checked = isCheckedOrNot;
                SetParentNodeCheckedState(currNode.Parent, isCheckedOrNot);
            }
            else //如果当前节点没有被选中，则当其父节点的子节点有一个被选中时，父节点被选中，否则父节点不被选中
            {
                bool checkedFlag = false;
                foreach (TreeNode tmpNode in currNode.Parent.Nodes)
                {
                    if (tmpNode.Checked)
                    {
                        checkedFlag = true;
                        break;
                    }
                }
                currNode.Parent.Checked = checkedFlag;
                SetParentNodeCheckedState(currNode.Parent, checkedFlag);
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tv_ExcludeItems_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //通过鼠标或者键盘触发事件，防止修改节点的Checked状态时候再次进入
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                string relative = GetRelativePath(currentProjectPath, e.Node.Name.ToString());
                SetChildNodeCheckedState(e.Node, e.Node.Checked);
                SetParentNodeCheckedState(e.Node, e.Node.Checked);
                if (e.Node.Checked)
                {
                    Log("排除" + e.Node.Tag + " " + relative);
                }else
                {
                    Log("不再排除" + e.Node.Tag + " " + relative);
                }
            }
            //生成前禁止其他功能
            btn_GeneratePackage.Enabled = false;
            btn_Publish.Enabled = false;
            btn_EditMDFile.Enabled = false;
        }

        private void btn_GeneratePackage_Click(object sender, EventArgs e)
        {
            WaitForm frm = new WaitForm();
            if (Directory.Exists(Path.GetDirectoryName(currentProjectPath) + "\\RelApp"))
            {
                Log("正在压缩文件，请稍等");
                frm.Show();

                //清理输出目录
                if (Directory.Exists(Path.GetDirectoryName(currentProjectPath) + "\\RelPack\\"))
                {
                    try
                    {
                        DirectoryInfo dir = new DirectoryInfo(Path.GetDirectoryName(currentProjectPath) + "\\RelPack\\");
                        dir.Delete(true);
                    }
                    catch { }
                }
                //调用7za.exe压缩文件夹
                CompressFiles(Path.GetDirectoryName(currentProjectPath) + "\\RelPack\\" + txt_packageFileName.Text, Path.GetDirectoryName(currentProjectPath) + "\\RelApp\\*");
                frm.Close();

                File.Copy(Path.GetDirectoryName(currentProjectPath) + "\\RelApp\\" + "upgrade.md", Path.GetDirectoryName(currentProjectPath) + "\\RelPack\\" + "\\" + "upgrade.md", true);

                //更新升级配置文件upgrade中的MD5字段
                XmlSerializer serializer = new XmlSerializer(typeof(Upgrade_XML_Info));
                StreamReader sr = new StreamReader(Path.GetDirectoryName(currentProjectPath) + "\\RelApp\\" + "upgrade.xml");
                Upgrade_XML_Info server_info = (Upgrade_XML_Info)(serializer.Deserialize(sr));
                sr.Close();
                server_info.MD5_Of_Package_File = GetMD5HashFromFile(Path.GetDirectoryName(currentProjectPath) + "\\RelPack\\" + txt_packageFileName.Text);
                TextWriter writer = new StreamWriter(Path.GetDirectoryName(currentProjectPath) + "\\RelPack\\" + "upgrade.xml");
                serializer.Serialize(writer, server_info);
                writer.Close();
            }
            else
            {
                Log("请先选择文件");
            }
            //使能其他功能
            btn_Publish.Enabled = true;
            btn_EditMDFile.Enabled = true;
            //保存当前控件配置
            MSaveControl.Save_All_SupportedControls(this.Controls);
        }
        //复制Treeview选中的文件到InstallPackage目录
        void CopyTreeNodeFiles(TreeNode root)
        {
            foreach (TreeNode tr in root.Nodes)
            {
                if (tr.Checked)
                {
                    if (tr.Nodes.Count != 0)
                    {
                        // 是目录
                        CopyTreeNodeFiles(tr);
                    }
                    else
                    {
                        //是文件
                        if (tr.Tag.ToString() == "文件")
                        {
                            string dir = Path.GetDirectoryName(Path.GetDirectoryName(currentProjectPath) + "\\RelApp\\" + tr.FullPath);
                            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                            File.Copy(tr.Name, Path.GetDirectoryName(currentProjectPath) + "\\RelApp\\" + tr.FullPath, true);
                        }else
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(currentProjectPath) + "\\RelApp\\" + tr.FullPath);
                        }
                    }

                }

            }
        }
        //把Treeview选中的文件，生成为字符串List
        List<string> GeneratedTreeNodeFiles(TreeNode root)
        {
            List<string> ls = new List<string>();
            foreach (TreeNode tr in root.Nodes)
            {
                if (tr.Checked)
                {
                    if (tr.Nodes.Count != 0)
                    {
                    // 是目录
                        List<string> ls_sub=GeneratedTreeNodeFiles(tr);
                        foreach(string item in ls_sub)
                        {
                            ls.Add(item);
                        }
                    }
                    else
                    {
                        //是选中的文件
                        if (tr.Tag.ToString() == "文件")
                        {
                            ls.Add(GetRelativePath(Path.GetFileName(currentProjectPath), tr.FullPath));
                        }
                    }

                }

            }
            return ls;
        }
        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath, true);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }
        public void CompressFiles(string s_7zfile, string source)
        {
            if (File.Exists(s_7zfile)) File.Delete(s_7zfile);
            try
            {
                ProcessStartInfo pro = new ProcessStartInfo();
                pro.WindowStyle = ProcessWindowStyle.Hidden;
                pro.FileName = Application.StartupPath + "\\bin32\\7za.exe";
                pro.Arguments = "a " + s_7zfile + " "  + source;
                pro.CreateNoWindow = true;
                pro.RedirectStandardOutput = true;
                pro.RedirectStandardInput = true;
                pro.RedirectStandardError = true;
                pro.UseShellExecute = false;              //是否指定操作系统外壳进程启动程序，这里需为false
                Process x = Process.Start(pro);
                x.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                x.ErrorDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                x.BeginOutputReadLine();
                x.WaitForExit();
                Log("Files compressed");
            }
            catch (System.Exception Ex)
            {
                Log("Error occurred while compressing files");
                Log(Ex.StackTrace);
            }
        }
        /// <summary>
        /// 过滤检测串口输出，并根据输出设置状态栏颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                this.BeginInvoke(new Action(() =>
                {
                    Log(e.Data, true);
                }));
            }
        }
        /// <summary>
        /// 生成upgrade.xml，upgrade.md，config.xml三个文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GenerateFiles_Click(object sender, EventArgs e)
        {
            //清理下待压缩的临时目录
            if (Directory.Exists(Path.GetDirectoryName(currentProjectPath) + "\\RelApp\\"))
            {
                DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(currentProjectPath) + "\\RelApp\\");
                di.Delete(true);
            }
            //复制待压缩的文件到与应用目录同级的RelApp目录
            if (tv_PacakItems.Nodes.Count > 0)
            {
                //复制Treeview选中的文件
                CopyTreeNodeFiles(tv_PacakItems.Nodes[0]);
            }

            //把Treeview选中的文件，生成为字符串List
            List<string> ls = new List<string>();
            if (tv_ExcludeItems.Nodes.Count > 0)
            {
                ls = GeneratedTreeNodeFiles(tv_ExcludeItems.Nodes[0]);
            }

            //生成cofig.xml(App_Loader外部配置文件)
            BaseCfg appcfg = new BaseCfg();
            appcfg.AppFile = txt_AppFile.Text;
            appcfg.AppTitle = txt_AppTitle.Text;
            appcfg.AppVerXmlUrl = txt_updateXMLURL.Text;
            appcfg.Debug = false;
            appcfg.UpgradeFolder = "uppack";
            XmlSerializer sz1 = new XmlSerializer(typeof(BaseCfg));
            TextWriter writer1 = new StreamWriter(currentProjectPath  +"\\"+ "config.xml");
            sz1.Serialize(writer1, appcfg);
            writer1.Close();

            //生成update.xml
            Upgrade_XML_Info local_info = new Upgrade_XML_Info();
            local_info.PackageUrl = txt_7zfileURL.Text;
            local_info.UpgradeMDFileUrl = txt_MdFileURL.Text;
            float ver = 0;
            float.TryParse(txt_ver.Text, out ver);
            local_info.Version = ver;
            local_info.Version_tail = txt_sub.Text;
            local_info.SkipFile = ls;
            XmlSerializer sz = new XmlSerializer(typeof(Upgrade_XML_Info));
            //生成upgrade.md
            if (Directory.Exists(Path.GetDirectoryName(currentProjectPath) + "\\RelApp\\"))
            {
                TextWriter writer = new StreamWriter(Path.GetDirectoryName(currentProjectPath) + "\\RelApp\\" + "\\" + "upgrade.xml");
                sz.Serialize(writer, local_info);
                writer.Close();

                writer = new StreamWriter(Path.GetDirectoryName(currentProjectPath) + "\\RelApp\\" + "\\" + "upgrade.md");
                writer.WriteLine("# 升级说明");
                writer.WriteLine("## 版本号");
                writer.WriteLine("### " + txt_ver.Text + txt_sub.Text);
                writer.Close();
                //使能其他功能
                btn_GeneratePackage.Enabled = true;
                btn_EditMDFile.Enabled = true;
                btn_Publish.Enabled = false;
                Log("配置文件已经生成");
                //显示读取配置文件
                StreamReader sr1 = new StreamReader(Path.GetDirectoryName(currentProjectPath) + "\\RelApp\\" + "\\" + "upgrade.xml");
                string uu = sr1.ReadToEnd();
                sr1.Close();
                Log( Path.GetDirectoryName(currentProjectPath) + "\\RelApp" + "\\" + "upgrade.xml"+" was saved");
                Log("---------upgrade.xml-----------" + "\n\r" +uu);
                //显示读取配置文件
                StreamReader sr2 = new StreamReader(currentProjectPath + "\\" + "config.xml");
                string uc = sr2.ReadToEnd();
                sr2.Close();
                Log(currentProjectPath + "\\" + "config.xml" + " was saved");
                Log("---------config.xml-----------" + "\n\r" + uc);
                //ShellRun("notepad.exe", Path.GetDirectoryName(currentProjectPath) + "\\RelApp" + "\\" + "upgrade.xml");
                //ShellRun("notepad.exe", currentProjectPath + "\\" + "config.xml");
            }
            else
            {
                Log("请首先在左侧的类表里配置安装包文件");
            }

        }
        public class Upgrade_XML_Info
        {
            //App版本号
            public float Version { get; set; }
            //版本号尾缀
            public string Version_tail { get; set; }
            //压缩包url
            public string PackageUrl { get; set; }
            //说明文档url
            public string UpgradeMDFileUrl { get; set; }
            //要跳过的文件夹
            public List<string> SkipFolder { get; set; }
            //要跳过的文件
            public List<string> SkipFile { get; set; }
            //压缩包的md5签名字符串
            public string MD5_Of_Package_File { get; set; }
        }

        private void txt_ver_TextChanged(object sender, EventArgs e)
        {
            txt_packageFileName.Text = txt_AppTitle.Text+"_ver" + txt_ver.Text  + txt_sub.Text+".pack";
            txt_7zfileURL.Text = txt_serverFolderURL.Text + "/" + txt_packageFileName.Text;
            //生成前禁止其他功能
            btn_GeneratePackage.Enabled = false;
            btn_Publish.Enabled = false;
            btn_EditMDFile.Enabled = false;
        }

        private void txt_sub_TextChanged(object sender, EventArgs e)
        {
            txt_packageFileName.Text = txt_AppTitle.Text + "_ver" + txt_ver.Text + txt_sub.Text + ".pack";
            txt_7zfileURL.Text = txt_serverFolderURL.Text + "/" + txt_packageFileName.Text;
            //生成前禁止其他功能
            btn_GeneratePackage.Enabled = false;
            btn_Publish.Enabled = false;
            btn_EditMDFile.Enabled = false;
        }

        private void txt_serverFolderURL_TextChanged(object sender, EventArgs e)
        {
            txt_updateXMLURL.Text = txt_serverFolderURL.Text + "/" + "upgrade.xml";
            txt_7zfileURL.Text = txt_serverFolderURL.Text + "/" + txt_packageFileName.Text;
            txt_MdFileURL.Text=txt_serverFolderURL.Text + "/" + "upgrade.md";
            //生成前禁止其他功能
            btn_GeneratePackage.Enabled = false;
            btn_Publish.Enabled = false;
            btn_EditMDFile.Enabled = false;
        }

        private void txt_updateXMLURL_TextChanged(object sender, EventArgs e)
        {

        }
        public void ShellRun(string app, string arg)
        {
            Process MyProcess = new Process();
            MyProcess.StartInfo.FileName = app;
            MyProcess.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(app);
            MyProcess.StartInfo.Verb = "Open";
            MyProcess.StartInfo.Arguments = arg;
            MyProcess.StartInfo.CreateNoWindow = true;
            MyProcess.Start();
        }

        private void btn_EditMDFile_Click(object sender, EventArgs e)
        {
            ShellRun(Application.StartupPath+"\\MDLoader\\MDLoader.exe", Path.GetDirectoryName(currentProjectPath) + "\\RelApp" + "\\" + "upgrade.md");
            //生成前禁止其他功能
            btn_GeneratePackage.Enabled = false;
            btn_Publish.Enabled = false;
            btn_EditMDFile.Enabled = false;
        }


        private void SetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupForm frm_setup = new SetupForm();
            frm_setup.Show();
        }

        private void UploadPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (panel10.Visible == true)
            {
                uploadpannelToolStripMenuItem.Checked = false;
                panel10.Visible = false;
            }
            else
            {
                uploadpannelToolStripMenuItem.Checked = true;
                panel10.Visible = true;
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("OTA Package Generator"+ "\n" +"Author Contact:64034373@qq.com","About");
        }

        //上传到服务器，并复制文件到打包目录
        private void btn_Publish_Click(object sender, EventArgs e)
        {
            panel10.Visible = true;
            WaitForm waitForm = new WaitForm();
            waitForm.Show();
            UploadToFTPServer();
            CopyToAppLoaderSourceFolder();

            DataRow drr = dt_UploadFiles.NewRow();
            drr["No"] = "All Done";
            drr["Local File"] = "";
            drr["Remote File"] = "";
            drr["Progress"] = "";
            dt_UploadFiles.Rows.Add(drr);

            waitForm.Close();
        }
        //上传到FTP服务器
        private void UploadToFTPServer()
        {
            //登录服务器
            FtpWeb ftp = new FtpWeb("ftp://" + SetupForm.cfg.ServerAddress + ":" + SetupForm.cfg.Port.ToString(), SetupForm.cfg.UserName, SetupForm.cfg.Password);
            //添加4列，一列是编号，LocalFile是本地文件，RemoteFile是要上传到的路径,Progress是否上传完成
            dt_UploadFiles.Rows.Clear();
            dt_UploadFiles.Columns.Clear();
            dt_UploadFiles.Columns.Add("No");
            dt_UploadFiles.Columns.Add("Local File");
            dt_UploadFiles.Columns.Add("Remote File");
            dt_UploadFiles.Columns.Add("Progress");

            dataGridView1.DataSource = dt_UploadFiles;
            dataGridView1.Columns[0].Width = (int)dataGridView1.Width / 20;
            dataGridView1.Columns[1].Width = (int)dataGridView1.Width / 10 * 4;
            dataGridView1.Columns[2].Width = (int)dataGridView1.Width / 10 * 4;
            dataGridView1.Columns[3].Width = (int)dataGridView1.Width / 15;

            //上传文件
            var ftproot = "ftp://" + SetupForm.cfg.ServerAddress + ":" + SetupForm.cfg.Port.ToString() + "/" + DateTime.Now.ToLocalTime().ToString().Replace(" ", "_").Replace(":", "-") + "/";
            var httproot = txt_serverFolderURL.Text + "/" + DateTime.Now.ToLocalTime().ToString().Replace(" ", "_").Replace(":", "-") + "/";

            Dictionary<string, string> dic_filelist = new Dictionary<string, string>();

            dic_filelist.Add(Path.GetDirectoryName(currentProjectPath) + "\\RelPack\\" + "upgrade.xml", "ftp://"+ SetupForm.cfg.ServerAddress + "/" + Path.GetFileName(txt_updateXMLURL.Text));
            dic_filelist.Add(Path.GetDirectoryName(currentProjectPath) + "\\RelPack\\" + "upgrade.md", "ftp://" + SetupForm.cfg.ServerAddress + "/" + Path.GetFileName(txt_MdFileURL.Text));
            dic_filelist.Add(Path.GetDirectoryName(currentProjectPath) + "\\RelPack\\" + txt_packageFileName.Text, "ftp://" + SetupForm.cfg.ServerAddress + "/" + Path.GetFileName(txt_7zfileURL.Text));

            foreach (string key in dic_filelist.Keys)
            {
                DataRow dr = dt_UploadFiles.NewRow();
                dr["No"] = dt_UploadFiles.Rows.Count+1;
                dr["Local File"] = key;
                dr["Remote File"] = dic_filelist[key];
                dr["Progress"] = "Done";
                try
                {
                    ftp.Upload2(dr["Remote File"].ToString(), dr["Local File"].ToString());
                }
                catch (Exception ee)
                {
                    dr["Progress"] = "Failed";
                }
                dt_UploadFiles.Rows.Add(dr);
                if (dataGridView1 != null)
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
                    dataGridView1.Refresh();
                    Application.DoEvents();
                }
            }


        }
        //复制项目文件到APPLoader源码目录，供编译用
        private void CopyToAppLoaderSourceFolder()
        {
            //清理打包目录
            DirectoryInfo target=new DirectoryInfo(Path.GetDirectoryName(currentProjectPath) + "\\APPLoader\\APP_Loader\\BinApp");
            if (Directory.Exists(target.FullName)) target.Delete(true);
            //建立空目录
            Directory.CreateDirectory(Path.GetDirectoryName(currentProjectPath) + "\\APPLoader\\APP_Loader\\BinApp");
            DirectoryInfo source = new DirectoryInfo(Path.GetDirectoryName(currentProjectPath) + "\\RelPack");
            //复制打包好的文件
            foreach (FileInfo file in source.GetFiles())
            {
                string targetFilePath = Path.Combine(target.FullName, file.Name);
                file.CopyTo(targetFilePath, true);
                DataRow dritem = dt_UploadFiles.NewRow();
                dritem["No"] = dt_UploadFiles.Rows.Count+1;
                dritem["Local File"] = file.FullName;
                dritem["Remote File"] = targetFilePath;
                dritem["Progress"] = "Done";
                dt_UploadFiles.Rows.Add(dritem);
            }
            //复制config.xml到config目录
            File.Copy(currentProjectPath + "\\" + "config.xml", Path.GetDirectoryName(currentProjectPath) + "\\APPLoader\\APP_Loader\\config\\" + "config.xml", true);
            DataRow dr = dt_UploadFiles.NewRow();
            dr["No"] = dt_UploadFiles.Rows.Count + 1;
            dr["Local File"] = currentProjectPath + "\\" + "config.xml";
            dr["Remote File"] = Path.GetDirectoryName(currentProjectPath) + "\\APPLoader\\APP_Loader\\config\\" + "config.xml";
            dr["Progress"] = "Done";
            dt_UploadFiles.Rows.Add(dr);

        }
        protected string GetMD5HashFromFile(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();
            String hex = BytesToHex(retVal);
            return hex;
        }
        private static string BytesToHex(byte[] bytes)
        {
            string result = "";
            foreach (char c in bytes)
            {
                result += Convert.ToString(c, 16);
            }
            return result.ToUpper();
        }


        private void btn_BrowserFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog fie = new OpenFileDialog();
            //创建对象

            fie.Title = "请选择被引导的主程序";
            //设置文本框标题

            fie.InitialDirectory = @"C:\Users\Administrator\Desktop";
            //对话框的初始目录

            fie.Filter = "程序文件|*.exe|所有文件|*.*";
            //设置文件类型


            if (fie.ShowDialog() == DialogResult.OK)
            {
                string str = fie.FileName;
                string relative = GetRelativePath(currentProjectPath, str);
                txt_AppFile.Text = relative;
                txt_AppTitle.Text = Path.GetFileNameWithoutExtension(relative);

            }

        }

        private void txt_AppTitle_TextChanged(object sender, EventArgs e)
        {
            txt_packageFileName.Text = txt_AppTitle.Text + "_ver" + txt_ver.Text + txt_sub.Text + ".pack";
            txt_7zfileURL.Text = txt_serverFolderURL.Text + "/" + txt_packageFileName.Text;
            //生成前禁止其他功能
            btn_GeneratePackage.Enabled = false;
            btn_Publish.Enabled = false;
            btn_EditMDFile.Enabled = false;
        }

        private void txt_AppFile_TextChanged(object sender, EventArgs e)
        {
            txt_packageFileName.Text = txt_AppTitle.Text + "_ver" + txt_ver.Text + txt_sub.Text + ".pack";
            txt_7zfileURL.Text = txt_serverFolderURL.Text + "/" + txt_packageFileName.Text;
            //生成前禁止其他功能
            btn_GeneratePackage.Enabled = false;
            btn_Publish.Enabled = false;
            btn_EditMDFile.Enabled = false;
        }

        private void btn_SaveControls_Click(object sender, EventArgs e)
        {

        }

        private void btn_LoadControls_Click(object sender, EventArgs e)
        {

        }
    }
}

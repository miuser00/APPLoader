using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Xml;   //xml
using System.IO;    //file
using System.Reflection;    //程序集

namespace K3SP
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// XML配置文件路径
        /// </summary>
        string strXmlPath = Application.StartupPath + @"\conf\menu.xml";

        /// <summary>
        /// logo图片文件路径
        /// </summary>
        string strImgPath = Application.StartupPath + @"\img\logo.jpg";

        /// <summary>
        /// 程序集名称：K3SP
        /// </summary>
        string strK3SP = Assembly.GetExecutingAssembly().GetName().Name;

        public FormMain()
        {
            InitializeComponent();
            treeViewK3SP.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(treeViewK3SP_NodeMouseDoubleClick);
            tabControlK3SP.MouseDoubleClick += new MouseEventHandler(tabControlK3SP_MouseDoubleClick);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            FormStyle();    // 设定窗体的外观大小
            ShowNavigationBar();    // 显示导航栏的菜单
        }

        /// <summary>
        /// 设定窗体的外观大小
        /// </summary>
        private void FormStyle()
        {
            this.Width = 1000;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            if (File.Exists(strImgPath))
            {
                try
                { splitContainerK3SP.Panel2.BackgroundImage = Image.FromFile(strImgPath); }
                catch
                { MessageBox.Show("打开logo文件错误！", "Error"); }
            }
            else
                MessageBox.Show("logo文件不存在！", "Error");
        }

        /// <summary>
        /// 显示导航栏的菜单
        /// </summary>
        private void ShowNavigationBar()
        {
            XmlDocument xDoc = new XmlDocument();

            if (File.Exists(strXmlPath))    //检验xml文件
            {
                try
                {
                    xDoc.Load(strXmlPath);      //加载xml文件
                    tabControlK3SP.TabPages[0].Text = xDoc.DocumentElement.Attributes["nodeName"].Value;     //初始化第一个TabPage，将菜单nodeName显示到TabPage栏中
                }
                catch
                {
                    MessageBox.Show("打开menu文件错误！", "Error");
                    return;
                }
            }
            else
            {
                MessageBox.Show("menu文件不存在！", "Error");
                return;
            }
            
            treeViewK3SP.Nodes.Clear();     //清空TreeView
            xmlToTreeView(xDoc.DocumentElement, treeViewK3SP.Nodes);    //将XML文件读取到TreeView中
            treeViewK3SP.ExpandAll();  //展开TreeView的所有节点
        }

        /// <summary>
        /// 将XML文件读取到TreeView中
        /// </summary>
        /// <param name="xmlNode">xml的DocumentElement</param>
        /// <param name="nodes">TreeView的TreeNodeCollection</param>
        private void xmlToTreeView(XmlNode xmlNode, TreeNodeCollection nodes)
        {
            foreach (XmlNode node in xmlNode.ChildNodes)    //循环遍历xml文件中当前元素的子元素集合
            {
                TreeNode new_child = new TreeNode();    //定义一个TreeNode新节点对象
                if (node.Attributes["moduleName"] != null)      //xml中此节点是否为模块？
                {
                    new_child.Name = node.Attributes["moduleName"].Value;
                    new_child.Tag = "module";   //在TreeNode的Tag属性中标记为【模块】
                }
                else
                {
                    new_child.Name = node.Attributes["formName"].Value;
                    new_child.Tag = new string[] { "form" };   //在TreeNode的Tag属性中标记为【窗体】
                }

                new_child.Text = node.Attributes["nodeName"].Value;
                nodes.Add(new_child);   //向当前TreeNodeCollection集合中添加当前节点
                xmlToTreeView(node, new_child.Nodes);   //递归
            }
        }

        //双击一个treeView的Node导航菜单
        private void treeViewK3SP_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            int i = 0;
            int count = tabControlK3SP.TabPages.Count;
            //不为模块节点、且有父节点的菜单才可新建选项卡
            if (e.Button == MouseButtons.Left && treeViewK3SP.SelectedNode.Parent != null && treeViewK3SP.SelectedNode.Tag.ToString() != "module")
            {
                //遍历所有已存在的TabPage，是否已打开此TabPage了？如果打开了只选到那个TabPage上，而不创建窗体。
                for (; i < count; i++)
                {
                    if (tabControlK3SP.TabPages[i].Text == treeViewK3SP.SelectedNode.Text)  //根据TabPage名判断
                    {
                        tabControlK3SP.SelectedTab = tabControlK3SP.TabPages[i];
                        break;
                    }
                }
                //遍历到最后一个任然没匹配上，则创建出此TabPage和窗体
                if (i == count)
                {
                    //窗体名称，拼接规则：K3SP+完整模块路径父节点Name+所双击的菜单节点Name
                    string strFormFullName = strK3SP + "." + getCurModulePath(treeViewK3SP.SelectedNode) + "." + treeViewK3SP.SelectedNode.Name;

                    addTabPage(treeViewK3SP.SelectedNode.Text, strFormFullName);   // 新增对应的TabPage

                }
            }
        }

        /// <summary>
        /// 遍历当前node的所有父节点，直到顶层
        /// </summary>
        /// <param name="treeNode">当前节点</param>
        /// <returns>当前节点的完整模块名称</returns>
        string getCurModulePath(TreeNode treeNode)
        {
            string strModule = "", strCurModulePath = "";//当前模块名称，当前完整模块名称
            bool falg = true;
            TreeNode tNode = null;

            if (treeNode.Parent != null)
                tNode = treeNode.Parent;   //当前节点的父节点
            //由近及远遍历，出来的格式为：module2，module1，
            while (falg)
            {
                if (tNode != null)
                {
                    strModule += tNode.Name + ",";
                    tNode = tNode.Parent;
                }
                else
                    falg = false;

            }
            string[] s = strModule.Trim(new char[] { ',' }).Split(new char[] { ',' });
            //颠倒模块名称
            for (int i = s.Length - 1; i >= 0; i--)
                strCurModulePath += s[i] + ".";
            return strCurModulePath.Trim(new char[] { '.' });   //格式为：module1.module2
        }

        /// <summary>
        /// 创建新的TabPage
        /// </summary>
        /// <param name="strTabPageName">TabPage的显示名称</param>
        /// <param name="strFormName">窗体完整名称</param>
        /// <param name="strAssemblyName">程序集名称</param>
        private void addTabPage(string strTabPageName, string strFormFullName)
        {
            TabPage tabPage = new TabPage(strTabPageName);      //新建一个栏目名为strTabPageName的TabPage
            this.tabControlK3SP.Controls.Add(tabPage);      //将新建的TabPage加到tabControlK3SP中
            loadFormToTabPage(tabPage, strFormFullName);    //利用"反射"将窗体嵌入到新创建的tabPage中
            tabControlK3SP.SelectedTab = tabPage;   //即刻选中当前tabPage
        }

        /// <summary>
        /// 利用"反射"将窗体嵌入到新创建的tabPage中
        /// </summary>
        /// <param name="tabPage"></param>
        /// <param name="strFormFullName"></param>
        private void loadFormToTabPage(TabPage tabPage, string strFormFullName)
        {
            //反射
            Type formType = Type.GetType(strFormFullName);
            if (formType != null)//查看反射是否成功
            {
                if (typeof(Form).IsAssignableFrom(formType)) //反射结果是否为窗体Form
                {
                    Form form = (Form)Activator.CreateInstance(formType); //创建反射窗体实例

                    form.TopLevel = false;  //非顶层窗口，即嵌入到tabPage的意思
                    form.FormBorderStyle = FormBorderStyle.None;   //无任何样式，这样看着就像打开一个tabpage，而不是一个窗体
                    form.Parent = tabPage;  //指定窗体的父容器
                    form.Show();
                }
                else
                { MessageBox.Show("指定的类型不能是从Form类型继承", "Error"); }
            }
            else
            { MessageBox.Show("窗体加载失败", "Error"); }
        }

        //双击TabPage头时关闭此TabPage，并close掉窗口（因为TabPage虽然关掉了 但其嵌入的窗体只是Hide而不是Close）
        private void tabControlK3SP_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //初始tab页不能被关闭
            if (e.Button == MouseButtons.Left && tabControlK3SP.SelectedTab.Text != tabControlK3SP.TabPages[0].Text)
            {
                TabPage tp = tabControlK3SP.SelectedTab;
                if (tp != null)
                {
                    foreach (Control i in tp.Controls)  //遍历当前TabPage下的所有控件
                    {
                        if (i is Form)  //如果是窗体控件，则释放资源并关闭
                        {
                            Form f = (Form)i;
                            f.Dispose();
                            f.Close();
                        }
                    }

                    //释放TabPage资源并从TabPages中移除此栏
                    tp.Dispose();
                    tabControlK3SP.TabPages.Remove(tp);
                }
            }
        }
        /// <summary>
        /// 主窗口关闭后，整个程序退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //右击模块节点
        private void treeViewK3SP_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;     //点的不是右键
            if (e.Node.Tag.ToString() != "module") return;  //点击的不是模块菜单
            TreeNode currentNode = this.treeViewK3SP.GetNodeAt(new Point(e.X, e.Y));
            ContextMenuStrip cms = new ContextMenuStrip();

            if (currentNode != null)
            {

                ToolStripMenuItem toolStripMenuItemExpandAll = new ToolStripMenuItem("展开所有节点");
                ToolStripMenuItem toolStripMenuItemExpandChild = new ToolStripMenuItem("展开子节点");
                ToolStripMenuItem toolStripMenuItemMergerAll = new ToolStripMenuItem("收起所有节点");
                ToolStripMenuItem toolStripMenuItemMergerChild = new ToolStripMenuItem("收起子节点");

                toolStripMenuItemExpandAll.Click += new EventHandler(toolStripMenuItemExpandAll_Click);
                toolStripMenuItemExpandChild.Click += new EventHandler(toolStripMenuItemExpandChild_Click);
                toolStripMenuItemMergerAll.Click += new EventHandler(toolStripMenuItemMergerAll_Click);
                toolStripMenuItemMergerChild.Click += new EventHandler(toolStripMenuItemMergerChild_Click);

                cms.Items.Add(toolStripMenuItemExpandAll);
                cms.Items.Add(toolStripMenuItemExpandChild);
                cms.Items.Add(toolStripMenuItemMergerAll);
                cms.Items.Add(toolStripMenuItemMergerChild);
                cms.Show(this.treeViewK3SP, e.X, e.Y);
                this.treeViewK3SP.SelectedNode = currentNode;

            }
        }

        //展开所有节点
        private void toolStripMenuItemExpandAll_Click(object sender, EventArgs e)
        {

        }

        //展开子节点
        private void toolStripMenuItemExpandChild_Click(object sender, EventArgs e)
        {

        }

        //收起所有节点
        private void toolStripMenuItemMergerAll_Click(object sender, EventArgs e)
        {

        }

        //收起子节点
        private void toolStripMenuItemMergerChild_Click(object sender, EventArgs e)
        {

        }

        //右击tabpage
        private void tabControlK3SP_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;     //点的不是右键即返回

            TabPage currentTabPage = null;
            for (int i = 0; i < tabControlK3SP.TabPages.Count; i++)     //遍历所有选项卡
            {
                currentTabPage = tabControlK3SP.TabPages[i];
                if (tabControlK3SP.GetTabRect(i).Contains(new Point(e.X, e.Y)))     //通过鼠标右击的坐标释放在选项卡的边框中来判断右击了那个选项卡
                {
                    tabControlK3SP.SelectedTab = currentTabPage;       //右击的选项卡获取焦点
                    break;
                }
            }       //实现右键选中选项卡

            ContextMenuStrip cms = new ContextMenuStrip();

            if (currentTabPage != null && currentTabPage != tabControlK3SP.TabPages[0])
            {

                ToolStripMenuItem toolStripMenuItemClose = new ToolStripMenuItem("关闭此栏");
                ToolStripMenuItem toolStripMenuItemCloseAll = new ToolStripMenuItem("关闭所有栏");
                ToolStripMenuItem toolStripMenuItemCloseOther = new ToolStripMenuItem("关闭其它栏");
                ToolStripMenuItem toolStripMenuItemCloseLeft = new ToolStripMenuItem("关闭左侧所有栏");
                ToolStripMenuItem toolStripMenuItemCloseRight = new ToolStripMenuItem("关闭右侧所有栏");

                //toolStripMenuItemClose.Click += new EventHandler(toolStripMenuItemClose_Click);
                //toolStripMenuItemCloseAll.Click += new EventHandler(toolStripMenuItemCloseAll_Click);
                //toolStripMenuItemCloseOther.Click += new EventHandler(toolStripMenuItemCloseOther_Click);
                //toolStripMenuItemCloseLeft.Click += new EventHandler(toolStripMenuItemCloseLeft_Click);
                //toolStripMenuItemCloseRight.Click += new EventHandler(toolStripMenuItemCloseRight_Click);

                cms.Items.Add(toolStripMenuItemClose);
                cms.Items.Add(toolStripMenuItemCloseAll);
                cms.Items.Add(toolStripMenuItemCloseOther);
                cms.Items.Add(toolStripMenuItemCloseLeft);
                cms.Items.Add(toolStripMenuItemCloseRight);
                cms.Show(tabControlK3SP, e.X, e.Y);
            }
        }
    }
}
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
using System.Reflection;    //����

namespace K3SP
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// XML�����ļ�·��
        /// </summary>
        string strXmlPath = Application.StartupPath + @"\conf\menu.xml";

        /// <summary>
        /// logoͼƬ�ļ�·��
        /// </summary>
        string strImgPath = Application.StartupPath + @"\img\logo.jpg";

        /// <summary>
        /// �������ƣ�K3SP
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
            FormStyle();    // �趨�������۴�С
            ShowNavigationBar();    // ��ʾ�������Ĳ˵�
        }

        /// <summary>
        /// �趨�������۴�С
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
                { MessageBox.Show("��logo�ļ�����", "Error"); }
            }
            else
                MessageBox.Show("logo�ļ������ڣ�", "Error");
        }

        /// <summary>
        /// ��ʾ�������Ĳ˵�
        /// </summary>
        private void ShowNavigationBar()
        {
            XmlDocument xDoc = new XmlDocument();

            if (File.Exists(strXmlPath))    //����xml�ļ�
            {
                try
                {
                    xDoc.Load(strXmlPath);      //����xml�ļ�
                    tabControlK3SP.TabPages[0].Text = xDoc.DocumentElement.Attributes["nodeName"].Value;     //��ʼ����һ��TabPage�����˵�nodeName��ʾ��TabPage����
                }
                catch
                {
                    MessageBox.Show("��menu�ļ�����", "Error");
                    return;
                }
            }
            else
            {
                MessageBox.Show("menu�ļ������ڣ�", "Error");
                return;
            }
            
            treeViewK3SP.Nodes.Clear();     //���TreeView
            xmlToTreeView(xDoc.DocumentElement, treeViewK3SP.Nodes);    //��XML�ļ���ȡ��TreeView��
            treeViewK3SP.ExpandAll();  //չ��TreeView�����нڵ�
        }

        /// <summary>
        /// ��XML�ļ���ȡ��TreeView��
        /// </summary>
        /// <param name="xmlNode">xml��DocumentElement</param>
        /// <param name="nodes">TreeView��TreeNodeCollection</param>
        private void xmlToTreeView(XmlNode xmlNode, TreeNodeCollection nodes)
        {
            foreach (XmlNode node in xmlNode.ChildNodes)    //ѭ������xml�ļ��е�ǰԪ�ص���Ԫ�ؼ���
            {
                TreeNode new_child = new TreeNode();    //����һ��TreeNode�½ڵ����
                if (node.Attributes["moduleName"] != null)      //xml�д˽ڵ��Ƿ�Ϊģ�飿
                {
                    new_child.Name = node.Attributes["moduleName"].Value;
                    new_child.Tag = "module";   //��TreeNode��Tag�����б��Ϊ��ģ�顿
                }
                else
                {
                    new_child.Name = node.Attributes["formName"].Value;
                    new_child.Tag = new string[] { "form" };   //��TreeNode��Tag�����б��Ϊ�����塿
                }

                new_child.Text = node.Attributes["nodeName"].Value;
                nodes.Add(new_child);   //��ǰTreeNodeCollection��������ӵ�ǰ�ڵ�
                xmlToTreeView(node, new_child.Nodes);   //�ݹ�
            }
        }

        //˫��һ��treeView��Node�����˵�
        private void treeViewK3SP_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            int i = 0;
            int count = tabControlK3SP.TabPages.Count;
            //��Ϊģ��ڵ㡢���и��ڵ�Ĳ˵��ſ��½�ѡ�
            if (e.Button == MouseButtons.Left && treeViewK3SP.SelectedNode.Parent != null && treeViewK3SP.SelectedNode.Tag.ToString() != "module")
            {
                //���������Ѵ��ڵ�TabPage���Ƿ��Ѵ򿪴�TabPage�ˣ��������ֻѡ���Ǹ�TabPage�ϣ������������塣
                for (; i < count; i++)
                {
                    if (tabControlK3SP.TabPages[i].Text == treeViewK3SP.SelectedNode.Text)  //����TabPage���ж�
                    {
                        tabControlK3SP.SelectedTab = tabControlK3SP.TabPages[i];
                        break;
                    }
                }
                //���������һ����Ȼûƥ���ϣ��򴴽�����TabPage�ʹ���
                if (i == count)
                {
                    //�������ƣ�ƴ�ӹ���K3SP+����ģ��·�����ڵ�Name+��˫���Ĳ˵��ڵ�Name
                    string strFormFullName = strK3SP + "." + getCurModulePath(treeViewK3SP.SelectedNode) + "." + treeViewK3SP.SelectedNode.Name;

                    addTabPage(treeViewK3SP.SelectedNode.Text, strFormFullName);   // ������Ӧ��TabPage

                }
            }
        }

        /// <summary>
        /// ������ǰnode�����и��ڵ㣬ֱ������
        /// </summary>
        /// <param name="treeNode">��ǰ�ڵ�</param>
        /// <returns>��ǰ�ڵ������ģ������</returns>
        string getCurModulePath(TreeNode treeNode)
        {
            string strModule = "", strCurModulePath = "";//��ǰģ�����ƣ���ǰ����ģ������
            bool falg = true;
            TreeNode tNode = null;

            if (treeNode.Parent != null)
                tNode = treeNode.Parent;   //��ǰ�ڵ�ĸ��ڵ�
            //�ɽ���Զ�����������ĸ�ʽΪ��module2��module1��
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
            //�ߵ�ģ������
            for (int i = s.Length - 1; i >= 0; i--)
                strCurModulePath += s[i] + ".";
            return strCurModulePath.Trim(new char[] { '.' });   //��ʽΪ��module1.module2
        }

        /// <summary>
        /// �����µ�TabPage
        /// </summary>
        /// <param name="strTabPageName">TabPage����ʾ����</param>
        /// <param name="strFormName">������������</param>
        /// <param name="strAssemblyName">��������</param>
        private void addTabPage(string strTabPageName, string strFormFullName)
        {
            TabPage tabPage = new TabPage(strTabPageName);      //�½�һ����Ŀ��ΪstrTabPageName��TabPage
            this.tabControlK3SP.Controls.Add(tabPage);      //���½���TabPage�ӵ�tabControlK3SP��
            loadFormToTabPage(tabPage, strFormFullName);    //����"����"������Ƕ�뵽�´�����tabPage��
            tabControlK3SP.SelectedTab = tabPage;   //����ѡ�е�ǰtabPage
        }

        /// <summary>
        /// ����"����"������Ƕ�뵽�´�����tabPage��
        /// </summary>
        /// <param name="tabPage"></param>
        /// <param name="strFormFullName"></param>
        private void loadFormToTabPage(TabPage tabPage, string strFormFullName)
        {
            //����
            Type formType = Type.GetType(strFormFullName);
            if (formType != null)//�鿴�����Ƿ�ɹ�
            {
                if (typeof(Form).IsAssignableFrom(formType)) //�������Ƿ�Ϊ����Form
                {
                    Form form = (Form)Activator.CreateInstance(formType); //�������䴰��ʵ��

                    form.TopLevel = false;  //�Ƕ��㴰�ڣ���Ƕ�뵽tabPage����˼
                    form.FormBorderStyle = FormBorderStyle.None;   //���κ���ʽ���������ž����һ��tabpage��������һ������
                    form.Parent = tabPage;  //ָ������ĸ�����
                    form.Show();
                }
                else
                { MessageBox.Show("ָ�������Ͳ����Ǵ�Form���ͼ̳�", "Error"); }
            }
            else
            { MessageBox.Show("�������ʧ��", "Error"); }
        }

        //˫��TabPageͷʱ�رմ�TabPage����close�����ڣ���ΪTabPage��Ȼ�ص��� ����Ƕ��Ĵ���ֻ��Hide������Close��
        private void tabControlK3SP_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //��ʼtabҳ���ܱ��ر�
            if (e.Button == MouseButtons.Left && tabControlK3SP.SelectedTab.Text != tabControlK3SP.TabPages[0].Text)
            {
                TabPage tp = tabControlK3SP.SelectedTab;
                if (tp != null)
                {
                    foreach (Control i in tp.Controls)  //������ǰTabPage�µ����пؼ�
                    {
                        if (i is Form)  //����Ǵ���ؼ������ͷ���Դ���ر�
                        {
                            Form f = (Form)i;
                            f.Dispose();
                            f.Close();
                        }
                    }

                    //�ͷ�TabPage��Դ����TabPages���Ƴ�����
                    tp.Dispose();
                    tabControlK3SP.TabPages.Remove(tp);
                }
            }
        }
        /// <summary>
        /// �����ڹرպ����������˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //�һ�ģ��ڵ�
        private void treeViewK3SP_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;     //��Ĳ����Ҽ�
            if (e.Node.Tag.ToString() != "module") return;  //����Ĳ���ģ��˵�
            TreeNode currentNode = this.treeViewK3SP.GetNodeAt(new Point(e.X, e.Y));
            ContextMenuStrip cms = new ContextMenuStrip();

            if (currentNode != null)
            {

                ToolStripMenuItem toolStripMenuItemExpandAll = new ToolStripMenuItem("չ�����нڵ�");
                ToolStripMenuItem toolStripMenuItemExpandChild = new ToolStripMenuItem("չ���ӽڵ�");
                ToolStripMenuItem toolStripMenuItemMergerAll = new ToolStripMenuItem("�������нڵ�");
                ToolStripMenuItem toolStripMenuItemMergerChild = new ToolStripMenuItem("�����ӽڵ�");

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

        //չ�����нڵ�
        private void toolStripMenuItemExpandAll_Click(object sender, EventArgs e)
        {

        }

        //չ���ӽڵ�
        private void toolStripMenuItemExpandChild_Click(object sender, EventArgs e)
        {

        }

        //�������нڵ�
        private void toolStripMenuItemMergerAll_Click(object sender, EventArgs e)
        {

        }

        //�����ӽڵ�
        private void toolStripMenuItemMergerChild_Click(object sender, EventArgs e)
        {

        }

        //�һ�tabpage
        private void tabControlK3SP_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;     //��Ĳ����Ҽ�������

            TabPage currentTabPage = null;
            for (int i = 0; i < tabControlK3SP.TabPages.Count; i++)     //��������ѡ�
            {
                currentTabPage = tabControlK3SP.TabPages[i];
                if (tabControlK3SP.GetTabRect(i).Contains(new Point(e.X, e.Y)))     //ͨ������һ��������ͷ���ѡ��ı߿������ж��һ����Ǹ�ѡ�
                {
                    tabControlK3SP.SelectedTab = currentTabPage;       //�һ���ѡ���ȡ����
                    break;
                }
            }       //ʵ���Ҽ�ѡ��ѡ�

            ContextMenuStrip cms = new ContextMenuStrip();

            if (currentTabPage != null && currentTabPage != tabControlK3SP.TabPages[0])
            {

                ToolStripMenuItem toolStripMenuItemClose = new ToolStripMenuItem("�رմ���");
                ToolStripMenuItem toolStripMenuItemCloseAll = new ToolStripMenuItem("�ر�������");
                ToolStripMenuItem toolStripMenuItemCloseOther = new ToolStripMenuItem("�ر�������");
                ToolStripMenuItem toolStripMenuItemCloseLeft = new ToolStripMenuItem("�ر����������");
                ToolStripMenuItem toolStripMenuItemCloseRight = new ToolStripMenuItem("�ر��Ҳ�������");

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
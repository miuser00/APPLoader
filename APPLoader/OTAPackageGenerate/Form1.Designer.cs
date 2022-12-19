
namespace OTAPackageGenerate
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_Publish = new System.Windows.Forms.Button();
            this.btn_GeneratePackage = new System.Windows.Forms.Button();
            this.btn_SelectRootFolder = new System.Windows.Forms.Button();
            this.btn_GenerateFiles = new System.Windows.Forms.Button();
            this.btn_EditMDFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tv_PacakItems = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tv_ExcludeItems = new System.Windows.Forms.TreeView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.txt_AppTitle = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_BrowserFile = new System.Windows.Forms.Button();
            this.txt_AppFile = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_updateXMLURL = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_serverFolderURL = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_MdFileURL = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_7zfileURL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_packageFileName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_sub = new System.Windows.Forms.TextBox();
            this.txt_ver = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadpannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel10 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btn_Publish);
            this.panel3.Controls.Add(this.btn_GeneratePackage);
            this.panel3.Controls.Add(this.btn_SelectRootFolder);
            this.panel3.Controls.Add(this.btn_GenerateFiles);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 448);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(552, 63);
            this.panel3.TabIndex = 13;
            // 
            // btn_Publish
            // 
            this.btn_Publish.Enabled = false;
            this.btn_Publish.Location = new System.Drawing.Point(416, 10);
            this.btn_Publish.Name = "btn_Publish";
            this.btn_Publish.Size = new System.Drawing.Size(118, 42);
            this.btn_Publish.TabIndex = 14;
            this.btn_Publish.Text = "发布";
            this.btn_Publish.UseVisualStyleBackColor = true;
            this.btn_Publish.Click += new System.EventHandler(this.btn_Publish_Click);
            // 
            // btn_GeneratePackage
            // 
            this.btn_GeneratePackage.Enabled = false;
            this.btn_GeneratePackage.Location = new System.Drawing.Point(278, 10);
            this.btn_GeneratePackage.Name = "btn_GeneratePackage";
            this.btn_GeneratePackage.Size = new System.Drawing.Size(118, 42);
            this.btn_GeneratePackage.TabIndex = 10;
            this.btn_GeneratePackage.Text = "生成安装文件";
            this.btn_GeneratePackage.UseVisualStyleBackColor = true;
            this.btn_GeneratePackage.Click += new System.EventHandler(this.btn_GeneratePackage_Click);
            // 
            // btn_SelectRootFolder
            // 
            this.btn_SelectRootFolder.Location = new System.Drawing.Point(10, 10);
            this.btn_SelectRootFolder.Name = "btn_SelectRootFolder";
            this.btn_SelectRootFolder.Size = new System.Drawing.Size(110, 42);
            this.btn_SelectRootFolder.TabIndex = 13;
            this.btn_SelectRootFolder.Text = "选择安装程序目录";
            this.btn_SelectRootFolder.UseVisualStyleBackColor = true;
            this.btn_SelectRootFolder.Click += new System.EventHandler(this.btn_SelectRootFolder_Click);
            // 
            // btn_GenerateFiles
            // 
            this.btn_GenerateFiles.Enabled = false;
            this.btn_GenerateFiles.Location = new System.Drawing.Point(139, 10);
            this.btn_GenerateFiles.Name = "btn_GenerateFiles";
            this.btn_GenerateFiles.Size = new System.Drawing.Size(118, 42);
            this.btn_GenerateFiles.TabIndex = 11;
            this.btn_GenerateFiles.Text = "生成安装配置";
            this.btn_GenerateFiles.UseVisualStyleBackColor = true;
            this.btn_GenerateFiles.Click += new System.EventHandler(this.btn_GenerateFiles_Click);
            // 
            // btn_EditMDFile
            // 
            this.btn_EditMDFile.Enabled = false;
            this.btn_EditMDFile.Location = new System.Drawing.Point(416, 70);
            this.btn_EditMDFile.Name = "btn_EditMDFile";
            this.btn_EditMDFile.Size = new System.Drawing.Size(106, 102);
            this.btn_EditMDFile.TabIndex = 12;
            this.btn_EditMDFile.Text = "编辑说明档";
            this.btn_EditMDFile.UseVisualStyleBackColor = true;
            this.btn_EditMDFile.Click += new System.EventHandler(this.btn_EditMDFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Controls.Add(this.tv_PacakItems);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(8, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 514);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "应用程序文件及目录：";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(3, 448);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(319, 63);
            this.panel5.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选中所有在安装压缩包里要包括的文件";
            // 
            // tv_PacakItems
            // 
            this.tv_PacakItems.CheckBoxes = true;
            this.tv_PacakItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_PacakItems.Font = new System.Drawing.Font("新宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tv_PacakItems.Location = new System.Drawing.Point(3, 17);
            this.tv_PacakItems.Name = "tv_PacakItems";
            this.tv_PacakItems.Size = new System.Drawing.Size(319, 494);
            this.tv_PacakItems.TabIndex = 0;
            this.tv_PacakItems.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_PacakItems_AfterCheck);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1259, 8);
            this.panel2.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel6);
            this.groupBox2.Controls.Add(this.tv_ExcludeItems);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(333, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 514);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "排除目录及文件：";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(3, 448);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(344, 63);
            this.panel6.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(281, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "请选中在安装过程中跳过更新的文件，如配置文件等";
            // 
            // tv_ExcludeItems
            // 
            this.tv_ExcludeItems.CheckBoxes = true;
            this.tv_ExcludeItems.Font = new System.Drawing.Font("新宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tv_ExcludeItems.Location = new System.Drawing.Point(3, 17);
            this.tv_ExcludeItems.Name = "tv_ExcludeItems";
            this.tv_ExcludeItems.Size = new System.Drawing.Size(344, 618);
            this.tv_ExcludeItems.TabIndex = 0;
            this.tv_ExcludeItems.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_ExcludeItems_AfterCheck);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtb_log);
            this.groupBox3.Controls.Add(this.panel8);
            this.groupBox3.Controls.Add(this.panel9);
            this.groupBox3.Controls.Add(this.panel3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(691, 33);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(558, 514);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log：";
            // 
            // rtb_log
            // 
            this.rtb_log.BackColor = System.Drawing.Color.White;
            this.rtb_log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_log.Location = new System.Drawing.Point(3, 17);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.ReadOnly = true;
            this.rtb_log.Size = new System.Drawing.Size(552, 183);
            this.rtb_log.TabIndex = 14;
            this.rtb_log.Text = "";
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.txt_AppTitle);
            this.panel8.Controls.Add(this.label11);
            this.panel8.Controls.Add(this.btn_BrowserFile);
            this.panel8.Controls.Add(this.txt_AppFile);
            this.panel8.Controls.Add(this.label10);
            this.panel8.Controls.Add(this.txt_updateXMLURL);
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.txt_serverFolderURL);
            this.panel8.Controls.Add(this.label8);
            this.panel8.Controls.Add(this.txt_MdFileURL);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.btn_EditMDFile);
            this.panel8.Controls.Add(this.txt_7zfileURL);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.txt_packageFileName);
            this.panel8.Controls.Add(this.label5);
            this.panel8.Controls.Add(this.label4);
            this.panel8.Controls.Add(this.txt_sub);
            this.panel8.Controls.Add(this.txt_ver);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(3, 200);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(552, 240);
            this.panel8.TabIndex = 15;
            // 
            // txt_AppTitle
            // 
            this.txt_AppTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_AppTitle.Location = new System.Drawing.Point(108, 68);
            this.txt_AppTitle.Name = "txt_AppTitle";
            this.txt_AppTitle.Size = new System.Drawing.Size(288, 21);
            this.txt_AppTitle.TabIndex = 23;
            this.txt_AppTitle.Text = "Lua-board Shop";
            this.txt_AppTitle.TextChanged += new System.EventHandler(this.txt_AppTitle_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(56, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 22;
            this.label11.Text = "程序名：";
            // 
            // btn_BrowserFile
            // 
            this.btn_BrowserFile.Enabled = false;
            this.btn_BrowserFile.Location = new System.Drawing.Point(346, 39);
            this.btn_BrowserFile.Name = "btn_BrowserFile";
            this.btn_BrowserFile.Size = new System.Drawing.Size(50, 21);
            this.btn_BrowserFile.TabIndex = 19;
            this.btn_BrowserFile.Text = "浏览";
            this.btn_BrowserFile.UseVisualStyleBackColor = true;
            this.btn_BrowserFile.Click += new System.EventHandler(this.btn_BrowserFile_Click);
            // 
            // txt_AppFile
            // 
            this.txt_AppFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_AppFile.Location = new System.Drawing.Point(108, 39);
            this.txt_AppFile.Name = "txt_AppFile";
            this.txt_AppFile.Size = new System.Drawing.Size(221, 21);
            this.txt_AppFile.TabIndex = 18;
            this.txt_AppFile.Text = "bin32\\app.exe";
            this.txt_AppFile.TextChanged += new System.EventHandler(this.txt_AppFile_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 17;
            this.label10.Text = "程序启动路径：";
            // 
            // txt_updateXMLURL
            // 
            this.txt_updateXMLURL.Location = new System.Drawing.Point(108, 151);
            this.txt_updateXMLURL.Name = "txt_updateXMLURL";
            this.txt_updateXMLURL.ReadOnly = true;
            this.txt_updateXMLURL.Size = new System.Drawing.Size(288, 21);
            this.txt_updateXMLURL.TabIndex = 16;
            this.txt_updateXMLURL.Text = "https://file.miuser.net/mide/update.xml";
            this.txt_updateXMLURL.TextChanged += new System.EventHandler(this.txt_updateXMLURL_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "配置文件url：";
            // 
            // txt_serverFolderURL
            // 
            this.txt_serverFolderURL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_serverFolderURL.Location = new System.Drawing.Point(108, 97);
            this.txt_serverFolderURL.Name = "txt_serverFolderURL";
            this.txt_serverFolderURL.Size = new System.Drawing.Size(288, 21);
            this.txt_serverFolderURL.TabIndex = 14;
            this.txt_serverFolderURL.Text = "https://file.miuser.net/mide";
            this.txt_serverFolderURL.TextChanged += new System.EventHandler(this.txt_serverFolderURL_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "服务器路径：";
            // 
            // txt_MdFileURL
            // 
            this.txt_MdFileURL.Location = new System.Drawing.Point(108, 205);
            this.txt_MdFileURL.Name = "txt_MdFileURL";
            this.txt_MdFileURL.ReadOnly = true;
            this.txt_MdFileURL.Size = new System.Drawing.Size(414, 21);
            this.txt_MdFileURL.TabIndex = 9;
            this.txt_MdFileURL.Text = "https://file.miuser.net/mide/update.md";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "说明文件Url：";
            // 
            // txt_7zfileURL
            // 
            this.txt_7zfileURL.Location = new System.Drawing.Point(108, 178);
            this.txt_7zfileURL.Name = "txt_7zfileURL";
            this.txt_7zfileURL.ReadOnly = true;
            this.txt_7zfileURL.Size = new System.Drawing.Size(414, 21);
            this.txt_7zfileURL.TabIndex = 7;
            this.txt_7zfileURL.Text = "https://file.miuser.net/mide/mide_ver0.01.pack";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "安装包Url：";
            // 
            // txt_packageFileName
            // 
            this.txt_packageFileName.Location = new System.Drawing.Point(108, 124);
            this.txt_packageFileName.Name = "txt_packageFileName";
            this.txt_packageFileName.ReadOnly = true;
            this.txt_packageFileName.Size = new System.Drawing.Size(288, 21);
            this.txt_packageFileName.TabIndex = 5;
            this.txt_packageFileName.Text = "mide_ver0.01.pack";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "安装包文件名：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "-";
            // 
            // txt_sub
            // 
            this.txt_sub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_sub.Location = new System.Drawing.Point(245, 13);
            this.txt_sub.Name = "txt_sub";
            this.txt_sub.Size = new System.Drawing.Size(23, 21);
            this.txt_sub.TabIndex = 2;
            this.txt_sub.TextChanged += new System.EventHandler(this.txt_sub_TextChanged);
            // 
            // txt_ver
            // 
            this.txt_ver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_ver.Location = new System.Drawing.Point(108, 13);
            this.txt_ver.Name = "txt_ver";
            this.txt_ver.Size = new System.Drawing.Size(117, 21);
            this.txt_ver.TabIndex = 1;
            this.txt_ver.Text = "0.01";
            this.txt_ver.TextChanged += new System.EventHandler(this.txt_ver_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "版本号：";
            // 
            // panel9
            // 
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(3, 440);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(552, 8);
            this.panel9.TabIndex = 18;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 33);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(8, 514);
            this.panel4.TabIndex = 14;
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 547);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1259, 10);
            this.panel7.TabIndex = 17;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 695);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1259, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(683, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(8, 514);
            this.panel1.TabIndex = 13;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.configToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1259, 25);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F2)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.newToolStripMenuItem.Text = "文件(&F)";
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetupToolStripMenuItem,
            this.uploadpannelToolStripMenuItem});
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.configToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.configToolStripMenuItem.Text = "设置(&S)";
            // 
            // SetupToolStripMenuItem
            // 
            this.SetupToolStripMenuItem.Name = "SetupToolStripMenuItem";
            this.SetupToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.SetupToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.SetupToolStripMenuItem.Text = "参数设置";
            this.SetupToolStripMenuItem.Click += new System.EventHandler(this.SetupToolStripMenuItem_Click);
            // 
            // uploadpannelToolStripMenuItem
            // 
            this.uploadpannelToolStripMenuItem.Name = "uploadpannelToolStripMenuItem";
            this.uploadpannelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.uploadpannelToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.uploadpannelToolStripMenuItem.Text = "上传面板";
            this.uploadpannelToolStripMenuItem.Click += new System.EventHandler(this.UploadPanelToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.helpToolStripMenuItem.Text = "帮助(&H)";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.AboutToolStripMenuItem.Text = "关于";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.dataGridView1);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(0, 557);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1259, 138);
            this.panel10.TabIndex = 25;
            this.panel10.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1259, 138);
            this.dataGridView1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1259, 717);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "OTA Package Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_GeneratePackage;
        private System.Windows.Forms.Button btn_EditMDFile;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_SelectRootFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView tv_PacakItems;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView tv_ExcludeItems;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rtb_log;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btn_Publish;
        private System.Windows.Forms.TextBox txt_MdFileURL;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_7zfileURL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_packageFileName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_sub;
        private System.Windows.Forms.TextBox txt_ver;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_updateXMLURL;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_serverFolderURL;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btn_GenerateFiles;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadpannelToolStripMenuItem;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_AppFile;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_BrowserFile;
        private System.Windows.Forms.TextBox txt_AppTitle;
        private System.Windows.Forms.Label label11;
    }
}


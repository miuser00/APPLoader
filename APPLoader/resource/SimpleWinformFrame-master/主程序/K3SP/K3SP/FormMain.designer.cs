namespace K3SP
{
    partial class FormMain
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStripK3SP = new System.Windows.Forms.MenuStrip();
            this.statusStripK3SP = new System.Windows.Forms.StatusStrip();
            this.tabControlK3SP = new System.Windows.Forms.TabControl();
            this.tabPageK3SP = new System.Windows.Forms.TabPage();
            this.splitContainerK3SP = new System.Windows.Forms.SplitContainer();
            this.treeViewK3SP = new System.Windows.Forms.TreeView();
            this.tabControlK3SP.SuspendLayout();
            this.tabPageK3SP.SuspendLayout();
            this.splitContainerK3SP.Panel1.SuspendLayout();
            this.splitContainerK3SP.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripK3SP
            // 
            this.menuStripK3SP.Location = new System.Drawing.Point(0, 0);
            this.menuStripK3SP.Name = "menuStripK3SP";
            this.menuStripK3SP.Size = new System.Drawing.Size(349, 24);
            this.menuStripK3SP.TabIndex = 0;
            this.menuStripK3SP.Text = "menuStrip1";
            // 
            // statusStripK3SP
            // 
            this.statusStripK3SP.Location = new System.Drawing.Point(0, 260);
            this.statusStripK3SP.Name = "statusStripK3SP";
            this.statusStripK3SP.Size = new System.Drawing.Size(349, 22);
            this.statusStripK3SP.TabIndex = 1;
            this.statusStripK3SP.Text = "statusStrip1";
            // 
            // tabControlK3SP
            // 
            this.tabControlK3SP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlK3SP.Controls.Add(this.tabPageK3SP);
            this.tabControlK3SP.Location = new System.Drawing.Point(0, 27);
            this.tabControlK3SP.Name = "tabControlK3SP";
            this.tabControlK3SP.SelectedIndex = 0;
            this.tabControlK3SP.Size = new System.Drawing.Size(349, 230);
            this.tabControlK3SP.TabIndex = 3;
            this.tabControlK3SP.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabControlK3SP_MouseDoubleClick);
            this.tabControlK3SP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControlK3SP_MouseClick);
            // 
            // tabPageK3SP
            // 
            this.tabPageK3SP.Controls.Add(this.splitContainerK3SP);
            this.tabPageK3SP.Location = new System.Drawing.Point(4, 21);
            this.tabPageK3SP.Name = "tabPageK3SP";
            this.tabPageK3SP.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageK3SP.Size = new System.Drawing.Size(341, 205);
            this.tabPageK3SP.TabIndex = 0;
            this.tabPageK3SP.Text = "【菜单】";
            this.tabPageK3SP.UseVisualStyleBackColor = true;
            // 
            // splitContainerK3SP
            // 
            this.splitContainerK3SP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerK3SP.Location = new System.Drawing.Point(3, 3);
            this.splitContainerK3SP.Name = "splitContainerK3SP";
            // 
            // splitContainerK3SP.Panel1
            // 
            this.splitContainerK3SP.Panel1.Controls.Add(this.treeViewK3SP);
            // 
            // splitContainerK3SP.Panel2
            // 
            this.splitContainerK3SP.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.splitContainerK3SP.Size = new System.Drawing.Size(335, 199);
            this.splitContainerK3SP.SplitterDistance = 48;
            this.splitContainerK3SP.TabIndex = 3;
            // 
            // treeViewK3SP
            // 
            this.treeViewK3SP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewK3SP.Location = new System.Drawing.Point(0, 0);
            this.treeViewK3SP.Name = "treeViewK3SP";
            this.treeViewK3SP.Size = new System.Drawing.Size(48, 199);
            this.treeViewK3SP.TabIndex = 2;
            this.treeViewK3SP.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewK3SP_NodeMouseDoubleClick);
            this.treeViewK3SP.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewK3SP_NodeMouseClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 282);
            this.Controls.Add(this.tabControlK3SP);
            this.Controls.Add(this.statusStripK3SP);
            this.Controls.Add(this.menuStripK3SP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripK3SP;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "K3补充程序";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.tabControlK3SP.ResumeLayout(false);
            this.tabPageK3SP.ResumeLayout(false);
            this.splitContainerK3SP.Panel1.ResumeLayout(false);
            this.splitContainerK3SP.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripK3SP;
        private System.Windows.Forms.StatusStrip statusStripK3SP;
        private System.Windows.Forms.TabControl tabControlK3SP;
        private System.Windows.Forms.TabPage tabPageK3SP;
        private System.Windows.Forms.SplitContainer splitContainerK3SP;
        private System.Windows.Forms.TreeView treeViewK3SP;
    }
}


namespace UpdateListBuilder
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
            this.labelUpDate = new System.Windows.Forms.Label();
            this.labelVer = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.listViewUpdateList = new System.Windows.Forms.ListView();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.buttonRead = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelUpDate
            // 
            this.labelUpDate.AutoSize = true;
            this.labelUpDate.Location = new System.Drawing.Point(12, 9);
            this.labelUpDate.Name = "labelUpDate";
            this.labelUpDate.Size = new System.Drawing.Size(65, 12);
            this.labelUpDate.TabIndex = 0;
            this.labelUpDate.Text = "更新日期：";
            // 
            // labelVer
            // 
            this.labelVer.AutoSize = true;
            this.labelVer.Location = new System.Drawing.Point(156, 9);
            this.labelVer.Name = "labelVer";
            this.labelVer.Size = new System.Drawing.Size(65, 12);
            this.labelVer.TabIndex = 1;
            this.labelVer.Text = "更新版本：";
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(300, 9);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(101, 12);
            this.labelSize.TabIndex = 2;
            this.labelSize.Text = "更新文件总大小：";
            // 
            // listViewUpdateList
            // 
            this.listViewUpdateList.FullRowSelect = true;
            this.listViewUpdateList.GridLines = true;
            this.listViewUpdateList.Location = new System.Drawing.Point(14, 24);
            this.listViewUpdateList.Name = "listViewUpdateList";
            this.listViewUpdateList.Size = new System.Drawing.Size(480, 198);
            this.listViewUpdateList.TabIndex = 3;
            this.listViewUpdateList.UseCompatibleStateImageBehavior = false;
            this.listViewUpdateList.View = System.Windows.Forms.View.Details;
            // 
            // buttonBuild
            // 
            this.buttonBuild.Location = new System.Drawing.Point(390, 228);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(104, 23);
            this.buttonBuild.TabIndex = 4;
            this.buttonBuild.Text = "生成更新列表";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
            // 
            // buttonRead
            // 
            this.buttonRead.Location = new System.Drawing.Point(12, 228);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(108, 23);
            this.buttonRead.TabIndex = 4;
            this.buttonRead.Text = "刷新更新信息";
            this.buttonRead.UseVisualStyleBackColor = true;
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 263);
            this.Controls.Add(this.buttonRead);
            this.Controls.Add(this.buttonBuild);
            this.Controls.Add(this.listViewUpdateList);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.labelVer);
            this.Controls.Add(this.labelUpDate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新列表";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUpDate;
        private System.Windows.Forms.Label labelVer;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.ListView listViewUpdateList;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.Button buttonRead;
    }
}


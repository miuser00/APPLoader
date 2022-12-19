namespace AutoUpdater
{
    partial class FormUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUpdate));
            this.panelTks = new System.Windows.Forms.Panel();
            this.labelTks = new System.Windows.Forms.Label();
            this.groupBoxFGX3 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panelUpdate = new System.Windows.Forms.Panel();
            this.progressBarDownFile = new System.Windows.Forms.ProgressBar();
            this.labelList = new System.Windows.Forms.Label();
            this.labelDownload = new System.Windows.Forms.Label();
            this.groupBoxFGX2 = new System.Windows.Forms.GroupBox();
            this.progressBarState = new System.Windows.Forms.ProgressBar();
            this.labelState = new System.Windows.Forms.Label();
            this.lvUpdateList = new System.Windows.Forms.ListView();
            this.chFileName = new System.Windows.Forms.ColumnHeader();
            this.chVersion = new System.Windows.Forms.ColumnHeader();
            this.chProgress = new System.Windows.Forms.ColumnHeader();
            this.groupBoxFGX1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxUpdate = new System.Windows.Forms.PictureBox();
            this.panelTks.SuspendLayout();
            this.panelUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUpdate)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTks
            // 
            this.panelTks.Controls.Add(this.labelTks);
            this.panelTks.Controls.Add(this.groupBoxFGX3);
            this.panelTks.Location = new System.Drawing.Point(15, 265);
            this.panelTks.Name = "panelTks";
            this.panelTks.Size = new System.Drawing.Size(112, 24);
            this.panelTks.TabIndex = 11;
            // 
            // labelTks
            // 
            this.labelTks.Location = new System.Drawing.Point(4, 6);
            this.labelTks.Name = "labelTks";
            this.labelTks.Size = new System.Drawing.Size(106, 13);
            this.labelTks.TabIndex = 9;
            this.labelTks.Text = "感谢使用自动更新";
            // 
            // groupBoxFGX3
            // 
            this.groupBoxFGX3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxFGX3.Location = new System.Drawing.Point(0, 22);
            this.groupBoxFGX3.Name = "groupBoxFGX3";
            this.groupBoxFGX3.Size = new System.Drawing.Size(112, 2);
            this.groupBoxFGX3.TabIndex = 7;
            this.groupBoxFGX3.TabStop = false;
            this.groupBoxFGX3.Text = "groupBox2";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(327, 265);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 24);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(231, 265);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 24);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "更新(&U)";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // panelUpdate
            // 
            this.panelUpdate.Controls.Add(this.progressBarDownFile);
            this.panelUpdate.Controls.Add(this.labelList);
            this.panelUpdate.Controls.Add(this.labelDownload);
            this.panelUpdate.Controls.Add(this.groupBoxFGX2);
            this.panelUpdate.Controls.Add(this.progressBarState);
            this.panelUpdate.Controls.Add(this.labelState);
            this.panelUpdate.Controls.Add(this.lvUpdateList);
            this.panelUpdate.Controls.Add(this.groupBoxFGX1);
            this.panelUpdate.Location = new System.Drawing.Point(127, 9);
            this.panelUpdate.Name = "panelUpdate";
            this.panelUpdate.Size = new System.Drawing.Size(280, 240);
            this.panelUpdate.TabIndex = 7;
            // 
            // progressBarDownFile
            // 
            this.progressBarDownFile.Location = new System.Drawing.Point(3, 220);
            this.progressBarDownFile.Name = "progressBarDownFile";
            this.progressBarDownFile.Size = new System.Drawing.Size(272, 13);
            this.progressBarDownFile.TabIndex = 15;
            // 
            // labelList
            // 
            this.labelList.Location = new System.Drawing.Point(16, 7);
            this.labelList.Name = "labelList";
            this.labelList.Size = new System.Drawing.Size(136, 16);
            this.labelList.TabIndex = 9;
            this.labelList.Text = "以下为更新文件列表";
            // 
            // labelDownload
            // 
            this.labelDownload.AutoSize = true;
            this.labelDownload.Location = new System.Drawing.Point(1, 204);
            this.labelDownload.Name = "labelDownload";
            this.labelDownload.Size = new System.Drawing.Size(65, 12);
            this.labelDownload.TabIndex = 14;
            this.labelDownload.Text = "正在下载：";
            // 
            // groupBoxFGX2
            // 
            this.groupBoxFGX2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxFGX2.Location = new System.Drawing.Point(0, 238);
            this.groupBoxFGX2.Name = "groupBoxFGX2";
            this.groupBoxFGX2.Size = new System.Drawing.Size(280, 2);
            this.groupBoxFGX2.TabIndex = 7;
            this.groupBoxFGX2.TabStop = false;
            this.groupBoxFGX2.Text = "groupBox2";
            // 
            // progressBarState
            // 
            this.progressBarState.Location = new System.Drawing.Point(3, 184);
            this.progressBarState.Name = "progressBarState";
            this.progressBarState.Size = new System.Drawing.Size(272, 13);
            this.progressBarState.TabIndex = 13;
            // 
            // labelState
            // 
            this.labelState.AutoSize = true;
            this.labelState.Location = new System.Drawing.Point(1, 168);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(77, 12);
            this.labelState.TabIndex = 12;
            this.labelState.Text = "更新进度 0/0";
            // 
            // lvUpdateList
            // 
            this.lvUpdateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFileName,
            this.chVersion,
            this.chProgress});
            this.lvUpdateList.Location = new System.Drawing.Point(3, 33);
            this.lvUpdateList.Name = "lvUpdateList";
            this.lvUpdateList.Size = new System.Drawing.Size(272, 132);
            this.lvUpdateList.TabIndex = 6;
            this.lvUpdateList.UseCompatibleStateImageBehavior = false;
            this.lvUpdateList.View = System.Windows.Forms.View.Details;
            // 
            // chFileName
            // 
            this.chFileName.Text = "组件名";
            this.chFileName.Width = 123;
            // 
            // chVersion
            // 
            this.chVersion.Text = "版本号";
            this.chVersion.Width = 98;
            // 
            // chProgress
            // 
            this.chProgress.Text = "进度";
            this.chProgress.Width = 47;
            // 
            // groupBoxFGX1
            // 
            this.groupBoxFGX1.Location = new System.Drawing.Point(0, 17);
            this.groupBoxFGX1.Name = "groupBoxFGX1";
            this.groupBoxFGX1.Size = new System.Drawing.Size(280, 8);
            this.groupBoxFGX1.TabIndex = 1;
            this.groupBoxFGX1.TabStop = false;
            // 
            // pictureBoxUpdate
            // 
            this.pictureBoxUpdate.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxUpdate.Image")));
            this.pictureBoxUpdate.Location = new System.Drawing.Point(15, 9);
            this.pictureBoxUpdate.Name = "pictureBoxUpdate";
            this.pictureBoxUpdate.Size = new System.Drawing.Size(96, 240);
            this.pictureBoxUpdate.TabIndex = 6;
            this.pictureBoxUpdate.TabStop = false;
            // 
            // FormUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 301);
            this.Controls.Add(this.panelTks);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.panelUpdate);
            this.Controls.Add(this.pictureBoxUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动更新";
            this.Load += new System.EventHandler(this.FormUpdate_Load);
            this.panelTks.ResumeLayout(false);
            this.panelUpdate.ResumeLayout(false);
            this.panelUpdate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUpdate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTks;
        private System.Windows.Forms.GroupBox groupBoxFGX3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel panelUpdate;
        private System.Windows.Forms.Label labelList;
        private System.Windows.Forms.GroupBox groupBoxFGX2;
        private System.Windows.Forms.ListView lvUpdateList;
        private System.Windows.Forms.ColumnHeader chFileName;
        private System.Windows.Forms.ColumnHeader chVersion;
        private System.Windows.Forms.ColumnHeader chProgress;
        private System.Windows.Forms.GroupBox groupBoxFGX1;
        private System.Windows.Forms.PictureBox pictureBoxUpdate;
        private System.Windows.Forms.Label labelTks;
        private System.Windows.Forms.ProgressBar progressBarDownFile;
        private System.Windows.Forms.Label labelDownload;
        private System.Windows.Forms.ProgressBar progressBarState;
        private System.Windows.Forms.Label labelState;


    }
}


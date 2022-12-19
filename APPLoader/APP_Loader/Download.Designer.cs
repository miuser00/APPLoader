
namespace AppLoader
{
    partial class DownloadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadForm));
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_CheckLatestVersion = new System.Windows.Forms.Button();
            this.btn_DownloadPackage = new System.Windows.Forms.Button();
            this.btn_Extract = new System.Windows.Forms.Button();
            this.btn_VerifyFile = new System.Windows.Forms.Button();
            this.btn_SaveSkippedFile = new System.Windows.Forms.Button();
            this.btn_CreateOKFlag = new System.Windows.Forms.Button();
            this.btn_Run = new System.Windows.Forms.Button();
            this.btn_UpdateXML = new System.Windows.Forms.Button();
            this.panel_buttons = new System.Windows.Forms.Panel();
            this.btn_RunExternalApp = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtb_log
            // 
            this.rtb_log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_log.Enabled = false;
            this.rtb_log.Location = new System.Drawing.Point(0, 0);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.Size = new System.Drawing.Size(732, 519);
            this.rtb_log.TabIndex = 0;
            this.rtb_log.Text = "";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rtb_log);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(734, 521);
            this.panel1.TabIndex = 1;
            // 
            // btn_CheckLatestVersion
            // 
            this.btn_CheckLatestVersion.Location = new System.Drawing.Point(33, 229);
            this.btn_CheckLatestVersion.Name = "btn_CheckLatestVersion";
            this.btn_CheckLatestVersion.Size = new System.Drawing.Size(113, 32);
            this.btn_CheckLatestVersion.TabIndex = 2;
            this.btn_CheckLatestVersion.Text = "检查最新版本";
            this.btn_CheckLatestVersion.UseVisualStyleBackColor = true;
            this.btn_CheckLatestVersion.Click += new System.EventHandler(this.btn_CheckLatestVersion_Click);
            // 
            // btn_DownloadPackage
            // 
            this.btn_DownloadPackage.Location = new System.Drawing.Point(33, 267);
            this.btn_DownloadPackage.Name = "btn_DownloadPackage";
            this.btn_DownloadPackage.Size = new System.Drawing.Size(113, 32);
            this.btn_DownloadPackage.TabIndex = 3;
            this.btn_DownloadPackage.Text = "下载文件";
            this.btn_DownloadPackage.UseVisualStyleBackColor = true;
            this.btn_DownloadPackage.Click += new System.EventHandler(this.btn_DownloadPackage_Click);
            // 
            // btn_Extract
            // 
            this.btn_Extract.Location = new System.Drawing.Point(33, 343);
            this.btn_Extract.Name = "btn_Extract";
            this.btn_Extract.Size = new System.Drawing.Size(113, 32);
            this.btn_Extract.TabIndex = 4;
            this.btn_Extract.Text = "解压缩包";
            this.btn_Extract.UseVisualStyleBackColor = true;
            this.btn_Extract.Click += new System.EventHandler(this.btn_Extract_Click);
            // 
            // btn_VerifyFile
            // 
            this.btn_VerifyFile.Location = new System.Drawing.Point(33, 305);
            this.btn_VerifyFile.Name = "btn_VerifyFile";
            this.btn_VerifyFile.Size = new System.Drawing.Size(113, 32);
            this.btn_VerifyFile.TabIndex = 5;
            this.btn_VerifyFile.Text = "校验文件";
            this.btn_VerifyFile.UseVisualStyleBackColor = true;
            this.btn_VerifyFile.Click += new System.EventHandler(this.btn_VerifyFile_Click);
            // 
            // btn_SaveSkippedFile
            // 
            this.btn_SaveSkippedFile.Location = new System.Drawing.Point(33, 381);
            this.btn_SaveSkippedFile.Name = "btn_SaveSkippedFile";
            this.btn_SaveSkippedFile.Size = new System.Drawing.Size(113, 32);
            this.btn_SaveSkippedFile.TabIndex = 7;
            this.btn_SaveSkippedFile.Text = "保存跳过的文件";
            this.btn_SaveSkippedFile.UseVisualStyleBackColor = true;
            this.btn_SaveSkippedFile.Click += new System.EventHandler(this.btn_SaveSkippedFile_Click);
            // 
            // btn_CreateOKFlag
            // 
            this.btn_CreateOKFlag.Location = new System.Drawing.Point(33, 419);
            this.btn_CreateOKFlag.Name = "btn_CreateOKFlag";
            this.btn_CreateOKFlag.Size = new System.Drawing.Size(113, 32);
            this.btn_CreateOKFlag.TabIndex = 8;
            this.btn_CreateOKFlag.Text = "建立Flag文件";
            this.btn_CreateOKFlag.UseVisualStyleBackColor = true;
            this.btn_CreateOKFlag.Click += new System.EventHandler(this.btn_CreateOKFlag_Click);
            // 
            // btn_Run
            // 
            this.btn_Run.Location = new System.Drawing.Point(33, 47);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(113, 66);
            this.btn_Run.TabIndex = 9;
            this.btn_Run.Text = "运行所有步骤";
            this.btn_Run.UseVisualStyleBackColor = true;
            this.btn_Run.Click += new System.EventHandler(this.btn_RunAll_Click);
            // 
            // btn_UpdateXML
            // 
            this.btn_UpdateXML.Location = new System.Drawing.Point(33, 457);
            this.btn_UpdateXML.Name = "btn_UpdateXML";
            this.btn_UpdateXML.Size = new System.Drawing.Size(113, 32);
            this.btn_UpdateXML.TabIndex = 10;
            this.btn_UpdateXML.Text = "更新本地版本";
            this.btn_UpdateXML.UseVisualStyleBackColor = true;
            this.btn_UpdateXML.Click += new System.EventHandler(this.btn_UpdateXML_Click);
            // 
            // panel_buttons
            // 
            this.panel_buttons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_buttons.Controls.Add(this.btn_RunExternalApp);
            this.panel_buttons.Controls.Add(this.btn_Run);
            this.panel_buttons.Controls.Add(this.btn_UpdateXML);
            this.panel_buttons.Controls.Add(this.btn_CheckLatestVersion);
            this.panel_buttons.Controls.Add(this.btn_DownloadPackage);
            this.panel_buttons.Controls.Add(this.btn_CreateOKFlag);
            this.panel_buttons.Controls.Add(this.btn_Extract);
            this.panel_buttons.Controls.Add(this.btn_SaveSkippedFile);
            this.panel_buttons.Controls.Add(this.btn_VerifyFile);
            this.panel_buttons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_buttons.Location = new System.Drawing.Point(563, 0);
            this.panel_buttons.Name = "panel_buttons";
            this.panel_buttons.Size = new System.Drawing.Size(171, 521);
            this.panel_buttons.TabIndex = 11;
            // 
            // btn_RunExternalApp
            // 
            this.btn_RunExternalApp.Location = new System.Drawing.Point(33, 192);
            this.btn_RunExternalApp.Name = "btn_RunExternalApp";
            this.btn_RunExternalApp.Size = new System.Drawing.Size(113, 31);
            this.btn_RunExternalApp.TabIndex = 12;
            this.btn_RunExternalApp.Text = "启动主程序";
            this.btn_RunExternalApp.UseVisualStyleBackColor = true;
            this.btn_RunExternalApp.Visible = false;
            this.btn_RunExternalApp.Click += new System.EventHandler(this.btn_RunExternalApp_Click);
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 521);
            this.Controls.Add(this.panel_buttons);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DownloadForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download";
            this.Load += new System.EventHandler(this.DownloadForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_log;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_CheckLatestVersion;
        private System.Windows.Forms.Button btn_DownloadPackage;
        private System.Windows.Forms.Button btn_Extract;
        private System.Windows.Forms.Button btn_VerifyFile;
        private System.Windows.Forms.Button btn_SaveSkippedFile;
        private System.Windows.Forms.Button btn_CreateOKFlag;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.Button btn_UpdateXML;
        private System.Windows.Forms.Panel panel_buttons;
        private System.Windows.Forms.Button btn_RunExternalApp;
    }
}


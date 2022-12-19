
namespace AppLoader
{
    partial class UpgradeForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_ClearFlag = new System.Windows.Forms.Button();
            this.btn_KissProcess = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btn_upself = new System.Windows.Forms.Button();
            this.btn_RunMain = new System.Windows.Forms.Button();
            this.btn_run = new System.Windows.Forms.Button();
            this.btn_ClearUp = new System.Windows.Forms.Button();
            this.btn_UpgradeAppFolder = new System.Windows.Forms.Button();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_ClearFlag);
            this.panel1.Controls.Add(this.btn_KissProcess);
            this.panel1.Controls.Add(this.btn_Exit);
            this.panel1.Controls.Add(this.btn_upself);
            this.panel1.Controls.Add(this.btn_RunMain);
            this.panel1.Controls.Add(this.btn_run);
            this.panel1.Controls.Add(this.btn_ClearUp);
            this.panel1.Controls.Add(this.btn_UpgradeAppFolder);
            this.panel1.Controls.Add(this.rtb_log);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 432);
            this.panel1.TabIndex = 2;
            // 
            // btn_ClearFlag
            // 
            this.btn_ClearFlag.Location = new System.Drawing.Point(28, 291);
            this.btn_ClearFlag.Name = "btn_ClearFlag";
            this.btn_ClearFlag.Size = new System.Drawing.Size(95, 48);
            this.btn_ClearFlag.TabIndex = 15;
            this.btn_ClearFlag.Text = "清除等待升级旗标文件";
            this.btn_ClearFlag.UseVisualStyleBackColor = true;
            this.btn_ClearFlag.Click += new System.EventHandler(this.btn_Clearflag_Click);
            // 
            // btn_KissProcess
            // 
            this.btn_KissProcess.Location = new System.Drawing.Point(136, 291);
            this.btn_KissProcess.Name = "btn_KissProcess";
            this.btn_KissProcess.Size = new System.Drawing.Size(95, 48);
            this.btn_KissProcess.TabIndex = 14;
            this.btn_KissProcess.Text = "杀进程";
            this.btn_KissProcess.UseVisualStyleBackColor = true;
            this.btn_KissProcess.Click += new System.EventHandler(this.btn_KissProcess_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(197, 88);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(142, 31);
            this.btn_Exit.TabIndex = 13;
            this.btn_Exit.Text = "退出";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_upself
            // 
            this.btn_upself.Location = new System.Drawing.Point(244, 345);
            this.btn_upself.Name = "btn_upself";
            this.btn_upself.Size = new System.Drawing.Size(95, 48);
            this.btn_upself.TabIndex = 12;
            this.btn_upself.Text = "更新自身";
            this.btn_upself.UseVisualStyleBackColor = true;
            this.btn_upself.Click += new System.EventHandler(this.btn_upself_Click);
            // 
            // btn_RunMain
            // 
            this.btn_RunMain.Location = new System.Drawing.Point(136, 345);
            this.btn_RunMain.Name = "btn_RunMain";
            this.btn_RunMain.Size = new System.Drawing.Size(95, 48);
            this.btn_RunMain.TabIndex = 11;
            this.btn_RunMain.Text = "启动主程序";
            this.btn_RunMain.UseVisualStyleBackColor = true;
            this.btn_RunMain.Click += new System.EventHandler(this.btn_RunMain_Click);
            // 
            // btn_run
            // 
            this.btn_run.Location = new System.Drawing.Point(41, 88);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(150, 31);
            this.btn_run.TabIndex = 10;
            this.btn_run.Text = "运行";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // btn_ClearUp
            // 
            this.btn_ClearUp.Location = new System.Drawing.Point(28, 345);
            this.btn_ClearUp.Name = "btn_ClearUp";
            this.btn_ClearUp.Size = new System.Drawing.Size(95, 48);
            this.btn_ClearUp.TabIndex = 9;
            this.btn_ClearUp.Text = "清理任务";
            this.btn_ClearUp.UseVisualStyleBackColor = true;
            this.btn_ClearUp.Click += new System.EventHandler(this.btn_ClearUp_Click);
            // 
            // btn_UpgradeAppFolder
            // 
            this.btn_UpgradeAppFolder.Location = new System.Drawing.Point(244, 291);
            this.btn_UpgradeAppFolder.Name = "btn_UpgradeAppFolder";
            this.btn_UpgradeAppFolder.Size = new System.Drawing.Size(95, 48);
            this.btn_UpgradeAppFolder.TabIndex = 8;
            this.btn_UpgradeAppFolder.Text = "更新应用";
            this.btn_UpgradeAppFolder.UseVisualStyleBackColor = true;
            this.btn_UpgradeAppFolder.Click += new System.EventHandler(this.btn_UpgradeAppFolder_Click);
            // 
            // rtb_log
            // 
            this.rtb_log.Location = new System.Drawing.Point(7, 135);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.Size = new System.Drawing.Size(376, 110);
            this.rtb_log.TabIndex = 7;
            this.rtb_log.Text = "";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(197, 23);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(122, 16);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(55, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "正在更新您的程序";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // UpgradeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(394, 444);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpgradeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.UpgradeForm_Load);
            this.Shown += new System.EventHandler(this.UpgradeForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btn_ClearUp;
        private System.Windows.Forms.Button btn_UpgradeAppFolder;
        private System.Windows.Forms.RichTextBox rtb_log;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Button btn_RunMain;
        private System.Windows.Forms.Button btn_upself;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btn_KissProcess;
        private System.Windows.Forms.Button btn_ClearFlag;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}


﻿namespace OTAPackageGenerate
{
    partial class SetupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_save = new System.Windows.Forms.Button();
            this.prg_config = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(443, 641);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(178, 30);
            this.btn_save.TabIndex = 3;
            this.btn_save.Text = "保存并重启";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // prg_config
            // 
            this.prg_config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prg_config.Location = new System.Drawing.Point(0, 0);
            this.prg_config.Margin = new System.Windows.Forms.Padding(4);
            this.prg_config.Name = "prg_config";
            this.prg_config.Size = new System.Drawing.Size(663, 684);
            this.prg_config.TabIndex = 2;
            // 
            // SetupForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(663, 684);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.prg_config);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SetupForm";
            this.ShowInTaskbar = false;
            this.Text = "参数设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SetupForm_FormClosed);
            this.Load += new System.EventHandler(this.SetupForm_Load);
            this.Resize += new System.EventHandler(this.SetupForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.PropertyGrid prg_config;
    }
}
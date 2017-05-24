namespace rsh_installer
{
    partial class Install
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Install));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gbInstallOps = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new MetroFramework.Controls.MetroButton();
            this.chkUseExtended = new System.Windows.Forms.CheckBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.chkAddContext = new System.Windows.Forms.CheckBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtInstallPath = new MetroFramework.Controls.MetroTextBox();
            this.btnInstall = new MetroFramework.Controls.MetroButton();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
            this.Progress = new MetroFramework.Controls.MetroProgressBar();
            this.btnUninstall = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbInstallOps.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::rsh_installer.Properties.Resources.install_preview;
            this.pictureBox1.Location = new System.Drawing.Point(23, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(174, 176);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // gbInstallOps
            // 
            this.gbInstallOps.Controls.Add(this.btnBrowse);
            this.gbInstallOps.Controls.Add(this.chkUseExtended);
            this.gbInstallOps.Controls.Add(this.metroLabel3);
            this.gbInstallOps.Controls.Add(this.chkAddContext);
            this.gbInstallOps.Controls.Add(this.metroLabel2);
            this.gbInstallOps.Controls.Add(this.metroLabel1);
            this.gbInstallOps.Controls.Add(this.txtInstallPath);
            this.gbInstallOps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbInstallOps.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.gbInstallOps.Location = new System.Drawing.Point(216, 63);
            this.gbInstallOps.Name = "gbInstallOps";
            this.gbInstallOps.Size = new System.Drawing.Size(396, 176);
            this.gbInstallOps.TabIndex = 1;
            this.gbInstallOps.TabStop = false;
            this.gbInstallOps.Text = "Select Installation Option";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(357, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(33, 23);
            this.btnBrowse.Style = MetroFramework.MetroColorStyle.Magenta;
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "...";
            this.btnBrowse.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnBrowse.UseSelectable = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // chkUseExtended
            // 
            this.chkUseExtended.AutoSize = true;
            this.chkUseExtended.Location = new System.Drawing.Point(336, 98);
            this.chkUseExtended.Name = "chkUseExtended";
            this.chkUseExtended.Size = new System.Drawing.Size(15, 14);
            this.chkUseExtended.TabIndex = 5;
            this.chkUseExtended.UseVisualStyleBackColor = true;
            this.chkUseExtended.CheckedChanged += new System.EventHandler(this.chkUseExtended_CheckedChanged);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(7, 93);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(319, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Magenta;
            this.metroLabel3.TabIndex = 4;
            this.metroLabel3.Text = "Require [Shift + Right-Click] to show in Context Menu";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // chkAddContext
            // 
            this.chkAddContext.AutoSize = true;
            this.chkAddContext.Checked = true;
            this.chkAddContext.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddContext.Location = new System.Drawing.Point(336, 72);
            this.chkAddContext.Name = "chkAddContext";
            this.chkAddContext.Size = new System.Drawing.Size(15, 14);
            this.chkAddContext.TabIndex = 3;
            this.chkAddContext.UseVisualStyleBackColor = true;
            this.chkAddContext.CheckedChanged += new System.EventHandler(this.chkAddContext_CheckedChanged);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(7, 67);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(183, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Magenta;
            this.metroLabel2.TabIndex = 2;
            this.metroLabel2.Text = "Add Rashell to Context Menu:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(7, 22);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(73, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Magenta;
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "Install Path:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtInstallPath
            // 
            // 
            // 
            // 
            this.txtInstallPath.CustomButton.Image = null;
            this.txtInstallPath.CustomButton.Location = new System.Drawing.Point(243, 1);
            this.txtInstallPath.CustomButton.Name = "";
            this.txtInstallPath.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtInstallPath.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtInstallPath.CustomButton.TabIndex = 1;
            this.txtInstallPath.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtInstallPath.CustomButton.UseSelectable = true;
            this.txtInstallPath.CustomButton.Visible = false;
            this.txtInstallPath.Lines = new string[0];
            this.txtInstallPath.Location = new System.Drawing.Point(86, 19);
            this.txtInstallPath.MaxLength = 32767;
            this.txtInstallPath.Name = "txtInstallPath";
            this.txtInstallPath.PasswordChar = '\0';
            this.txtInstallPath.ReadOnly = true;
            this.txtInstallPath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtInstallPath.SelectedText = "";
            this.txtInstallPath.SelectionLength = 0;
            this.txtInstallPath.SelectionStart = 0;
            this.txtInstallPath.ShortcutsEnabled = true;
            this.txtInstallPath.Size = new System.Drawing.Size(265, 23);
            this.txtInstallPath.Style = MetroFramework.MetroColorStyle.Magenta;
            this.txtInstallPath.TabIndex = 0;
            this.txtInstallPath.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtInstallPath.UseSelectable = true;
            this.txtInstallPath.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtInstallPath.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(508, 306);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(101, 35);
            this.btnInstall.Style = MetroFramework.MetroColorStyle.Magenta;
            this.btnInstall.TabIndex = 2;
            this.btnInstall.Text = "&Install";
            this.btnInstall.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnInstall.UseSelectable = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(29, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 35);
            this.btnCancel.Style = MetroFramework.MetroColorStyle.Magenta;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Exit";
            this.btnCancel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnCancel.UseSelectable = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(29, 267);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(583, 23);
            this.Progress.Style = MetroFramework.MetroColorStyle.Magenta;
            this.Progress.TabIndex = 7;
            this.Progress.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // btnUninstall
            // 
            this.btnUninstall.Location = new System.Drawing.Point(401, 306);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(101, 35);
            this.btnUninstall.Style = MetroFramework.MetroColorStyle.Magenta;
            this.btnUninstall.TabIndex = 8;
            this.btnUninstall.Text = "&Uninstall";
            this.btnUninstall.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnUninstall.UseSelectable = true;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // Install
            // 
            this.AcceptButton = this.btnInstall;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(632, 364);
            this.Controls.Add(this.btnUninstall);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.gbInstallOps);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Install";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Magenta;
            this.Text = "Install Rashell";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.Install_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbInstallOps.ResumeLayout(false);
            this.gbInstallOps.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox gbInstallOps;
        private MetroFramework.Controls.MetroTextBox txtInstallPath;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.CheckBox chkAddContext;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.CheckBox chkUseExtended;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroButton btnInstall;
        private MetroFramework.Controls.MetroButton btnCancel;
        private MetroFramework.Controls.MetroButton btnBrowse;
        private MetroFramework.Controls.MetroProgressBar Progress;
        private MetroFramework.Controls.MetroButton btnUninstall;
    }
}


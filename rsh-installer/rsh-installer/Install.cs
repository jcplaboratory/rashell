using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MetroFramework.Forms;
using Microsoft.Win32;

namespace rsh_installer
{
    public partial class Install : MetroForm
    {
        public Install()
        {
            InitializeComponent();
            string Installdir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\J.C.P Laboratory";
            txtInstallPath.Text = Installdir;

            if (!File.Exists(Application.StartupPath + "\\bin\\rsh.exe"))
            {
                MessageBox.Show("Files required for installation was not found. Please re-download Rashell.","File Missing",MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            if (this.CheckIfInstalled())
            {
                btnInstall.Enabled = false;
            } else
            {
                btnUninstall.Enabled = false;
            }
        }

        public void Uninstall()
        {
            string[] paths;
            string installdir = null;
            string NewPaths = null;

            Progress.Step = 20;
            Progress.PerformStep();

            try
            {
                using (var rashellKey = Registry.ClassesRoot.OpenSubKey(@"Directory\\Background\\Shell\\Rashell", true))
                {
                    installdir = (string)rashellKey.GetValue("InstallationDir");
                    rashellKey.Close();
                }

                using (var path = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment", true))
                {
                    paths = path.GetValue("Path").ToString().Split(';').ToArray();

                    foreach (string pth in paths)
                    {
                        if (!pth.Contains("\\Rashell\\bin"))
                        {
                            NewPaths = NewPaths + pth + ";";
                        }
                    }

                    path.SetValue("Path", NewPaths);
                    path.Close();

                }

                Progress.Step = 40;
                Progress.PerformStep();

                using (var rashellKey = Registry.ClassesRoot.OpenSubKey(@"Directory\\Background\\Shell\\Rashell", true))
                {
                    string[] values = rashellKey.GetValueNames();
                    foreach (string value in values)
                    {
                        rashellKey.DeleteValue(value);
                    }

                    rashellKey.Close();
                }

                using (var rshCommand = Registry.ClassesRoot.OpenSubKey(@"Directory\\Background\\Shell\\Rashell\\command", true))
                {
                    string[] values = rshCommand.GetValueNames();
                    foreach (string value in values)
                    {
                        rshCommand.DeleteValue(value);
                    }

                    rshCommand.Close();
                }

                Progress.Step = 80;
                Progress.PerformStep();
                Registry.ClassesRoot.DeleteSubKey(@"Directory\\Background\\Shell\\Rashell\\command");
                Registry.ClassesRoot.DeleteSubKey(@"Directory\\Background\\Shell\\Rashell");

                Directory.Delete(installdir + "\\Rashell", true);

                Progress.Step = 100;
                Progress.PerformStep();

                MessageBox.Show("Rashell was Uninstalled Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }catch (Exception x)
            {
                MessageBox.Show("Something happened and we unable to uninstall Rashell!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }                      

            btnUninstall.Enabled = false;
            btnInstall.Enabled = true;
        }
        public bool CheckIfInstalled()
        {
            

            using (var rashellKey = Registry.ClassesRoot.OpenSubKey(@"Directory\\Background\\Shell\\Rashell", true))
            {
                string output = null;
                try
                {
                  output  = (string)rashellKey.GetValue("InstallationDir");
                  rashellKey.Close();
                    
                } catch (Exception e)
                {

                }

                if (output == null)
                {
                    return false;
                }

                
            }
            return true;
        }
        private void metroUserControl1_Load(object sender, EventArgs e)
        {

        }

        private void Install_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open = new FolderBrowserDialog();
            open.ShowDialog();

            if (!string.IsNullOrEmpty(open.SelectedPath))
            {
                txtInstallPath.Text = open.SelectedPath;
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            btnInstall.Enabled = false;

            string installdir = txtInstallPath.Text;
            string pathSystemVariables = null;
            bool addContext = false;
            bool setExtendedContext = false;

            if (chkAddContext.Checked) { addContext = true; }
            if (chkUseExtended.Checked) { setExtendedContext = true; }

            Progress.ProgressBarStyle = ProgressBarStyle.Continuous;
            Progress.Step = 18;
            Progress.PerformStep();


            //copy file
            try
            {
                if (Directory.Exists(installdir))
                {
                    Directory.Delete(installdir, true);
                    Directory.CreateDirectory(installdir + "\\Rashell\\bin");
                }
                else
                {
                    Directory.CreateDirectory(installdir + "\\Rashell\\bin");
                }

                string filename = "rsh.exe";
                string sourcepath = Application.StartupPath + "\\bin\\" + filename;
                string targetpath = installdir + "\\Rashell\\bin";

                File.Copy(sourcepath, targetpath + "\\rsh.exe", true);

                Progress.Step = 50;
                Progress.PerformStep();
            }
            catch (Exception x)
            {

            }
            //set registry values
            try
            {
                if (addContext)
                {
                    Registry.ClassesRoot.CreateSubKey(@"Directory\\Background\\Shell\\Rashell");
                    Registry.ClassesRoot.CreateSubKey(@"Directory\\Background\\Shell\\Rashell\\command");

                    using (var rashellKey = Registry.ClassesRoot.OpenSubKey(@"Directory\\Background\\Shell\\Rashell", true))
                    {
                        rashellKey.SetValue("", "Open Rashell Here", RegistryValueKind.String);
                        rashellKey.SetValue("Icon", "\"" + installdir + "\\Rashell\\bin\\rsh.exe" + "\"", RegistryValueKind.String);
                        rashellKey.SetValue("InstallationDir", installdir);

                        if (setExtendedContext)
                        {
                            rashellKey.SetValue("Extended", "", RegistryValueKind.String);
                        }
                        
                        rashellKey.Close();

                        Progress.Step = 75;
                        Progress.PerformStep();
                    }

                    using (var rshCommand = Registry.ClassesRoot.OpenSubKey(@"Directory\\Background\\Shell\\Rashell\\command", true))
                    {
                        rshCommand.SetValue("", "\"" + installdir + "\\Rashell\\bin\\rsh.exe" + "\"" + " -cev cd %v", RegistryValueKind.String);

                        rshCommand.Close();
                    }
                }

                Progress.Step = 90;

                using (var path = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment", true))
                {
                    pathSystemVariables = (string)path.GetValue("Path");
                    path.SetValue("Path", pathSystemVariables + installdir + "\\Rashell\\bin;");
                    path.Close();
                }

                Progress.Step = 100;
                Progress.PerformStep();

                
            }
            catch (Exception x)
            {

            }

            MessageBox.Show("Installation was Successfull", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnInstall.Enabled = false;
            btnUninstall.Enabled = true;
        }

        private void chkAddContext_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddContext.Checked == false)
            {
                chkUseExtended.Enabled = false;
                chkUseExtended.Checked = false;
            } else
            {
                chkUseExtended.Enabled = true;
            }
        }

        private void chkUseExtended_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            Progress.Step = 0;
            Progress.PerformStep();

            DialogResult result = new DialogResult();
            result = MessageBox.Show("Are you sure you want to Uninstall Rashell?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Uninstall();
            }
           
        }
    }
}

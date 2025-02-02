﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB
{
    public partial class frmSet : Form
    {

        public string Server = "";
        public string Login = "";
        public string Password = "";
        public string Database = "";

        public bool BackupEnable = true;
        public string ServerBackup = "";
        public string LoginBackup = "";
        public string PasswordBackup = "";

        public string BackupDBName = "";
        public string BackupFolder = "";
        public int BackupInterval = 1;

        public string cFileName = "";
        public frmSet()
        {
            InitializeComponent();
        }

        

        private void frmSet_Load(object sender, EventArgs e)
        {
            frmSet readconfig = LoadConfig();

            tbLogin.Text = readconfig.Login;
            tbPassword.Text = readconfig.Password;
            tbServer.Text = readconfig.Server;
            cbResultDatabase.Text = readconfig.Database;





            cbBackupEnable.Checked = readconfig.BackupEnable;
            tbServerBackup.Text = readconfig.ServerBackup;
            tbLoginBackup.Text = readconfig.LoginBackup;
            tbPasswordBackup.Text = readconfig.PasswordBackup;
            tbBackupDBName.Text = readconfig.BackupDBName;
            tbDBFolderBackup.Text = readconfig.BackupFolder;
            nudBackupInterval.Value = readconfig.BackupInterval; 

            #region help
            #endregion 
        }


        public frmSet LoadConfig()
        {
            string file = Application.StartupPath + "\\INI\\dbInterface.INI";

            frmSet config = new frmSet();
            StringBuilder retdata;
            retdata = new StringBuilder("", 256);
            try
            {
                INI.GetPrivateProfileString("DB_SERVER", "Server", "", retdata, 256, file);
                config.Server = retdata.ToString();
                INI.GetPrivateProfileString("DB_SERVER", "Login", "", retdata, 256, file);
                config.Login = retdata.ToString();
                INI.GetPrivateProfileString("DB_SERVER", "Password", "", retdata, 256, file);
                config.Password = retdata.ToString();
                INI.GetPrivateProfileString("DB_SERVER", "Database", "", retdata, 256, file);
                config.Database = retdata.ToString();

                INI.GetPrivateProfileString("BACKUP", "BackupEnable", "", retdata, 256, file);
                try { config.BackupEnable = Convert.ToBoolean(retdata.ToString()); }
                catch { config.BackupEnable = true; }
                INI.GetPrivateProfileString("BACKUP", "ServerBackup", "", retdata, 256, file);
                config.ServerBackup = retdata.ToString();
                INI.GetPrivateProfileString("BACKUP", "LoginBackup", "", retdata, 256, file);
                config.LoginBackup = retdata.ToString();
                INI.GetPrivateProfileString("BACKUP", "PasswordBackup", "", retdata, 256, file);
                config.PasswordBackup = retdata.ToString();

                INI.GetPrivateProfileString("BACKUP", "BackupDBName", "", retdata, 256, file);
                config.BackupDBName = retdata.ToString();
                INI.GetPrivateProfileString("BACKUP", "BackupFolder", "", retdata, 256, file);
                config.BackupFolder = retdata.ToString();

                INI.GetPrivateProfileString("BACKUP", "BackupInterval", "", retdata, 256, file);
                try { config.BackupInterval = Convert.ToInt16(retdata.ToString()); }
                catch { config.BackupInterval = 1; }
            }
            catch (Exception exp) { LogMsg.CMsg.Show("EXCONFG", "INI file open fail \n" + file, exp.ToString(), true, true); }
            return config;
        }

        private void btnNewDB_Click(object sender, EventArgs e)
        {
            DataBase db = new DataBase();
            string dbname = tbNewDBName.Text; 
            if (dbname != "")
            {
                if (db.CreateDatabase(tbServer.Text, tbLogin.Text, tbPassword.Text, dbname, tbDBFolder.Text))
                {
                    MessageBox.Show("The database has been generated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("The database was not created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            DataBase db = new DataBase();
            List<string> dblist = db.LoadDataBaseList(tbServer.Text, tbLogin.Text, tbPassword.Text);

            cbResultDatabase.Items.Clear();
            if (dblist == null) return;
            for (int i = 0; i < dblist.Count; i++)
            {
                object item = dblist[i];
                cbResultDatabase.Items.Add(item);
                //cbParDatabase.Items.Add(item);
            }
        }

        private void btnDBFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbDBFolder.Text = dlg.SelectedPath;
            }
        }

        private void btnDBFolderBackup_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                 
            }
        }

        private void tbDBConfig_Selected(object sender, TabControlEventArgs e)
        {
            WebBrowser wbHelp = new WebBrowser();

            wbHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            wbHelp.Location = new System.Drawing.Point(3, 3);
            wbHelp.MinimumSize = new System.Drawing.Size(20, 20);
            wbHelp.Name = "wbHelp";
            wbHelp.Size = new System.Drawing.Size(540, 254);
            wbHelp.TabIndex = 0;
            tabHelp.Controls.Add(wbHelp);

            #region help
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string sv = fvi.FileVersion;

            string temp = "";
            temp += "<html><head>";
            temp += "<style type ='Text/css'>";
            temp += "h2 {font-family:Verdana; font-size: 18px; line-height:100%; color:red;}";
            temp += "p {font-family:Sans-serif; font-size: 12px; line-height:100%;color:blue;}";
            temp += "p2 {font-family:Verdana; font-size: 14px; line-height:100%;color:red;}";
            temp += "</style type>";

            temp += "</head><body>";
            temp += "<h2>Database interfae module <br/></h2>";
            temp += "<p>Version : " + sv + "<br/></p>";
            temp += "<p><br/></p>";
            //temp += "<p> Description:" + d[1] + " <br/></p>";

            temp += "<p2>Using the data backup function<br/></p2>";
            temp += "<p>Check the 'Enable backup' and enter the server connection information for data backup.<br/></p>";
            temp += "<p>Enter the name of the database. The actual database name is used by attaching the year and month on the name you entered.<br/></p>";
            temp += "<p>Select a folder to store the data files.<br/></p>";
            temp += "<p>Enter the interval for the creation of a new data file on a monthly basis.<br/></p>";


            temp += "</body></html>";

            try { wbHelp.DocumentText = temp; }
            catch { }
            #endregion 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cFileName = Application.StartupPath + "\\INI\\dbInterface.INI";

            #region // heder
            FileInfo f = new FileInfo(cFileName);

            if (f.Exists) { f.Delete(); }

            StreamWriter ws = File.CreateText(cFileName);
            ws.WriteLine("A&G Technology");
            ws.WriteLine("dbInterface configuration file ");
            ws.WriteLine("Saved: " + DateTime.Now.ToLongDateString());
            ws.WriteLine("===========================================");
            ws.WriteLine("");
            ws.Flush();
            ws.Close();
            #endregion


            INI.WritePrivateProfileString("DB_SERVER", "Server", tbServer.Text, cFileName);
            INI.WritePrivateProfileString("DB_SERVER", "Login", tbLogin.Text, cFileName);
            INI.WritePrivateProfileString("DB_SERVER", "Password", tbPassword.Text, cFileName);
            INI.WritePrivateProfileString("DB_SERVER", "Database", cbResultDatabase.Text, cFileName);


            INI.WritePrivateProfileString("BACKUP", "BackupEnable", cbBackupEnable.Checked.ToString(), cFileName);
            INI.WritePrivateProfileString("BACKUP", "ServerBackup", tbServerBackup.Text, cFileName);
            INI.WritePrivateProfileString("BACKUP", "LoginBackup", tbLoginBackup.Text, cFileName);
            INI.WritePrivateProfileString("BACKUP", "PasswordBackup", tbPasswordBackup.Text, cFileName);

            INI.WritePrivateProfileString("BACKUP", "BackupDBName", tbBackupDBName.Text, cFileName);
            INI.WritePrivateProfileString("BACKUP", "BackupFolder", tbDBFolderBackup.Text, cFileName);
            INI.WritePrivateProfileString("BACKUP", "BackupInterval", nudBackupInterval.Text, cFileName);



            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDBFolderBackup_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbDBFolderBackup.Text = dlg.SelectedPath;
            }
        }
    }
}

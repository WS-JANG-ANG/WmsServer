using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ANGFileInfo
{
    partial class ANGAboutBox : Form
    {
        List<string> fileList = new List<string>();

        public ANGAboutBox()
        {
            InitializeComponent();
            this.Text = String.Format("{0} Information", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;

            string fileinfo = Application.ExecutablePath;
            PropertyInfo[] parm;
            Assembly assm = null;
            Type typeinfo = null;
            string sType = "";
            List<string> hinfo = new List<string>();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(fileinfo);


            try
            {
                assm = Assembly.LoadFile(fileinfo);
                typeinfo = assm.GetType("ANGFileInfo.ANGFileInfo");
                object obj = Activator.CreateInstance(typeinfo);

                parm = null;
                parm = typeinfo.GetProperties();

                if (parm != null)
                {
                    foreach (PropertyInfo fi in parm)
                    {
                        if (fi.Name == "Type")
                            sType = Convert.ToString(fi.GetValue(assm, null));
                        if (fi.Name == "HistoryInfo")
                            hinfo = (List<string>)(fi.GetValue(assm, null));
                    }
                }
            }
            catch { }
            string[] strarray = new string[hinfo.Count + 2];
            strarray[0] = fvi.Comments;
            for (int i = 0; i < hinfo.Count; i++)
            {
                strarray[strarray.Length - 1 - i] = hinfo[i];
            }
            this.textBoxDescription.Lines = strarray;


            LoadVersion();
        }

        #region 어셈블리 특성 접근자

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }



        #endregion

        private void LoadVersion()
        {
            string Path;
            PropertyInfo[] parm;
            Assembly assm = null;
            Type typeinfo = null; ;

            string sFile = "";
            string sDescrption = "";
            string sType = "";
            string sVersion = "";
            string sPath = "";
            string sDate1 = "";
            string sFileName = "";

            Path = Application.StartupPath;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Path);

            int progresscount = 0;
            int filecount = di.GetFiles("*.exe", SearchOption.AllDirectories).Length
                + di.GetFiles("*.dll", SearchOption.AllDirectories).Length;

            foreach (FileInfo file in di.GetFiles("*.exe", SearchOption.AllDirectories))
            {
                sFile = "";
                sDescrption = "";
                sType = "";
                sVersion = "";
                sPath = "";
                sDate1 = "";
                sFileName = file.Name;
                progresscount++;

                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(file.FullName);

                sFile = fvi.OriginalFilename;
                sVersion = fvi.FileVersion;
                sDescrption = fvi.Comments;

                System.IO.FileInfo fi = new FileInfo(fvi.FileName);
                sDate1 = fi.CreationTime.ToString();

                int idx = fvi.FileName.LastIndexOf('\\');
                sPath = fvi.FileName.Remove(idx);
                if ((sFile == null) || (sFile == "")) sFile = fvi.FileName.Substring(idx);
                string[] f_str = new string[2];

                try
                {
                    f_str = sFile.Split('.');
                    assm = Assembly.LoadFile(file.FullName);
                    typeinfo = assm.GetType("ANGFileInfo.ANGFileInfo");
                    object obj = Activator.CreateInstance(typeinfo);

                    parm = null;
                    parm = typeinfo.GetProperties();
                    if (parm != null)
                    {
                        foreach (PropertyInfo pi1 in parm)
                        {
                            //if (fi.Name == "DeviceName")
                            //    sName = Convert.ToString(fi.GetValue(assm, null));
                            //if (fi.Name == "Description")
                            //    sDescrption = Convert.ToString(fi.GetValue(assm, null));
                            if (pi1.Name == "Type")
                                sType = Convert.ToString(pi1.GetValue(assm, null));
                            //if (fi.Name == "HistoryInfo")
                            //    sVersion = Convert.ToString(fi.GetValue(assm, null));
                        }
                    }
                    obj = null;
                }
                catch (Exception) { }
                fileList.Add(sFile + "\t" + sVersion + "\t" + sDescrption + "\t" + sType + "\t" + sPath + "\t" + sDate1 + "\t" + sFileName);
                Thread.Sleep(1);
                fvi = null;
                assm = null;
                typeinfo = null;
            }

            foreach (FileInfo file in di.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                sFile = "";
                sDescrption = "";
                sType = "";
                sVersion = "";
                sPath = "";
                sDate1 = "";
                sFileName = file.Name;
                progresscount++;

                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(file.FullName);

                sFile = fvi.OriginalFilename;
                sVersion = fvi.FileVersion;
                sDescrption = fvi.Comments;

                System.IO.FileInfo fi = new FileInfo(fvi.FileName);
                sDate1 = fi.CreationTime.ToString();

                int idx = fvi.FileName.LastIndexOf('\\');
                sPath = fvi.FileName.Remove(idx);
                if ((sFile == null) || (sFile == "")) sFile = fvi.FileName.Substring(idx);
                string[] f_str = new string[2];

                try
                {
                    f_str = sFile.Split('.');
                    assm = Assembly.LoadFile(file.FullName);
                    typeinfo = assm.GetType("ANGFileInfo.ANGFileInfo");
                    object obj = Activator.CreateInstance(typeinfo);

                    parm = null;
                    parm = typeinfo.GetProperties();
                    if (parm != null)
                    {
                        foreach (PropertyInfo pi1 in parm)
                        {
                            //if (fi.Name == "DeviceName")
                            //    sName = Convert.ToString(fi.GetValue(assm, null));
                            //if (fi.Name == "Description")
                            //    sDescrption = Convert.ToString(fi.GetValue(assm, null));
                            if (pi1.Name == "Type")
                                sType = Convert.ToString(pi1.GetValue(assm, null));
                            //if (fi.Name == "HistoryInfo")
                            //    sVersion = Convert.ToString(fi.GetValue(assm, null));
                        }
                    }
                    obj = null;
                }
                catch (Exception) { }
                fileList.Add(sFile + "\t" + sVersion + "\t" + sDescrption + "\t" + sType + "\t" + sPath + "\t" + sDate1 + "\t" + sFileName);
                Thread.Sleep(1);
                fvi = null;
                assm = null;
                typeinfo = null;
            }

            fileList.Sort();

            // add to list
            for (int i = 0; i < fileList.Count; i++)
            {
                string[] tmpstr = fileList[i].Split('\t');

                ListViewItem newitem;

                newitem = new ListViewItem(tmpstr[0]);
                newitem.SubItems.Add(tmpstr[1]);
                newitem.SubItems.Add(tmpstr[2]);
                newitem.SubItems.Add(tmpstr[3]);
                newitem.SubItems.Add(tmpstr[4]);
                newitem.SubItems.Add(tmpstr[5]);
                newitem.SubItems.Add(tmpstr[6]);

                if (i % 2 == 0) newitem.BackColor = Color.Aquamarine;
                else newitem.BackColor = Color.LightGray;
                lvFileList.Items.Add(newitem);
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            string sFileName;


            SaveFileDialog SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.InitialDirectory = Application.StartupPath;

            SaveFileDialog.OverwritePrompt = true;
            SaveFileDialog.CheckFileExists = false;
            SaveFileDialog.Filter = "*.txt|*.txt";

            SaveFileDialog.AddExtension = true;
            SaveFileDialog.DefaultExt = "txt";
            SaveFileDialog.FileName = "VersionInformation";
            if (SaveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                sFileName = SaveFileDialog.FileName;

                FileStream fs = new FileStream(sFileName, FileMode.Create);
                fs.Close();

                StreamWriter ws = File.AppendText(sFileName);
                ws.WriteLine("A&G Technology \r\nFile version information \r\n" + DateTime.Now.ToLongDateString());
                ws.WriteLine("\r\n");
                ws.WriteLine("Name\t" + "Version\t" + "Description\t" + "Type\t" + "Path\t" + "Create\t" + "Filename\r\n");

                for (int i = 0; i < fileList.Count; i++)
                {
                    ws.WriteLine(fileList[i]);
                }

                ws.Flush();
                ws.Close();
            }
        }

        private void ANGAboutBox_Resize(object sender,EventArgs e)
        {
            tabControl1.Height = this.Height - 90;
        }
    }
}

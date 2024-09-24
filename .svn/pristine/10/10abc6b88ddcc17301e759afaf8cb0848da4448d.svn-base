using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Reflection;
using System.Net;

namespace SiemensPLC
{
    public partial class frmSet : Form
    {
        string IniFilePath = Application.StartupPath + "\\Devices\\SiemensPLC.INI";
        List<string> plcNames = new List<string>();
        int selectedPlc = 0;

        List<PLC> listPlc = new List<PLC>();

        public frmSet()
        {
            InitializeComponent();
        }

        private void frmSet_Load(object sender, EventArgs e)
        {
            Text = Assembly.GetExecutingAssembly().GetName().Name + " Setup - version:" + Assembly.GetExecutingAssembly().GetName().Version;

            #region get ip
            List<string> ips = new List<string>();

            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    ips.Add(ip.ToString());
                }
            }
            object[] items = new object[ips.Count];
            for (int i = 0; i < ips.Count; i++) items[i] = ips[i];
            cbLocalIp.Items.AddRange(items);
            #endregion

            dgvPLC.Rows.Clear();
            listPlc = new List<PLC>();
            ClearScreen();

            StringBuilder SB = new StringBuilder();
            SB.Capacity = 256;

            SiemensPLC.GetPrivateProfileString("PLC", "LIST", "", SB, 256, IniFilePath);
            string[] temp = SB.ToString().Split(new char[] { ',' });
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] != "") plcNames.Add(temp[i]);
            }

            for (int i = 0; i < plcNames.Count; i++)
            {
                dgvPLC.Rows.Add(1);
                dgvPLC.Rows[i].Cells[0].Value = plcNames[i];

                SiemensPLC.GetPrivateProfileString("PLCLIST", plcNames[i], "", SB, 256, IniFilePath);
                string[] sa = SB.ToString().Split(',');
                try
                {
                    PLC plc = new PLC();
                    plc.Name = plcNames[i];
                    plc.PLC_IP = sa[0].ToString();
                    plc.PLC_Port = sa[1].ToString();
                    plc.Local_IP = sa[2].ToString();
                    plc.Local_Port = sa[3].ToString();

                    plc.NoofRead = Convert.ToUInt16(sa[4].ToString());
                    plc.NoofWrite = Convert.ToUInt16(sa[5].ToString());
                    listPlc.Add(plc);
                }
                catch { }
            }
            return;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            FileInfo f = new FileInfo(IniFilePath);
            if (f.Exists) { f.Delete(); }

            StreamWriter ws = File.CreateText(IniFilePath);
            ws.WriteLine("A&G Technology");
            ws.WriteLine("MelsecPLC_UTL configuration file ");
            ws.WriteLine("Saved: " + DateTime.Now.ToLongDateString());
            ws.WriteLine("===========================================");
            ws.WriteLine("");
            ws.Flush();
            ws.Close();

            string plcs = "";
            SiemensPLC.WritePrivateProfileString("PLC", "LIST", plcs, IniFilePath);
            for (int i = 0; i < listPlc.Count; i++)
            {
                plcs += listPlc[i].Name + ",";

                string temp = "";
                temp += listPlc[i].PLC_IP + ",";
                temp += listPlc[i].PLC_Port + ",";
                temp += listPlc[i].Local_IP + ",";
                temp += listPlc[i].Local_Port + ",";
                temp += listPlc[i].NoofRead.ToString() + ",";
                temp += listPlc[i].NoofWrite.ToString() + ",";

                SiemensPLC.WritePrivateProfileString("PLCLIST", listPlc[i].Name, temp, IniFilePath);
            }
            if(plcs.Length > 0) plcs = plcs.Remove(plcs.Length - 1);
            SiemensPLC.WritePrivateProfileString("PLC", "LIST", plcs, IniFilePath);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            frmTest test = new frmTest();
            test.ShowDialog();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            ClearScreen();

            PLC nplc = new PLC();
            nplc.Name = "NEW" + listPlc.Count.ToString();
            listPlc.Add(nplc);
            selectedPlc = listPlc.Count - 1;

            dgvPLC.Rows.Clear();
            for (int i = 0; i < listPlc.Count; i++)
            {
                dgvPLC.Rows.Add(listPlc[i].Name);
            }
        }

        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            #region // update to list
            if (tbPLCName.Text != "")
            {
                PLC plc = new PLC();

                plc.Name = tbPLCName.Text;
                plc.PLC_IP = tbPlcIp.Text;
                plc.PLC_Port = tbPlcPort.Text;
                plc.Local_IP = cbLocalIp.Text;
                plc.Local_Port = tbLocalPort.Text;
                plc.NoofRead= (int)nudReadNo.Value;
                plc.NoofWrite = (int)nudWriteNo.Value;

                if (listPlc.Count == 0) listPlc.Add(plc);
                else listPlc[selectedPlc] = plc;
            }
            #endregion

            dgvPLC.Rows.Clear();
            for (int i = 0; i < listPlc.Count; i++)
            {
                dgvPLC.Rows.Add(listPlc[i].Name);
            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            if (listPlc.Count <= 0) return;
            listPlc.RemoveAt(selectedPlc);
            if (selectedPlc >= listPlc.Count) selectedPlc--;
            dgvPLC.Rows.Clear();
            for (int i = 0; i < listPlc.Count; i++)
            {
                dgvPLC.Rows.Add(listPlc[i].Name);
            }
        }

        private void ClearScreen()
        {
            #region clear screen
            tbPLCName.Text = "";

            try
            {
                tbPlcIp.Text = "";
                tbPlcPort.Text = "";
                cbLocalIp.Text = "";
                tbLocalPort.Text = "";
                nudReadNo.Value = 0;
                nudWriteNo.Value = 0;
            }
            catch { }
            #endregion
        }

        private void dgvPLC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPLC.Rows.Count <= 0) return;

            #region // save to list
            if (tbPLCName.Text != "")
            {
                PLC plc = new PLC();
                plc.Name = tbPLCName.Text;
                plc.PLC_IP = tbPlcIp.Text;
                plc.PLC_Port = tbPlcPort.Text;
                plc.Local_IP = cbLocalIp.Text;
                plc.Local_Port = tbLocalPort.Text;
                plc.NoofRead = (int)nudReadNo.Value;
                plc.NoofWrite = (int)nudWriteNo.Value;

                listPlc[selectedPlc] = plc;
            }
            #endregion

            selectedPlc = dgvPLC.SelectedCells[0].RowIndex;

            dgvPLC.Rows.Clear();
            for (int i = 0; i < listPlc.Count; i++)
            {
                dgvPLC.Rows.Add(listPlc[i].Name);
            }
            dgvPLC.Rows[selectedPlc].Selected = true;

            #region // update screen
            tbPLCName.Text = listPlc[selectedPlc].Name;

            tbPlcIp.Text = listPlc[selectedPlc].PLC_IP;
            tbPlcPort.Text = listPlc[selectedPlc].PLC_Port;
            cbLocalIp.Text = listPlc[selectedPlc].Local_IP;
            tbLocalPort.Text = listPlc[selectedPlc].Local_Port;
            nudReadNo.Value = listPlc[selectedPlc].NoofRead;
            nudWriteNo.Value = listPlc[selectedPlc].NoofWrite;
            #endregion
        }
    }
    public struct PLC
    {
        public string Name;
        public string PLC_IP;
        public string PLC_Port;
        public string Local_IP;
        public string Local_Port;
        public int NoofWrite;
        public int NoofRead;

        public bool Connected;
    }
}

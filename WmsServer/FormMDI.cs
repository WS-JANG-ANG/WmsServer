﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.Threading;
using DB;
using System.Net.Sockets;
using System.Reflection;

namespace WmsServer
{
    public partial class FormMDI : Form
    {
        private System.Windows.Forms.Timer watchdogTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer plcUpdateTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer sendTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerDisplay = new System.Windows.Forms.Timer();
        
        static public DB.DBInterface db = new DB.DBInterface();


        Server WmsServer;
        DateTime convertDateTime;

        private double checkWatchDog = 0;
        private double checkInReset = 0;
        private double checkOutReset = 0;
        private int checkWatchDogCount = 0;
        private bool twinKleCount = true;

        public FormMDI()
        {
            InitializeComponent();
        }

        private void FormMDI_Load(object sender, EventArgs e)
        {
            LogMsg.CMsg.Show("FMDI", "*** start program - version :", Assembly.GetExecutingAssembly().GetName().Version.ToString(), false, true);
            //ip 목록 가져오기
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
            comboBoxServerIp.Items.AddRange(items);

            #endregion



            //ip, port
            comboBoxServerIp.Text = INI.ReadValue(Data.iniPath,"WMSSERVER", "IP","");
            try
            {
                nudServerPort.Value = Convert.ToDecimal(INI.ReadValue(Data.iniPath, "WMSSERVER", "PORT","0"));
            }

            catch
            {
                nudServerPort.Value = 0;
            }


            Data.PlcIoDic = Data.LoadPlcIoDic();


            #region start plc
            DevicePLC.Start();
            watchdogTimer.Interval = 500;
            watchdogTimer.Start();
            watchdogTimer.Tick += WatchdogTimer_Tick;
            plcUpdateTimer.Interval = 500;
            plcUpdateTimer.Start();
            plcUpdateTimer.Tick += PlcUpdateTimer_Tick;
            timerDisplay.Interval = 500;
            timerDisplay.Tick += TimerDisplay_Tick;
            timerDisplay.Start();
        }


        private void FormMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            buttonStop_Click(null, null);            

            if (WmsServer != null)
                WmsServer.StopServer();

            SiemensPLC.SiemensPLC.Stop();
            sendTimer.Stop();
            timerDisplay.Stop();
            watchdogTimer.Stop();
            plcUpdateTimer.Stop();

            Application.Exit();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            LogMsg.CMsg.Show("WMSSV", "start ", "", false, true);
            
            LogMsg.CMsg.Show("WMSSV", "initialized plc", "", false, true);
            #endregion

            #region start socket server
            WmsServer = new Server();
            WmsServer.StartServer(comboBoxServerIp.Text, (int)nudServerPort.Value);
            WmsServer.DataReceiveEventStr += Server_DataReceiveEventStr;


            INI.WriteValue(Data.iniPath,"WMSSERVER", "IP", comboBoxServerIp.Text);
            INI.WriteValue(Data.iniPath, "WMSSERVER", "PORT", nudServerPort.Value.ToString());
            #endregion

            Data.pList = db.GetRefList("PL");

                     

            #region sendTimer
            sendTimer.Interval = 1000;
            sendTimer.Tick += SendTimer_Tick;
            sendTimer.Start();
            #endregion

            LogMsg.CMsg.Show("WMSSV", "started :::::::", "", false, true);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (WmsServer != null)
            {
                WmsServer.DataReceiveEventStr -= Server_DataReceiveEventStr;
                WmsServer.StopServer();
            }


            updateTimer.Stop();
            LogMsg.CMsg.Show("WMSSV", "stop update timer", "", false, true);

            Thread.Sleep(200);

            DevicePLC.Stop();
            Thread.Sleep(200);
            LogMsg.CMsg.Show("WMSSV", "stop plc", "", false, true);

            sendTimer.Stop();
            LogMsg.CMsg.Show("WMSSV", "stop send timer", "", false, true);

            timerDisplay.Start();
        }

        private void WatchdogTimer_Tick(object sender, EventArgs e)
        {
            if (DevicePLC.ReadPLC(Data.PlcIoDic["OutWatchdog"]) == 0)
                DevicePLC.WritePLC(Data.PlcIoDic["OutWatchdog"], 1);
            else
                DevicePLC.WritePLC(Data.PlcIoDic["OutWatchdog"], 0);

            if (DevicePLC.ReadPLC(Data.PlcIoDic["InWatchdog"]) == checkWatchDog)
            {
                checkWatchDogCount++;
                if (checkWatchDogCount > 5) Data.PlcWatchdog = false;
            }
            else
            {
                checkWatchDogCount = 0;
                checkWatchDog = DevicePLC.ReadPLC(Data.PlcIoDic["InWatchdog"]);
                Data.PlcWatchdog = true;
            }
        }

        private void PlcUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (DevicePLC.ReadPLC(Data.PlcIoDic["ResetAccept"]) == 1)
            {
                DevicePLC.WritePLC(Data.PlcIoDic["ResetAccept"], 0);
            }
            if (DevicePLC.ReadPLC(Data.PlcIoDic["InReset"]) == 1 && checkInReset == 0)
            {
                DevicePLC.WritePLC(Data.PlcIoDic["InOk"], 0);
                DevicePLC.WritePLC(Data.PlcIoDic["InNg"], 0);
                DevicePLC.WritePLC(Data.PlcIoDic["InDu"], 0);
                DevicePLC.WritePLC(Data.PlcIoDic["ResetAccept"], 1);
            }
            if (DevicePLC.ReadPLC(Data.PlcIoDic["OutReset"]) == 1 && checkOutReset == 0)
            {
                DevicePLC.WritePLC(Data.PlcIoDic["OutOk"], 0);
                DevicePLC.WritePLC(Data.PlcIoDic["OutNg"], 0);
                DevicePLC.WritePLC(Data.PlcIoDic["OutNoship"], 0);
                DevicePLC.WritePLC(Data.PlcIoDic["OutNodata"], 0);
                DevicePLC.WritePLC(Data.PlcIoDic["OutNoSeq"], 0);
                DevicePLC.WritePLC(Data.PlcIoDic["ResetAccept"], 1);
            }
            checkInReset = DevicePLC.ReadPLC(Data.PlcIoDic["InReset"]);
            checkOutReset = DevicePLC.ReadPLC(Data.PlcIoDic["OutReset"]);
        }

        private void SendTimer_Tick(object sender, EventArgs e)
        {
            List<Socket> client = WmsServer.CheckConnectedList();
            string outs = SendOUTS(); 
            string inst = SendINST();

            for (int i = 0; i < client.Count; i++)
            {
                WmsServer.SendMsg(client[i], outs);
                WmsServer.SendMsg(client[i], inst);
                //LogMsg.CMsg.Show("WMSSV", "send :"+i.ToString(), "inst: " + inst + "outs: " + outs, false, true);
            }

            //LogMsg.CMsg.Show("WMSSV", " SendTick", "INST: " + inst + "\r\n" + "OUTS: " + outs, false, true);

        }

        private void TimerDisplay_Tick(object sender, EventArgs e)
        {
            if (Data.DB_Connected) label_DB.BackColor = Color.Lime;
            else label_DB.BackColor = Color.Red;

            if (Data.PlcWatchdog) label_PLC.BackColor = Color.Lime;
            else label_PLC.BackColor = Color.Red;

            if (twinKleCount) label_PLC.BackColor = Color.White;

            if (WmsServer != null)
            {
                List<Socket> client = WmsServer.CheckConnectedList();

                label_Client1.Text = "";
                label_Client2.Text = "";
                label_Client3.Text = "";
                label_Client4.Text = "";
                label_Client1.BackColor = SystemColors.Control;
                label_Client2.BackColor = SystemColors.Control;
                label_Client3.BackColor = SystemColors.Control;
                label_Client4.BackColor = SystemColors.Control;

                int index = 0;
                foreach (Socket s in client)
                {
                    int idx = s.RemoteEndPoint.ToString().IndexOf(":");
                    string ip = s.RemoteEndPoint.ToString().Substring(0, idx);
                    index++;
                    if (index == 1)
                    {
                        label_Client1.Text = ip;
                        label_Client1.BackColor = Color.LightBlue;
                    }
                    else if (index == 2)
                    {
                        label_Client2.Text = ip;
                        label_Client2.BackColor = Color.LightBlue;
                    }
                    else if (index == 3)
                    {
                        label_Client3.Text = ip;
                        label_Client3.BackColor = Color.LightBlue;
                    }
                    else if (index == 4)
                    {
                        label_Client4.Text = ip;
                        label_Client4.BackColor = Color.LightBlue;
                    }
                }
            }
            twinKleCount = !twinKleCount;
        }


        private void Server_DataReceiveEventStr(Socket client, string str)
        {
            LogMsg.CMsg.Show("WMSS", "socket recvd: ", str + " client: "+ client.RemoteEndPoint.ToString(), false, true);
            
            string[] cmd = str.Trim().Replace("\u0002", "").Replace("\u0003", "").Split(':');
            string s = "";
            
            try
            {
                if (cmd[0].Contains("INWH"))
                {
                    int ret = db.Incomming(cmd[2], cmd[1], "", "", cmd[4], cmd[3]);
                    // 1: 정상 2: 에러 발생 4: 중복
                    LogMsg.CMsg.Show("WMSS", "db returned : ", ret.ToString(), false, true);

                    if (ret == 1)
                    {
                        WmsServer.SendMsg(client, $"INOK|{cmd[2]}");
                        DevicePLC.WritePLC(Data.PlcIoDic["InOk"], 1);
                    }
                    else if (ret == 2)
                    {
                        WmsServer.SendMsg(client, $"INNG|{cmd[2]}");
                        DevicePLC.WritePLC(Data.PlcIoDic["InNg"], 1);
                    }
                    else if (ret == 4 && cmd[4] == "0")
                    {
                        WmsServer.SendMsg(client, $"INDU|{cmd[2]}");
                        DevicePLC.WritePLC(Data.PlcIoDic["InDu"], 1);
                    }

                    if(ret == 4 && cmd[4] != "0")// 중복이고 1:noship or 2: acceptship 인경우
                    {
                        if (cmd[4] == "1")
                            ret = db.Update(" NoShip = 1 ", "ID = '" + cmd[2] + "' ");
                        if (cmd[4] == "2")
                            ret = db.Update(" NoShip = 0 ", "ID = '" + cmd[2] + "' ");

                        if (ret == 1) WmsServer.SendMsg(client, $"INUK|{cmd[2]}");
                        else if (ret == 2) WmsServer.SendMsg(client, $"INNG|{cmd[2]}");
                    }
                    Thread.Sleep(100);
                    s = SendINST();
                    Thread.Sleep(100);
                }
                else if(cmd[0].Contains("OUTC"))
                {
                    //id,model, Handler, to plant
                    int ret = db.Outgoing(cmd[2], cmd[1], null, cmd[3], Convert.ToInt16(cmd[4]));

                    LogMsg.CMsg.Show("WMSS", "out going return : ", ret.ToString(), false, true);
                    if (ret == 1)
                    {
                        WmsServer.SendMsg(client, $"OUTK|{cmd[2]}");// Ok
                        DevicePLC.WritePLC(Data.PlcIoDic["OutOk"], 1);
                    }
                    else if (ret == 2)
                    {
                        WmsServer.SendMsg(client, $"OUTE|{cmd[2]}");// error
                        DevicePLC.WritePLC(Data.PlcIoDic["OutNg"], 1);
                    }
                    else if (ret == 3)
                    {
                        WmsServer.SendMsg(client, $"OUTB|{cmd[2]}");// no ship
                        DevicePLC.WritePLC(Data.PlcIoDic["OutNoship"], 1);
                    }
                    else if (ret == 4)
                    {
                        WmsServer.SendMsg(client, $"OUTN|{cmd[2]}");// no data
                        DevicePLC.WritePLC(Data.PlcIoDic["OutNodata"], 1);
                    }
                    else if (ret == 5)
                    {
                        WmsServer.SendMsg(client, $"OUTQ|{cmd[2]}");// no sequence
                        DevicePLC.WritePLC(Data.PlcIoDic["OutNoSeq"], 1);
                    }

                    Thread.Sleep(100);
                    s = SendOUTS();
                    Thread.Sleep(100);
                }
                else if(cmd[0].Contains("PLST"))
                {
                    WmsServer.SendMsg(client, $"PLST|{Data.pList}");
                }

                WmsServer.SendMsg(client, s);
            }
            catch
            {
            }
        }

        private string SendINST()
        {
            //최근 입고 날짜 내림차순
            List<string> result = db.GetList("TOP (5) [Key]; [inDateTime]; [Model]; [ID]; [FromPlant]; [NoShip] ",
               $"[inDateTime] is not null Order by [inDateTime] desc");

            if (result == null)
            {
                Data.DB_Connected = false;
                return "";
            }
            Data.DB_Connected = true;

            string str = "INST|" + result.Count + "|";
            List<string> formattedResults = new List<string>();

            foreach (var res in result)
            {
                string[] s = res.Split(',');

                int seq = Convert.ToInt16(s[0].Substring(8));
                DateTime.TryParse(s[1], out DateTime dateTime);

                formattedResults.Add($"{dateTime.ToString("yy-MM-dd")}|{dateTime.ToString("HH:mm:ss")}|{s[2]}|{s[3]}|{seq.ToString()}|{s[4]}|{s[5]}");
            }

            str += string.Join("|", formattedResults);

            return str;
        }

        private string SendOUTS()
        {
            //처리 안된 상위 10개 날짜 오름차순
            List<string> result = db.GetShipList("TOP (5) [Key]; [Date]; [Sequence]; [Model]; [Quantity]; [CurrentCount]; [ToPlant]",
                $"Quantity <> [CurrentCount] OR [CurrentCount] IS NULL Order by [Key] ");

            if (result == null)
            {
                Data.DB_Connected = false;
                return "";
            }
            Data.DB_Connected = true;

            string str = "OUTS|" + result.Count + "|";
            List<string> formattedResults = new List<string>();

            foreach (var res in result)
            {
                string[] s = res.Split(',');
                //DateTime.TryParse(s[0], out DateTime dateTime);

                formattedResults.Add($"{s[0]}|{s[1]}|{s[2]}|{s[3]}|{s[4]}|{s[5]}|{s[6]}");
            }

            str += string.Join("|", formattedResults);

            return str;
        }

        private bool checkOUTB(string id, string model)
        {
            List<string> ban = db.GetList("NoShip", $"ID = '{id}' and Model = '{model}'");
            if (ban == null)
            {
                Data.DB_Connected = false;
                return false;
            }
            Data.DB_Connected = true;

            foreach (string b in ban)
            {
                if (bool.TryParse(b, out bool result))
                {
                    if (result)
                        return false;
                }

            }
            return true;
        }

        private bool checkINVH(string id, string model)
        {
            List<string> num = db.GetList("*", $"ID ='{id}' and Model = '{model}'");
            if (num == null)
            {
                Data.DB_Connected = false;
                return false;
            }
            Data.DB_Connected = true;

            if (num.Count > 0)
                return false;
            return true;
        }



        #region button event

        private void pLCConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SiemensPLC.frmSet cfg = new SiemensPLC.frmSet();
            cfg.ShowDialog();
        }

        private void dBConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DB.frmDBSet cfg = new DB.frmDBSet();
            cfg.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sendTimer.Stop();

            WmsServer.StopServer();
            SiemensPLC.SiemensPLC.Stop();


            Application.Exit();
        }

        
        #endregion



        #region WritePLC
        private ushort TurnOnBits(ref ushort nData, ushort mask)
        {
            ushort newData = (ushort)(nData | mask);  // OR 연산을 사용하여 비트를 켭니다.
            nData = newData;
            return WriteToPLC(newData);
        }

        private ushort TurnOffBits(ref ushort nData, ushort mask)
        {
            ushort newData = (ushort)(nData & ~mask);  // AND 연산과 NOT 연산을 사용하여 비트를 끕니다.
            nData = newData;
            return WriteToPLC(newData);
        }

        private ushort WriteToPLC(ushort data)
        {
            try
            {
                SiemensPLC.SiemensPLC.WriteAOData(SiemensPLC.SiemensPLC.DescriptionofChannel[SiemensPLC.SiemensPLC.m_nReadNum], data);
                Data.PLC_WriteData[0] = data;
                return data;
            }
            catch (Exception ex)
            {
                LogMsg.CMsg.Show("FormMDI", "WriteToPLC", ex.ToString(), false, true);
                return data;
            }
        }





        #endregion
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UpdateGrid();
        }

        private void engineShipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form c in this.MdiChildren) c.Close();
            FormEngine_Ship form = new FormEngine_Ship();
            form.MdiParent = this;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Show();
        }

        private void engineListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form c in this.MdiChildren) c.Close();
            FormEngineList form = new FormEngineList();
            form.MdiParent = this;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Show();
        }

        private void shippingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form c in this.MdiChildren) c.Close();
            FormShipList form = new FormShipList();
            form.MdiParent = this;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Show();
        }

        private void pLCIOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form c in this.MdiChildren) c.Close();
            FormPLC form = new FormPLC();
            form.MdiParent = this;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ANGFileInfo.ANGAboutBox about = new ANGFileInfo.ANGAboutBox();
            about.ShowDialog();
        }
    }
}

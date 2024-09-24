using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;//rex

namespace WmsServer
{
    public delegate void DeviceEventHandeler(string message);
    public delegate void DisplayEventHandeler(string message, bool display);
    public delegate void SendMsgEventHandeler(string message);

    static class DevicePLC
    {
        #region 변수
        public static event DeviceEventHandeler DeviceEvent;

        static System.Timers.Timer WriteTimer;
        static private Thread MeasuringThreadManager;
        static private bool MeasuringThreadCheck;

        #endregion

        static public void Start()///////////////////////start
        {
            LogMsg.CMsg.Show("DEVPLC", "start", false, true, LogMsg.CMsg.Picture.Blank);
            LoadDllImport();
            ThreadFuction();
        }

        static public void Stop()////////////////////////stop
        {
            try
            {
                LogMsg.CMsg.Show("DEVPLC", "stop ", false, true, LogMsg.CMsg.Picture.Blank);

                MeasuringThreadManager.Abort();
                MeasuringThreadCheck = false;
                //WSJ Reset ALL Value
                //for (int i = 0; i < Data.PlcData.CHANNEL_COUNT; i++)
                //{
                //    if (Data.PlcData.ChannelList[i].SgType == "AO" || Data.PlcData.ChannelList[i].SgType == "DO")
                //    {
                //        Data.PlcData.chValue[i] = 0.0F;
                //    }
                //}

                WriteTimer.Stop();

            }
            catch (Exception ExceptionResult)
            {
                LogMsg.CMsg.Show("DEVPLC", "stop error " + ExceptionResult.Message.ToString(), false, true, LogMsg.CMsg.Picture.Blank);
            }
        }

        static public bool LoadDllImport()
        {
            Data.deviceName = SiemensPLC.SiemensPLC.DeviceName;

            //Log.LogStr("", "Initialize 전");
            if (SiemensPLC.SiemensPLC.Initialize())
            {
                LogMsg.CMsg.Show("DEVPLC", "initialize passed " + Data.deviceName, false, true, LogMsg.CMsg.Picture.Blank);
                Data.AiCount = SiemensPLC.SiemensPLC.NoofAIChannel;
                Data.AoCount = SiemensPLC.SiemensPLC.NoofAOChannel;
                Data.DiCount = SiemensPLC.SiemensPLC.NoofDIChannel;
                Data.DoCount = SiemensPLC.SiemensPLC.NoofDOChannel;
            }
            else
            {
                LogMsg.CMsg.Show("DEVPLC", "initialize failed " + Data.deviceName, false, true, LogMsg.CMsg.Picture.Blank);
            }
            Data.IoDescription = SiemensPLC.SiemensPLC.DescriptionofChannel;
            int iocount = Data.AiCount + Data.AoCount + Data.DiCount + Data.DoCount;

            Data.IoDatas = new IODATA[iocount];
            for (int i = 0; i < iocount; i++)
            {
                Data.IoDatas[i].Name = Data.IoDescription[i];
                if (i < Data.AiCount) Data.IoDatas[i].Type = "AI";
                else if (i < Data.AiCount + Data.AoCount) Data.IoDatas[i].Type = "AO";
                else if (i < Data.AiCount + Data.AoCount + Data.DiCount) Data.IoDatas[i].Type = "DI";
                else if (i < Data.AiCount + Data.AoCount + Data.DiCount + Data.DoCount) Data.IoDatas[i].Type = "DO";

                Data.IoTypeDic.Add(Data.IoDatas[i].Name, Data.IoDatas[i].Type);
                Data.IoValueDic.Add(Data.IoDatas[i].Name, Data.IoDatas[i].fValue);
            }

            if (SiemensPLC.SiemensPLC.Start())
            {
                LogMsg.CMsg.Show("DEVPLC", "start device passed " + Data.deviceName, false, true, LogMsg.CMsg.Picture.Blank);
            }
            else
                LogMsg.CMsg.Show("DEVPLC", "start device failed " + Data.deviceName, false, true, LogMsg.CMsg.Picture.Blank);

            LogMsg.CMsg.Show("DEVPLC", "end of device loading ", false, true, LogMsg.CMsg.Picture.Blank);
            return true;
        }

        static public double ReadPLC(string chname)
        {
            if (chname == null) return 0;
            if (Data.IoDatas == null) return 0;

            if (Data.IoValueDic.ContainsKey(chname))
            {
                double value = Data.IoValueDic[chname];
                return value;
            }
            else
            {
                //if(DiReadOK) LogMsg.CMsg.Show("DEVPLC", "read plc ", chname +" is not used channel", false, true);
                return 0;
            }
        }

        static public double ReadPLC32(string chname)
        {
            if (chname == null) return 0;
            if (Data.IoDatas == null) return 0;
            try
            {
                string add1 = chname;
                int no = Convert.ToInt16(add1.Split(new char[] { 'D' })[1]);
                string add2 = add1.Split(new char[] { 'D' })[0] + "D" + (no + 1).ToString();

                double value = (Int32)(((uint)Data.IoValueDic[add1] & 0x0000FFFF) + (uint)Data.IoValueDic[add2] * 0x10000);
                return value;
            }
            catch { return 0; }
        }

        static public bool WritePLC(string chname, bool value)
        {
            if (value) return WritePLC(chname, 1.0);
            else return WritePLC(chname, 0.0);
        }

        static public bool WritePLC(string chname, double value)
        {
            if (Data.IoDescription == null) return false;

            int idx = Array.IndexOf(Data.IoDescription, chname.Trim());
            if (idx == -1) return false;
            Data.IoDatas[idx].fValue = (float)value;//update out memory
            Data.IoValueDic[chname] = (float)value;//update out memory

            if (Data.IoTypeDic[chname] == "AO")
            {
                return SiemensPLC.SiemensPLC.WriteAOData(chname, value);
            }
            else if (Data.IoTypeDic[chname] == "DO")
            {
                return SiemensPLC.SiemensPLC.WriteDOData(chname, Convert.ToBoolean(value));
            }
            return false;
        }

        static public bool WritePLC32(string add, double value)
        {
            bool ret = false;
            try
            {
                Int32 val = Convert.ToInt32(value);
                string add1 = add;
                int no = Convert.ToInt16(add1.Split(new char[] { 'D' })[1]);
                string add2 = add1.Split(new char[] { 'D' })[0] + "D" + (no + 1).ToString();
                int val1 = val & 0x0000FFFF;
                int val2 = (val >> 16) & 0x0000FFFF;

                ret = DevicePLC.WritePLC(add1, (double)val1);
                if (ret) ret = DevicePLC.WritePLC(add2, (double)val2);
            }
            catch { ret = false; }
            return ret;
        }

        static private void ThreadFuction()
        {
            LogMsg.CMsg.Show("DEVPLC", "start thread function", false, true, LogMsg.CMsg.Picture.Blank);
            if (MeasuringThreadCheck == false)
            {
                MeasuringThreadCheck = true;
                MeasuringThreadManager = new Thread(ReadThread);
                MeasuringThreadManager.Priority = ThreadPriority.Highest;
                MeasuringThreadManager.Start();    // Batch Read Start
            }
        }

        static private void ReadThread() //쓰레드로 dll값 읽어오기
        {
            #region 초기화
            long lStartTime;
            long lEndTime;
            long lReadIntervalTime = 100 * 10000;//100msec
            int nReadNum = 1;

            float[,] fAIData = new float[Data.AiCount, nReadNum];
            float[,] bDIData = new float[Data.DiCount, nReadNum];

            WriteTimer = new System.Timers.Timer();
            WriteTimer.Interval = 100;
            WriteTimer.Elapsed += new System.Timers.ElapsedEventHandler(WriteTimer_Tick);

            WriteTimer.Start();

            lStartTime = DateTime.Now.Ticks;

            LogMsg.CMsg.Show("DEVPLC", "start data reading ", "", false, true);
            #endregion 

            while (MeasuringThreadCheck)
            {
                lEndTime = DateTime.Now.Ticks;
                long lMeasuredInterval = (lEndTime - lStartTime);
                if (lMeasuredInterval >= lReadIntervalTime * 0.8)
                {
                    ////Daq.RealSamplingInterval = lMeasuredInterval;
                    //LogMsg.CMsg.Show("S", DateTime.Now.Ticks.ToString(), false, true, LogMsg.CMsg.Picture.Blank);  
                    if (lMeasuredInterval >= lReadIntervalTime * 1.1)// 시간이 10% 초과한 경우 표시
                    {
                        //LogMsg.CMsg.Show("measured interval", lMeasuredInterval.ToString() + "__" + (lStartTime3 - lStartTime2).ToString(), false, true, LogMsg.CMsg.Picture.Blank);
                    }
                    lStartTime += lReadIntervalTime;

                    Object[] parameters = new Object[2];

                    int startidx = 0;
                    try
                    {
                        if (Data.AiCount > 0)
                        {
                            startidx = 0;
                            if (SiemensPLC.SiemensPLC.ReadOneAI(ref fAIData))
                            {
                                for (int i = 0; i < Data.AiCount; i++)
                                {
                                    Data.IoDatas[startidx + i].fValue = fAIData[i, 0];
                                    Data.IoValueDic[Data.IoDatas[startidx + i].Name] = fAIData[i, 0];
                                }
                            }
                            else
                            {
                                LogMsg.CMsg.Show("DEVPLC", "batch read ai error" + Data.deviceName, false, true, LogMsg.CMsg.Picture.Blank);
                            }
                        }
                    }
                    catch (Exception ExceptionResult)
                    { LogMsg.CMsg.Show("DEVPLC", "batch read ai::" + Data.deviceName + "::" + ExceptionResult.Message.ToString(), false, true, LogMsg.CMsg.Picture.Blank); }
                    try
                    {
                        if (Data.DiCount > 0)
                        {
                            startidx = Data.AiCount + Data.AoCount;
                            if (SiemensPLC.SiemensPLC.ReadOneDI(ref bDIData))
                            {
                                for (int i = 0; i < Data.DiCount; i++)
                                {
                                    Data.IoDatas[startidx + i].fValue = bDIData[i, 0];
                                    Data.IoValueDic[Data.IoDatas[startidx + i].Name] = bDIData[i, 0];
                                }
                            }
                            else
                            {
                                LogMsg.CMsg.Show("DEVPLC", "batch read di error" + Data.deviceName, false, true, LogMsg.CMsg.Picture.Blank);
                            }
                        }
                    }
                    catch (Exception ExceptionResult)
                    { LogMsg.CMsg.Show("DEVPLC", "batch read di::" + Data.deviceName + "::" + ExceptionResult.Message.ToString(), false, true, LogMsg.CMsg.Picture.Blank); }

                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }

        static private void WriteTimer_Tick(object sender, EventArgs e)
        {
            for (int i = Data.AiCount; i < Data.AiCount + Data.AoCount; i++)
            {
                SiemensPLC.SiemensPLC.WriteAOData(Data.IoDatas[i].Name, Data.IoDatas[i].fValue);
            }
            for (int i = Data.AiCount + Data.AoCount + Data.DiCount; i < Data.AiCount + Data.AoCount + Data.DiCount + Data.DoCount; i++)
            {
                SiemensPLC.SiemensPLC.WriteDOData(Data.IoDatas[i].Name, Convert.ToBoolean(Data.IoDatas[i].fValue));
            }
        }

        static public void EventfromDevice(string s)
        {
            LogMsg.CMsg.Show("PLCSEQ", "Receive Event from device [" + s + "]", false, true, LogMsg.CMsg.Picture.Blank);
            DeviceEvent(s);
        }

        static public void SendMessagetoDevice(string device, string message)
        {
            //int deviceindex = 0;
            ////for (int i = 0; i < DAQData.DeviceList.Count; i++)
            ////{
            ////    if (DAQData.DeviceList[i].ToString() == device)
            ////    {
            ////        deviceindex = i; break;
            ////    }
            ////}

            ////Object[] parameters = new Object[1];
            ////object b;
            ////parameters[0] = message;
            ////MethodInfo mi = typeinfo[deviceindex].GetMethod("SendMessage");
            ////b = mi.Invoke(obj[deviceindex], parameters);
            //LogMsg.CMsg.Show("DAQ", "SendMessage : " + deviceindex.ToString() + "*" + device + " " + message, false, true, LogMsg.CMsg.Picture.Error);
        }
    }
}

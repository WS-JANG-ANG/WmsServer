using System;
using System.IO;
using System.Text;

using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Net;

namespace SiemensPLC
{
    public class SiemensPLC
    {
        #region // 표준 속성
        public static string FileName
        {
            get { return "SiemensPLC"; }
        }

        public static string Description
        {
            get { return "SiemensPLC"; }
        }

        public static string Version
        {
            get { return "SiemensPLC"; }
        }

        public static string Type
        {
            get { return "DAQ_DEVICE"; }
        }

        public static string DeviceName
        {
            get { return m_strDevName; }
        }

        public static int NoofAIChannel
        {
            get { return m_nAIChNum; }
        }

        public static int NoofAOChannel
        {
            get { return m_nAOChNum; }
        }

        public static int NoofDIChannel
        {
            get { return m_nDIChNum; }
        }

        public static int NoofDOChannel
        {
            get { return m_nDOChNum; }
        }

        public static string[] DescriptionofChannel
        {
            get { return m_strDescofCh; }
        }

        public static string[] PhysicalRangeofChannel
        {
            get { return m_strPhysicalRangeofCh; }
        }

        protected static int m_nSamplingRate = 1000;   // Sampling Number / 1 second
        public static int SamplingRate
        {
            get { return m_nSamplingRate; }
            set { m_nSamplingRate = value; }
        }

        #endregion

        #region // 표준 변수

        static string m_strDevName = "Siemens PLC";  // Card Device Name 

        static int m_nAIChNum = 0;              // 제공되는 AI 채널 최대 개수
        static int m_nDIChNum = 0;              // 제공되는 DI 채널 최대 개수
        static int m_nAOChNum = 0;              // 제공되는 AO 채널 최대 개수
        static int m_nDOChNum = 0;              // 제공되는 DO 채널 최대 개수

        protected static int m_nNoofAICh = 0;   // 제공되는 AI 채널 최대 개수
        protected static int m_nNoofAOCh = 0;   // 제공되는 AO 채널 최대 개수
        protected static int m_nNoofDICh = 0;   // 제공되는 DI 채널 최대 개수
        protected static int m_nNoofDOCh = 0;   // 제공되는 DO 채널 최대 개수
        protected static int m_nNoofCICh = 0;   // 제공되는 Pulse 채널 최대 개수

        static int m_nTotalChNum = 0;           // 전체 채널
        static string[] m_strDescofCh;
        protected static string[] m_strPhysicalRangeofCh;

        public static UInt16[] m_nAO;
        public static UInt16[] m_nDO;

        public static UInt16[] m_nAI;
        public static UInt16[] m_nDI;

        #endregion

        #region // 임의변수
        private static List<string> plcNames = new List<string>();
        private static List<PLC> listPlc = new List<PLC>();

        public static Dictionary<string, List<string>> IniLoadDic = new Dictionary<string, List<string>>();

        // 단일 버퍼
        public static float[] sBuffer_DI;
        public static float[] sBuffer_AI;
        public static float[] sBuffer_DO;
        public static float[] sBuffer_AO;

        // 다중 버퍼
        public static float[,] ArrayBuffer_DI;
        public static float[,] ArrayBuffer_AI;
        public static float[,] ArrayBuffer_DO;
        public static float[,] ArrayBuffer_AO;

        // 다중 버퍼 색인
        public static int ArrayBuffer_DI_Index = 0;
        public static int ArrayBuffer_AI_Index = 0;
        public static int ArrayBuffer_DO_Index = 0;
        public static int ArrayBuffer_AO_Index = 0;

        // 다중 버퍼 색인
        public static int ArrayBuffer_DI_Index_Read = 0;
        public static int ArrayBuffer_AI_Index_Read = 0;
        public static int ArrayBuffer_DO_Index_Read = 0;
        public static int ArrayBuffer_AO_Index_Read = 0;

        // 스레드 핸들
        public static Thread thdValueWatch;
        public static bool thdValueWatchChk = false;
        public static bool ThreadActive = false;

        public static Thread thdValueWatch_W;
        public static bool thdValueWatchChk_W = false;

        public static bool bMode = false; // true: daq. false: 단일      

        // 채널 테이블
        public static List<string> AI_TABLE = new List<string>();
        public static List<string> AO_TABLE = new List<string>();
        public static List<string> DI_TABLE = new List<string>();
        public static List<string> DO_TABLE = new List<string>();

        public static System.Windows.Forms.Timer RetryConnectionTimer = new System.Windows.Forms.Timer();

        public static int m_nReadNum = 0;
        public static int m_nWriteNum = 0;

        public static UInt16[] m_nReadData;
        public static UInt16[] m_nWriteData;

        private static Socket[] m_socClient;
        static Boolean[] m_bSocketConnected;
        static int BUF_LEN = 4096;
        private static int m_nCommErrCnt = 0;

        //static byte[] m_tmpBuf = new byte[BUF_LEN];
        private static int m_nPLCTotalErrorCnt = 0;
        #endregion

        #region // 표준함수
        public static Boolean Initialize()
        {
            LogMsg.CMsg.Show("SiePLC", "initialize", "", false, true);
            listPlc = IniOpen();
            if (listPlc == null)
            {
                MessageBox.Show("File Not Found ");
                return false;
            }

            #region //외부 노출 변수 조합     
            List<string> ChannelList = new List<string>();

            for (int i = 0; i < listPlc.Count; i++)
            {
                for (int j = 0; j < listPlc[i].NoofRead; j++)
                {
                    ChannelList.Add(String.Format(listPlc[i].Name + "_IN{0:D2}", j));
                }
            }
            m_nAIChNum = ChannelList.Count;
            m_nNoofAICh = m_nAIChNum;
            m_nReadNum = m_nAIChNum;

            for (int i = 0; i < listPlc.Count; i++)
            {
                for (int j = 0; j < listPlc[i].NoofWrite; j++)
                {
                    ChannelList.Add(String.Format(listPlc[i].Name + "_OUT{0:D2}", j));
                }
            }
            m_nAOChNum = ChannelList.Count - m_nAIChNum;
            m_nNoofAOCh = m_nAOChNum;
            m_nWriteNum = m_nAOChNum;

            m_nReadData = new UInt16[m_nAIChNum];
            m_nWriteData = new UInt16[m_nAOChNum];

            m_nTotalChNum = m_nAIChNum + m_nAOChNum;
            m_strDescofCh = new string[m_nTotalChNum];

            for (int i = 0; i < ChannelList.Count; i++)
                m_strDescofCh[i] = ChannelList[i];
            #endregion

            // 물리주소
            m_strPhysicalRangeofCh = new string[] { "0 ~ +10V", "-10 ~ +10V", "-5 ~ +5V"};

            // 단일 버퍼
            sBuffer_DI = new float[m_nDIChNum];
            sBuffer_AI = new float[m_nAIChNum];
            sBuffer_DO = new float[m_nDOChNum];
            sBuffer_AO = new float[m_nAOChNum];

            ArrayBuffer_DI = new float[m_nDIChNum, m_nSamplingRate];
            ArrayBuffer_AI = new float[m_nAIChNum, m_nSamplingRate];
            ArrayBuffer_DO = new float[m_nDOChNum, m_nSamplingRate];
            ArrayBuffer_AO = new float[m_nAOChNum, m_nSamplingRate];

            RetryConnectionTimer.Interval = 5000;
            RetryConnectionTimer.Tick += new EventHandler(RetryConnectionTimer_Tick);
            RetryConnectionTimer.Start();
            LogMsg.CMsg.Show("SiePLC", "Initializeed", "", false, true);
            return true;
        }   
    
        private static List<PLC> IniOpen()
        {
            string IniFilePath = Application.StartupPath + "\\Devices\\SiemensPLC.INI";
            List<PLC> result = new List<PLC>();

            FileInfo f = new FileInfo(IniFilePath);
            if (f.Exists == false) return null;

            StringBuilder SB = new StringBuilder();
            SB.Capacity = 256;

            SiemensPLC.GetPrivateProfileString("PLC", "LIST", "", SB, 256, IniFilePath);
            string[] temp = SB.ToString().Split(new char[] { ',' });
            plcNames.Clear();
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] != "") plcNames.Add(temp[i]);
            }

            for (int i = 0; i < plcNames.Count; i++)
            {
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
                    result.Add(plc);
                }
                catch { }
            }


            LogMsg.CMsg.Show("SiePLC", "ini opened", "", false, true);
            return result;
        }

        // 스레드 시작
        public static Boolean Start()
        {
            LogMsg.CMsg.Show("SiePLC", "start", "", false, true);
            InitSocket();
            for (int i = 0; i < listPlc.Count; i++)
            {
                Connect(i);
            }
            if (thdValueWatchChk == false)
            {
                thdValueWatch = new Thread(thdValueWatch_Thread);
                thdValueWatch.Start();
                thdValueWatchChk = true;
            }
            else if (thdValueWatchChk == true)
            {
                thdValueWatchChk = false;
                thdValueWatch.Abort();
            }

            return true;
        }        

        // 스레드 종료
        public static Boolean Stop()
        {
            LogMsg.CMsg.Show("SiePLC", "stop", "", false, true);
            if (m_socClient == null)
                return false;
            for (int i = 0; i < listPlc.Count; i++)
            {
                Disconnect(i);
            }
            Thread.Sleep(100);
            RetryConnectionTimer.Stop();

            if (thdValueWatchChk == true)
            {
                thdValueWatchChk = false;
                thdValueWatch.Abort();
            }
            return true;
        }

        private static Boolean InitSocket()
        {
            LogMsg.CMsg.Show("SiePLC", "init socket", "", false, true);

            m_bSocketConnected = new bool[listPlc.Count];
            m_socClient = new Socket[listPlc.Count];

            for (int i = 0; i < listPlc.Count; i++)
            {
                try
                {
                    m_bSocketConnected[i] = false;
                    m_socClient[i] = null;

                    m_socClient[i] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                    // Local End Point Set
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(listPlc[i].Local_IP), int.Parse(listPlc[i].Local_Port));
                    m_socClient[i].SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                    Thread.Sleep(500);

                    m_socClient[i].Bind(endPoint);
                }
                catch (Exception exep)
                {
                    LogMsg.CMsg.Show("SiePLC", "init socket error", exep.Message, false, true);
                }
            }
            return true;

        }

        public static void Connect(int pidx)
        {
            Disconnect(pidx);
            m_bSocketConnected[pidx] = false;
            LogMsg.CMsg.Show("SiePLC", "try connect", listPlc[pidx].Name +"  " +listPlc[pidx].PLC_IP + "," + listPlc[pidx].PLC_Port, false, true);

            try
            {
                // Ping Test
                Ping pingSender = new Ping();
                PingReply mReply = pingSender.Send(listPlc[pidx].PLC_IP, int.Parse(listPlc[pidx].PLC_Port));
                if (mReply.Status != IPStatus.Success)
                {
                    LogMsg.CMsg.Show("SiePLC", "siemens plc server is not reponse", listPlc[pidx].PLC_IP + "," + listPlc[pidx].PLC_Port, true, true);
                    return;
                }

                m_socClient[pidx].Connect(listPlc[pidx].PLC_IP, int.Parse(listPlc[pidx].PLC_Port));
                m_bSocketConnected[pidx] = m_socClient[pidx].Connected;
                Thread.Sleep(100);
            }
            catch (SocketException exep)
            {
                LogMsg.CMsg.Show("SiePLC", "connect error ", listPlc[pidx].Name + "  " + exep.Message, false, true);
                Disconnect(pidx);
            }
            catch (InvalidOperationException exep)
            {
                LogMsg.CMsg.Show("SiePLC", "connect error ", listPlc[pidx].Name + "  " + exep.Message, false, true);
                m_socClient[pidx].Close();
                m_socClient[pidx] = null;
                InitSocket();
            }

            finally
            {
                if (m_socClient[pidx] == null)
                {
                    m_bSocketConnected[pidx] = false;
                    //bThreadChk = false;

                    //m_socClient.Close();
                    //m_socClient = null;
                }
            }
            LogMsg.CMsg.Show("SiePLC", "connected", listPlc[pidx].Name, false, true);
        }

        // TCP Port Close
        public static void Disconnect(int pidx)
        {
            try
            {
                if (m_socClient[pidx] != null)
                {
                    LogMsg.CMsg.Show("SiePLC", "disconnect", listPlc[pidx].Name, false, true);
                    if (m_socClient[pidx].Connected)
                    {
                        m_socClient[pidx].Close();
                        m_socClient[pidx].Disconnect(false);
                    }
                }
            }
            catch (Exception exep)
            {
                LogMsg.CMsg.Show("SiePLC", "disconnect error", exep.Message, false, true);
                return;
            }

            m_bSocketConnected[pidx] = false;
            LogMsg.CMsg.Show("SiePLC", "disconnected", listPlc[pidx].Name, false, true);
        }
        #endregion

        #region // DI- 다중버퍼 읽기
        public static Boolean ReadDI(int nReadNum, ref float[,] _fData)
        {
            //for (int i = 0; i < nReadNum; i++)
            //{
            //    for (int j = 0; j < m_nNoofDICh; j++)
            //    {
            //        _fData[j, i] = ArrayBuffer_DI[j, ArrayBuffer_DI_Index_Read];

            //        if (ArrayBuffer_DI_Index_Read >= (m_nSamplingRate - 1))
            //            ArrayBuffer_DI_Index_Read = 0;
            //        ArrayBuffer_DI_Index_Read++;
            //    }
            //}           
            return true;
        }

        #endregion

        #region // AI- 다중버퍼 읽기
        public static Boolean ReadAI(int nReadNum, ref float[,] _fData)
        {
            int count = 0;

            if (ArrayBuffer_AI_Index > ArrayBuffer_AI_Index_Read)
            {
                count = ArrayBuffer_AI_Index - ArrayBuffer_AI_Index_Read;
            }
            else
            {
                count = ArrayBuffer_AI_Index + (m_nSamplingRate - ArrayBuffer_AI_Index_Read);
                if (count > m_nSamplingRate) count = count - m_nSamplingRate;
            }
            float interval = (float)count / (float)nReadNum;
            int index = ArrayBuffer_AI_Index_Read;

            for (int i = 0; i < nReadNum; i++)
            {
                for (int j = 0; j < m_nNoofAICh; j++)
                {
                    _fData[j, i] = ArrayBuffer_AI[j, ArrayBuffer_AI_Index_Read];
                }

                ArrayBuffer_AI_Index_Read = index + (int)((i + 1) * interval);
                if (ArrayBuffer_AI_Index_Read >= m_nSamplingRate)
                    ArrayBuffer_AI_Index_Read = 0;
                Thread.Sleep(2);
            }

            return true;
        }
        #endregion

        #region // DO - 쓰기 by UTS Call Function
        static int m_nWriteDOCallCount = 0;
        public static Boolean WriteDOData(string _port, bool _bData)
        {
            m_nWriteDOCallCount++;
 

            int sgindex = 0;
            for (int i = m_nNoofAICh + m_nNoofAOCh + m_nNoofDICh;
                    i < m_nNoofDOCh + m_nNoofAICh + m_nNoofAOCh + m_nNoofDICh;
                    i++)
            {
                if (m_strDescofCh[i] == _port)
                {
                    sgindex = i - (m_nNoofAICh + m_nNoofAOCh + m_nNoofDICh);
                    break;
                }
            }

            if (_bData == true)
                sBuffer_DO[sgindex] = 1.0F;
            else
                sBuffer_DO[sgindex] = 0.0F;

            return true;
        }
        #endregion

        #region // AO - 쓰기 by UTS Call Function
        static int m_nWriteAOCallCount = 0;
        public static Boolean WriteAOData(string _port, double _bData)
        {
            m_nWriteAOCallCount++;

            int sgindex = 0;
            for (int i = m_nNoofAICh; i < m_nNoofAOCh + m_nNoofAICh; i++)
            {
                if (m_strDescofCh[i] == _port)
                {
                    sgindex = i - m_nNoofAICh;
                    break;
                }
            }

            sBuffer_AO[sgindex] = Convert.ToSingle(_bData);// 기존 사용 버퍼
            m_nWriteData[sgindex] = Convert.ToUInt16(_bData);// 지멘스용 버퍼
            return true;
        }
        #endregion

        #region // AI - 단일버퍼 읽기 by UTS Call Function
        static int m_nReadOneAICallCount = 0;
        public static Boolean ReadOneAI(ref float[,] _fData)
        {
            m_nReadOneAICallCount++;
            for (int xcount = 0; xcount < m_nNoofAICh; xcount++)            
                _fData[xcount, 0] = sBuffer_AI[xcount];         
            return true;
        }
        #endregion

        #region // DI - 단일버퍼 읽기 by UTS Call Function
        static int m_nReadOneDICallCount = 0;
        public static Boolean ReadOneDI(ref float[,] _fData)
        {
            m_nReadOneDICallCount++;

            for (int xcount = 0; xcount < m_nNoofDICh; xcount++)            
                _fData[xcount, 0] = sBuffer_DI[xcount];                            
            return true;
        }

        // 설정
        public static void SetDlg()
        {
            frmSet mDlg = new frmSet();
            mDlg.ShowDialog();
        }

        #endregion

        #region // 임의함수
        public static void thdValueWatch_Thread()
        {
            // PLC 값을 읽고 쓰기위한 thread
            LogMsg.CMsg.Show("SiePLC", "start thread", "", false, true);
            while (thdValueWatchChk)
            {
                for (int i = 0; i < listPlc.Count; i++)
                {
                    m_bSocketConnected[i] = m_socClient[i].Connected;
                    //LogMsg.CMsg.Show("SiePLC", "check conn", listPlc[i].Name + " " + m_bSocketConnected[i].ToString(), false, true);
                    try
                    {
                        if (m_bSocketConnected[i])
                        {
                            if (!ReadPLCData(i))
                                m_nCommErrCnt++;
                            else
                                m_nCommErrCnt = 0;

                            if (!WritePLCData(i))
                                m_nCommErrCnt++;
                            else
                                m_nCommErrCnt = 0;
                        }
                        else
                        {
                            LogMsg.CMsg.Show("SiePLC", "connection fail", listPlc[i].Name, false, true);
                            Disconnect(i);
                            m_nCommErrCnt = 0;

                            Thread.Sleep(200);
                            Connect(i);
                        }

                        Thread.Sleep(200);
                    }
                    catch (Exception e)
                    {
                        LogMsg.CMsg.Show("SiePLC", listPlc[i].Name + "::thread error", e.Message, false, true);
                    }
                }
          
            }
            LogMsg.CMsg.Show("SiePLC", "end thread", "", false, true);
        }

        public static bool ReadPLCData(int pidx)
        {
            byte[] szTmp = new byte[BUF_LEN];
            int nLen = 0;
            int nStartIndex = 0;
            for (int i = 0; i < pidx; i++)// 버퍼 시작 위치 결정, 이전 PLC 숫자 이후에 사용
            {
                nStartIndex = +listPlc[i].NoofRead;
            }

            try
            {
                if (!m_bSocketConnected[pidx]) return false;
                //m_socClient.Blocking = true;
                m_socClient[pidx].ReceiveTimeout = 100;
                nLen = m_socClient[pidx].Receive(szTmp);
                //m_socClient.Blocking = false;

                if (nLen > 0)
                {
                    if (nLen > BUF_LEN) nLen = BUF_LEN;
                    else
                    {
                        if (nLen >= listPlc[pidx].NoofRead)
                        {
                            for (int j = 0; j < listPlc[pidx].NoofRead; j++)
                            {
                                m_nReadData[nStartIndex] = (ushort)(szTmp[j]);
                                ArrayBuffer_AI[nStartIndex, ArrayBuffer_AI_Index_Read] = (float)m_nReadData[nStartIndex];
                                sBuffer_AI[nStartIndex] = (float)m_nReadData[nStartIndex];
                                nStartIndex++;
                            }
                        }
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception exep)
            {
                LogMsg.CMsg.Show("SiePLC", listPlc[pidx].Name+"::read plc data error", exep.Message, false, true);
                return false;
            }
        }

        public static bool WritePLCData(int pidx)
        {
            try
            {
                byte[] bSendData = new byte[BUF_LEN];
                int nStartIndex = 0;
                for (int i = 0; i < pidx; i++)// 버퍼 시작 위치 결정, 이전 PLC 숫자 이후에 사용
                {
                    nStartIndex = +listPlc[i].NoofRead;
                }

                for (int i = 0; i < listPlc[pidx].NoofWrite; i++)
                {
                    bSendData[i] = (byte)(m_nWriteData[nStartIndex + i] & 0xFF);
                }

                //m_socClient.Blocking = true;
                m_socClient[pidx].SendTimeout = 100;
                m_socClient[pidx].Send(bSendData, m_nWriteNum, SocketFlags.None);
                //m_socClient.Blocking = false;
                return true;
            }
            catch (Exception exep)
            {
                LogMsg.CMsg.Show("SiePLC", listPlc[pidx].Name + "::write plc data error", exep.Message, false, true);
                return false;
            }
        }

        static void RetryConnectionTimer_Tick(object sender, EventArgs e)
        {
            ///////////////////////////////////////////////////////////////////////////////
            // UTS Function Call Check
            if (m_nAIChNum > 0)
            {
                if (m_nReadOneAICallCount == 0)
                    //LogMsg.CMsg.Show("SiePLC", "uts readoneai function call timeout", "", false, true);
                m_nReadOneAICallCount = 0;
            }
                        
            if (m_nAOChNum > 0)
            {
                if (m_nWriteAOCallCount == 0)
                    //LogMsg.CMsg.Show("SiePLC", "uts writeaodata function call timeout", "", false, true);

                m_nWriteAOCallCount = 0;
            }
            ///////////////////////////////////////////////////////////////////////////////
       
            ///////////////////////////////////////////////////////////////////////////////
            // Retry Connection & PLC Function Call Error
            if (m_nPLCTotalErrorCnt > 10)
            {
                //LogMsg.CMsg.Show("PLC", "DI Error Count = " + m_nDIErrorCnt.ToString(), "", false, true);
                //LogMsg.CMsg.Show("PLC", "DO Error Count = " + m_nDOErrorCnt.ToString(), "", false, true);
                //LogMsg.CMsg.Show("PLC", "AI Error Count = " + m_nAIErrorCnt.ToString(), "", false, true);
                //LogMsg.CMsg.Show("PLC", "AO Error Count = " + m_nAOErrorCnt.ToString(), "", false, true);


                //LogMsg.CMsg.Show("PLC", "PLC Try ReConnect", "", false, true);
                //MxClose();
                Thread.Sleep(10);
                //bool bOK = MxOpen();

                //if (bOK == true)
                //    LogMsg.CMsg.Show("PLC", "PLC ReConnect OK", "", false, true);
                //else
                //    LogMsg.CMsg.Show("PLC", "PLC ReConnect Fail", "", false, true);

                //m_nAIErrorCnt = 0;
                //m_nDIErrorCnt = 0;
                //m_nAOErrorCnt = 0;
                //m_nDOErrorCnt = 0;
                m_nPLCTotalErrorCnt = 0;             
            
            }
            ///////////////////////////////////////////////////////////////////////////////

        }

        #endregion

        #region // for frmTest
        public static UInt16[] ReadData()
        {
            return m_nReadData;
        }

        ////public static Boolean WriteData(UInt16[] _nWriteData)
        ////{
        ////    m_nWriteData = _nWriteData;
        ////    return true;
        ////}
        #endregion

        #region // INI
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int WritePrivateProfileString(string asAppName, string asKeyName, string asValue, string asFileName);
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileString(string asAppName, string asKeyName, string asDefault, StringBuilder asValue, int aiSize, string asFileName);
        #endregion
    }
}
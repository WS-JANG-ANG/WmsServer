﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace WmsServer
{
    class Client
    {
        public delegate void ReceiveEventHandler(byte[] ba);
        public event ReceiveEventHandler ReceiveEvent;

        public delegate void DataReceiveStringEventHandler(string str);
        public event DataReceiveStringEventHandler DataReceiveEventStr;
        public delegate void DataReceiveByteEventHandler(byte[] ba);
        public event DataReceiveByteEventHandler DataReceiveEventByte;

        TcpClient tcpClient;

        private Thread tcpClientRcvThread;
        private bool onRecieve = true;
        private Thread KeepAliveThread;
        private bool keepAliveCheck = false;
        private string sReceive = "";
        private string sSend = "";
        private bool bConnected = false;

        private string IP;
        private int Port;

        public Boolean Connected { get { return bConnected; } set { bConnected = value; } }

        #region client
        public bool StartClient(string ip, int port)
        {
            if ((ip == null) || (port == 0))
            {
                LogMsg.CMsg.Show("TCP_C", "start fail ip=null or port=0", "", false, true);
                return false;
            }
            LogMsg.CMsg.Show("TCP_C", "start client " + ip + ":" + port.ToString(), "", false, true);
            IP = ip;
            Port = port;
            try
            {
                Connect(ip, port);

                KeepAliveThread = new Thread(KeepAlive);
                keepAliveCheck = true;
                KeepAliveThread.Start();
            }
            catch (Exception ex)
            {
                LogMsg.CMsg.Show("TCP_C", "start fail ip =" + ip + ",port=" + port.ToString(), "", false, true);
                LogMsg.CMsg.Show("TCP_C", "start fail " + ex.ToString(), "", false, true);
                return false;
            }

            return true;
        }

        public bool StopClient()
        {
            Disconnect();
            keepAliveCheck = false;
            if (KeepAliveThread != null)
                KeepAliveThread.Abort();
            LogMsg.CMsg.Show("TCP_C", "stop tcp client ", "", false, true);
            return true;
        }

        private Boolean Connect(String ipaddress, int nPort)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(ipaddress);
                IPEndPoint srvEndip = new IPEndPoint(ip, nPort);
                tcpClient = new TcpClient();
                tcpClient.Connect(srvEndip);

                //tcpClient.BeginConnect(ip, nPort, m_pfnCallBack, null);

                //tcpClientRcvThread = new Thread(new ThreadStart(tcpClientThread));
                //tcpClientRcvThread.IsBackground = true;
                //onRecieve = true;
                //tcpClientRcvThread.Start();

                //Ready = true;
                //MessageBox.Show("connected");
                return true;
            }
            catch (Exception err)
            {
                LogMsg.CMsg.Show("TCP_C", "connect fail: ", err.Message, false, true);
                return false;
            }
        }

        private Boolean Disconnect()
        {
            if (tcpClient != null)
            {
                if (tcpClient.Connected) tcpClient.Close();
                if (tcpClientRcvThread != null) tcpClientRcvThread.Abort();
            }
            return true;
        }

        //thread for check receive
        private void tcpClientThread()
        {
            //CheckForIllegalCrossThreadCalls = false;
            try
            {
                //MessageBox.Show("received");
                NetworkStream rcvStream = tcpClient.GetStream();
                while (onRecieve)
                {
                    int d = tcpClient.Available;
                    byte[] rcvMsg = new byte[d];
                    int tSize = rcvStream.Read(rcvMsg, 0, rcvMsg.Length);
                    //MessageBox.Show("received" + tSize.ToString());
                    if (tSize > 0)
                    {
                        Array.Resize<byte>(ref rcvMsg, tSize);          //크기를 다시 맞춤.

                        if (rcvMsg.Length != 0)
                        {
                            ReceiveData(rcvMsg);
                            if (ReceiveEvent != null) ReceiveEvent(rcvMsg);
                        }
                    }
                    else
                    {
                        //onRecieve = false;
                    }
                }
            }
            catch (Exception err)
            {
                //Ready = false;
                LogMsg.CMsg.Show("TCP_C", "error: ", err.Message, false, true);
            }
        }

        private void ReceiveData(byte[] Data)
        {
            try
            {
                //LogMsg.CMsg.Show("TCP", "ReceiveData_" + Data.Length.ToString());
                //receivedData.AddRange(Data);
                sReceive = Encoding.Default.GetString(Data);
                //LogMsg.CMsg.Show("TCP_C", "received : " + sReceive);

                //if (DataReceiveEvent2 != null) DataReceiveEvent2(Data);
                //if (DataReceiveEvent != null) DataReceiveEvent(sReceive);

                if (DataReceiveEventByte != null) DataReceiveEventByte(Data);
                if (DataReceiveEventStr != null) DataReceiveEventStr(sReceive);

                #region check stx etx
                //int istx = -1;
                //int ietx = -1;
                //byte[] buffT;
                //LogMsg.CMsg.Show("TCP", "ReceiveData_" + receivedData.Count.ToString());
                //for (int i = 0; i < receivedData.Count; i++)
                //{
                //    if (receivedData[i] == 0x02)
                //    {
                //        istx = i;
                //        break;
                //    }
                //}
                //if (istx > -1)
                //{
                //    for (int i = istx; i < receivedData.Count; i++)
                //    {
                //        if (receivedData[i] == 0x03)
                //        {
                //            ietx = i;
                //            break;
                //        }
                //    }
                //}
                //else
                //{
                //    receivedData.Clear();
                //}

                //LogMsg.CMsg.Show("TCP", "stx=" + istx.ToString() + "etx=" + ietx.ToString());
                //if (istx > -1 && ietx > -1 && istx < ietx)
                //{
                //    buffT = new byte[ietx - istx +1];
                //    receivedData.CopyTo(istx, buffT, 0, buffT.Length);
                //    receivedData.RemoveRange(0, ietx+1);

                //    sReceive = Encoding.Default.GetString(buffT);
                //    LogMsg.CMsg.Show("TCP", "receive : " + sReceive);
                //    if (DataReceiveEvent2 != null)
                //        DataReceiveEvent2(buffT);

                //    if (DataReceiveEvent != null)
                //        DataReceiveEvent(sReceive);

                //}
                //else
                //{
                //    sReceive = "";
                //}

                #endregion


            }
            catch (Exception err)
            {
                LogMsg.CMsg.Show("TCP_C", "receive data error ", err.ToString(), false, true);
            }
        }

        public Boolean SendData(byte[] _bSendData, int _nSize)
        {
            try
            {
                NetworkStream sndStream = tcpClient.GetStream();
                sndStream.Write(_bSendData, 0, _bSendData.Length);

                sSend = Encoding.Default.GetString(_bSendData);
                //LogMsg.CMsg.Show("TCP_C", "sent msg : " + sSend);

                return true;
            }
            catch (Exception err)
            {
                LogMsg.CMsg.Show("TCP_C", "receive data error ", err.ToString(), false, true);
                return false;
            }
        }

        public Boolean SendMsg(string buffer)
        {
            //LogMsg.CMsg.Show("TCP_C", "send msg : " + buffer);

            byte[] buf = Encoding.Default.GetBytes(buffer);
            byte[] message = new byte[buf.Length];

            message = new byte[buf.Length + 2];
            message[0] = 0x02;
            Array.Copy(buf, 0, message, 1, buf.Length);
            message[buf.Length + 1] = 0x03;

            try
            {
                NetworkStream sndStream = tcpClient.GetStream();
                sndStream.Write(message, 0, message.Length);

                sSend = Encoding.Default.GetString(message);
                //LogMsg.CMsg.Show("TCP_C", "sent msg : " + sSend);
                //string DataTime = DateTime.Now.ToString("HH:mm:ss");
                //txt_View.AppendText("[" + DataTime + "]" + "[send]" + sSend + "\n");

                return true;
            }
            catch (Exception err)
            {
                LogMsg.CMsg.Show("TCP_C", "receive data error ", err.ToString(), false, true);
                return false;
            }
        }

        private void KeepAlive()
        {
            while (keepAliveCheck)
            {
                try
                {
                    bConnected = tcpClient.Connected;

                    if (!bConnected)
                    {
                        Disconnect();
                        Thread.Sleep(1000);
                        Connect(IP, Port);
                    }
                }
                catch { }

                Thread.Sleep(1000);
            }
        }

        #endregion
    }

    class Server
    {
        //public delegate void ReceiveEventHandler(byte[] ba);
        //public event ReceiveEventHandler ReceiveEvent;

        public delegate void DataReceiveStringEventHandler(Socket client, string str);
        public event DataReceiveStringEventHandler DataReceiveEventStr;
        public delegate void DataReceiveByteEventHandler(Socket client, byte[] ba);
        public event DataReceiveByteEventHandler DataReceiveEventByte;


        private Thread KeepAliveThread;
        private bool keepAliveCheck = false;

        private const int BUF_SIZE = 8192;  // 소켓 버퍼 사이즈
        private const int CLIENT_NUM = 20;  // 클라이언트 접속 최대 개수
        private const byte STX = 0x02;      // 데이타 Frame Start
        private const byte ETX = 0x03;      // 데이타 Frame End
        private Socket m_socServer = null;  // 클라이언트 접속 관리용 소켓
        private Socket[] m_socClient = new Socket[CLIENT_NUM];   // 클라이언트 접속 소켓
        public Boolean m_bEnd = false;

        private Thread m_threadServerService;
        private Boolean m_bThreadChk = false;   // 쓰레드 수행 체크 변수
        private Boolean bWaiting = false;
        private bool bConnected = false;

        private string IP;
        private int Port;

        public Boolean Waiting { get { return bWaiting; } set { bWaiting = value; } }
        public Boolean Connected { get { return bConnected; } set { bConnected = value; } }

        #region server
        public bool StartServer(string ip, int port)
        {
            if ((ip == null) || (port == 0))
            {
                LogMsg.CMsg.Show("TCP_S", "start fail ip=null or", "port=0", false, true);
                return false;
            }
            LogMsg.CMsg.Show("TCP_S", "start server " + ip + ":" + port.ToString(), "", false, true);
            IP = ip;
            Port = port;
            try
            {
                m_bEnd = true;
                m_bThreadChk = true;
                m_threadServerService = new Thread(StartServerService);
                m_threadServerService.Start();

                KeepAliveThread = new Thread(KeepAlive);
                keepAliveCheck = true;
                KeepAliveThread.Start();
            }
            catch (Exception ex)
            {
                LogMsg.CMsg.Show("TCP_S", "start fail ip=" + ip + ",port=" + port.ToString(), "", false, true);
                LogMsg.CMsg.Show("TCP_S", "start fail ", ex.ToString(), false, true);
                return false;
            }

            return true;
        }

        public bool StopServer()
        {
            for (int i = 0; i < m_socClient.Length; i++)
            {
                if (m_socClient[i] == null) continue;
                try
                {
                    m_socClient[i].Shutdown(SocketShutdown.Both);
                    m_socClient[i].Close();
                }
                catch { }
            }
            if (m_socServer != null)
            {
                m_socServer.Close();
                m_socServer = null;
            }
            m_bThreadChk = false;
            if (m_threadServerService != null) m_threadServerService.Abort();
            keepAliveCheck = false;
            if (KeepAliveThread != null) KeepAliveThread.Abort();
            LogMsg.CMsg.Show("TCP_S", "stop server ", "", false, true);
            return true;
        }

        private void StartServerService()
        {
            // create socket server
            try
            {
                m_socServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                m_socServer.Bind(new IPEndPoint(IPAddress.Parse(IP), Port));   // Server binding
                m_socServer.Listen(CLIENT_NUM);  // listening
                LogMsg.CMsg.Show("TCP_S", "start server service " + IP + ":" + Port.ToString(), "", false, true);
            }
            catch(Exception ex)
            {
                LogMsg.CMsg.Show("TCP_S", "start fail server service" + IP + ":" + Port.ToString(), ex.Message, false, true);
                return;
            }
            while (m_bThreadChk)
            {
                try
                {

                    for (int e = 0; e < CLIENT_NUM; e++)
                    {
                        if (m_socClient[e] != null)
                        {
                            if (m_socClient[e].Connected == true)
                            {
                                IPEndPoint remoteIpEndPoint = m_socClient[e].RemoteEndPoint as IPEndPoint;

                                if (remoteIpEndPoint != null)
                                {

                                    LogMsg.CMsg.Show("TCP_S", "connected", m_socClient[e].RemoteEndPoint.ToString(), false, true);
                                }
                            }

                        }

                    }



                    if (m_socServer == null) continue;

                    //LogMsg.CMsg.Show("TCP_S", "m_socServer user connect ", m_socServer.RemoteEndPoint.ToString(), false, true); 

                    Socket socTemp = (Socket)m_socServer.Accept();  // 클라이언트 접속 
                    //socTemp.Connect(socTemp.RemoteEndPoint);
                    LogMsg.CMsg.Show("TCP_S", "user connect ", socTemp.RemoteEndPoint.ToString(), false, true);
                    //m_socClient[0] = null;
                    // 최대 CLIENT_NUM개의 Client 접속을 허용하여 클라이언트 소켓객체를 저장한다.


                    byte[] arrBuf = new byte[BUF_SIZE];

                    CConnectSocket socConnect = new CConnectSocket();   // 클라이언트 소켓 관리 객체 생성

                    socConnect.m_socConnect = socTemp;  // 클라이언트 소켓 할당
                    socConnect.m_arrBuf = arrBuf;

                    // 데이타 수신 허용
                    socTemp.BeginReceive(arrBuf, 0, BUF_SIZE, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socConnect);

                    for (int i = 0; i < CLIENT_NUM; i++)
                    {
                        //if (m_socClient[i] != null)
                        {
                            

                            if (m_socClient[i] == null || m_socClient[i].Connected == false)
                            {
                                m_socClient[i] = socTemp;
                                LogMsg.CMsg.Show("TCP_S", "m_socClient", i.ToString(), false, true);
                                break;
                            }
                            else if (m_socClient[i].RemoteEndPoint.ToString() == socTemp.RemoteEndPoint.ToString())
                            {
                                LogMsg.CMsg.Show("TCP_S", "m_socClient - Conneced Status :", i.ToString() + "," + m_socClient[i].Connected.ToString(), false, true);
                            }

                        }

                    }

                    //m_bEnd = false;
                }
                catch (Exception exp)
                {
                    LogMsg.CMsg.Show("TCP_S", "stop server: ", exp.ToString(), false, true);
                }
            }
        }

        // 데이타 수신용 Callback 함수
        public void ReceiveCallBack(IAsyncResult _ar)
        {
            // 클라이언트와 연결된 소켓 객체 읽기 
            CConnectSocket socConnect = (CConnectSocket)_ar.AsyncState;

            try
            {
                int nRecv = socConnect.m_socConnect.EndReceive(_ar);         // 수신 데이타 사이즈를 읽고 소켓 수신 잠시 보류
                if (nRecv == 0) throw new Exception("client socket is closed");

                byte[] RcvData = socConnect.m_arrBuf;// 수신 데이타 버퍼에 읽어오기
                byte[] rdata = new byte[nRecv];
                Array.Copy(RcvData, rdata, nRecv);

                LogMsg.CMsg.Show("TCP_S", "received : " + socConnect.m_socConnect.RemoteEndPoint + " Length:" + rdata.Length.ToString(),"",false,true);
                //if (ReceiveEvent != null) ReceiveEvent(rdata);

                // 데이타 수신 허용
                //socConnect.m_socConnect.BeginReceive(socConnect.m_arrBuf, 0, buf., SocketFlags.None, WaitCallBack, MessageString);
                socConnect.m_socConnect.BeginReceive(socConnect.m_arrBuf, 0, BUF_SIZE, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socConnect);


                int index = -1;
                for (int i = 0; i < m_socClient.Length; i++)
                {
                    if (m_socClient[i].RemoteEndPoint == socConnect.m_socConnect.RemoteEndPoint)
                    {
                        index = i;
                        break;
                    }
                }
                string sReceive = Encoding.Default.GetString(rdata);

                if (DataReceiveEventByte != null) DataReceiveEventByte(m_socClient[index], rdata);
                if (DataReceiveEventStr != null) DataReceiveEventStr(m_socClient[index], sReceive);
                //Server_DataReceiveEventStr(index, sReceive);


                //ParsingFrame(socConnect.m_socConnect.RemoteEndPoint,rdata);    // STX, ETX Frame 처리
                // 데이타 수신 허용
                //socConnect.m_socConnect.BeginReceive(socConnect.m_arrBuf, 0, BUF_SIZE, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socConnect);
            }
            catch (Exception ex)
            {
                LogMsg.CMsg.Show("SOCKET", "receive callback: ", ex.ToString(), false, true);
                socConnect.m_socConnect.Close();    // 소켓 연결 종료
                if (socConnect != null)
                {
                    // m_hParent.addEvent(String.Format("User Disconnect : {0}", socConnect.m_socConnect.RemoteEndPoint));

                }
            }
        }


        // STX, ETX Frame 처리
        private void ParsingFrame(EndPoint ep, byte[] Data)
        {
            int index = -1;
            for (int i = 0; i < m_socClient.Length; i++)
            {
                if (m_socClient[i].RemoteEndPoint == ep)
                {
                    index = i;
                    break;
                }
            }

            string sReceive = Encoding.Default.GetString(Data);
            if (DataReceiveEventByte != null) DataReceiveEventByte(m_socClient[index], Data);
            if (DataReceiveEventStr != null) DataReceiveEventStr(m_socClient[index], sReceive);

            #region parsing 
            //int nSearchIndex = 0;

            //while (_strRcvData.Length > 0)
            //{
            //    // STX 처리
            //    if (!m_bSTXFind)
            //    {
            //        nSearchIndex = _strRcvData.IndexOf("STX", 0);
            //        if (nSearchIndex < 0)
            //        {
            //            m_strRcvData = "";
            //            _strRcvData = "";
            //        }
            //        else
            //        {
            //            _strRcvData = _strRcvData.Remove(0, nSearchIndex + 3);    // 수신 데이타에서 STX 이전 내용은 쓰레기 값으로 간주하여 지운다
            //            m_bSTXFind = true;
            //        }
            //    }
            //    // ETX 처리
            //    else
            //    {
            //        nSearchIndex = _strRcvData.IndexOf("ETX", 0);

            //        if (nSearchIndex < 0)
            //        {
            //            m_strRcvData += _strRcvData;    // STX 를 찾은후에 ETX 를 찾을때가지 수신데이타를 버퍼에 저장한다.
            //            _strRcvData = "";
            //        }
            //        else
            //        {
            //            if (nSearchIndex != 0) m_strRcvData += _strRcvData.Substring(0, nSearchIndex);
            //            _strRcvData = _strRcvData.Substring(nSearchIndex + 3, _strRcvData.Length - nSearchIndex - 3);

            //            m_strRcvData = "";
            //            m_bSTXFind = false;
            //        }
            //    }
            //}
            #endregion 
        }

        // server Frame parsing
        private int ParsingTorken(String _strRcvData, ref String[] _arrStrPara)
        {
            char cTemp;
            int nParaNum = 0;
            int nSearchIndex = 0;
            int nIndex;

            // client 로부터 전송된 Frame 을 구분자로 분류하여 parsing 한다.
            for (nIndex = 0; nIndex < _strRcvData.Length; nIndex++)
            {
                cTemp = _strRcvData[nIndex];

                if ((cTemp == '\0') || (cTemp == ' ') || (cTemp == '\t'))
                {
                    _arrStrPara[nParaNum++] = _strRcvData.Substring(nSearchIndex, nIndex - nSearchIndex);
                    nSearchIndex = nIndex + 1;
                }
            }

            if (nSearchIndex < nIndex)
                _arrStrPara[nParaNum] = _strRcvData.Substring(nSearchIndex, _strRcvData.Length - nSearchIndex);

            return 0;
        }

        // send data to client
        public void SendData(Socket client, byte[] _bSendData, int _nSize)
        {
            if (client == null)
            {
                // 접속된 Client 들에게 데이타를 전송한다.
                for (int i = 0; i < CLIENT_NUM; i++)
                {
                    try
                    {
                        if (m_socClient[i] == null) return;
                        if (m_socClient[i].Connected)
                        {
                            m_socClient[i].Send(_bSendData, _nSize, SocketFlags.None);

                        }
                    }
                    catch (SocketException exp)
                    {
                        LogMsg.CMsg.Show("TCP_S", "send data error ", exp.ToString(), false, true);

                        m_socClient[i].Close();
                    }
                }
            }
            else
            {
                //int i = clientIndex;
                {
                    try
                    {
                        if (client.Connected)
                        {
                            client.Send(_bSendData, _nSize, SocketFlags.None);
                            //LogMsg.CMsg.Show("TCP_S", "send data "+ clientIndex.ToString(), "length:"+_nSize.ToString(), false, true);
                        }
                    }
                    catch (SocketException exp)
                    {
                        LogMsg.CMsg.Show("TCP_S", "send data error ", exp.ToString(), false, true);
                        //m_socClient[i].Close();
                    }
                }
            }
        }

        public List<Socket> CheckConnectedList()
        {
            List<Socket> ret = new List<Socket>();
            for (int i = 0; i < CLIENT_NUM; i++)
            {
                try
                {
                    if (m_socClient[i] == null) continue;
                    if (m_socClient[i].Connected) ret.Add(m_socClient[i]);
                    }
                catch (SocketException exp)
                {
                }
                
            }
            return ret;
        }

        //private void BatchSend()
        //{
        //    LogMsg.CMsg.Show("TCP_S", "start batch data send ");

        //    while (m_bThreadChk)
        //    {
        //        //string sSend = "";
        //        ////sSend += DaqData.CHANNEL_COUNT.ToString() + ",";
        //        ////for (int i = 0; i < DaqData.CHANNEL_COUNT; i++)
        //        ////{
        //        ////    sSend += DaqData.ChannelList[i].SignalName  + ",";
        //        ////    sSend += DaqData.chValue[i] + ",";
        //        ////}
        //        //for (int i = 0; i < DaqData.CHANNEL_COUNT; i++)
        //        //{
        //        //    sSend += DaqData.ChannelList[i].SignalName + ",";
        //        //}
        //        //sSend = sSend.Remove(sSend.Length - 1);
        //        //sSend += "\t";
        //        //for (int i = 0; i < DaqData.CHANNEL_COUNT; i++)
        //        //{

        //        //    sSend += DaqData.chValue[i] + ",";
        //        //}
        //        //sSend = sSend.Remove(sSend.Length - 1);

        //        //byte[] arrSendBuf = new byte[sSend.Length + 2];
        //        //arrSendBuf[0] = STX;

        //        //arrSendBuf[sSend.Length + 1] = ETX;

        //        //Encoding.ASCII.GetBytes(sSend, 0, sSend.Length, arrSendBuf, 1);

        //        //SendData(arrSendBuf, arrSendBuf.Length);
        //        //Thread.Sleep(100);
        //    }
        //}

        public Boolean SendMsg(Socket client, string buffer)
        {
            //LogMsg.CMsg.Show("TCP_S", "send msg to : " + client.RemoteEndPoint.ToString(), buffer, false, true);

            byte[] buf = Encoding.Default.GetBytes(buffer);
            byte[] message = new byte[buf.Length + 2];

            message[0] = 0x02;
            Array.Copy(buf, 0, message, 1, buf.Length);
            message[buf.Length + 1] = 0x03;

            try
            {
                SendData(client, message, message.Length);
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        private void KeepAlive()
        {
            while (keepAliveCheck)
            {
                try
                {
                    if(m_socServer != null)
                        bWaiting = m_socServer.IsBound;
                    foreach(Socket s in m_socClient) {
                        if (s == null) continue;
                        else bConnected = s.Connected;
                    }
                }
                catch { }

                Thread.Sleep(1000);
            }
        }

        #endregion 

    }
    class CConnectSocket
    {
        public Socket m_socConnect;   // 클라이언트 접속 관리 임시 저장 소켓
        public byte[] m_arrBuf;
        public CConnectSocket()
        {
            m_socConnect = null;
            m_arrBuf = null;
        }
    }
}

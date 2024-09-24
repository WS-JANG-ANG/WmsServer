using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading; //mutex용

namespace WmsServer
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                // Exception Event
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);
                Application.ThreadExit += Application_ThreadExit;
                Application.ApplicationExit += Application_ApplicationExit;

                //프로그램 중복실행 방지
                bool firstInstance = false;
                string appName = Application.UserAppDataPath.Replace(@"\", "_");
                Mutex mutex = new Mutex(true, appName, out firstInstance);
                if (!firstInstance)
                {
                    
                    if (MessageBox.Show("program is running.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        return;
                    }
                }

                LogMsg.CMsg.Show("", "", "", false, true);
                int et = Environment.TickCount;
                int dy = (et / 1000 / 60 / 60 / 24);
                int hr = (et / 1000 / 60 / 60) - (dy * 24);
                int mn = (et / 1000 / 60) - (dy * 24 * 60) - (hr * 60);
                int ss = (et / 1000) - (dy * 24 * 60 * 60) - (hr * 60 * 60) - (mn * 60);
                string st = dy.ToString() + " days " + hr.ToString() + ":" + mn.ToString() + ":" + ss.ToString();
                //LogMsg.CMsg.Show("PROG", "system started before : ", st, false, true);
                TimeSpan elapsedTime = new TimeSpan(dy, hr, mn, ss);
                DateTime start = DateTime.Now - elapsedTime;

                LogMsg.CMsg.Show("PROG", "system started at : ", start.ToString("yyyy-MM-dd HH:mm:ss"), false, true);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new Frm_Login());
                Application.Run(new FormMDI());

            }
            catch (Exception exp)
            {
                LogMsg.CMsg.Show("PROG", "program exception " + exp.Message + "\r\n", exp.ToString(), false, true);
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (sender != null)
                LogMsg.CMsg.Show("PROG", "[UE| " + sender.GetType().Name + "__" + sender.ToString().Replace("\r\n", " ") + "]\r\n", e.ExceptionObject.ToString(), false, true);
        }

        private static void ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (sender != null)
                LogMsg.CMsg.Show("PROG", "[EX| " + sender.GetType().Name + "__" + sender.ToString().Replace("\r\n", " ") + "]\r\n", e.Exception.ToString(), false, true);
        }

        private static void Application_ThreadExit(object sender, EventArgs e)
        {
            if(sender != null)
            LogMsg.CMsg.Show("PROG", "[TE| " + sender.GetType().Name + "__" + sender.ToString().Replace("\r\n", " ") + "]\r\n", e.ToString(), false, true);
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (sender != null)
                LogMsg.CMsg.Show("PROG", "[AE| " + sender.GetType().Name + "__" + sender.ToString().Replace("\r\n", " ") + "]\r\n", e.ToString(), false, true);
        }
    }
}

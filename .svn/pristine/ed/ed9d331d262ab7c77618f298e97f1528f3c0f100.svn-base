using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

/******************************************************************************
 * 파일 버전을 관리 파일 생성시 이파일을 반드시 포함한며
 * 컴파일하여 배포하고자 할 경우는 반드시 버전을 수정하여 배포한다.
 *****************************************************************************/

namespace ANGFileInfo
{
    class ANGFileInfo
    {
        #region version Information
        static List<string> hi = new List<string>();
               
        public static string Type
        {
            get { return "DAQ_DEVICE"; }
        }

        public static List<string> HistoryInfo
        {
            get 
            {
                hi.Clear();
                hi.Add("1.00.2024.0418 \t[PD] new version");


                return hi; 
            }
        }

        public static string Version
        {
            get
            {
                Version ver = Assembly.GetExecutingAssembly().GetName().Version;
                return ver.ToString();
            }
        }
        #endregion
    }
}

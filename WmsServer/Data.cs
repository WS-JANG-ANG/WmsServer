using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WmsServer
{
    static class Data
    {
        public static string pList;

        public static ushort[] PLC_ReadData;
        public static ushort[] PLC_WriteData;

        //public static SystemConfig SysConfig = new SystemConfig();
        //static public string ConfigFileSystem = Application.StartupPath + "\\Ini\\ConfigSystem.ini";


        public static bool PlcWatchdog = false;


        public static bool DB_Connected = false;

        public static string iniPath = System.Windows.Forms.Application.StartupPath + @"\ini\Setting.ini";
        public static Dictionary<string, string> PlcIoDic = new Dictionary<string, string>();

        // for plc data
        public static string deviceName;
        public static IODATA[] IoDatas;
        public static int AiCount;
        public static int AoCount;
        public static int DiCount;
        public static int DoCount;

        public static string[] IoDescription;
        public static Dictionary<string, string> IoTypeDic = new Dictionary<string, string>();
        public static Dictionary<string, float> IoValueDic = new Dictionary<string, float>();


        public static Dictionary<string, string> LoadPlcIoDic()
        {
            string file = System.Windows.Forms.Application.StartupPath + @"\ini\PLC_IO.ini";
            string[] inio = { "InWatchdog", "InReset", "OutReset" };
            string[] outio = { "OutWatchdog", "InOk", "InNg", "InDu","OutOk", "OutNg", "OutNoship", "OutNodata", "OutNoseq", "ResetAccept" };
            Dictionary<string, string> dic = new Dictionary<string, string>();

            for (int i = 0; i < inio.Length; i++)
            {
                string plc= INI.ReadValue(file, "IN", inio[i], "");
                dic.Add(inio[i], plc);
            }
            for (int i = 0; i < outio.Length; i++)
            {
                string plc = INI.ReadValue(file, "OUT", outio[i], "");
                dic.Add(outio[i], plc);
            }
            return dic;
        }

    }

    public struct IODATA
    {
        public string Name;
        public string Type;
        public bool bValue;
        public int iValue;
        public float fValue;
    }

}


class INI
{
    [DllImport("Kernel32")]
    private static extern long WritePrivateProfileString(String section, String key, String val, String filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(String section, String key, String def, StringBuilder retVal, int size, String filePath);

    //파일 쓰기
    public static void WriteValue(string file, string strSection, string strKey, string strValue)
    {
        WritePrivateProfileString(strSection, strKey, strValue, file);
    }

    //파일 읽기
    public static string ReadValue(string file, string strSection, string Key, string defaultvalue)

    {
        StringBuilder strValue = new StringBuilder(8192);
        int i = GetPrivateProfileString(strSection, Key, defaultvalue, strValue, 8192, file);
        return strValue.ToString();
    }

}


using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.Xml.Linq;

namespace DB
{
    public class DataBase
    {
        #region create database lite
        public bool CreateDatabase(string server, string login, string password, string databaseName)
        { return CreateDatabase(server, login, password, databaseName, ""); }

        public bool CreateDatabase(string server, string login, string password, string databaseName, string path)
        {
            string strDBConnection = "SERVER = " + server + " ; DATABASE = master" + //databaseName +
                              "; UID = " + login + "; PWD = " + password + ";";

            string strSql = "";
            string strFilePath = "";

            try
            {
                using (SqlConnection dbcon = new SqlConnection(strDBConnection))
                {
                    dbcon.Open();                    

                    strSql = "sp_helpFile";
                    SqlCommand cmdSql = new SqlCommand(strSql, dbcon);
                    SqlDataReader readOdbcSql = cmdSql.ExecuteReader(CommandBehavior.CloseConnection);
                    if (readOdbcSql.Read()) strFilePath = readOdbcSql.GetValue(2).ToString();
                    readOdbcSql.Dispose();
                    cmdSql.Dispose();
                    dbcon.Close();

                    strFilePath = strFilePath.Substring(0, strFilePath.IndexOf("master"));
                    if (path != "") strFilePath = path + "\\";

                    dbcon.Open();
                    #region create database
                    strSql = "CREATE DATABASE [" + databaseName + "] ON  PRIMARY " +
                        "( NAME = N'" + databaseName + "', FILENAME = N'" + strFilePath + databaseName + ".mdf' , " +
                        " SIZE = 5MB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB ) " +
                        " LOG ON " +
                        " ( NAME = N'" + databaseName + "_log', FILENAME = N'" + strFilePath + databaseName + "_log.ldf' ," +
                        " SIZE = 5MB , MAXSIZE = 2048GB , FILEGROWTH = 10%)";

                    cmdSql = new SqlCommand(strSql, dbcon);
                    cmdSql.ExecuteNonQuery();
                    dbcon.ChangeDatabase(databaseName);

                    #region engine list
                    strSql = "CREATE TABLE [dbo].[EngineList]( " +
                        "[Key][varchar](16) NOT NULL, " +
                        "[ID] [varchar] (30) NOT NULL, " +
                        "[Model] [varchar] (20) NOT NULL, " +
                        "[InDateTime] [datetime] NOT NULL, " +
                        "[OutDateTime] [datetime] NULL, " +
                        "[Location] [varchar] (50) NULL, " +
                        "[InHandler] [varchar] (50) NULL, " +
                        "[OutHandler] [varchar] (50) NULL, " +
                        "[NoShip] [int] NULL, " +
                        "[FromPlant] [varchar] (50) NOT NULL, " +
                        "[ToPlant] [varchar] (50) NULL, " +
                        "[ShipKey] [varchar](12) NULL, "+
                        "[Flag] [int] NULL, " +
                        "[Status] [int] NULL, " +
                        "CONSTRAINT[PK_EngineList] PRIMARY KEY CLUSTERED ([Key] ASC) " +
                        "WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                        " ) ON [PRIMARY]  ";
                    cmdSql = new SqlCommand(strSql, dbcon);
                    cmdSql.ExecuteNonQuery();
                    cmdSql.Dispose();
                    #endregion

                    #region shiplist
                    strSql = "CREATE TABLE [dbo].[ShipList]( " +
                        "[Key] [varchar](12) NOT NULL, " +
                        "[Date] [date]  NOT NULL, " +
                        "[Sequence] [int] NOT NULL, " +
                        "[Model] [varchar](20) NOT NULL, " +
                        "[Quantity] [int] NOT NULL, " +
                        "[CurrentCount] [int] NULL," +
                        "[ToPlant] [varchar](50) NOT NULL, " +
                        "[Flag] [int] NULL, " +
                        "[Status] [int] NULL, " +
                        "CONSTRAINT[PK_ShipList] PRIMARY KEY CLUSTERED ([Key] ASC) " +
                        "WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                        " ) ON [PRIMARY]  ";
                    cmdSql = new SqlCommand(strSql, dbcon);
                    cmdSql.ExecuteNonQuery();
                    cmdSql.Dispose();
                    #endregion

                    #region ref data
                    strSql = "CREATE TABLE [dbo].[RefData]( " +
                        "[Code] [varchar](10) NOT NULL, " +
                        "[Name] [varchar](50) NOT NULL, " +
                        "CONSTRAINT[PK_RefData] PRIMARY KEY CLUSTERED ([Code] ASC) " +
                        "WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                        " ) ON [PRIMARY]  ";
                    cmdSql = new SqlCommand(strSql, dbcon);
                    cmdSql.ExecuteNonQuery();
                    cmdSql.Dispose();
                    #endregion
                    #endregion
                    dbcon.Close();
                }
            }
            catch (Exception e)
            {
                LogMsg.CMsg.Show("DBI", "create db error", e.Message, false, true);
                return false;
            }
            finally
            {
            }

            return true;
        }
        #endregion

        #region load database list
        public List<string> LoadDataBaseList(string server, string login, string password)
        {
            string strDBConnection = "SERVER = " + server + " ; DATABASE = master" + //dbInfo.DatabaseName +
                              "; UID = " + login + "; PWD = " + password + ";";
            List<string> dbList = new List<string>();

            try
            {
                using (SqlConnection dbcon = new SqlConnection(strDBConnection))
                {
                    dbcon.Open();
                    string strSqlTx = "";

                    strSqlTx = "select * from sysdatabases";
                    SqlCommand cmdTbSql = new SqlCommand(strSqlTx, dbcon);
                    SqlDataReader readOdbcSql = cmdTbSql.ExecuteReader(CommandBehavior.CloseConnection);


                    while (readOdbcSql.Read())
                    {
                        string dc = readOdbcSql.GetValue(0).ToString();
                        if ((dc != "master") && (dc != "model") && (dc != "tempdb") && (dc != "msdb"))
                            dbList.Add(dc);
                    }
                    readOdbcSql.Dispose();
                    cmdTbSql.Dispose();
                    dbcon.Close();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Failed to load database list \n\r" + exp.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dbList;
            }
            finally
            {
            }
            return dbList;
        }
        #endregion

        #region external call function from UTS
        public bool ConfigDB()
        {
            frmDBSet cfg = new frmDBSet();
            cfg.ShowDialog();
            return true;
        }

        public DBConnectInfo LoadDBInformation()
        {
            DBConnectInfo dbInfo;
            frmDBSet cfg = new frmDBSet();

            cfg = cfg.LoadConfig();

            dbInfo.DatabaseName = cfg.Database;
            dbInfo.ServerIP = cfg.Server;
            dbInfo.UserID = cfg.Login;
            dbInfo.UserPassword = cfg.Password;
            return dbInfo;
        }
        #endregion
    }

    public struct DBConnectInfo
    {
        public string ServerIP;
        public string DatabaseName;
        public string UserID;
        public string UserPassword;
    }

    public class DBInterface
    {
        public DBInterface()
        {
            cfg = cfg.LoadConfig();
        }

        public NetworkPing TestPing = new NetworkPing();
        public DBConnectInfo dbInfo = new DBConnectInfo();
        private frmDBSet cfg = new frmDBSet();

        //입고
        public int Incomming(string id, string model, string location, string inhandler, string noship,string fromplant)
        {
            LogMsg.CMsg.Show("DBI", "incomming ", id +"_"+ model+"_"+fromplant, false, true);

            string SQLConnectionInfo = "SERVER = " + cfg.Server + " ; DATABASE = " + cfg.Database +
                "; UID = " + cfg.Login + "; PWD = " + cfg.Password + ";";

            string strSql = "";
            using (SqlConnection dbcn = new SqlConnection(SQLConnectionInfo))
            {
                try
                {
                    dbcn.Open();

                    #region 중복검사
                    strSql = "Select Count(*) From EngineList Where ID = '" + id + "'";
                    SqlCommand cmdsql= new SqlCommand(strSql, dbcn);
                    int nCount = Convert.ToInt32(cmdsql.ExecuteScalar());
                    cmdsql.Dispose();
                    if (nCount > 0)
                    {
                        LogMsg.CMsg.Show("DBI", "duplicated id ", id, false, true);
                        dbcn.Close();
                        return 4;
                    }
                    #endregion

                    #region 키 생성
                    string key = "";
                    strSql = "Select Top 1 [Key] From EngineList Order by [Key] Desc";
                    cmdsql = new SqlCommand(strSql, dbcn);
                    object ret = cmdsql.ExecuteScalar();
                    string lkey = "";
                    if(ret !=null) lkey = ret.ToString();
                    cmdsql.Dispose();

                    string today = DateTime.Now.ToString("yyyyMMdd");
                    if (lkey.Length == 16)
                    {
                        if (lkey.StartsWith(today))
                        {
                            int n = Convert.ToInt16(lkey.Substring(8));
                            n = n + 1;
                            key = today + string.Format("{0:D8}", n);
                        }
                        else key = today + "00000001";
                    }
                    else {
                        key = today + "00000001";
                    }
                    #endregion

                    #region 저장
                    if (noship == "2") noship = "0"; 

                    strSql = "INSERT INTO EngineList ([Key], ID, Model, Location, " +
                        "InHandler, InDateTime, NoShip, FromPlant, Flag, Status ) VALUES(" +
                        "'" + key + "'," +
                        "'" + id + "'," +
                        "'" + model + "'," +
                        "'" + location + "'," +
                        "'" + inhandler + "'," +
                        "GETDATE()," +
                        "'" + noship + "'," +
                        "'" + fromplant + "'," +
                        "0,0)";

                    SqlCommand cmdListSql = new SqlCommand(strSql, dbcn);
                    cmdListSql.ExecuteNonQuery();
                    cmdListSql.Dispose();
                    #endregion

                    LogMsg.CMsg.Show("DBI", "finish incomming ", "", false, true);
                    dbcn.Close();
                    return 1;
                }
                catch (Exception e)
                {
                    LogMsg.CMsg.Show("DBI", "incomming error", e.ToString(), false, true);
                    LogMsg.CMsg.Show("DBI", "incomming query", strSql, false, true);
                }

                if (dbcn.State == ConnectionState.Open) dbcn.Close();
            }
            return 2;
        }

        //출고
        public int Outgoing(string id, string model, string outhandler, string toplant, int status)
        {

            LogMsg.CMsg.Show("DBI", "outgoing ", id + "_"+toplant, false, true);
            string SQLConnectionInfo = "SERVER = " + cfg.Server + " ; DATABASE = " + cfg.Database +
               "; UID = " + cfg.Login + "; PWD = " + cfg.Password + ";";

            string strSql = "";
            using (SqlConnection dbcn = new SqlConnection(SQLConnectionInfo))
            {
                try
                {
                    dbcn.Open();

                    #region 있는지 확인
                    strSql = "Select Count(*) From EngineList Where ID = '" + id + "' and [Status] = 0";
                    SqlCommand cmdSql = new SqlCommand(strSql, dbcn);
                    int nCount = Convert.ToInt32(cmdSql.ExecuteScalar());
                    cmdSql.Dispose();
                    if (nCount == 0)
                    {
                        dbcn.Close();
                        return 4;// no data
                    }
                    #endregion

                    #region 출고금지 확인 
                    strSql = "Select NoShip From EngineList Where ID = '" + id + "'";
                    cmdSql = new SqlCommand(strSql, dbcn);

                    int noship = 0;
                    try { noship = Convert.ToInt32(cmdSql.ExecuteScalar()); }
                    catch { }
                    cmdSql.Dispose();
                    if (noship == 1)
                    {
                        dbcn.Close();
                        return 3;// no ship
                    }
                    #endregion

                    #region 저장 
                    // 현재 진행중인 ship order 확인 
                    strSql = "Select top 1 [Key] From ShipList Where ToPlant = '" + toplant + "'"+
                        " and Model =  '" + model + "' and [Quantity] <> [CurrentCount] OR [CurrentCount] IS NULL"+
                        " order by [Key] asc";
                    cmdSql = new SqlCommand(strSql, dbcn);
                    if(cmdSql.ExecuteScalar() == null)
                    {
                        dbcn.Close();
                        return 5;// no sequence
                    }
                    string key = cmdSql.ExecuteScalar().ToString();
                    cmdSql.Dispose();


                    strSql = "UPDATE EngineList SET OutDateTime = GETDATE(), " +
                            "OutHandler = '" + outhandler + "', Location = 'OUT', " +
                            "ToPlant = '" + toplant + "', Status = " + status + ", ShipKey = '" + key + "'" +
                            "WHERE ID = '" + id + "'";
                    SqlCommand cmdSql1 = new SqlCommand(strSql, dbcn);
                    cmdSql1.ExecuteNonQuery();
                    cmdSql1.Dispose();
                    LogMsg.CMsg.Show("DBI", "save outgoing ", "", false, true);

                    // update shiplist
                    strSql = "UPDATE ShipList SET CurrentCount = COALESCE(CurrentCount, 0) + 1 " +
                            "where [Key] =  '" + key + "'";
                    cmdSql = new SqlCommand(strSql, dbcn);
                    cmdSql.ExecuteNonQuery();
                    cmdSql.Dispose();
                    LogMsg.CMsg.Show("DBI", "update ship list to key : ", key, false, true);

                    return 1;
                    #endregion
                }
                catch (Exception e)
                {
                    LogMsg.CMsg.Show("DBI", "outgoing error", e.ToString(), false, true);
                    LogMsg.CMsg.Show("DBI", "outgoing query", strSql, false, true);
                }

                if (dbcn.State == ConnectionState.Open) dbcn.Close();
            }
            return 2;
        }

        public int Update(string update, string condition)
        {
            LogMsg.CMsg.Show("DBI", "update ", "condition:'" + condition + "'", false, true);

            string SQLConnectionInfo = "SERVER = " + cfg.Server + " ; DATABASE = " + cfg.Database +
               "; UID = " + cfg.Login + "; PWD = " + cfg.Password + ";";

            using (SqlConnection dbcn = new SqlConnection(SQLConnectionInfo))
            {
                try
                {
                    dbcn.Open();
                    #region 
                    string strSqlTxEn = "Select Count(*) From EngineList Where " + condition;
                    SqlCommand cmdSelSqlEn = new SqlCommand(strSqlTxEn, dbcn);
                    int nCount = Convert.ToInt32(cmdSelSqlEn.ExecuteScalar());
                    cmdSelSqlEn.Dispose();

                    if (nCount > 0)
                    {
                        string strSqlTx1 = "UPDATE EngineList SET " + update + " WHERE " + condition;

                        SqlCommand cmdListSql11 = new SqlCommand(strSqlTx1, dbcn);
                        cmdListSql11.ExecuteNonQuery();
                        cmdListSql11.Dispose();
                        LogMsg.CMsg.Show("DBInterface", "Update ", "", false, true);
                        dbcn.Close();
                        return 1;
                    }
                    #endregion

                }
                catch (Exception e)
                {
                    LogMsg.CMsg.Show("DBInterface", "Update error", e.ToString(), true, true);
                }

                if (dbcn.State == ConnectionState.Open) dbcn.Close();
            }
            return 2;
        }


        public List<string> GetList(string col, string condition)
        {
            //LogMsg.CMsg.Show("DBI", "get list ", col+"__"+ condition, false, true);
            //LogMsg.CMsg.Show("DBI", "get list ", "", false, true);
            List<string> engineList = new List<string>();

            string SQLConnectionInfo = $"SERVER={cfg.Server}; DATABASE={cfg.Database}; UID={cfg.Login}; PWD={cfg.Password};";

            using (SqlConnection dbcn = new SqlConnection(SQLConnectionInfo))
            {
                try
                {
                    dbcn.Open();
                    string columns = col.Replace(";", ",");
                    string strSqlTxEn = $"SELECT {columns} FROM EngineList WHERE {condition}";

                    using (SqlCommand cmdSelSqlEn = new SqlCommand(strSqlTxEn, dbcn))
                    using (SqlDataReader readOdbcSql = cmdSelSqlEn.ExecuteReader())
                    {
                        int numCol = col.Split(';').Length;

                        while (readOdbcSql.Read())
                        {
                            List<string> rowValues = new List<string>();

                            for (int i = 0; i < numCol; i++)
                            {
                                rowValues.Add(readOdbcSql.GetValue(i).ToString());
                            }

                            engineList.Add(string.Join(",", rowValues));
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogMsg.CMsg.Show("DBI", "get list error: " + ex.Message, "", false, true);
                    return null;
                }
            }

            return engineList;


        }

        public bool DeleteDB(string barcode)
        {

            LogMsg.CMsg.Show("DBInterface", "Delete Barcode ", "'" + barcode + "'", false, true);




            string SQLConnectionInfo = "SERVER = " + cfg.Server + " ; DATABASE = " + cfg.Database +
                "; UID = " + cfg.Login + "; PWD = " + cfg.Password + ";";




            using (SqlConnection dbcn = new SqlConnection(SQLConnectionInfo))
            {
                try
                {
                    dbcn.Open();


                    #region testinfo
                    string strSqlTxEn = "Select Count(*) From EngineList Where Barcode = '" + barcode;
                    SqlCommand cmdSelSqlEn = new SqlCommand(strSqlTxEn, dbcn);
                    int nCount = Convert.ToInt32(cmdSelSqlEn.ExecuteScalar());
                    cmdSelSqlEn.Dispose();

                    if (nCount > 0)
                    {
                        string strSqlTx1 = "DELETE FROM EngineList WHERE Barcode = '" + barcode;
                        SqlCommand cmdListSql11 = new SqlCommand(strSqlTx1, dbcn);
                        cmdListSql11.ExecuteNonQuery();
                        cmdListSql11.Dispose();
                    }




                    #endregion


                    LogMsg.CMsg.Show("DBInterface", "Finish Delete Barcode ", "", false, true);
                    dbcn.Close();
                    return true;
                }
                catch (Exception e)
                {
                    LogMsg.CMsg.Show("DBInterface", "Delete barcode error", e.Message, true, true);
                }

                if (dbcn.State == ConnectionState.Open) dbcn.Close();
            }

            return false;

        }

        #region ShipList DB

        public List<string> GetShipList(string col, string condition)
        {
            //LogMsg.CMsg.Show("DBI", "get ship list", "", false, true);

            List<string> barcodeList = new List<string>();

            string SQLConnectionInfo = $"SERVER={cfg.Server}; DATABASE={cfg.Database}; UID={cfg.Login}; PWD={cfg.Password};";

            using (SqlConnection dbcn = new SqlConnection(SQLConnectionInfo))
            {
                try
                {
                    dbcn.Open();
                    string columns = col.Replace(";", ",");
                    string strSqlTxEn = $"SELECT {columns} FROM ShipList WHERE {condition}";

                    using (SqlCommand cmdSelSqlEn = new SqlCommand(strSqlTxEn, dbcn))
                    using (SqlDataReader readOdbcSql = cmdSelSqlEn.ExecuteReader())
                    {
                        int numCol = col.Split(';').Length;

                        while (readOdbcSql.Read())
                        {
                            List<string> rowValues = new List<string>();

                            for (int i = 0; i < numCol; i++)
                            {
                                rowValues.Add(readOdbcSql.GetValue(i).ToString());
                            }

                            barcodeList.Add(string.Join(",", rowValues));
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogMsg.CMsg.Show("DBI", "get ship list error: " + ex.Message, "", false ,true);
                    return null;
                }
            }

            return barcodeList;

        }

        //Date, Sequence, Model, Quantity, Currently
        public bool InsertShipList(string Date, int Sequence, string Model, int Quantity)
        {
            LogMsg.CMsg.Show("DBInterface", "Insert MES ", $"Date: {Date}, Sequence: {Sequence}, Model: {Model}, Quatity: {Quantity}", false, true);




            string SQLConnectionInfo = "SERVER = " + cfg.Server + " ; DATABASE = " + cfg.Database +
                "; UID = " + cfg.Login + "; PWD = " + cfg.Password + ";";




            using (SqlConnection dbcn = new SqlConnection(SQLConnectionInfo))
            {
                try
                {
                    dbcn.Open();


                    #region testinfo
                    //string strSqlTxEn = "Select Count(*) From EngineList Where Barcode = '" + barcode + "'";
                    //SqlCommand cmdSelSqlEn = new SqlCommand(strSqlTxEn, dbcn);
                    //int nCount = Convert.ToInt32(cmdSelSqlEn.ExecuteScalar());
                    //cmdSelSqlEn.Dispose();


                    string strSqlTx1 = "INSERT INTO MES (Date, " +
                                       " Sequence, Model, Quantity ) VALUES(" +
                            "'" + Date + "'," +
                              "'" + Sequence + "'," +
                              "'" + Model + "'," +
                              "'" + Quantity + "')";


                    SqlCommand cmdListSql11 = new SqlCommand(strSqlTx1, dbcn);
                    cmdListSql11.ExecuteNonQuery();
                    cmdListSql11.Dispose();


                    #endregion


                    LogMsg.CMsg.Show("DBInterface", "MES Inserted ", "", false, true);
                    dbcn.Close();
                    return true;
                }
                catch (Exception e)
                {
                    LogMsg.CMsg.Show("DBInterface", "MES Insert error", e.Message, true, true);
                }

                if (dbcn.State == ConnectionState.Open) dbcn.Close();
            }

            return false;

        }


        public bool ShipListUpdate(string update, string condition)
        {
            LogMsg.CMsg.Show("DBI", "ship update ", "condition:'" + condition + "'", false, true);


            string SQLConnectionInfo = "SERVER = " + cfg.Server + " ; DATABASE = " + cfg.Database +
               "; UID = " + cfg.Login + "; PWD = " + cfg.Password + ";";




            using (SqlConnection dbcn = new SqlConnection(SQLConnectionInfo))
            {
                try
                {
                    dbcn.Open();


                    #region testinfo
                    string strSqlTxEn = "Select Count(*) From ShipList Where " + condition;
                    SqlCommand cmdSelSqlEn = new SqlCommand(strSqlTxEn, dbcn);
                    int nCount = Convert.ToInt32(cmdSelSqlEn.ExecuteScalar());
                    cmdSelSqlEn.Dispose();

                    if (nCount > 0)
                    {

                        string strSqlTx1 = "UPDATE top(1) Mes SET " + update + " WHERE " + condition;

                        SqlCommand cmdListSql11 = new SqlCommand(strSqlTx1, dbcn);
                        cmdListSql11.ExecuteNonQuery();
                        cmdListSql11.Dispose();
                        LogMsg.CMsg.Show("DBInterface", "MES Update ", strSqlTx1, false, true);
                        dbcn.Close();
                        return true;

                    }
                    #endregion



                }
                catch (Exception e)
                {
                    LogMsg.CMsg.Show("DBInterface", "Update error", e.Message, true, true);
                }

                if (dbcn.State == ConnectionState.Open) dbcn.Close();
            }
            return false;
        }



        public bool DeleteMes(string condition)
        {

            LogMsg.CMsg.Show("DBInterface", "Delete MES ", condition, false, true);




            string SQLConnectionInfo = "SERVER = " + cfg.Server + " ; DATABASE = " + cfg.Database +
                "; UID = " + cfg.Login + "; PWD = " + cfg.Password + ";";




            using (SqlConnection dbcn = new SqlConnection(SQLConnectionInfo))
            {
                try
                {
                    dbcn.Open();


                    #region testinfo
                    string strSqlTxEn = $"Select Count(*) From MES Where " + condition;
                    SqlCommand cmdSelSqlEn = new SqlCommand(strSqlTxEn, dbcn);
                    int nCount = Convert.ToInt32(cmdSelSqlEn.ExecuteScalar());
                    cmdSelSqlEn.Dispose();

                    if (nCount > 0)
                    {
                        string strSqlTx1 = "DELETE FROM MES WHERE " + condition;
                        SqlCommand cmdListSql11 = new SqlCommand(strSqlTx1, dbcn);
                        cmdListSql11.ExecuteNonQuery();
                        cmdListSql11.Dispose();
                    }




                    #endregion


                    LogMsg.CMsg.Show("DBInterface", "Finish Delete Mes ", "", false, true);
                    dbcn.Close();
                    return true;
                }
                catch (Exception e)
                {
                    LogMsg.CMsg.Show("DBInterface", "Delete Mes error", e.ToString(), true, true);
                }

                if (dbcn.State == ConnectionState.Open) dbcn.Close();
            }

            return false;

        }



        #endregion


        public string GetRefList(string startwith)
        {
            LogMsg.CMsg.Show("DBI", "get ref list start with : ", startwith, false, true);
            string list = string.Empty;

            string SQLConnectionInfo = $"SERVER={cfg.Server}; DATABASE={cfg.Database}; UID={cfg.Login}; PWD={cfg.Password};";

            using (SqlConnection dbcn = new SqlConnection(SQLConnectionInfo))
            {
                try
                {
                    dbcn.Open();
                    string strSql = $"SELECT Name FROM RefData WHERE Code like '{startwith}%'";

                    using (SqlCommand cmdSql = new SqlCommand(strSql, dbcn))
                    using (SqlDataReader readSql = cmdSql.ExecuteReader())
                    {
                        int idx = 0;
                        while (readSql.Read())
                        {
                            list += (("|" + readSql.GetValue(idx).ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogMsg.CMsg.Show("DBI", "get redf list error: " + ex.Message, "", false, true);
                    return null;
                }
            }

            return list;


        }








        public bool SaveBackupDB(string  result)
        {
            LogMsg.CMsg.Show("DBInterface", "Save backup", result, false, true);

            string SQLConnectionInfo = "SERVER = " + cfg.ServerBackup + /*" ; DATABASE = " + "    " +*/
                            "; UID = " + cfg.LoginBackup + "; PWD = " + cfg.PasswordBackup + ";";

            string[] s = result.Split(',');
            using (SqlConnection dbcn = new SqlConnection(SQLConnectionInfo))
            {
                dbcn.Open();
                #region check DB
                String tempDB = "";
                if (DateTime.TryParse(s[3], out DateTime dateTime))
                {
                    tempDB = cfg.BackupDBName + "_"
                   + BackupDBName(dateTime, cfg.BackupInterval);
                }
                else
                {
                    return false;
                }
               
                try
                {
                    dbcn.ChangeDatabase(tempDB);
                }
                catch
                {
                    DataBase db = new DataBase();
                    if (tempDB != "")
                    {
                        //if (db.CreateResultDatabase(cfg.ServerBackup, cfg.LoginBackup, cfg.PasswordBackup, tempDB, cfg.BackupFolder))
                        //{
                        //    //MessageBox.Show("The backup database has been generated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    LogMsg.CMsg.Show("DBI_L", "backup database has been generated", "", false, true);
                        //}
                        //else
                        //{
                        //    MessageBox.Show("The backup database was not created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    LogMsg.CMsg.Show("DBI_L", "backup database was not created", "", false, true);
                        //}
                    }
                    dbcn.ChangeDatabase(tempDB);
                }
                #endregion

                #region testinfo
                //string strSqlTxEn = "Select Count(*) From TEST_LIST Where TEST_ID = '" + test.TestInfo.TEST_ID + "'";
                //SqlCommand cmdSelSqlEn = new SqlCommand(strSqlTxEn, dbcn);
                //int nTestCount = Convert.ToInt32(cmdSelSqlEn.ExecuteScalar());
                //cmdSelSqlEn.Dispose();

                string inDateTime = ConvertToStandardDateTime(s[2]);
                string outDateTime = ConvertToStandardDateTime(s[3]);

                 
                string strSqlTx1 = "INSERT INTO ENGINELIST(BARCODE, ENGINETYPE, INDATETIME, OUTDATETIME, LOCATION, INHANDLER, OUTHANDLER) " +
                    $"VALUES('{s[0]}', '{s[1]}', '{inDateTime}', '{outDateTime}','{s[4]}','{s[5]}','{s[6]}')";

                SqlCommand cmdListSql11 = new SqlCommand(strSqlTx1, dbcn);
                cmdListSql11.ExecuteNonQuery();
                cmdListSql11.Dispose();

               

                    Thread.Sleep(10);
                
                #endregion

                

            }

            

            return true;



        }

        private string BackupDBName(DateTime date, int interval)
        {
            string name = "";

            try
            {
                string dt = date.ToString("yyyyMM");
                string year = dt.Substring(0, 4);
                string month = dt.Substring(4, 2);

                int num = Convert.ToInt32(month);
                if (num > 1)
                {
                    num = (num - 1) / interval;
                    name = string.Format("{0}_{1:00}_{2:00}", year, num * interval + 1, (num + 1) * interval);
                }
                else
                {
                    name = string.Format("{0}_{1:00}_{2:00}", year, 1, interval);
                }
            }
            catch { name = ""; }

            return name;
        }


        private string ConvertToStandardDateTime(string koreanDateTime)
        {
            string[] formats = { "yyyy-MM-dd 오전 h:mm:ss", "yyyy-MM-dd 오후 h:mm:ss" };
            DateTime dateTime;

            if (DateTime.TryParseExact(koreanDateTime, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateTime))
            {
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                throw new FormatException("Invalid date format");
            }
        }
    }




    class INI
    {

        #region //////////////// iNI 파일
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int WritePrivateProfileString(string asAppName, string asKeyName, string asValue, string asFileName);
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileString(string asAppName, string asKeyName, string asDefault, StringBuilder asValue, int aiSize, string asFileName);
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool QueryPerformanceFrequency(out long lpFrequency);
        //////////////// iNI 파일

        public static string ReadINI(string asFileName, string asAppName, string asKeyName)
        {
            StringBuilder retstr;
            retstr = new StringBuilder();
            retstr.Capacity = 32767;
            GetPrivateProfileString(asAppName, asKeyName, "", retstr, 32767, asFileName);
            return retstr.ToString();
        }

        public static void WriteINI(string asFileName, string asAppName, string asKeyName, string asValue)
        {
            INI.WritePrivateProfileString(asAppName, asKeyName, asValue, asFileName);

        }
        #endregion
    }

    public class NetworkPing
    {
        #region network test
        public bool NetworkToTest(string pIP)
        {
            if (pIP == null || pIP == "") return false;
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(pIP);

            if (reply.Status == IPStatus.Success)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Microsoft.Win32;

namespace ePlusReplication
{
    public struct xmlSettings
    {
        public string serverName;
        public string dbName;
        public string dbPort;
        public string userName;
        public string password;
        public bool success;
        public string errorMessage;
        public string timer;
    }

    public struct eMailSettings
    {
        public string fromID;
        public string fromPass;
        public string toID;
    }

    public class clsReplicate
    {
        public Boolean mainDBConnection;
        protected DatabaseInfo MainDBInfo, LogDBInfo;
        protected DatabaseInfo[] slaveDatabaseInfo;
        protected MySqlConnection MainDBConn, LogDBConn, slaveDatabase;
        protected System.Collections.Queue replicationQueue;
        protected string errorMessage;
        protected string dbCode;
        protected xmlSettings settings;
        protected eMailSettings testMail;
        public clsReplicate(xmlSettings xmlsettings)
        {
            settings = xmlsettings;
            mainDBConnection = initReplicate();
        }
        public Boolean initReplicate()
        {
            testMail.fromID = "info@falcons.in";
            testMail.fromPass = "info@abcd123";
            testMail.toID = "thahir@falcons.in";
            clsStatus.UdpateStatusText("Loading Application...\r\n");
            System.Threading.Thread.Sleep(500);
            replicationQueue = new System.Collections.Queue();

            if (string.IsNullOrEmpty( settings.serverName))
            {
                clsStatus.UdpateStatusText("Invalid Setting\r\n");
                return false;
            }
            MainDBInfo.ServerName = settings.serverName;
            MainDBInfo.DBName = settings.dbName;
            MainDBInfo.DBPort = settings.dbPort;
            MainDBInfo.DBUser = settings.userName;
            MainDBInfo.DBPWD = settings.password;

            MainDBInfo.ConnectionString = clsDBUtility.CreateConnectionString(MainDBInfo);
            try
            {
                clsStatus.ClearStatusText();
                clsStatus.UdpateStatusText("Connecting to Master Database...\r\n");
                System.Threading.Thread.Sleep(500);
                MainDBConn = new MySqlConnection(MainDBInfo.ConnectionString);
                MainDBConn.Open();
                dbCode = clsDBUtility.GetConnectedDatabaseCode(MainDBInfo.ServerName, MainDBInfo.DBName, MainDBInfo.DBPort, MainDBConn, ref errorMessage);
                if (dbCode == null)
                {
                    clsStatus.UdpateStatusText("Unable to locate current database from the list\r\n");
                    System.Threading.Thread.Sleep(500);
                    return false;
                }
                clsStatus.UdpateStatusText("Successfully Connected to Master Database\r\n");
                System.Threading.Thread.Sleep(500);
                clsStatus.UdpateStatusText("Fetching data from Master Database\r\n");
                System.Threading.Thread.Sleep(500);
                LogDBInfo = clsDBUtility.GetLogDBDetails(MainDBConn, dbCode, ref errorMessage);
                LogDBInfo.DBUser = settings.userName;
                LogDBInfo.DBPWD = settings.password;
                clsDBUtility.GetReplicationLogDBInfo(MainDBConn, dbCode, ref errorMessage, ref slaveDatabaseInfo);
                clsStatus.UdpateStatusText("Closing Master Database Connection...\r\n");
                System.Threading.Thread.Sleep(500);
                MySqlConnection.ClearPool(MainDBConn);
                MainDBConn.Close();
                MainDBConn.Dispose();
                clsStatus.UdpateStatusText("Successfully Closed Master Database Connection.\r\n\r\n\r\n");
                System.Threading.Thread.Sleep(500);
                LogDBInfo.ConnectionString = clsDBUtility.CreateConnectionString(LogDBInfo);
            }
            catch (MySqlException sqlEx)
            {
                clsStatus.UdpateStatusText(sqlEx.Message + "\n\r\n\r" );
                return false;
            }
            return true;
        }
        public Boolean ConnectLogDB()
        {
            try
            {
                clsStatus.UdpateStatusText("Connecting to Log Database...\r\n");
                System.Threading.Thread.Sleep(500);
                LogDBConn = new MySqlConnection(LogDBInfo.ConnectionString);
                LogDBConn.Open();
                clsStatus.UdpateStatusText("Successfully Connected to Log Database\r\n");
                clsDBUtility.UpdateActivityLog(LogDBConn, "Connect", "Connecting to Log Database", "Success", ref errorMessage);
                System.Threading.Thread.Sleep(500);
            }
            catch (MySqlException ex)
            {
                clsStatus.UdpateStatusText(ex.Message.ToString());
                clsDBUtility.UpdateActivityLog(LogDBConn, "Connect", "Connecting to Log Database", ex.Message.ToString(), ref errorMessage);
                return false;
            }
            return true;
        }
        public long getReplicateLastID(string dbCode)
        {
            try
            {
                long lastID = clsDBUtility.getDBLongValue(LogDBConn, "SELECT IFNULL(MAX(ID),0) FROM replicationlog WHERE DBCODE = '" + dbCode + "'", ref errorMessage);
                if (lastID == 0)
                {
                    lastID = Convert.ToInt64(clsDBUtility.getDBuLongValue(LogDBConn, "SELECT FIRSTID FROM replicationinfo WHERE CODE = '" + dbCode + "'", ref errorMessage));
                    clsStatus.UdpateStatusText("\r\nInitial log id is : " + lastID.ToString() + "\r\n");
                    clsDBUtility.UpdateActivityLog(LogDBConn, "SELECT", "SELECT", "Initial log id is : " + lastID.ToString(), ref errorMessage);
                    lastID--;
                }
                else
                {
                    clsStatus.UdpateStatusText("\r\nLast log id is : " + lastID.ToString() + "\r\n");
                    clsDBUtility.UpdateActivityLog(LogDBConn, "SELECT", "SELECT", "Last log id is : " + lastID.ToString(), ref errorMessage);
                }
                System.Threading.Thread.Sleep(500);
                return lastID;
            }
            catch (MySqlException ex)
            {
                clsStatus.UdpateStatusText(ex.Message.ToString());
                clsDBUtility.UpdateActivityLog(LogDBConn, "Connect", "Connecting to Log Database", ex.Message.ToString(), ref errorMessage);
                return -1;
            }
        }
        public Boolean Replicate()
        {
            long lastID;
            int i;
            if (!mainDBConnection)
                return false;
            try
            {
                ConnectLogDB();
                for (i = 0; i < slaveDatabaseInfo.LongLength; i++)
                {
                    slaveDatabaseInfo[i].DBUser = settings.userName;
                    slaveDatabaseInfo[i].DBPWD = settings.password;
                    slaveDatabaseInfo[i].ConnectionString = clsDBUtility.CreateConnectionString(slaveDatabaseInfo[i]);
                }

                for (i = 0; i < slaveDatabaseInfo.LongLength; i++)
                {
                    try
                    {
                        if(!clsDBUtility.IsRequiredTimeGap(LogDBConn,slaveDatabaseInfo[i].DBCode,ref errorMessage))
                            continue;
                        slaveDatabase = SlaveDB(slaveDatabaseInfo[i]);
                        if (slaveDatabase.State == System.Data.ConnectionState.Closed)
                            continue;
                        lastID = getReplicateLastID(slaveDatabaseInfo[i].DBCode);
                        clsDBUtility.GetReplicationLog(slaveDatabase, lastID, replicationQueue, ref errorMessage);
                        clsStatus.UdpateStatusText(replicationQueue.Count.ToString() + " Update(s) found on slave database\r\n");
                        clsDBUtility.UpdateActivityLog(LogDBConn, "SELECT", "Check for SQL Commands(" + slaveDatabaseInfo[i].DBName + "@" + slaveDatabaseInfo[i].ServerName + ":" + slaveDatabaseInfo[i].DBPort + ")...", replicationQueue.Count.ToString() + " Update(s) found on slave database", ref errorMessage);
                        clsDBUtility.UpdateConnectionInfo(LogDBConn, slaveDatabaseInfo[i].DBCode, ref errorMessage);
                        MySqlConnection.ClearPool(slaveDatabase);
                        clsStatus.UdpateStatusText("Closing Slave Database Connection...\r\n");
                        slaveDatabase.Close();
                        slaveDatabase.Dispose();
                        clsStatus.UdpateStatusText("Successfully Closed Salve Database Connection\r\n");
                        if (replicationQueue.Count > 0)
                        {
                            clsStatus.UdpateStatusText("Creating Replication Log...\r\n");
                            clsDBUtility.CreateReplicationLog(LogDBConn, replicationQueue, ref errorMessage);
                            clsStatus.UdpateStatusText("Successfully Created Replication Log.\r\n");
                        }
                    }
                    catch (MySqlException sqlEx)
                    {
                        slaveDatabaseInfo[i].Success = false;
                        clsStatus.UdpateStatusText(sqlEx.Message + "\n\r" + slaveDatabaseInfo[0].DBName + "@" + slaveDatabaseInfo[0].ServerName + ":" + slaveDatabaseInfo[0].DBPort + "\n\r\n\r");
                        clsDBUtility.UpdateActivityLog(LogDBConn, "Connect", "Connecting to Slave Database(" + slaveDatabaseInfo[0].DBName + "@" + slaveDatabaseInfo[0].ServerName + ":" + slaveDatabaseInfo[0].DBPort + ")...", sqlEx.Message, ref errorMessage);
                        return false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                clsStatus.UdpateStatusText(ex.Message.ToString());
                return false;
            }
            return true;
        }

        protected MySqlConnection SlaveDB(DatabaseInfo slaveDBInfo)
        {
            MySqlConnection dbConn;
            dbConn = new MySqlConnection(slaveDBInfo.ConnectionString);
            try
            {
                dbConn.Open();
                clsStatus.UdpateStatusText("Successfully Connected to Slave Database " + slaveDBInfo.DBName + "@" + slaveDBInfo.ServerName + ":" + slaveDBInfo.DBPort);
                clsDBUtility.UpdateActivityLog(LogDBConn, "Connect", "Connecting to Slave Database(" + slaveDBInfo.DBName + "@" + slaveDBInfo.ServerName + ":" + slaveDBInfo.DBPort + ")...", "Success", ref errorMessage);
                slaveDBInfo.Success = true;
            }
            catch (MySqlException sqlEx)
            {
                slaveDBInfo.Success = false;
                clsStatus.UdpateStatusText(sqlEx.Message + "\n\r" + slaveDBInfo.DBName + "@" + slaveDBInfo.ServerName + ":" + slaveDBInfo.DBPort + "\n\r\n\r");
                clsDBUtility.UpdateActivityLog(LogDBConn, "Connect", "Connecting to Slave Database(" + slaveDBInfo.DBName + "@" + slaveDBInfo.ServerName + ":" + slaveDBInfo.DBPort + ")...", sqlEx.Message, ref errorMessage);
            }
            return dbConn;
        }

        public void verifySQL()
        {
            clsStatus.UdpateStatusText("Checking for pending SQL Commands...\r\n");
            System.Threading.Thread.Sleep(500);
            Boolean result;
            QueryInfo query;
            long count;
            string dbCode="";
            ulong replicationID=0;
            count = clsDBUtility.GetReplicationCount(LogDBConn, ref errorMessage);
            clsDBUtility.UpdateActivityLog(LogDBConn, "Checking", "Checking for Pending SQL Commands(Verification)", count.ToString() + " SQL Command(s) found.", ref errorMessage);
            clsStatus.UdpateStatusText(count.ToString() + " SQL Command(s) found.\r\n");
            if (count > 0)
            {
                clsStatus.UdpateStatusText("Verifying SQL Commands...\r\n");
                do
                {
                    clsDBUtility.GetReplicationNextID(LogDBConn, ref  dbCode, ref replicationID, ref errorMessage);
                    query = clsDBUtility.GetReplicationSQL(LogDBConn,dbCode, replicationID, ref errorMessage);
                    result = clsDBUtility.VerifyReplicationSQL(LogDBConn, MainDBInfo.DBName, query, ref errorMessage);
                    clsDBUtility.UpdateReplicationSQL(LogDBConn, "COMMANDSTATUS",dbCode, replicationID, result, ref errorMessage);
                    if (!result)
                    {
                        clsMail.sendEmail(testMail.fromID, testMail.fromPass, testMail.toID, "SQL Verification failed!!!", eMailBody(query));
                    }
                    clsStatus.UdpateStatusText(".");
                    System.Threading.Thread.Sleep(1);
                    //clsDBUtility.GetReplicationNextID(LogDBConn, ref  dbCode, ref replicationID, ref errorMessage);
                }
                while (replicationID > 0);
                clsStatus.UdpateStatusText("Sql Command Verification Completed.\r\n");
                clsDBUtility.UpdateActivityLog(LogDBConn, "StatusUpdate", "Sql Command Verification Completed", "Successfully completed", ref errorMessage);
                System.Threading.Thread.Sleep(500);
            }
        }
        public void applySQL()
        {
            clsStatus.UdpateStatusText("Checking for pending SQL Commands(Apply)...\r\n");
            System.Threading.Thread.Sleep(500);
            Boolean result;
            QueryInfo query;

            long count;
            string dbCode = "";
            ulong replicationID=0;
            LogDBConn = new MySqlConnection(LogDBInfo.ConnectionString);
            LogDBConn.Open();
            MainDBConn = new MySqlConnection(MainDBInfo.ConnectionString);
            MainDBConn.Open();
            count = clsDBUtility.GetReplicationPendingCount(LogDBConn, ref errorMessage);
            clsDBUtility.UpdateActivityLog(LogDBConn, "Checking", "Checking for Pending SQL Commands(Apply)", count.ToString() + " SQL Command(s) found.", ref errorMessage);
            clsStatus.UdpateStatusText(count.ToString() + " SQL Command(s) found.\r\n");
            if (count > 0)
            {
                clsStatus.UdpateStatusText("Applying SQL Commands...\r\n");
                do
                {
                    clsDBUtility.GetReplicationNextPendingID(LogDBConn, ref  dbCode, ref replicationID, ref errorMessage);
                    if (replicationID > 0)
                    {
                        if (clsDBUtility.GetReplicationNextPendingIDStatus(LogDBConn,dbCode, replicationID, ref errorMessage))
                        {
                            query = clsDBUtility.GetReplicationSQL(LogDBConn,dbCode, replicationID, ref errorMessage);
                            result = clsDBUtility.ApplyReplicationSQL(MainDBConn, query, ref errorMessage); //Apply to Master Database
                            clsDBUtility.UpdateReplicationSQL(LogDBConn, "UPDATESTATUS", dbCode,replicationID, result, ref errorMessage);
                            if (!result)
                            {
                                clsMail.sendEmail(testMail.fromID, testMail.fromPass, testMail.toID, "Error On Database Updation!!!", eMailBody(query));
                            }
                            clsStatus.UdpateStatusText(".");
                            System.Threading.Thread.Sleep(1);
                        }
                        else
                        {
                            //clsStatus.UdpateStatusText("Error!!! Error!!! Error!!!\r\n");
                            break;
                        }
                    }
                }
                while (replicationID > 0);
                clsStatus.UdpateStatusText("Completed\r\n");
            }
            System.Threading.Thread.Sleep(500);
            MySqlConnection.ClearPool(MainDBConn);
            clsStatus.UdpateStatusText("Closing Master Database Connection...\r\n");
            System.Threading.Thread.Sleep(500);
            MainDBConn.Close();
            MainDBConn.Dispose();
            clsStatus.UdpateStatusText("Closed Master Database Connection\r\n");
            System.Threading.Thread.Sleep(500);
            clsStatus.UdpateStatusText("Closing Log Database Connection...\r\n");
            System.Threading.Thread.Sleep(500);
            MySqlConnection.ClearPool(LogDBConn);
            LogDBConn.Close();
            LogDBConn.Dispose();
            clsStatus.UdpateStatusText("Closed Log Database Connection\r\n");
            System.Threading.Thread.Sleep(500);
            clsStatus.UdpateStatusText("Connection Closed\r\n");
            System.Threading.Thread.Sleep(500);
        }
        private string eMailBody(QueryInfo query)
        {
            string eMailBody;
            eMailBody = "Error Message : " + errorMessage + "\r\n";
            eMailBody += "Database : " + MainDBInfo.DBName + "@";
            eMailBody += MainDBInfo.ServerName + ":";
            eMailBody += MainDBInfo.DBPort + "\r\n";
            eMailBody += "SQL ID : " + query.ID + "\r\n";
            eMailBody += "SQL Batch ID : " + query.BatchID + "\r\n";
            eMailBody += "Command Type : " + query.CommandType + "\r\n";
            eMailBody += "Object Type : " + query.ObjectType + "\r\n";
            eMailBody += "Object Name : " + query.ObjectName + "\r\n";
            eMailBody += "Command String : " + query.CommandString + "\r\n";
            return eMailBody;
        }
    }
}

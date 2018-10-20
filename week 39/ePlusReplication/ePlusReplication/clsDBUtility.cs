using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;


namespace ePlusReplication
{
    public struct DatabaseInfo
    {
        public string DBCode;
        public string ServerName;
        public string DBName;
        public string DBPort;
        public string DBUser;
        public string DBPWD;
        public string ConnectionString;
        public Boolean Success;
    }

    public struct QueryInfo
    {
        public string DBCode;
        public long ID;
        public long BatchID;
        public string ObjectType;
        public string ObjectName;
        public string CommandType;
        public string CommandString;

    }
    
    class clsDBUtility
    {
        public static string CreateConnectionString(DatabaseInfo dbInfo)
        {
            return "uid=" + dbInfo.DBUser + "; password="+dbInfo.DBPWD+"; host = " + dbInfo.ServerName + "; database=" + dbInfo.DBName + "; port = " + dbInfo.DBPort;
        }

        public static string GetConnectedDatabaseCode(string serverName, string dbName, string dbPort, MySqlConnection conn, ref string errorString)
        {
            string strSql = "SELECT CODE FROM DATABASEMASTER WHERE SERVERNAME = '" + serverName + "'";
            strSql += " AND DBNAME = '" + dbName + "' AND PORT='" + dbPort + "' AND STATUS='Active'";
            return getDBStringValue(conn,strSql,ref errorString);
        }

        public static Boolean GetPrimaryKey(MySqlConnection conn, string dbName, string tableName, ref string[] pKey,ref string errorString)
        {
            long count, i = 0;
            string strSql = "SELECT COUNT(COLUMN_NAME) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '" + dbName + "' AND TABLE_NAME = '" + tableName + "' AND COLUMN_KEY = 'PRI'";
            count = getDBLongValue(conn,strSql,ref errorString);

            pKey = new string[count];
            strSql = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '" + dbName + "' AND TABLE_NAME = '" + tableName + "' AND COLUMN_KEY = 'PRI'";
            MySqlCommand Com = new MySqlCommand();
            MySqlDataReader reader;
            Com.Connection = conn;
            try
            {
                Com.CommandText = strSql;
                reader = Com.ExecuteReader();
                while (reader.Read())
                {
                    pKey[i] = reader["COLUMN_NAME"].ToString();
                    i++;
                }
                reader.Close();
            }
            catch (MySqlException sqlEx)
            {
                errorString = sqlEx.Message;
                return false;
            }
            Com.Dispose();
            return true;
        }
        public static Boolean GetReplicationLog(MySqlConnection conn, long id, System.Collections.Queue queue ,ref string errorString)
        {
            QueryInfo query = new QueryInfo();
            string strSql = "SELECT DBCODE,ID,BATCHID,OBJECTTYPE,OBJECTNAME,COMMANDTYPE,COMMANDSTRING FROM sqllog WHERE ID > " + id.ToString();
            strSql += " ORDER BY ID";
            MySqlCommand Com = new MySqlCommand();
            MySqlDataReader reader;
            Com.Connection = conn;
            try
            {
                Com.CommandText = strSql;
                reader = Com.ExecuteReader();
                while (reader.Read())
                {
                    query.DBCode = reader["DBCODE"].ToString();
                    query.ID = Convert.ToInt64(reader["ID"].ToString());
                    query.BatchID = Convert.ToInt64(reader["BATCHID"].ToString());
                    query.ObjectType = reader["OBJECTTYPE"].ToString();
                    query.ObjectName = reader["OBJECTNAME"].ToString();
                    query.CommandType = reader["COMMANDTYPE"].ToString();
                    query.CommandString = reader["COMMANDSTRING"].ToString();
                    queue.Enqueue(query);
                }
                reader.Close();
            }
            catch (MySqlException sqlEx)
            {
                errorString = sqlEx.Message;
                return false;
            }
            Com.Dispose();
            return true;

        }
        public static void CreateReplicationLog(MySqlConnection conn, System.Collections.Queue sqlCommands, ref string errorString)
        {
            if (sqlCommands.Count == 0)
                return;
            MySqlCommand cmd;
            QueryInfo query;
            string strSql;
            do
            {
                query = (QueryInfo)sqlCommands.Dequeue();
                strSql = "INSERT INTO REPLICATIONLOG(DBCODE,ID,BATCHID,OBJECTTYPE,OBJECTNAME,COMMANDTYPE,COMMANDSTRING)";
                strSql += " VALUES ('" + query.DBCode + "','" + query.ID + "','" + query.BatchID + "','" + query.ObjectType + "','" + query.ObjectName + "','";
                strSql += query.CommandType + "',\"" + query.CommandString+ "\"" + ")";
                try
                {
                    cmd = new MySqlCommand(strSql, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    errorString = e.Message;
                }
            }
            while (sqlCommands.Count != 0);
        }
        public static QueryInfo GetReplicationSQL(MySqlConnection conn, string dbCode,ulong id, ref string errorString)
        {
            QueryInfo query;
            query.DBCode = dbCode;
            query.ID = 0;
            query.BatchID = 0;
            query.ObjectType = "";
            query.ObjectName = "";
            query.CommandType = "";
            query.CommandString = "";
            string strSql = "SELECT ID,BATCHID,OBJECTTYPE,OBJECTNAME,COMMANDTYPE,COMMANDSTRING FROM replicationlog WHERE DBCODE = '"+dbCode+"' AND ID = " + id.ToString();
            MySqlCommand Com = new MySqlCommand();
            MySqlDataReader reader;
            Com.Connection = conn;
            try
            {
                Com.CommandText = strSql;
                reader = Com.ExecuteReader();
                while (reader.Read())
                {
                    query.ID = Convert.ToInt64(reader["ID"].ToString());
                    query.BatchID = Convert.ToInt64(reader["BATCHID"].ToString());
                    query.ObjectType = reader["OBJECTTYPE"].ToString();
                    query.ObjectName = reader["OBJECTNAME"].ToString();
                    query.CommandType = reader["COMMANDTYPE"].ToString();
                    query.CommandString = reader["COMMANDSTRING"].ToString();
                }
                reader.Close();
            }
            catch (MySqlException sqlEx)
            {
                errorString = sqlEx.Message;
                return query;
            }
            Com.Dispose();
            return query;
        }
        public static long GetReplicationCount(MySqlConnection conn, ref string errorString)
        {
            long count;
            string strSql = "SELECT COUNT(ID) FROM replicationlog WHERE COMMANDSTATUS = 'Pending' AND SKIP = 'No'";
            count = getDBLongValue(conn, strSql, ref errorString);
            return count;
        }
        public static long GetReplicationPendingCount(MySqlConnection conn, ref string errorString)
        {
            long count;
            string strSql = "SELECT COUNT(ID) FROM replicationlog WHERE COMMANDSTATUS = 'Success' AND UPDATESTATUS = 'Pending' AND SKIP = 'No'";
            count = getDBLongValue(conn, strSql, ref errorString);
            return count;
        }
        public static void GetReplicationNextID(MySqlConnection conn,ref string dbCode,ref ulong nextID,ref string errorString)
        {
            string strSql = "SELECT DBCODE,ID FROM replicationlog WHERE COMMANDSTATUS = 'Pending' AND SKIP = 'No' ORDER BY DBCODE,ID LIMIT 1";
            nextID = 0;
            MySqlCommand Com = new MySqlCommand();
            MySqlDataReader reader;
            Com.Connection = conn;
            try
            {
                Com.CommandText = strSql;
                reader = Com.ExecuteReader();
                while (reader.Read())
                {
                    dbCode = reader["DBCODE"].ToString();
                    nextID = Convert.ToUInt64(reader["ID"].ToString());
                }
                reader.Close();
            }
            catch (MySqlException sqlEx)
            {
                errorString = sqlEx.Message;
                return;
            }
            Com.Dispose();
        }
        public static void GetReplicationNextPendingID(MySqlConnection conn, ref string dbCode, ref ulong nextID, ref string errorString)
        {

            string strSql = "SELECT DBCODE,ID FROM replicationlog WHERE UPDATESTATUS = 'Pending' AND SKIP = 'No' ORDER BY DBCODE,ID LIMIT 1";
            MySqlCommand Com = new MySqlCommand();
            MySqlDataReader reader;
            Com.Connection = conn;
            try
            {
                Com.CommandText = strSql;
                reader = Com.ExecuteReader();
                while (reader.Read())
                {
                    dbCode = reader["DBCODE"].ToString();
                    nextID = Convert.ToUInt64(reader["ID"].ToString());
                }
               reader.Close();
            }
            catch (MySqlException sqlEx)
            {
                errorString = sqlEx.Message;
                return;
            }
            Com.Dispose();
        }

        public static bool GetReplicationNextPendingIDStatus(MySqlConnection conn,string dbCode, ulong nextID, ref string errorString)
        {
            string strSql = "SELECT COMMANDSTATUS,UPDATESTATUS FROM replicationlog WHERE DBCODE = '" + dbCode + "' AND ID = '" + nextID + "'";
            string cmdStatus = "", updStatus = "";
            MySqlCommand Com = new MySqlCommand();
            MySqlDataReader reader;
            Com.Connection = conn;
            try
            {
                Com.CommandText = strSql;
                reader = Com.ExecuteReader();
                while (reader.Read())
                {
                    cmdStatus = reader["COMMANDSTATUS"].ToString();
                    updStatus = reader["UPDATESTATUS"].ToString();
                }
                reader.Close();
            }
            catch (MySqlException sqlEx)
            {
                errorString = sqlEx.Message;
                return false;
            }
            Com.Dispose();
            if (cmdStatus == "Success" && updStatus == "Pending")
                return true;
            else
                return false;
        }

        public static Boolean UpdateReplicationSQL(MySqlConnection conn,string cmdStatus,string dbCode,ulong id,Boolean status,ref string errorString)
        {
            string strSql="";
            MySqlCommand cmd;
            try
            {
                switch (cmdStatus)
                {
                    case "COMMANDSTATUS":
                        if (status)
                            strSql = "UPDATE REPLICATIONLOG SET COMMANDSTATUS = 'Success' WHERE DBCODE = '" + dbCode + "' AND ID =" + id.ToString() + " AND COMMANDSTATUS = 'Pending'";
                        else
                            strSql = "UPDATE REPLICATIONLOG SET COMMANDSTATUS = 'Failed',REMARKS1='" + errorString + "' WHERE DBCODE = '" + dbCode + "' AND ID =" + id.ToString() + " AND COMMANDSTATUS = 'Pending'";
                        break;
                    case "UPDATESTATUS":
                        if (status)
                            strSql = "UPDATE REPLICATIONLOG SET UPDATESTATUS = 'Success' WHERE DBCODE = '" + dbCode + "' AND ID =" + id.ToString() + " AND UPDATESTATUS = 'Pending' ";
                        else
                            strSql = "UPDATE REPLICATIONLOG SET UPDATESTATUS = 'Failed',REMARKS2=\"" + errorString + "\" WHERE DBCODE = '" + dbCode + "' AND ID =" + id.ToString() +" AND UPDATESTATUS = 'Pending' ";
                        break;
                }
                cmd = new MySqlCommand(strSql, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                errorString = e.Message;
                return false;
            }
            return true;
        }
        public static Boolean UpdateCommandString(MySqlConnection conn, string cmdString, string dbCode, ulong id, ref string errorString)
        {
            string strSql = "";
            MySqlCommand cmd;
           // int index;
            try
            {
              strSql = "UPDATE REPLICATIONLOG SET COMMANDSTRING = \""+cmdString+"\" WHERE DBCODE = '" + dbCode + "' AND ID =" + id.ToString();
              cmd = new MySqlCommand(strSql, conn);
              cmd.CommandType = System.Data.CommandType.Text;
              cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                errorString = e.Message;
                return false;
            }
            return true;
        }
        public static Boolean ApplyReplicationSQL(MySqlConnection conn, QueryInfo query, ref string errorString)
        {
            MySqlCommand cmd;
            try
            {
                cmd = new MySqlCommand(query.CommandString, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                errorString = e.Message;
                return false;
            }
            return true;
        }
        private static Boolean UpdateReplicationSQL(MySqlConnection conn, QueryInfo query, ref string errorString)
        {
            string strSql = "";
            MySqlCommand cmd;
            try
            {
                strSql = "UPDATE REPLICATIONLOG SET COMMANDSTRING = \""+query.CommandString+"\" WHERE DBCODE = '" + query.DBCode + "' AND ID =" + query.ID;
                cmd = new MySqlCommand(strSql, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                errorString = e.Message;
                return false;
            }
            return true;
        }
        public static Boolean VerifyReplicationSQL(MySqlConnection conn, string dbName, QueryInfo query, ref string errorString)
        {
           /*string newString;
            newString = updateSingleQuote(query.CommandString);
            if (newString != query.CommandString)
            {
                query.CommandString = newString;
                UpdateReplicationSQL(conn, query, ref errorString);
            }

            if (CheckSQLInjectionAttacks(query.CommandString))
            {
                errorString = "SQL injection attack detected";
                return false;
            }  */
            if (!CheckSQLSyntax(query))
            {
                errorString = "Syntax error";
                return false;
            }
            if (query.CommandType == "Update" || query.CommandType == "Delete")
            {
                string[] primaryKey = new string[0];
                GetPrimaryKey(conn, dbName, query.ObjectName, ref primaryKey, ref errorString);
                if (!CheckWhereClause(query.CommandString, primaryKey))
                {
                    errorString = "Insufficent Key(s) in Where Clause";
                    return false;
                }
            }
            return true;
        }

        private static Boolean CheckSQLInjectionAttacks(string sqlStr)
        {
            if (sqlStr.IndexOf(";") == sqlStr.Length - 1)
                return false;
            else
                return true;
        }
        private static Boolean CheckSQLSyntax(QueryInfo query)
        {
            string sql = query.CommandString;
            int pos;
            if (query.CommandType != "Execute" && query.CommandType != "DDLCommand")
            {
                if (query.CommandString.Substring(0, query.CommandType.Length).ToUpper() != query.CommandType.ToUpper())
                    return false;
                sql = sql.Substring(query.CommandType.Length, sql.Length - query.CommandType.Length).Trim();
            }

            switch (query.CommandType)
            {
                case "Insert":
                    if (sql.Substring(0, 4).ToUpper() != "INTO")
                        return false;
                    sql = sql.Substring(4).Trim();
                    if (sql.Substring(1, query.ObjectName.Length).ToUpper() != query.ObjectName.ToUpper())
                        return false;
                    sql = sql.Substring(query.ObjectName.Length + 2, sql.Length - query.ObjectName.Length - 2).Trim();
                    if (sql.Substring(0, 1).ToUpper() != "(")
                        return false;
                    pos = sql.IndexOf(')');
                    if (pos == -1)
                        return false;
                    sql = sql.Substring(pos + 1).Trim();
                    if (sql.Substring(0, 6).ToUpper() != "VALUES")
                        return false;
                    sql = sql.Substring(6).Trim();
                    if (sql.Substring(0, 1).ToUpper() != "(")
                        return false;
                    pos = sql.IndexOf(')');
                    if (pos == -1)
                        return false;
                    break;
                case "Update":
                    if (sql.Substring(1, query.ObjectName.Length).ToUpper() != query.ObjectName.ToUpper())
                        return false;
                    sql = sql.Substring(query.ObjectName.Length + 2, sql.Length - query.ObjectName.Length - 2).Trim();
                    if(sql.Substring(0,3).ToUpper() != "SET")
                        return false;
                    pos = sql.IndexOf("WHERE");
                    if(pos ==-1)
                        return false;
                    break;
                case "Delete":
                    if (sql.Substring(0, 4).ToUpper() != "FROM")
                        return false;
                    sql = sql.Substring(4).Trim();
                    if (sql.Substring(1, query.ObjectName.Length).ToUpper() != query.ObjectName.ToUpper())
                        return false;
                    break;
                case "Execute":
                    if (sql.Substring(0, 4).ToUpper() != "CALL")
                        return false;
                    break;
                case "DDLCommand":
                    if (sql.Substring(0, 11).ToUpper() != "CREATE USER" && sql.Substring(0, 5).ToUpper() != "GRANT" && sql.Substring(0, 12).ToUpper() != "SET PASSWORD")
                        return false;
                    break;
            }

            return true;
        }
        private static string updateSingleQuote(string sqlValue)
        {
            string columnSeparator = "','";
            int columnSeparatorPos, singleQuotePos,pos,stPos;
            stPos = sqlValue.IndexOf("VALUES");
            if (stPos == -1)
                return sqlValue;
            pos = stPos + 9;
            string tmpStr = sqlValue.Substring(pos, sqlValue.Length - (pos+2));  //Remove first & last Quote  
            do
            {
                columnSeparatorPos = tmpStr.IndexOf(columnSeparator);
                singleQuotePos = tmpStr.IndexOf("'");
                if (columnSeparatorPos == -1)
                    break;
                if (columnSeparatorPos == singleQuotePos)
                {
                    tmpStr = tmpStr.Substring(columnSeparatorPos + columnSeparator.Length, tmpStr.Length - (columnSeparatorPos + columnSeparator.Length));
                }
                else
                {
                    sqlValue = sqlValue.Substring(0, pos + singleQuotePos) + "\\\\" + sqlValue.Substring(pos + singleQuotePos, sqlValue.Length - (pos + singleQuotePos));
                    tmpStr = tmpStr.Substring(columnSeparatorPos + columnSeparator.Length, tmpStr.Length - (columnSeparatorPos + columnSeparator.Length));
                }
                pos += columnSeparatorPos + columnSeparator.Length;
            } 
            while (tmpStr != "");
            return sqlValue;
        }
        /*     private static string updateSingleQuote2(string sqlValue)
             {
                 string substr,value1;
                 int str1,str2;
                 int index=0;
                 int singleQuotePos, pos;
                 int sqlvaluepos = sqlValue.IndexOf(";");
                 string value=sqlValue.Remove(sqlvaluepos, 2);
                 pos = 0;
                 string tmpStr;//= value.Substring(pos, value.Length - (pos + 2));  //Remove first & last Quote  

                 str1 = value.IndexOf("'");
                 str2 = value.LastIndexOf("'");             
               //  substr = value.Substring(value.IndexOf("'") + 1,value.Length);
                 while ((index = value.IndexOf("'", index)) != -1)
                 {
                     value = value.Substring(0, pos + index) + "\\\\" + value.Substring(index, str2)+"\\\\";
                 }    
                     do
                 {
                     singleQuotePos = value.IndexOf("'");



                     tmpStr = value.Substring(singleQuotePos+5);

                     //Spos += columnSeparatorPos + columnSeparator.Length;
                 }
                 while (tmpStr != "");
                 return value;
             }       */
        public static string UpdateQuotationMark(string input)
        {
            int index;
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (c == '"' || c == '\'')
                {
                    sb.Append("\\\\");
                    sb.Append(c);
                }
                else
                {
                    sb.Append(c);
                }
                
            }
           
            return sb.ToString();
        }
        private static Boolean CheckWhereClause(string sql,string[] pKey)
        {
            int pos;
            pos = sql.IndexOf("WHERE");
            if (pos == -1)
                return false;
            sql = sql.Substring(pos, sql.Length - pos);
            foreach(string key in pKey)
            {
                if (sql.IndexOf(key) == -1)
                    return false;
            }
            return true;
        }
        public static Boolean GetReplicationLogDBInfo(MySqlConnection conn, string MainDBCode, ref string errorString,ref DatabaseInfo[] ReplicationDBInfo)
        {
            long cnt;
            string strSql = "SELECT COUNT(LOGSERVERNAME) FROM DATABASEMASTER WHERE CODE <> '" + MainDBCode + "'";
            cnt = getDBLongValue(conn, strSql,ref errorString);
            ReplicationDBInfo = new DatabaseInfo[cnt];
            strSql = "SELECT CODE,LOGSERVERNAME,LOGDBNAME,LOGPORT FROM DATABASEMASTER WHERE CODE <> '" + MainDBCode + "'";
            MySqlCommand Com = new MySqlCommand();
            MySqlDataReader reader;
            int i = 0;
            Com.Connection = conn;
            try
            {
                Com.CommandText = strSql;
                reader = Com.ExecuteReader();
                while (reader.Read())
                {
                    ReplicationDBInfo[i].DBCode = reader["CODE"].ToString();
                    ReplicationDBInfo[i].ServerName = reader["LOGSERVERNAME"].ToString();
                    ReplicationDBInfo[i].DBName = reader["LOGDBNAME"].ToString();
                    ReplicationDBInfo[i].DBPort = reader["LOGPORT"].ToString();
                    i++;
                }
                reader.Close();
            }
            catch (MySqlException sqlEx)
            {
                errorString = sqlEx.Message;
                return false;
            }
            Com.Dispose();
            return true;
        }
        public static DatabaseInfo GetLogDBDetails(MySqlConnection conn, string dbCode, ref string errorString)
        {
            DatabaseInfo logInfo;
            string strSql = "SELECT LOGSERVERNAME,LOGDBNAME,LOGPORT FROM DATABASEMASTER WHERE CODE = '" + dbCode + "'";
            MySqlCommand Com = new MySqlCommand();
            MySqlDataReader reader;
            logInfo.DBCode = null;
            logInfo.ServerName = null;
            logInfo.DBName = null;
            logInfo.DBPort = null;
            logInfo.DBUser = null;
            logInfo.DBPWD = null;
            logInfo.ConnectionString = null;
            logInfo.Success = false;
            Com.Connection = conn;
            try
            {
                Com.CommandText = strSql;
                reader = Com.ExecuteReader();
                while (reader.Read())
                {
                    logInfo.DBCode = dbCode;
                    logInfo.ServerName= reader["LOGSERVERNAME"].ToString();
                    logInfo.DBName = reader["LOGDBNAME"].ToString();
                    logInfo.DBPort = reader["LOGPORT"].ToString();
                }
                reader.Close();
            }
            catch (MySqlException sqlEx)
            {
                errorString = sqlEx.Message;
            }
            Com.Dispose();
            return logInfo;
        }

        public static string getDBStringValue(MySqlConnection conn, string sqlString,ref string errorString)
        {
            MySqlCommand Com = new MySqlCommand();
            Com.Connection = conn;
            Com.CommandText = sqlString;
            try
            {
                string strResult = (string)Com.ExecuteScalar();
                return strResult;
            }
            catch (MySqlException sqlEx)
            {
                errorString = sqlEx.Message;
                return "";
            }
            catch
            {
                return "";
            }
        }
        public static ulong getDBuLongValue(MySqlConnection conn, string sqlString, ref string errorString)
        {
            MySqlCommand Com = new MySqlCommand();
            Com.Connection = conn;
            Com.CommandText = sqlString;
            try
            {
                ulong ulongResult = (ulong)Com.ExecuteScalar();
                return ulongResult;
            }
            catch (MySqlException sqlEx)
            {
                errorString = sqlEx.Message;
                return 0;
            }
            catch(Exception ex)
            {
                errorString = ex.Message;
                return 0;
            }
        }
        public static long getDBLongValue(MySqlConnection conn, string sqlString,ref string errorString)
        {
            MySqlCommand Com = new MySqlCommand();
            Com.Connection = conn;
            Com.CommandText = sqlString;
            try
            {
                long longResult = (long)Com.ExecuteScalar();
                return longResult;
            }
            catch (MySqlException sqlEx)
            {
                errorString = sqlEx.Message;
                return 0;
            }
        }
        public static bool IsRequiredTimeGap(MySqlConnection conn, string dbCode,ref string errorString)
        {
            string strSql,result;
            strSql = "SELECT CASE WHEN LASTCONNECTION IS NULL THEN 'T' ELSE";
            strSql += " (CASE WHEN ADDDATE(LASTCONNECTION, INTERVAL MINIMUMTIMEGAP MINUTE)<NOW() THEN 'T' ELSE 'F' END)";
            strSql += " END A FROM REPLICATIONINFO WHERE CODE = '" + dbCode + "'";
            result = getDBStringValue(conn, strSql, ref errorString);
            if (result == "T")
                return true;
            else
                return false;
        }
        public static void UpdateConnectionInfo(MySqlConnection conn,string dbCode, ref string errorString)
        {
            MySqlCommand cmd;
            string strSql;
            strSql = "UPDATE REPLICATIONINFO SET LASTCONNECTION=NOW() WHERE CODE='" + dbCode + "';";

            try
            {
                cmd = new MySqlCommand(strSql, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                errorString = e.Message;
            }
        }

        public static void UpdateActivityLog(MySqlConnection conn, string activityType, string activity, string result, ref string errorString)
        {
            MySqlCommand cmd;
            string strSql;
            strSql = "INSERT INTO ACTIVITYLOG(`ACTIVITYTYPE`, `ACTIVITY`, `RESULT`) ";
            strSql += " VALUES ('" + activityType + "', '" + activity + "', \"" + result + "\")";
            try
            {
                cmd = new MySqlCommand(strSql, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                errorString = e.Message;
            }
        }
    }
}

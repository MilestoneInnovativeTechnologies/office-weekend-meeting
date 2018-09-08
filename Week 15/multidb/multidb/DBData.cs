using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multidb
{
    public struct dbset
    {
    public string id;
    public string category;
    public string name;
    public string servername;
    public string port;
    public string status;
   }
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
    public class DBData
    {
        public string dbpath;
        public DBData()
        {

            string stringsql = "use eplusmasterdb;";
            //dbpath = @"eplusmasterdb//Table(1)/dblist";
        }
        public dbset Readdb(string c, string d)
        {
            dbset db1 = new dbset();
            string var1 = c;
            string var2 = d;
            string strsql = "SELECT * from dblist";
            return db1;
        }
    }

}

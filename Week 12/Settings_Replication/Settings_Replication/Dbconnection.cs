using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Xml;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;


namespace Settings_Replication
{
    public struct DBinfo
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
    public class Dbconnection
    {
      
        clsXMLData settings1 = new clsXMLData();
        xmlSettings settings = new xmlSettings();
        string connectionString = null;
        MySqlConnection cnnLog;
        MySqlCommand cmd = new MySqlCommand();
        public string getConnectionString (DBinfo dbconnstring)
        {
            //string a = "Data Source=" + dbconnstring.ServerName + ";Initial Catalog=" + dbconnstring.DBName + ";User ID=root" + ";Password=metalic";
            return "Data Source="+dbconnstring.ServerName+";Initial Catalog="+dbconnstring.DBName+";User ID=root"+";Password=metalic";
        }
      
        public void createLogFile(DBinfo dbconnstring)
        {
            string connString = @"server=" + dbconnstring.ServerName + ";userid=root" + ";password=metalic";
            MessageBox.Show(connString);
            cnnLog = new MySqlConnection(connString); 
            try
            {
                cnnLog.Open();
                // MessageBox.Show("Connection Open ! ");
                MessageBox.Show("Connection Open!\n", "Connection", MessageBoxButtons.OK, MessageBoxIcon.None);

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Can not open connection ! \n" + ex.ToString());
                //MessageBox.Show(ex.ToString());
            }
            cmd.Connection = cnnLog;
            cmd.CommandText = "CREATE DATABASE " + dbconnstring.DBName + "_log";
            cmd.ExecuteNonQuery();
        }
    }
}

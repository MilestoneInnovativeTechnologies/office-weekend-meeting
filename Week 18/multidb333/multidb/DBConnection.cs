using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;


namespace multidb
{
    public struct DBinfo
    {
        public string ServerName;
        public string ConnectionString;
        public Boolean Success;
    }
    public class DBConnection
    {
        string connectionString = null;
       // MySqlConnection cnnLog;
        MySqlCommand cmd = new MySqlCommand();
        public string getConnectionString(DBinfo dbconnString)
        {
            return "Data Source=localhost" + ";Initial Catalog=eplusmasterdb" + ";User ID=root" + ";Password=metalic";
        }
    }
}

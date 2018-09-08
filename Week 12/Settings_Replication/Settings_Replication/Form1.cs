using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Xml;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;



namespace Settings_Replication
{
  
    public partial class txtDatabase : Form
    {
       
        clsXMLData settings1 = new clsXMLData();
        xmlSettings settings = new xmlSettings();
        string connectionString = null;
        MySqlConnection cnn;
        Dbconnection dbinfo = new Dbconnection();
        DBinfo dbinfo1 = new DBinfo();
        DataSet ds = new DataSet();
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        MySqlCommand cmd = new MySqlCommand();
        int change = 0;
        int found = 0;
        public txtDatabase()
        {
            InitializeComponent();
           
            settings = settings1.ReadSettings();
            
            //SqlCommand cmd = new SqlCommand();
            //SqlDataReader reader;
           
            dbinfo1.ServerName = settings.serverName;
            dbinfo1.DBName = settings.dbName;
            dbinfo1.DBPort = settings.dbPort;
            dbinfo1.DBUser = settings.userName;
            dbinfo1.DBPWD = settings.password;
            //connectionString = "Data Source=localhost;Initial Catalog=eplus040_data;User ID=root;Password=metalic";
            //connectionString = "Data Source=serverName;Initial Catalog=settings.dbName;User ID=root;Password=metalic";
            connectionString = dbinfo.getConnectionString(dbinfo1);
            //connectionString = "Data Source=mserver;Initial Catalog=eplus030_data;User ID=admin;Password=";
            cnn = new MySqlConnection(connectionString); 
            try
            {
                cnn.Open();
               // MessageBox.Show("Connection Open ! ");
                MessageBox.Show("Connection Open!\n", "Connection", MessageBoxButtons.OK, MessageBoxIcon.None);
                //dbinfo.createLogFile(dbinfo1);

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Can not open connection ! \n"+ ex.ToString());
                //MessageBox.Show(ex.ToString());
            }
            //DataSet ds = new DataSet();
            //MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT brcode from branchstore",cnn);
            adapter.SelectCommand = new MySqlCommand("SELECT code from branchmaster", cnn);
            try
            {
                adapter.Fill(ds,"branchmaster");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("No Branches Available!\n" + ex.ToString());           
            }

            //this.lstBranch.DataSource = ds.Tables[0];
            //this.lstBranch.DisplayMember = "Branch";
            //this.lstBranch.DisplayMember = "brcode";
           // int i = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                 for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                 {
                    lstBranch.Items.Add(row[i].ToString());
                /*lstBranch.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                i++;*/
                }
            }
       }
        /*private void Form1_Load(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }*/

        private void lblUpdate_Click(object sender, EventArgs e)
        {
            cmd.Connection = cnn;
            int cnt = 01;
            string c = lstBranch.Items[lstBranch.SelectedIndex].ToString();
            int ins = 0;
            if (found == 0)
            {               
                cmd.CommandText = "select * from databasemaster";
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cnt += 1;
                }
                rdr.Close();
                var iString = cnt.ToString().PadLeft(2, '0');
                //MessageBox.Show(iString.ToString());
                cmd.CommandText = "insert into databasemaster (" + "code," + "servername," + "dbname," + "port) values( '"  + iString + "' , '" + txtServer.Text + "' , '" + txtDb.Text + "' , '" + txtPort.Text + " ')";
                try
                {
                    cmd.ExecuteNonQuery();
                    ins = 1;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Cannot Insert!\n" + ex.ToString());
                    //MessageBox.Show("Cannot Insert!\n", "Inserting Data",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //MessageBox.Show("insert into databasemapping (" + "cocode," + "brcode," + "fycode," + "dbcode" + ") SELECT" + " branchmaster.cocode" + "," + " branchmaster.code" + "," + " fiscalyearmaster.code" + "," + " databasemaster.code" + " FROM branchmaster, fiscalyearmaster , databasemaster" + " WHERE" + " branchmaster.code ='" + c + "' and databasemaster.code =" + "cnt" + " and fiscalyearmaster.status = "ReadWrite" " );
                if (ins == 1)
                {
                    //cmd.CommandText = "insert into databasemapping (" + "cocode," + "brcode," + "fycode," + "dbcode" + ") SELECT" + " branchmaster.cocode" + "," + " branchmaster.code" + "," + " fiscalyearmaster.code" + "," + " databasemaster.code" + " FROM branchmaster, fiscalyearmaster , databasemaster" + " WHERE" + " branchmaster.code ='" + c + "' and databasemaster.code =" + iString + " and fiscalyearmaster.status = + " + " \"ReadWrite\" ";
                    cmd.CommandText = "insert into databasemapping (" + "cocode," + "brcode," + "fycode," + "dbcode" + ") SELECT" + " branchmaster.cocode" + "," + " branchmaster.code" + "," + " fiscalyearmaster.code" + "," + " databasemaster.code" + " FROM branchmaster, fiscalyearmaster , databasemaster" + " WHERE" + " branchmaster.code ='" + c + "' and databasemaster.code =" + iString;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Inserted");
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Cannot Insert!\n" + ex.ToString());
                        //MessageBox.Show("Cannot Insert!\n", "Inserting Data",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                txtServer.Clear();
                txtDb.Clear();
                txtPort.Clear();
                /*adapter.InsertCommand = new MySqlCommand("insert into databasemaster (servername,dbname,port) values( " + txtServer + "," + txtDb + "," + txtPort + ")", cnn);
                adapter.Update(ds);*/
            }
                if (found == 1 && change > 3)
            {

                //MessageBox.Show(ds.Tables[0].Rows[0][0].ToString());
                // MessageBox.Show("update databasemaster set" + " servername= '" + txtServer.Text + "' ," + " dbname= '" + txtDb.Text + "' ," + " port ='" + txtPort.Text + "' where" + " code =" + "\'" + ds.Tables[0].Rows[0][0].ToString() + "\'");
                /*adapter.UpdateCommand = new MySqlCommand("update databasemaster set" + " servername= '" + txtServer.Text + "' ," + " dbname= '" + txtDb.Text + "' ," + " port = '" + txtPort.Text + "' where" + " code = '" +  ds.Tables[0].Rows[0][0].ToString() + "\'", cnn);
                 ds.Tables[0].Rows[0][1] = txtServer.Text;
                 ds.Tables[0].Rows[0][3] = txtDb.Text;
                 ds.Tables[0].Rows[0][5] = txtPort.Text;
                adapter.Update(ds, "databasemaster");*/
                
                /*adapter.InsertCommand = new SqlCommand(sql, connection);
                adapter.InsertCommand.ExecuteNonQuery();*/
                cmd.CommandText = "update databasemaster set" + " servername= '" + txtServer.Text + "' ," + " dbname= '" + txtDb.Text + "' ," + " port ='" + txtPort.Text + "' where" + " code =" + "\'" + ds.Tables[0].Rows[0][0].ToString() + "\'";
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Cannot Update!\n"+ ex.ToString());

                }
                //MessageBox.Show(a.ToString());
                //MessageBox.Show(ds.Tables.Count.ToString());
                /*ds.Tables[0].AcceptChanges();*/
                // MessageBox.Show(ds.Tables[0].Rows[0][3].ToString());

            }
            

        }
       
        private void lstBranch_Click(object sender, EventArgs e)
        {
            cmd.Connection = cnn;
            int indx = lstBranch.SelectedIndex;
            if (indx >= 0)
            {
                txtServer.Clear();
                txtDb.Clear();
                txtPort.Clear();
                found = 0;
                change = 0;
                string index = lstBranch.Items[lstBranch.SelectedIndex].ToString();
                cmd.CommandText = "SELECT name from branchmaster where code = " + "\"" + index + "\"";
                try
                {
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("Name selected");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot Select Name!\n" + ex.ToString());

                }
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    txtbranchName.Text = rdr.GetValue(0).ToString();
                }
                rdr.Close();
                //string index = lstBranch.SelectedIndex.ToString();
                //MessageBox.Show(index);
                //DataSet ds = new DataSet();
                ds.Reset();
                adapter.SelectCommand = new MySqlCommand("SELECT * from databasemaster where code in (select dbcode from databasemapping where brcode=" + "\"" + index + "\"" + ")", cnn); 
                adapter.Fill(ds,"databasemaster");
                //MessageBox.Show(ds.Tables[0].Rows[0][1].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    found = 1;//MessageBox.Show(found.ToString());
                    txtServer.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtDb.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtPort.Text = ds.Tables[0].Rows[0][5].ToString();
                }
               // MessageBox.Show(ds1.Tables[0].Rows[0][0].ToString());
            }
        }

        private void txtDb_TextChanged(object sender, EventArgs e)
        {
            change += 1;
        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
            change += 1;
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            change += 1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            cnn.Close();
            Close();
        }

    }
}

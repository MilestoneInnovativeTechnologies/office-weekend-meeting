using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Xml;
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
        DataSet ds1 = new DataSet();
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        MySqlCommand cmd = new MySqlCommand();
        int update = 0;
        int found = 0;
        string dbname;
        public txtDatabase()
        {
            InitializeComponent(); 
            settings = settings1.ReadSettings();
            dbinfo1.ServerName = settings.serverName;
            dbinfo1.DBName = settings.dbName;
            dbinfo1.DBPort = settings.dbPort;
            dbinfo1.DBUser = settings.userName;
            dbinfo1.DBPWD = settings.password;
            connectionString = dbinfo.getConnectionString(dbinfo1);
            cnn = new MySqlConnection(connectionString); 
            try
            {
                cnn.Open();
                //MessageBox.Show("Connection Open!\n", "Connection", MessageBoxButtons.OK, MessageBoxIcon.None);
                //dbinfo.createLogFile(dbinfo1,null);
                cmd.Connection = cnn;
                //cmd.CommandText = "UPDATE " + dbinfo1.DBName + ".setup SET `DBTYPE`='DistributedServer' WHERE  `CODE`='01'";
                //cmd.ExecuteNonQuery();
                //cmd.CommandText = "update databasemaster set logdbname = '"  + dbinfo1.DBName.Replace("_data", "_log") + "' , logservername = '" + dbinfo1.ServerName + "' , logport = '" + dbinfo1.DBPort +"' where code = '01'";
               // cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Can not open connection ! \n"+ ex.ToString());   
            }
            adapter.SelectCommand = new MySqlCommand("SELECT code from branchmaster", cnn);
            try
            {
                adapter.Fill(ds,"branchmaster");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("No Branches Available!\n" + ex.ToString());           
            }
            /* foreach (DataRow row in ds.Tables[0].Rows)
             {
                  for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                  {
                     lstBranch.Items.Add(row[i].ToString());          
                 }
             }*/
            this.lstBranch.SelectedIndexChanged -= new System.EventHandler(this.lstBranch_SelectedIndexChanged);
            lstBranch.DataSource = ds.Tables["branchmaster"];
            lstBranch.DisplayMember = "code";
            lstBranch.ValueMember = "code";
            this.lstBranch.SelectedIndexChanged += new System.EventHandler(this.lstBranch_SelectedIndexChanged);
        }
     
        private void lblUpdate_Click(object sender, EventArgs e)
        {
            cmd.Connection = cnn;
            int cnt = 01;
            string c = lstBranch.SelectedValue.ToString();
            //string c = lstBranch.Items[lstBranch.SelectedIndex].ToString();
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
                
                //cmd.CommandText = "UPDATE " + dbinfo1.DBName + ".setup SET `DBTYPE`='DistributedClient' WHERE  `CODE`='01'";
                //cmd.ExecuteNonQuery();
                cmd.CommandText = "insert into databasemaster ( code, servername, logservername, dbname, logdbname, port, logport) values( '" + iString + "' , '" + txtServer.Text + "' , '" + txtServer.Text + "' , '"  + txtDb.Text + "' , '" + txtDb.Text.Replace("_data","_log") + "' , '"  + txtPort.Text + "' , '" + txtPort.Text + " ')";             
                try
                {
                    cmd.ExecuteNonQuery();
                    ins = 1;
                    //dbinfo.createLogFile(dbinfo1,txtDb.Text);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Cannot Insert!\n" + ex.ToString());             
                }              
                if (ins == 1)
                {
                    //cmd.CommandText = "insert into databasemapping ( cocode, brcode, fycode, dbcode) SELECT branchmaster.cocode, branchmaster.code, fiscalyearmaster.code, databasemaster.code FROM branchmaster, fiscalyearmaster , databasemaster WHERE branchmaster.code ='" + c + "' and databasemaster.code =" + iString;
                    cmd.CommandText = "insert into databasemapping ( cocode, brcode, fycode, dbcode) SELECT branchmaster.cocode, branchmaster.code, fiscalyearmaster.code, databasemaster.code FROM branchmaster, fiscalyearmaster , databasemaster WHERE branchmaster.code ='" + c + "' and databasemaster.code =" + iString + " and fiscalyearmaster.status = + " + " \"ReadWrite\" ";
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Inserted");
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Cannot Insert!\n" + ex.ToString());                      
                    }
                }
                txtServer.Clear();
                txtDb.Clear();
                txtPort.Clear();
            }
                if (found == 1 && update == 1)
            {
                
                cmd.CommandText = "update databasemaster set" + " servername= '" + txtServer.Text + "' ," + "logservername= '" + txtServer.Text + "' ," + " dbname= '" + txtDb.Text + "' ," + "logdbname= '" + txtDb.Text.Replace("_data","_log") + "' ," + " port ='" + txtPort.Text + "' ," + " logport= '"+ txtPort.Text + "' where" + " code =" + "\'" + ds1.Tables[0].Rows[0][0].ToString() + "\'";
                try
                {
                    cmd.ExecuteNonQuery();
                    /*if (dbname != txtDb.Text)
                    {
                        MessageBox.Show(txtDb.Text);
                        cmd.CommandText = "UPDATE " + txtDb.Text + ".setup SET `DBTYPE`='DistributedClient' WHERE  `CODE`='01'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "DROP DATABASE IF EXISTS " + dbname.Replace("_data","") + "_log";
                        cmd.ExecuteNonQuery();
                        dbinfo.createLogFile(dbinfo1, txtDb.Text);
                    }*/
                    MessageBox.Show("Updated");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Cannot Update!\n"+ ex.ToString());

                }
                update = 0;          
            }
            

        }
       
        private void lstBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd.Connection = cnn;
            int indx = lstBranch.SelectedIndex;
            if (indx >= 0)
            {
                
                /*txtServer.Clear();
                txtDb.Clear();
                txtPort.Clear();*/
                dbname = "";
                found = 0;
                //change = 0;
                string index = lstBranch.SelectedValue.ToString();
                //string index = lstBranch.SelectedItem.ToString(); MessageBox.Show(index);
                //string index = lstBranch.Items[lstBranch.SelectedIndex].ToString();
                cmd.CommandText = "SELECT name from branchmaster where code = " + "\"" + index + "\"";
                try
                {
                    cmd.ExecuteNonQuery();
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
                ds1.Reset();
                adapter.SelectCommand = new MySqlCommand("SELECT * from databasemaster where code in (select dbcode from databasemapping where brcode=" + "\"" + index + "\"" + ")", cnn); 
                adapter.Fill(ds1,"databasemaster");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    found = 1;
                    clean();

                    txtServer.DataBindings.Add("Text",ds1.Tables["databasemaster"],"SERVERNAME");         
                    txtPort.DataBindings.Add("Text", ds1.Tables["databasemaster"], "port");
                    txtDb.DataBindings.Add("Text", ds1.Tables["databasemaster"], "dbname");
                   /*
                    txtServer.Text = ds1.Tables[0].Rows[0][1].ToString();
                    txtDb.Text = ds1.Tables[0].Rows[0][3].ToString();
                    txtPort.Text = ds1.Tables[0].Rows[0][5].ToString();*/
                    dbname = txtDb.Text; 
                }
            }
        }
        private void clean()
        {
            txtServer.DataBindings.Clear();
            txtPort.DataBindings.Clear();
            txtDb.DataBindings.Clear();
        }

        private void txtDb_TextChanged(object sender, EventArgs e)
        {
            update = 1;
        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
            update = 1;
        }

        private void txtDb_TextChanged_1(object sender, EventArgs e)
        {
            update = 1;
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            update = 1;
        }

       /* private void form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }*/

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();    
        }
      
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)

        {         
            cmd.Connection = cnn;
            int cnt = 0;
            cmd.CommandText = "SELECT CODE,NAME FROM BRANCHMASTER WHERE CODE NOT IN (SELECT BRCODE FROM DATABASEMAPPING)";
            try
            {
                MySqlDataReader chkPending = cmd.ExecuteReader();
                string outputMessage = "Database Not Mapped For:\n\n";
                DialogResult dialogResult;
                while (chkPending.Read())
                {
                    cnt += 1;
                    outputMessage += chkPending.GetValue(0) + "- " + chkPending.GetValue(1) + "\n";
                }
                chkPending.Close();
                if (cnt > 0)
                {

                    //outputMessage += "\nWould you like to Exit ?";
                    //dialogResult = MessageBox.Show(outputMessage, "Pending Mappings", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    outputMessage += "\nFill Details of all branches before closing!!!";
                    dialogResult = MessageBox.Show(outputMessage, "Pending Mappings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.OK)
                    {
                        if (e.CloseReason == CloseReason.UserClosing)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            cnn.Close();
                        }
                    }
                    /*if (dialogResult == DialogResult.No)
                    {
                        if (e.CloseReason == CloseReason.UserClosing)
                        { 
                             e.Cancel = true;
                        }
                    }
                    else if (dialogResult == DialogResult.Yes)
                    {
                        cnn.Close();
                        if (e.CloseReason != CloseReason.UserClosing)
                        {
                            e.Cancel = true;
                        }
                    }*/
                }
                else
                {
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
    
    }
}









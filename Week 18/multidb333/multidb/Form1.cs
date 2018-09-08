using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



namespace multidb
{
    public partial class Form1 : Form
    {
        string connectionString = null;
        MySqlConnection cnn;
        DBConnection dbinfo = new DBConnection();
        DBinfo dbinfo1 = new DBinfo();
        DataSet ds = new DataSet();
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        MySqlCommand cmd = new MySqlCommand();
        String dlist;
        string idi;
        public Form1()
        {
            InitializeComponent();
            connectionString = dbinfo.getConnectionString(dbinfo1);
            cnn = new MySqlConnection(connectionString);
            try
            {
                cnn.Open();
                MessageBox.Show("Connection Open!\n", "Connection", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Can not open connection ! \n" + ex.ToString());
            }
        }
        private void Form_Load()
        {
            using (MySqlConnection cnn = new MySqlConnection(connectionString))
            {
             cnn.Open();
            dlist = comboBox1.Text;
                MessageBox.Show(dlist);
                //using (MySqlCommand cmd = new MySqlCommand("SELECT ID,NAME,SERVERNAME,DBNAME,PORT FROM dblist where CATEGORY = @cat", cnn))
                    adapter.SelectCommand = new MySqlCommand("SELECT ID,NAME,SERVERNAME,DBNAME,PORT FROM dblist where CATEGORY = STD", cnn);
                {
                    //cmd.Parameters.AddWithValue("@cat", dlist);
                   // cmd.Parameters.Add(new MySqlParameter("@cat", dlist));
                    try
                    {
                        adapter.Fill(ds, "dblist");
                        MessageBox.Show("selected");
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("No Databases Available!\n" + ex.ToString());
                    }
                    testfunctionlist();
                }
            } 
        }

        private void testfunctionlist()
        {
            // DataSet ds = new DataSet();
            // dlist = comboBox1.Text;
            //adapter.SelectCommand = new MySqlCommand("SELECT ID,NAME,SERVERNAME,DBNAME,PORT FROM dblist where CATEGORY = " + "\"" + dlist + "\"", cnn);
            //adapter.SelectCommand = new MySqlCommand(" ", cmd);
           
            try
            {
                this.textBox1.DataBindings.Clear();
                this.textBox1.DataBindings.Add("Text", this.ds.Tables[0], "NAME");
                this.textBox2.DataBindings.Clear();
                this.textBox2.DataBindings.Add("Text", this.ds.Tables[0], "SERVERNAME");
                this.textBox4.DataBindings.Clear();
                this.textBox4.DataBindings.Add("Text", this.ds.Tables[0], "DBNAME");
                this.textBox5.DataBindings.Clear();
                this.textBox5.DataBindings.Add("Text", this.ds.Tables[0], "PORT");
                textBox3.Text = dlist;
                listBox1.DataSource = ds.Tables[0];
                listBox1.DisplayMember = "NAME";
                listBox1.ValueMember = "ID";
                var selectedValue = listBox1.SelectedItem;
                
                if (selectedValue != null)
                {
                    idi = listBox1.SelectedValue.ToString();
                }
            }
             catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void update()
        {
            cmd.Connection = cnn;
            string index1 = listBox1.Items[listBox1.SelectedIndex].ToString();
            string index11 = listBox1.Text;
            string var1 = comboBox1.Text;
            cmd.CommandText = "update dblist set" + " NAME ='" + textBox1.Text + "' where " + " ID = " + "\'" + idi + "\'";
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Cannot Insert!\n" + ex.ToString());
            }

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //testfunctionlist();
            Form_Load();
        }
        private void button1_Click(object sender, EventArgs e)
        {
             update();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
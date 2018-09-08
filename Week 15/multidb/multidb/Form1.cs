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
        int found = 0;
        String dlist;

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
        private void comboBox1_Click(object sender, EventArgs e)
        {
            dlist = comboBox1.Text;
            adapter.SelectCommand = new MySqlCommand("SELECT NAME FROM dblist where CATEGORY = " + "\"" + dlist + "\"", cnn);
            try
            {
                adapter.Fill(ds, "dblist");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("No Databases Available!\n" + ex.ToString());
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    listBox1.Items.Add(row[i].ToString());
                }
            }
        }
        private void listBox1_Click(object sender, EventArgs e)
        {
            cmd.Connection = cnn;
            int id = listBox1.SelectedIndex;
            string dname = listBox1.SelectedItem.ToString();

            id = id + 1;

            int found = 0;
            if (id >= 0)
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                string index = listBox1.Items[listBox1.SelectedIndex].ToString();
                cmd.CommandText = ("SELECT SERVERNAME FROM dblist where NAME = " + "\"" + index + "\"");
                string var = comboBox1.Text;
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Name selected");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot selected\n" + ex.ToString());

                }
                cmd.CommandText = (" select ID from dblist where CATEGORY = '" + var + "' and NAME = '" + index + "'");
                int result = ((int)cmd.ExecuteScalar());
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot selected\n" + ex.ToString());
                }
                ds.Reset();
                adapter.SelectCommand = new MySqlCommand("SELECT * from dblist where ID IN(SELECT ID FROM dblist WHERE CATEGORY = '" + var + "' and NAME =" + "\"" + index + "\"" + ")", cnn);
               // adapter.SelectCommand = new MySqlCommand("SELECT * FROM dblist WHERE CATEGORY = '" + var + "' and NAME =" + "\"" + index + "\"" , cnn);
                adapter.Fill(ds, "dblist");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    found = 1;
                    textBox1.Text = ds.Tables[0].Rows[0][2].ToString();
                    textBox2.Text = ds.Tables[0].Rows[0][3].ToString();
                    textBox3.Text = ds.Tables[0].Rows[0][1].ToString();
                    textBox4.Text = ds.Tables[0].Rows[0][4].ToString();
                    textBox5.Text = ds.Tables[0].Rows[0][5].ToString();
                }
            }
        }
        public void Form_Load(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            cmd.Connection = cnn;
            string index1 = listBox1.Items[listBox1.SelectedIndex].ToString();
            string var1 = comboBox1.Text;
            if (found == 0)
            {
                cmd.CommandText = (" select ID from dblist where CATEGORY = '" + var1 + "' and NAME = '" + index1 + "'");
                int result1 = ((int)cmd.ExecuteScalar());
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot selected\n" + ex.ToString());
                }
                cmd.CommandText = "update dblist set" + " NAME ='" + textBox1.Text + "' where " + " ID = " + "\'" + result1 + "\'";
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

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
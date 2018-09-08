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
    public partial class frmMasterDB : Form
    {
        string connectionString = null;
        MySqlConnection cnn;
        DBConnection dbinfo = new DBConnection();
        DBinfo dbinfo1 = new DBinfo();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataRow drcurrent;
        MySqlDataAdapter da;
        MySqlCommand cmd = new MySqlCommand();
        public frmMasterDB()
        {
            InitializeComponent();
            connectionString = dbinfo.getConnectionString(dbinfo1);
            cnn = new MySqlConnection(connectionString);
            try
            {
                cnn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Can not open connection ! \n" + ex.ToString());
                return;
            }
            
            string sqlStr = "SELECT ID,NAME,SERVERNAME,DBNAME,PORT,CATEGORY FROM dblist where CATEGORY = @CATEGORY";
            cmd = new MySqlCommand(sqlStr, cnn);
            cmd.Parameters.Add(new MySqlParameter("@CATEGORY", MySqlDbType.VarChar, 15, "@CATEGORY"));
            da = new MySqlDataAdapter(cmd);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            da.UpdateCommand = cb.GetUpdateCommand();
            da.DeleteCommand = cb.GetDeleteCommand();
            txtCompanyName.DataBindings.Clear();
            txtCompanyName.DataBindings.Add("Text", dt, "NAME");
            txtServerName.DataBindings.Clear();
            txtServerName.DataBindings.Add("Text", dt, "SERVERNAME");
            txtCategory.DataBindings.Clear();
            txtCategory.DataBindings.Add("Text", dt, "CATEGORY");
            txtDBName.DataBindings.Clear();
            txtDBName.DataBindings.Add("Text", dt, "DBNAME");
            txtPort.DataBindings.Clear();
            txtPort.DataBindings.Add("Text", dt, "PORT");
           retrieveData();
            lstname.DataSource = dt;
            lstname.DisplayMember = "NAME";
            lstname.ValueMember = "ID";
            
        }
        
        private void retrieveData()
        {
            cmd.Parameters[0].Value = cmbEdition.Text;
            dt.Clear();
            da.Fill(dt);
        }

        private void cmbEdition_SelectedIndexChanged(object sender, EventArgs e)
        {
            retrieveData();
        }
        
        private void update()
        {
            this.BindingContext[dt].EndCurrentEdit();
            DataTable dtc = dt.GetChanges();
            da.Update(dtc);
            MessageBox.Show("updated");
        }
        private void delete()
        {
            DataRowView rowView = lstname.SelectedItem as DataRowView;

            if (null == rowView)
            {
                return;
            }
            rowView.Row.Delete();
            DataTable dtc2 = dt.GetChanges(DataRowState.Deleted);
            foreach (DataRow drcurrent in dtc2.Rows)
            {
                drcurrent.Delete();
            }
            da.Update(dtc2);
            MessageBox.Show("Deleted");
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtCompanyName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btndelete_Click(object sender, EventArgs e)
        {                                        
            delete();
        }
    }
}
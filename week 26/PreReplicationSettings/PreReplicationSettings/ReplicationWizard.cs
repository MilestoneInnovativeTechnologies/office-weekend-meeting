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
using Settings_Replication;
using LogFileCreation;

namespace PreReplicationSettings
{
    public partial class ReplicationWizard : Form
    {
        public ReplicationWizard()
        {
            InitializeComponent();  
            btnLogCreation.Enabled = false;
            btnMaster.Enabled = false;       
        }

        private void btnMaster_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Settings_Replication.txtDatabase SetRep = new Settings_Replication.txtDatabase();
            txtDatabase SetRep = new txtDatabase();
            SetRep.ShowDialog();
            this.Show();
            btnLogCreation.Enabled = true;
            btnMaster.Enabled = false;
        }

        private void btnLogCreation_Click(object sender, EventArgs e)
        {
            this.Hide();
            //LogFileCreation.LogCreation LogForm = new LogFileCreation.LogCreation();
            LogCreation LogForm = new LogCreation(this.rbHeadoffice.Checked);
            LogForm.ShowDialog();
            this.Show();
            btnLogCreation.Enabled = false;
        }

        private void rbHeadoffice_CheckedChanged(object sender, EventArgs e)
        {
            btnMaster.Enabled = true;
            btnLogCreation.Enabled = false;
        }

        private void rbBranch_CheckedChanged(object sender, EventArgs e)
        {
            btnMaster.Enabled = false;
            btnLogCreation.Enabled = true;
        }

        /*public string selection()
        {                 
            if (rbHeadoffice.Checked)
            {
                MessageBox.Show("fghfgjf");
                return "ePlus Professional Edition";
            }
            return "";
        }*/

        private void ReplicationWizard_Load(object sender, EventArgs e)
        {
            //ToolTip toolTip = new ToolTip();
            //toolTip.ShowAlways = true;           
        }
    }
}

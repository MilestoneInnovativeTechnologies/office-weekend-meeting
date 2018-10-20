using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ePlusReplication
{
    
    public partial class Form1 : Form
    {
        public static long Fetchvalue, Applyvalue, Verifyvalue;
        clsXMLData clsxml = new clsXMLData();
        public Boolean mainDBConnection;
        protected DatabaseInfo MainDBInfo, LogDBInfo;
        protected DatabaseInfo[] slaveDatabaseInfo;
        protected MySqlConnection MainDBConn, slaveDatabase, LogDBConn;
        protected string dbCode,errorMessage;
        protected xmlSettings settings;
        protected eMailSettings testMail;    
        protected System.Collections.Queue replicationQueue;
        protected long count;
        public static string str;
        delegate void StringParameterDelegate(string Text);
        delegate void StringClearParameterDelegate();
        delegate void SplashShowCloseDelegate();
         bool CloseSplashScreenFlag = false;
        DataTable dt = new DataTable();





        public Form1()
        {
            InitializeComponent();
        }       
        public void ShowScreen()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SplashShowCloseDelegate(ShowScreen));
                return;
            }
            this.Show();
            Application.Run(this);
        }
        /// Closes the SplashScreen
        /// </summary>
        public void CloseScreen()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SplashShowCloseDelegate(CloseScreen));
                return;
            }
            CloseSplashScreenFlag = true;
            this.Close();
        }
        /// Update text in default green color of success message
        /// </summary>
        /// <param name="Text">Message</param>
        public void UdpateStatusText(string Text)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new StringParameterDelegate(UdpateStatusText), new object[] { Text });
                return;
            }
            // Must be on the UI thread if we've got this far
            //label1.ForeColor = Color.Green;

            txtStatus.Text += Text;
            str = txtStatus.Text;
           
        }

        public void ClearStatusText()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new StringClearParameterDelegate(ClearStatusText));
                return;
            }

            txtStatus.Text = "";
        }


        /// <summary>
        /// Prevents the closing of form other than by calling the CloseSplashScreen function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplashForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseSplashScreenFlag == false)
                e.Cancel = true;
        }
       
    

          public void UpdateGrid(long value)
          {
            
            if (InvokeRequired)
            {
                DataTable dt = new DataTable();
                //LoadSerial();
                //dt.Columns.Add("SNo", typeof(string));
                dt.Columns.Add("Branch", typeof(string));
                dt.Columns.Add("LastReplication", typeof(DateTime));
                dt.Columns.Add("FetchedQuery", typeof(long));
                dt.Columns.Add("VerifiedQuery", typeof(long));
                dt.Columns.Add("AppliedQuery", typeof(long));
                //dt.Columns.Add("NextReplication", typeof(DateTime));
                dt.Columns.Add("Status", typeof(string));

                dt.Rows.Add("BR1", frmMain.lstTime, clsReplicate.Fetchvalue, clsReplicate.Verifyvalue, clsReplicate.Applyvalue);
                if (clsReplicate.Fetchvalue == clsReplicate.Applyvalue)
                {
                    dt.Rows[0]["Status"] = "Success";
                }
                else
                {
                    dt.Rows[0]["Status"] = "Pending";
                }
                dgview.DataSource = dt;

                return;
            }
           
        }
        private void LoadSerial()
        {
            int i = 1;
            foreach (DataGridViewRow row in dgview.Rows)
            {
               row.Cells["SNo"].Value = i; i++;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           // updateGridView();
           
        }
        private void dgview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             txtDetails.Enabled = true;
           
            txtDetails.Text = "Next Replication:"+frmMain.nxtTime+ "\r\n"+"ServerName :" + clsXMLData.branchName;

        }

      
        private void dgview_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {

           
            Form1 frm = new Form1();
            clsxml.ReadSettings();        
             
              e.Row.Cells["Branch"].Value = "BR1";
              e.Row.Cells["LastReplication"].Value =DateTime.Now ;
              e.Row.Cells["FetchedQuery"].Value = Fetchvalue;
              e.Row.Cells["VerifiedQuery"].Value = Verifyvalue;
              e.Row.Cells["AppliedQuery"].Value = Applyvalue;
              e.Row.Cells["NextReplication"].Value = "__/__/__  __:__:__";  
              e.Row.Cells["Status"].Value = "";
        }             

        private void btnAdd_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            //LoadSerial();
            //dt.Columns.Add("SNo", typeof(string));
            dt.Columns.Add("Branch", typeof(string));
            dt.Columns.Add("LastReplication", typeof(DateTime));
            dt.Columns.Add("FetchedQuery", typeof(long));
            dt.Columns.Add("VerifiedQuery", typeof(long));
            dt.Columns.Add("AppliedQuery", typeof(long));
            dt.Columns.Add("NextReplication", typeof(DateTime));
            dt.Columns.Add("Status", typeof(string));

            dt.Rows.Add("BR1", frmMain.lstTime, clsReplicate.Fetchvalue, clsReplicate.Verifyvalue, clsReplicate.Applyvalue, frmMain.nxtTime);
            if (clsReplicate.Fetchvalue == clsReplicate.Applyvalue)
            {
                dt.Rows[0]["Status"] = "Success";
            }
            else
            {
                dt.Rows[0]["Status"] = "Pending";
            }
            // dgview.Rows[0].Cells["NextReplication"].Value = frmMain.nxtTime;
            dgview.DataSource = dt;



        }

        public void updateGridView()

        {
          
            DataTable dt = new DataTable();
            //LoadSerial();
            //dt.Columns.Add("SNo", typeof(string));
            dt.Columns.Add("Branch", typeof(string));
            dt.Columns.Add("LastReplication", typeof(DateTime));
            dt.Columns.Add("FetchedQuery", typeof(long));
            dt.Columns.Add("VerifiedQuery", typeof(long));
            dt.Columns.Add("AppliedQuery", typeof(long));
            dt.Columns.Add("NextReplication", typeof(DateTime));
            dt.Columns.Add("Status", typeof(string));

            dt.Rows.Add("BR1", frmMain.lstTime, clsReplicate.Fetchvalue, clsReplicate.Verifyvalue, clsReplicate.Applyvalue, frmMain.nxtTime);
            if (clsReplicate.Fetchvalue == clsReplicate.Applyvalue)
            {
                dt.Rows[0]["Status"] = "Success";
            }
            else
            {
                dt.Rows[0]["Status"] = "Pending";
            }
           // dgview.Rows[0].Cells["NextReplication"].Value = frmMain.nxtTime;
            dgview.DataSource = dt;
          

        }
    }
}

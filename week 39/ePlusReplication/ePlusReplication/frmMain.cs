using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace ePlusReplication
{
    public partial class frmMain : Form
    {
        Form1 frm = new Form1();
        public static string lstTime;
        public static string nxtTime;
        int connectionAttempt = 3;
        int idleTimebetweenAttempt = 1;
      
        clsReplicate Replication;
        Boolean startReplication = false;
        clsXMLData xmlData;
        xmlSettings settings;
        public frmMain()
        {
            Thread splashthread = new Thread(new ThreadStart(clsStatus.ShowScreen));
            splashthread.IsBackground = true;
            splashthread.Start();
            InitializeComponent();

            xmlData = new clsXMLData();
            xmlData.connect();
            settings = xmlData.ReadSettings();
           
            clsStatus.UdpateStatusText(DateTime.Now.ToString());
            clsStatus.UdpateStatusText("\r\nTimer : " + settings.timer + " Minute(s)\r\n");
            Relicate();
            try
            {
                System.Timers.Timer replicationTimer = new System.Timers.Timer();
                replicationTimer.Interval = Convert.ToInt32(settings.timer) * 60 * 1000;
                replicationTimer.Elapsed += new System.Timers.ElapsedEventHandler(MyTimer_Elapsed);
                replicationTimer.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
           
        private void Relicate()
        {
            DateTime dt;
            dt = DateTime.Now;
            if (startReplication)
                return;
            Replication = new clsReplicate(settings);
            if (!Replication.mainDBConnection)
            {
                return;
            }
            startReplication = true;
            int i = 0;
            Boolean result;
            do
            {
                result = Replication.Replicate();
                if (result)
                    break;
                System.Threading.Thread.Sleep(idleTimebetweenAttempt * 1000);
                i++;
            } while (i < connectionAttempt);

            System.Threading.Thread.Sleep(1000);
            Replication.verifySQL();
            System.Threading.Thread.Sleep(1000);
            Replication.applySQL();
            System.Threading.Thread.Sleep(10000);
            clsStatus.ClearStatusText();
            lstTime = dt.ToLocalTime().ToString();
            clsStatus.UdpateStatusText("Last Replication : " + lstTime);
             nxtTime = dt.AddMinutes(Convert.ToDouble(settings.timer)).ToLocalTime().ToString();
            clsStatus.UdpateStatusText("\r\nNext Replication : " +nxtTime + "\r\n");
            frm.updateGridView();
            startReplication = false;
           
        }
       
        private void MyTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
          Relicate();
        }   
   }

}


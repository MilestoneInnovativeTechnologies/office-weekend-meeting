using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace Databasechanging
{
    public struct update1Settings
    {
        public string userName;
        public string serverName;
        public string dbName;
        public string dbPort;
        public string multidb;
        public bool success;
        public string errorMessage;

    }
   /* public class update
    {
        //public DataSet ds = new DataSet();
        private string update1Path, update1SettingsFile;
        public String text11 = Form1.text1;
        public String text22 = Form1.text2;
        public String text33 = Form1.text3;
        public String text44 = Form1.text4;
        public update()
        {
            // xmlPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;
            update1Path = @"C:\Users\Amritha\AppData\Roaming\ePlus Professional Edition";
            update1SettingsFile = update1Path + "\\Settings.xml";
        }
        public update1Settings updateSettings()
        {
            update1Settings settings2 = new update1Settings();
           if (!File.Exists(update1SettingsFile))
            {
                settings2.success = false;
                settings2.errorMessage = "File not found (" + update1SettingsFile + ")";
                return settings2;
            }
            string xmlElement = "";
            XmlTextWriter updated = new XmlTextWriter(update1SettingsFile);
            while (updated.Up)
            {
                switch (updated.NodeType)
                {
                    case XmlNodeType.Element:
                        xmlElement = updated.WriteName;
                        //MessageBox.Show(objXmlTextReader.Name);
                        break;
                    case XmlNodeType.Text:
                        switch (xmlElement)
                        {
                            case "USER":
                                MessageBox.Show(" " + text11);
                                settings2.userName = updated.Value;
                               Form1 text11 = new Form1();
                                xmlSettings settings = new xmlSettings();
                                settings = settings1.ReadSettings();*/
                                /*if (settings2.userName == text11)
                                {
                                    MessageBox.Show(" " + settings2.userName);
                                }
                                else
                                {
                                    settings2.userName = text11;
                                    MessageBox.Show("" + settings2.userName);
                                }
                                break;
                            case "SERVER":
                                settings2.serverName = updated.Value;
                                break;
                            case "DATABASE":
                                settings2.dbName = updated.Value;
                                break;
                            case "PORT":
                                settings2.dbPort = updated.Value;
                                break;
                            case "MULTIDATABASE":
                                settings2.multidb = updated.Value;
                                break;
                        }
                        break;
                }
            }
            updated.Close();
            settings2.success = true;
            settings2.errorMessage = "";
            return settings2;
        }

        public string setCompanyCode(DataRow currentRow)
        {
            return currentRow["COCODE"].ToString();
        }
        public string getServerName(DataRow currentRow)
        {
            return currentRow["SERVERNAME"].ToString();
        }
        public string getDBName(DataRow currentRow)
        {
            return currentRow["DBNAME"].ToString();
        }
        public string getDBPort(DataRow currentRow)
        {
            return currentRow["PORT"].ToString();
        }

        public string getMultidatabase(DataRow currentRow)
        {
            return currentRow["MULTIDB"].ToString();
        }
    }*/
}






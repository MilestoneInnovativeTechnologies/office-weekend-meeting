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
    public struct xmlSettings
    {
        public string userName;
        public string serverName;
        public string dbName;
        public string dbPort;
        public string multidb;
        public string xmllPath;
        public bool success;
        public string errorMessage;
        public Boolean mode;
    }
    public struct pathset
    {
        public string xmlPath;
    }
    public class clsXMLData
    {
        public string xPath, xmlSettingsFile;

        public clsXMLData()
        {

        }
        public pathset readPath(string Path)
        {
            //MessageBox.Show(Path);
            xPath = Path;
            xmlSettingsFile = xPath + "\\Settings.xml";
            pathset x = new pathset();
            return x;

        }
        public xmlSettings ReadSettings()
        {
            xmlSettings settings = new xmlSettings();
            if (!File.Exists(xmlSettingsFile))
            {
                settings.success = false;
                settings.errorMessage = "File not found (" + xmlSettingsFile + ")";
                return settings;
            }
            string xmlElement = " ";
            XmlTextReader reader = new XmlTextReader(xmlSettingsFile);
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        xmlElement = reader.Name;

                        break;
                    case XmlNodeType.Text:
                        switch (xmlElement)
                        {
                            case "USER":
                                settings.userName = reader.Value;
                                break;
                            case "SERVER":
                                settings.serverName = reader.Value;
                                break;
                            case "DATABASE":
                                settings.dbName = reader.Value;
                                break;
                            case "PORT":
                                settings.dbPort = reader.Value;
                                break;
                            case "MULTIDATABASE":
                                settings.multidb = reader.Value;
                                break;
                        }
                        break;
                }
            }

            reader.Close();
            settings.success = true;
            settings.errorMessage = "";
            return settings;
        }
   
        public xmlSettings writesettings(string usr,string svr,string dbs,string prt,string pat)
        {
            string xmlpath1 = pat;
            string xsettingsfile = xmlpath1 + "\\Settings.xml";
            xmlSettings xsettings = new xmlSettings();
            if (!File.Exists(xsettingsfile))
            {  
                xsettings.success = false;
                xsettings.errorMessage = "File not found (" + xsettingsfile + ")";
                return xsettings;
            }
            else
            { 
            XmlTextWriter writer = new XmlTextWriter(xsettingsfile, System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            

            writer.WriteStartElement("Settings");
            

            writer.WriteStartElement("USER");
                xsettings.userName = usr;
            writer.WriteString(xsettings.userName);
            writer.WriteEndElement();

            writer.WriteStartElement("SERVER");
                xsettings.serverName = svr;
            writer.WriteString(xsettings.serverName);
            writer.WriteEndElement();

            writer.WriteStartElement("DATABASE");
                xsettings.dbName = dbs;
            writer.WriteString(xsettings.dbName);
            writer.WriteEndElement();

            writer.WriteStartElement("PORT");
                xsettings.dbPort = prt;
            writer.WriteString(xsettings.dbPort);
            writer.WriteEndElement();

            writer.WriteStartElement("MULTIDATABASE");
            writer.WriteString("False");
            writer.WriteEndElement();

           
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            
                }
            xsettings.success = true;
            xsettings.errorMessage = "";
            return xsettings;
        }
    }
    }

    




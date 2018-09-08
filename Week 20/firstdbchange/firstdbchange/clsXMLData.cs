using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;

namespace firstdbchange
{
    public struct xmlSettings
    {
        public string userName;
        public string serverName;
        public string dbName;
        public string dbPort;
        public string multidb;
        public bool success;
        public string errorMessage;
        public Boolean mode;
    }
    public class clsXMLData
    {
        private string xmlPath, xmlSettingsFile;

        public  clsXMLData()
        {
            xmlPath = @"C:\Users\Amritha\AppData\Roaming\ePlus Standard Edition";
            xmlSettingsFile = xmlPath + "\\Settings.xml";
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
        public xmlSettings writesettings(string usr, string svr, string dbs, string prt)
        {

            xmlSettings set1 = new xmlSettings();

            if (!File.Exists(xmlSettingsFile))
            {
                set1.success = false;
                set1.errorMessage = "File not found (" + xmlSettingsFile + ")";
                return set1;
            }
            else
            {
                XmlTextWriter writer = new XmlTextWriter(xmlSettingsFile, System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
              //  writer.WriteStartElement("?xml version");

                writer.WriteStartElement("Settings");


                writer.WriteStartElement("USER");
                set1.userName = usr;
                writer.WriteString(set1.userName);
                writer.WriteEndElement();

                writer.WriteStartElement("SERVER");
                set1.serverName = svr;
                writer.WriteString(set1.serverName);
                writer.WriteEndElement();

                writer.WriteStartElement("DATABASE");
                set1.dbName = dbs;
                writer.WriteString(set1.dbName);
                writer.WriteEndElement();

                writer.WriteStartElement("PORT");
                set1.dbPort = prt;
                writer.WriteString(set1.dbPort);
                writer.WriteEndElement();

                writer.WriteStartElement("MULTIDATABASE");
                writer.WriteString("False");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
                return set1;
            }
        }
    }
}

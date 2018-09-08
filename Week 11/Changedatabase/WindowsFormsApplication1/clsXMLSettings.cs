using System;
using System.Data;
using System.Xml;
using System.IO;


namespace ePlusConfigWizard
{
    struct xmlAppSettings
    {
        public string softwareCategory;
        public string dbUser;
        public string dbPwd;
        public string user;
        public string pwd;
    }
    struct xmlSettings
    {
        public string serverName;
        public string dbName;
        public string dbPort;
        public string userName;
        public Boolean mode;
    }
    class clsXMLSettings
    {
        public DataSet ds = new DataSet();
        private string xmlPath, xmlDBListFile, xmlSettingsFile;

        public clsXMLSettings()
        {
            xmlPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + clsGlobal.SoftwareName;
            xmlDBListFile = xmlPath + "\\DBList.xml";
            xmlSettingsFile = xmlPath + "\\Settings.xml";
        }

        public xmlAppSettings ReadAppSettings()
        {
            xmlAppSettings appSettings;
            appSettings.softwareCategory="";
            appSettings.dbUser = "";
            appSettings.dbPwd = "";
            appSettings.user = "";
            appSettings.pwd = "";

            if (!File.Exists(clsGlobal.xmlAppSettingsFile))
            {
                return appSettings;
            }

            string xmlElement = "";
            XmlTextReader reader = new XmlTextReader(clsGlobal.xmlAppSettingsFile);
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
                            case "CATEGORY":
                                appSettings.softwareCategory = reader.Value;
                                break;
                            case "DBUSER":
                                appSettings.dbUser = reader.Value;
                                break;
                            case "DBPWD":
                                appSettings.dbPwd = reader.Value;
                                break;
                            case "USER":
                                appSettings.user = reader.Value;
                                break;
                            case "PWD":
                                appSettings.pwd = reader.Value;
                                break;

                        }
                        break;
                }
            }
            reader.Close();
            return appSettings;
        }
        public Boolean CreateSettingsXMLFile(xmlSettings settings,ref string ErrorMessage)
        {
            try
            {
                bool isExists = System.IO.Directory.Exists(xmlPath);

                if (!isExists)
                    System.IO.Directory.CreateDirectory(xmlPath);

                if (System.IO.File.Exists(xmlSettingsFile))
                {
                    FileAttributes xmlAttribute = System.IO.File.GetAttributes(xmlSettingsFile);
                    if ((xmlAttribute & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        ErrorMessage = "Unable to modify settings file!" + Environment.NewLine + "File Name : " + xmlSettingsFile + Environment.NewLine + "File Attribute: ReadOnly";
                        return false;
                    }
                }

                XmlTextWriter writer = new XmlTextWriter(xmlSettingsFile, System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement("Settings");

                writer.WriteStartElement("USER");
                writer.WriteString(settings.userName);
                writer.WriteEndElement();

                writer.WriteStartElement("SERVER");
                writer.WriteString(settings.serverName);
                writer.WriteEndElement();

                writer.WriteStartElement("DATABASE");
                writer.WriteString(settings.dbName);
                writer.WriteEndElement();

                writer.WriteStartElement("PORT");
                writer.WriteString(settings.dbPort);
                writer.WriteEndElement();

                writer.WriteStartElement("MODE");
                if (settings.mode)
                    writer.WriteString("True");
                else
                    writer.WriteString("False");
                writer.WriteEndElement();

                writer.WriteStartElement("MULTIDATABASE");
                writer.WriteString("False");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public xmlSettings ReadSettings()
        {
            xmlSettings settings;
            settings.serverName = "";
            settings.dbName = "";
            settings.dbPort = "";
            settings.userName = "";
            settings.mode = false;
            if(!Directory.Exists(xmlPath))
            {
                Directory.CreateDirectory(xmlPath);
                return settings;
            }
            if (!File.Exists(xmlSettingsFile))
                return settings;
            
            string xmlElement = "";
            XmlTextReader reader = new XmlTextReader(xmlSettingsFile);
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        xmlElement = reader.Name;
                        //MessageBox.Show(objXmlTextReader.Name);
                        break;
                    case XmlNodeType.Text:
                        switch (xmlElement)
                        {
                            case "SERVER":
                                settings.serverName = reader.Value;
                                break;
                            case "DATABASE":
                                settings.dbName = reader.Value;
                                break;
                            case "PORT":
                                settings.dbPort = reader.Value;
                                break;
                            case "USER":
                                settings.userName = reader.Value;
                                break;
                            case "MODE":
                                if(reader.Value=="True")
                                    settings.mode = true;
                                else
                                    settings.mode = false;
                                break;
                        }
                        break;
                }
            }
            reader.Close();
            return settings;
        }


    }
}



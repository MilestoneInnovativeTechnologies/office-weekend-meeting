/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databasechanging
{
    public class clsXMLWriter
    {
    }
}*/
/*public xmlSettings WriteSettings()
{
    xmlSettings settings = new xmlSettings();
    if (!File.Exists(xmlSettingsFile))
    {
        settings.success = false;
        settings.errorMessage = "File not found (" + xmlSettingsFile + ")";
        return settings;
    }
    string xmlElement = "";
    XmlTextWriter writer = new XmlTextWriter(xmlSettingsFile, System.Text.Encoding.UTF8);
    //XmlText reader = new XmlTextReader(xmlSettingsFile);
    while (writer.Writestartdocument())
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
}*/

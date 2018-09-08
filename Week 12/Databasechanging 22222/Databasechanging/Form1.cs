using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Windows.Forms;


namespace Databasechanging
{
    
    public partial class Form1 : Form
    {
        public string var;
        int flag;
       public Form1()
        {
            InitializeComponent();
            
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var = comboBox1.Text;
            
        }
        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            clsXMLData settings1 = new clsXMLData();
            xmlSettings settings = new xmlSettings();
            pathset set3 = new pathset();
            flag = 1;
      
            if (var == "ePlus Basic Edition")
            {
                set3.xmlPath = @"C:\ProgramData\ePlus Basic Edition";
                set3 = settings1.readPath(set3.xmlPath);
            }
            if (var == "ePlus Proffessional Edition")
            {
                set3.xmlPath = @"C:\ProgramData\ePlus Professional Edition";
                set3 = settings1.readPath(set3.xmlPath);            }
            if (var == "ePlus Standard Edition")
            {
                set3.xmlPath = @"C:\ProgramData\ePlus Standard Edition";
                set3 = settings1.readPath(set3.xmlPath);
            }
            settings = settings1.ReadSettings();
            textBox1.Text = settings.userName;
            textBox2.Text = settings.serverName;
            textBox3.Text = settings.dbName;
            textBox4.Text = settings.dbPort;
        }
        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            clsXMLData settings1 = new clsXMLData();
            xmlSettings settings = new xmlSettings();
            pathset set3 = new pathset();
            flag = 0;

            if (var == "ePlus Basic Edition")
            {
                set3.xmlPath = @"C:\Users\Amritha\AppData\Roaming\ePlus Basic Edition";
                set3 = settings1.readPath(set3.xmlPath);
            }
            if (var == "ePlus Proffessional Edition")
            {
                set3.xmlPath = @"C:\Users\Amritha\AppData\Roaming\ePlus Professional Edition";
                set3 = settings1.readPath(set3.xmlPath);
            }
            if (var == "ePlus Standard Edition")
            {
                set3.xmlPath = @"C:\Users\Amritha\AppData\Roaming\ePlus Standard Edition";
                set3 = settings1.readPath(set3.xmlPath);
            }
            settings = settings1.ReadSettings();
            textBox1.Text = settings.userName;
            textBox2.Text = settings.serverName;
            textBox3.Text = settings.dbName;
            textBox4.Text = settings.dbPort;
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void label2_Click(object sender, EventArgs e)
        {
        }
        private void label3_Click(object sender, EventArgs e)
        {
        }
        private void label4_Click(object sender, EventArgs e)
        {
        }
        private void label5_Click(object sender, EventArgs e)
        {
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        { 
        }
        private void Update_Button_Click(object sender, EventArgs e)
        {
            clsXMLData xdata = new clsXMLData();
            xmlSettings xsettings = new xmlSettings();
            xsettings.userName = textBox1.Text;
            xsettings.serverName = textBox2.Text;
            xsettings.dbName = textBox3.Text;
            xsettings.dbPort = textBox4.Text;
            if(flag == 1)
            {
                if (var == "ePlus Basic Edition")
                    xsettings.xmllPath = @"C:\ProgramData\ePlus Basic Edition";
                else if (var == "ePlus Proffessional Edition")
                    xsettings.xmllPath = @"C:\ProgramData\ePlus Professional Edition";
                else
                    xsettings.xmllPath = @"C:\ProgramData\ePlus Standard Edition";
            }
            if (flag == 0)
            {
                if (var == "ePlus Basic Edition")
                    xsettings.xmllPath = @"C:\Users\Amritha\AppData\Roaming\ePlus Basic Edition";
                else if (var == "ePlus Proffessional Edition")
                    xsettings.xmllPath = @"C:\Users\Amritha\AppData\Roaming\ePlus Professional Edition";
                else
                    xsettings.xmllPath = @"C:\Users\Amritha\AppData\Roaming\ePlus Standard Edition";
            }
            xsettings = xdata.writesettings(xsettings.userName, xsettings.serverName, xsettings.dbName, xsettings.dbPort,xsettings.xmllPath);
        }
        private void Close_button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
        }
        private void textBox4_Enter(object sender, EventArgs e)
        {
        }
        private void label6_Click(object sender, EventArgs e)
        {
        }
    }
}
  


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace firstdbchange
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }
        private void MainWindow_Loaded(object sender, EventArgs e)
        {
            clsXMLData settings1 = new clsXMLData();
            xmlSettings settings = new xmlSettings();
            settings = settings1.ReadSettings();
            textBox.Text = settings.userName;
            textBox1.Text = settings.serverName;
            textBox2.Text = settings.dbName;
            textBox3.Text = settings.dbPort;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            clsXMLData xdata = new clsXMLData();
            xmlSettings xsettings = new xmlSettings();
            xsettings.userName = textBox.Text;
            xsettings.serverName = textBox1.Text;
            xsettings.dbName = textBox2.Text;
            xsettings.dbPort = textBox3.Text;
            xsettings = xdata.writesettings(xsettings.userName, xsettings.serverName, xsettings.dbName, xsettings.dbPort);
            MessageBox.Show("Updated");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}

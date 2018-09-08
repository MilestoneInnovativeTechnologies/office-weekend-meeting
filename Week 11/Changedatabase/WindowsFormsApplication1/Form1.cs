using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = "User";
            label2.Text = "Server";
            label3.Text = "Current db";
            label4.Text = "New DB";
            comboBox1.Items.Add("eplus031_data");
            comboBox1.Items.Add("eplus032_data");
            comboBox1.Items.Add("eplus001_data");
            comboBox2.Items.Add("eplus031_data");
            comboBox2.Items.Add("eplus032_data");
            comboBox2.Items.Add("eplus001_data");
            button1.Text = "Update";
            button2.Text = "Cancel";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "User";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.Text = "Server";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            label3.Text = "Current db";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Add("eplus031_data");
            comboBox1.Items.Add("eplus032_data");
            comboBox1.Items.Add("eplus001_data");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label4.Text = "New DB";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Add("eplus031_data");
            comboBox2.Items.Add("eplus032_data");
            comboBox2.Items.Add("eplus001_data");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Update";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Text = "Cancel";
        }
    }
}

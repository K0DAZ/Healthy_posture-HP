using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            foreach (string str in SerialPort.GetPortNames())
            {
                MessageBox.Show(str);
                comboBox1.Items.Add(str);
            }
            //int lengthnumberCOM = str.Length;
            //while (comboBox1.Items.Count != lengthnumberCOM) comboBox1.Items.Add(SerialPort.GetPortNames());
            
        }

        public string PortName;
        public int BaudRate = 9600;


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.Close();
            PortName = comboBox1.SelectedItem.ToString();
            serialPort1.PortName = PortName;
            serialPort1.BaudRate = BaudRate;
            serialPort1.Open();
            if (serialPort1.IsOpen)
            {
                MessageBox.Show("открыт");
            }
        }
    }
}

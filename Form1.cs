using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SerialPortCommunicationTest.Data_Reading;

namespace SerialPortCommunicationTest
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var serialCommunication = new SerialCommunication();
            serialCommunication.DataReceived += ProcessData;
        }

        private void ProcessData(byte[] data)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<byte[]>(ProcessData), new object[] { data });
                return;
            }

            var processedData = Encoding.Default.GetString(data);

            lblDataReading.Text = processedData;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
        }

        
    }
}

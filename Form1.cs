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
        private readonly SerialCommunication _serialCommunication;
        private string _dataIn = "";

        public Form1()
        {
            InitializeComponent();

            _serialCommunication = new SerialCommunication();
            _serialCommunication.InitializeConnection();

            _serialCommunication.DataReceived += SerialCommunication_DataReceived;

        }


        private void SerialCommunication_DataReceived(byte[] data)
        {
            // get string from byte array 
            var dataReadingStringIn = Encoding.Default.GetString(data);
            Console.WriteLine(dataReadingStringIn);
            
            DisplayDataReceived(dataReadingStringIn);
        }

        private void DisplayDataReceived(string dataReceived)
        {
            _dataIn += dataReceived;
            lblDataReading.Text = _dataIn;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _serialCommunication.SerialPortOpening();
        }
    }
}

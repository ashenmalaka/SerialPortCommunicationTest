using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPortCommunicationTest.Data_Reading
{
    public class SerialCommunication
    {
        private readonly SerialPort _serialPort;
        //public string DataReading { get; set; }

        public event Action<byte[]> DataReceived;

        public SerialCommunication()
        {
            _serialPort = new SerialPort();
        }

        public void InitializeConnection()
        {
            _serialPort.PortName = SerialPortProperties.PortName;
            _serialPort.BaudRate = Convert.ToInt32(SerialPortProperties.BaudRate);
            _serialPort.DataBits = Convert.ToInt32(SerialPortProperties.DataBits);
            _serialPort.StopBits = (StopBits)Enum.Parse((typeof(StopBits)), SerialPortProperties.StopBits);
            _serialPort.Parity = (Parity)Enum.Parse((typeof(Parity)), SerialPortProperties.Parity);

            _serialPort.RtsEnable = true;

            //_serialPort.DataReceived += DataReceivedHandler;

        }

        public void ComPortBufferRead(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] inputData = new byte[_serialPort.BytesToRead];
            _serialPort.Read(inputData, 0, _serialPort.BytesToRead);

            DataReceived?.Invoke(inputData);
        }

        /*private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            DataReading = _serialPort.ReadExisting();
            //Console.WriteLine(DataReading);
        }*/

        public void SerialPortOpening()
        {
            try
            {
                _serialPort.Open();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SerialPortClosing()
        {
            _serialPort.Close();
        }

    }
}

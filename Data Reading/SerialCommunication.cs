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
        private readonly SerialPort mySerialPort;

        public Action<byte[]> DataReceived;

        //Created the actual serial port in the constructor here, 
        //as it makes more sense than having the caller need to do it.
        //you'll also need access to it in the event handler to read the data
        public SerialCommunication()
        {
            mySerialPort = new SerialPort
            {
                PortName = SerialPortProperties.PortName,
                BaudRate = Convert.ToInt32(SerialPortProperties.BaudRate),
                DataBits = Convert.ToInt32(SerialPortProperties.DataBits),
                StopBits = (StopBits)Enum.Parse((typeof(StopBits)), SerialPortProperties.StopBits),
                Parity = (Parity)Enum.Parse((typeof(Parity)), SerialPortProperties.Parity),

                RtsEnable = true
            };

            mySerialPort.DataReceived += mySerialPort_DataReceived;

            try
            {
                mySerialPort.Open();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void mySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //no. of data at the port
                var byteToRead = mySerialPort.BytesToRead;

                //create array to store buffer data
                var inputData = new byte[byteToRead];

                //read the data and store
                mySerialPort.Read(inputData, 0, byteToRead);

                var copy = DataReceived;
                copy?.Invoke(inputData);

            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, @"Data Received Event");
            }
        }


        public void SerialPortClosing()
        {
            mySerialPort.Close();
        }

    }
}

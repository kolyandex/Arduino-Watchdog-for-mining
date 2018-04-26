using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Watchdog
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;
        private bool _deviceExist;
        public Form1()
        {
            InitializeComponent();
        }

        private void SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = (SerialPort)sender;
            var indata = sp.ReadExisting();
            if (indata.Trim() == "glad_for_you") radioButton2.Invoke(new MethodInvoker(() => { radioButton2.Checked = !radioButton2.Checked; }));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InfoLabel.Text = "Watchdog not found";
            var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);
            if (rk.GetValueNames().Contains(this.Text)) AutorunCheckBox.Checked = true;
            rk.Close();
            FindDevice();
        }

        void FindDevice()
        {
            try
            {
                var ports = SerialPort.GetPortNames();
                foreach (var port in ports)
                {
                    _serialPort?.Close();
                    if (CheckDevice(port))
                    {
                        InfoLabel.Text = "Watchdog found on " + _serialPort.PortName;
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                
            }

        }

        bool CheckDevice(string portname)
        {
            _serialPort = new SerialPort(portname, 9600);
            try
            {
                _serialPort.ReadTimeout = 2000;
                _serialPort.Open();
                _serialPort.WriteLine("who_are_you?");
                
                if (_serialPort.ReadLine().Trim() != "watchdog")
                {
                    _serialPort.Close();
                    return false;
                }

                _serialPort.DataReceived += SerialDataReceived;
                _deviceExist = true;
                return true;
            }
            catch (Exception e)
            {
               //MessageBox.Show(e.Message);
            }
            return false;
        }

        void DeviceDisconnected()
        {
            _deviceExist = false;
            _serialPort.Close();
            InfoLabel.Text = "Watchdog disconnected";
        }

        private void SerialTimer_Tick(object sender, EventArgs e)
        {
            if (_deviceExist && !_serialPort.IsOpen)
            {
                DeviceDisconnected();
            }

            if (!_deviceExist) FindDevice();

            if (_deviceExist && _serialPort.IsOpen)
            {
                _serialPort.WriteLine("im_fine");
            }
        }

        private void SetStartup()
        {
            var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (AutorunCheckBox.Checked) rk.SetValue(this.Text, Application.ExecutablePath);
            else rk.DeleteValue(this.Text, false);
            rk.Close();
        }

        private void AutorunCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetStartup();
        }
    }
}

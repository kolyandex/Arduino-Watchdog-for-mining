using System;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
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

        private void FindDevice()
        {
            InfoLabel.Text = "Looking for watchdog O_O";

            var ports = SerialPort.GetPortNames();
            foreach (var port in ports)
            {
                _serialPort?.Close();
                CheckDevice(port);
            }
        }

        private void CheckDevice(string portname)
        {
            _serialPort = new SerialPort(portname, 9600);
            try
            {
                if (_serialPort.IsOpen) return;
                _serialPort.ReadTimeout = 100;
                _serialPort.Open();
                _serialPort.WriteLine("who_are_you?");

                if (_serialPort.ReadLine().Trim() != "watchdog")
                {
                    _serialPort.Close();
                    _serialPort.Dispose();
                    return;
                }
                InfoLabel.Text = "Watchdog found on " + _serialPort.PortName;
                _serialPort.DataReceived += SerialDataReceived;
                _deviceExist = true;
            }
            catch (Exception e)
            {
                if (!_deviceExist) _serialPort.Close();
                Logger.Instance.Log(portname + " " + e.Message);
            }
        }


        private void DeviceDisconnected()
        {
            _serialPort.Close();
            _deviceExist = false;
            radioButton2.Checked = false;
            InfoLabel.Text = "Watchdog disconnected";
        }

        private void SerialTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_deviceExist)
                {
                    if (_serialPort.IsOpen) _serialPort.WriteLine("im_fine");
                    else DeviceDisconnected();
                }
                else FindDevice();
            }
            catch (Exception exception)
            {
                Logger.Instance.Log(exception.Message);
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

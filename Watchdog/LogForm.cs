using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watchdog
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }

        public void Log(string msg)
        {
            if (loggerRichTextBox.InvokeRequired)
                loggerRichTextBox.Invoke(new MethodInvoker(() => { loggerRichTextBox.Text += msg + "\n"; }));
            else
                loggerRichTextBox.Text += msg + "\n";
        }
    }
}

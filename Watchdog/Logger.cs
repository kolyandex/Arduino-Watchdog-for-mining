using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watchdog
{
    class Logger
    {
        private static Logger _instance;
        private LogForm _logForm;

        private Logger()
        {
            _logForm = new LogForm();
            //_logForm.Show();
        }
        public static Logger Instance => _instance ?? (_instance = new Logger());

        public void Log(string msg)
        {
            //_logForm.Log(msg);
        }
    }
}

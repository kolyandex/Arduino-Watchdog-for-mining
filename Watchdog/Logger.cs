using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchdog
{
    class Logger
    {
        private static Logger _instance;

        private Logger()
        {
        }
        public static Logger Instance => _instance ?? (_instance = new Logger());

        public void Log(string msg)
        {
            
        }
    }
}

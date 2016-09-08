using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace DAL.NLog
{
    public class LogForum : ILogForum
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public void Info(string info)
        {
           _logger.Info(info);
        }

        public void Error(string error)
        {
            _logger.Error(error);
        }

        public void Error(Exception ex, string errorInfo)
        {
          _logger.Error(ex,errorInfo);
        }
    }
}

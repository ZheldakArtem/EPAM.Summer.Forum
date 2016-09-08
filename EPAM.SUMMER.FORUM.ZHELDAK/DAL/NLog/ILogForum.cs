using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL.NLog
{
    public interface ILogForum
    {
        void Info(string info);
        void Error(string error);
        void Error(Exception ex, string infoErorr);
    }
}

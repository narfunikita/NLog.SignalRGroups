using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLog.SignalR
{
    public interface ILoggingHub
    {
        void log(LogEventInfo logEvent, string message);
    }
}
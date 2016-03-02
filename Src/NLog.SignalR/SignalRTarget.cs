using Microsoft.AspNet.SignalR;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.SignalR;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLog.SignalR
{
    [Target("SignalR")]
    public class SignalRTarget : TargetWithLayout
    {
        [RequiredParameter]
        public string GroupIdPrefix { get; set; }

        public string LoggingHubName { get; set; }

        public SignalRTarget()
        {
            LoggingHubName = "loggingHub";
        }

        protected override void Write(LogEventInfo logEvent)
        {
            Log(logEvent);
        }

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            Log(logEvent.LogEvent);
        }

        private void Log(LogEventInfo logEvent)
        {
            //var loggerName = logEvent.LoggerName;
            var renderedMessage = Layout.Render(logEvent);
            var context = GlobalHost.ConnectionManager.GetHubContext<ILoggingHub>(LoggingHubName).Clients;

            context.Group(GetGroupId(logEvent)).log(logEvent, renderedMessage);
        }

        private string GetGroupId(LogEventInfo logEvent)
        {
            return GroupIdPrefix + logEvent.Level.ToString().ToLower();
        }
    }
}
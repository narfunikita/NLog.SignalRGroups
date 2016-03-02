using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using NLog.Common;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLog.SignalR.Demo.Hubs
{
    [HubName("loggingHub")]
    public class LoggingHub : Hub<ILoggingHub>
    {
        public void Subscribe(string groupIdPrefix, string level)
        {
            Groups.Add(Context.ConnectionId, groupIdPrefix + level.ToLower());
        }
    }
}
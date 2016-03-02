NLog.SignalR
==========
NLog target for sending logs to SignalR groups

## Usage
#### 1. Add logging hub to your project inherited from Hub<ILoggingHub>

```csharp
[HubName("loggingHub")]
public class LoggingHub : Hub<ILoggingHub>
{
}
```

#### 2. Configure NLog to use `NLog.SignalR`

##### NLog.config

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
       throwExceptions="true">
  <extensions>
    <add assembly="NLog.SignalR" />
  </extensions>
  <targets  async="true">
	<target xsi:type="SignalR"
            name="signalrDemo"
            groupIdPrefix="demoGroup123"
            layout="${message}" />
  </targets>
  <rules>
    <logger name="*" minlevel="Debug" writeTo="signalrDemo" />
  </rules>
</nlog>
```

##### Configuration Options

Key        | Description
----------:| -----------
groupIdPrefix   | Prefix for group names. {groupIdPrefix}error, {groupIdPrefix}info, etc.
loggingHubName | Optional. Name of your logging hub. Default: loggingHub.


## Client (js)

You can crete page and add javascript code:
```javascript
$.connection.hub.start().done(function () {
                var groupId = "demoGroup123";
                var typeArray = ["error", "debug"]
                for (var index in typeArray) {
                    $.connection.loggingHub.server.subscribe(groupId, typeArray[index]);
                }
            }).fail(function () { console.log('Could not Connect!'); });

$.connection.loggingHub.client.log = function (logEvent, message) {
	console.log(logeEvent, message)
};

```

And add method subscribe to logging hub:
```csharp
[HubName("loggingHub")]
public class LoggingHub : Hub<ILoggingHub>
{
        public void Subscribe(string groupIdPrefix, string level)
        {
            Groups.Add(Context.ConnectionId, groupIdPrefix + level.ToLower());
        }
}
```




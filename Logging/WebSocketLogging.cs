using System.Collections.Generic;
using System.Collections.ObjectModel;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace hitaBot.WS.Logging
{
    public static class WebSocketLogging
    {
        public static void CreateLogConfig()
        {
            if (LogManager.Configuration != null) return;
            var config = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);

            consoleTarget.Layout = @"${date:format=hh\:mm\:ss tt}: ${message} ${newline}";

            var rule = new LoggingRule("*", LogLevel.Trace, consoleTarget);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;
        }
    }
}
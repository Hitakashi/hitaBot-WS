using System.Collections.Generic;
using System.Collections.ObjectModel;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;

namespace hitaBot.WS.Logging
{
    public static class WebSocketLogging
    {
        private static LoggingConfiguration _config;
        public static void CreateLogConfig()
        {
            // We're doing this so we know not to reconfigure NLog.
            if (_config == null)
                _config = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget();
            _config.AddTarget("console", consoleTarget);

            consoleTarget.Layout = @"${date:format=hh\:mm\:ss tt}: ${message} ${newline}";

            var rule = new LoggingRule("*", LogLevel.Trace, consoleTarget);
            _config.LoggingRules.Add(rule);

            LogManager.Configuration = _config;
        }
    }
}
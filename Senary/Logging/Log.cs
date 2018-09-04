using System;
using DarkRift;
using DarkRift.Server;
using DarkRift.Server.Plugins.LogWriters;

namespace Senary.Logging
{
    public static class Log
    {
        private static ConsoleWriter consoleWriter;

        private static WriteEventArgs writeEventArgs;

        private static void Write(string message, params object[] objects)
        {
            message = string.Format(message, objects);
            message = "\n" + message + "\n";
            
            writeEventArgs.Message = message;
            writeEventArgs.FormattedMessage = message;
            writeEventArgs.LogTime = DateTime.Now;
            
            consoleWriter.WriteEvent(writeEventArgs);
        }

        public static void SetPluginLoadData(PluginLoadData pluginLoadData)
        {
            writeEventArgs = new WriteEventArgs("Log", "", LogType.Info, new Exception("Error"), "", DateTime.Now);
            
            consoleWriter = new ConsoleWriter(pluginLoadData);
        }

        public static void WriteLog(string message, params object[] objects)
        {
            writeEventArgs.LogType = LogType.Info;
            
            Write(message, objects);
        }
    }
}
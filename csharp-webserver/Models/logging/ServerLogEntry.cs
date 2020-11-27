using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace csharp_webserver
{
    public class ServerLogEntry
    {
        private DateTime created;
        private LogLevel logLevel;
        private string message;
        private Type traceType;

        public ServerLogEntry()
        {
            created = DateTime.Now;
            logLevel = LogLevel.INFO;
            message = "";

            StackTrace stackTrace = new StackTrace();
            traceType = stackTrace.GetFrame(1).GetType();
        }

        public static ServerLogEntry builder() { return new ServerLogEntry(); }
        public ServerLogEntry setLogLevel(LogLevel ll) { logLevel = ll; return this; }
        public ServerLogEntry setMessage(string s) { message = s; return this; }
        public ServerLogEntry setTraceType(Type t) { traceType = t; return this; }

        public LogLevel getLogLevel() { return logLevel; }
        public string getMessage() { return message; }
        public string getTraceType() { return traceType.Name; }

        public DateTime getCreationTime() { return created; }
        public string getCreationTimeAsString() { return $"{created.ToLongDateString()} {created.ToLocalTime()}"; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace csharp_webserver
{
    public class LoggerService
    {
        private List<ServerLogEntry> logs;

        public LoggerService()
        {
            
            logs = new List<ServerLogEntry>();
        }

        public LoggerService addLog(ServerLogEntry sle)
        {
            
            logs.Add(sle);
            return this;
        }

        public List<ServerLogEntry> getLogMessages()
        {
            
            return logs;
        }
    }
}
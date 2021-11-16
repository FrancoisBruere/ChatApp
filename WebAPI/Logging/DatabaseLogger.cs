using Common;
using DataAccess;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Server.Logging
{
    public class DatabaseLogger : ILogger
    {
        private readonly ApplicationDbContext _db;

        public DatabaseLogger(ApplicationDbContext db)
        {
            _db = db;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // go directly to DB not DTO
            ServerLog serverLog = new();
            serverLog.Log_Level = logLevel.ToString();
            serverLog.Event_Name = eventId.Name;
            serverLog.Exception_Message = exception?.Message;
            serverLog.Stack_Trace = exception?.StackTrace;
            serverLog.Source = SD.LogError_Server;
            serverLog.Created_date = DateTime.Now.ToString();


            _db.ServerLogs.Add(serverLog);

            _db.SaveChanges();




        }
    }
}

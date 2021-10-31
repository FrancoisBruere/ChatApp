using ChatApp.Shared;
using Common;

using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChatApp.Client.Logging
{
    public class DatabaseLogger// : ILogger
    {

        //private readonly HttpClient _httpClient;

        //public DatabaseLogger(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        //public IDisposable BeginScope<TState>(TState state)
        //{
        //    return null;
        //}

        //public bool IsEnabled(LogLevel logLevel)
        //{
        //    return true;
        //}

        //public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        //{
        //    // go directly to DB not DTO
        //    LogDTO log = new();
        //    log.Log_Level = logLevel.ToString();
        //    log.Event_Name = eventId.Name;
        //    log.Exception_Message = exception?.Message;
        //    log.Stack_Trace = exception?.StackTrace;
        //    log.Source = SD.LogError_Client;
        //    log.Created_date = DateTime.Now.ToString();

        //    await _httpClient.PostAsJsonAsync<LogDTO>("/Logs", log);

        //}
    }
}

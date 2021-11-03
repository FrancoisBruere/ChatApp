using DataAccess;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Server.Logging
{
    public class ApplicationLoggerProvider : ILoggerProvider
    {
        private readonly ApplicationDbContext _db;
        public ApplicationLoggerProvider(ApplicationDbContext db)
        {
            _db = db;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(_db);
        }

        public void Dispose()
        {

        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<ChatHistory> ChatHistories { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ServerLog> ServerLogs { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }

        public DbSet<UserActivity> UserActivities { get; set; }
    }
}

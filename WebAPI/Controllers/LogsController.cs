using AutoMapper;
using ChatApp.Shared;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
   // [Authorize]
    public class LogsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly Mapper _mapper;
        //private readonly ILogger<ProfileController> _logger;

        public LogsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<ActionResult<Log>> PostLog(Log log)
        {

            _db.Logs.Add(log);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetLog", new { log.Log_Id }, log);

        }



    }
}

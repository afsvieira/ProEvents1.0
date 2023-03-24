using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos1._0.API.Data;
using ProEventos1._0.API.Models;

namespace ProEventos1._0.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly DataContext _context;
        public EventController(DataContext context)
        {            
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {         
            return  _context.Events;           
        }

        [HttpGet("{id}")]
        public Event GetById(int id)
        {         
            return _context.Events.FirstOrDefault(e => e.EventId == id);           
        }

        [HttpPost]
        public string Post()
        {         
            return "Example of Post";
        }
    }
}
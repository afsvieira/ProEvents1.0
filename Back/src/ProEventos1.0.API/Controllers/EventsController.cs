using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence;
using ProEventos.Domain;
using ProEventos.Persistence.Context;

namespace ProEventos1._0.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly ProEventosContext _context;
        public EventsController(ProEventosContext context)
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
            return _context.Events.FirstOrDefault(e => e.Id == id);           
        }

        [HttpPost]
        public string Post()
        {         
            return "Example of Post";
        }
    }
}
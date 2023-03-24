using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos1._0.API.Models;

namespace ProEventos1._0.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        public EventController()
        {            
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {         
            return new Event[] {
                new Event() {
                    EventId = 1,
                    Subject = "Angular and .NET 5",
                    Local = "Toronto",
                    Lote = "1º Lote",
                    QtyGuests = 250,
                    EventDate = DateTime.Now.AddDays(2).ToString(),
                    ImageURL = "figure.png"
                },
                new Event() {
                    EventId = 1,
                    Subject = "Angular and .NET 5",
                    Local = "Toronto",
                    Lote = "1º Lote",
                    QtyGuests = 250,
                    EventDate = DateTime.Now.AddDays(2).ToString(),
                    ImageURL = "figure.png"
                },
            };            
        }

        [HttpPost]
        public string Post()
        {         
            return "Example of Post";
        }
    }
}
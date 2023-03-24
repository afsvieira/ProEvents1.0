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
        public IEnumerable<Event> _events = new Event[] {
                new Event() {
                    EventId = 1,
                    Subject = "Angular 11 and .NET 5",
                    Local = "Toronto",
                    Lote = "1º Lote",
                    QtyGuests = 250,
                    EventDate = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
                    ImageURL = "figure.png"
                },
                new Event() {
                    EventId = 2,
                    Subject = "Rhe news of Angular",
                    Local = "Montreal",
                    Lote = "2º Lote",
                    QtyGuests = 350,
                    EventDate = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy"),
                    ImageURL = "figure1.png"
                },
            };
        public EventController()
        {            
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {         
            return  _events;           
        }

        [HttpGet("{id}")]
        public IEnumerable<Event> GetById(int id)
        {         
            return _events.Where(e => e.EventId == id);           
        }

        [HttpPost]
        public string Post()
        {         
            return "Example of Post";
        }
    }
}
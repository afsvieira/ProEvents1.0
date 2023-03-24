using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos1._0.API.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Local { get; set; }
        public string EventDate { get; set; }
        public string Subject { get; set; }
        public int QtyGuests { get; set; }
        public string Lote { get; set; }
        public string ImageURL { get; set; }        
    }
}
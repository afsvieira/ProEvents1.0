using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Application.DTOS
{
    public class LoteDTO
    {
        public int Id { get; set; }
        public string  Name {get; set;}
        public decimal Price { get; set; }
        public string DateStart  { get; set; }
        public string DateEnd  { get; set; }
        public int Quantity { get; set; }
        public int EventId { get; set; }
        public EventDTO Event { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Application.DTOS
{
    public class LoteDTO
    {
        public int Id { get; set; }
        public string  Name {get; set;}
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string DateStart  { get; set; }
        [Required]
        public string DateEnd  { get; set; }
        public int Quantity { get; set; }
        [Required]
        public int EventId { get; set; }
        public EventDTO Event { get; set; }
    }
}
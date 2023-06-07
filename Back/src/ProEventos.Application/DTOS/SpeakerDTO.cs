using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Application.DTOS
{
    public class SpeakerDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string MiniResume { get; set; }
        public string ImageURL { get; set; }
        public string Phone { get; set; }
        public string Email {get; set;}
        public IEnumerable<SocialMediaDTO> SocialMedias { get; set; }
        public IEnumerable<SpeakerDTO> SpeakerEvents { get; set; }
    }
}
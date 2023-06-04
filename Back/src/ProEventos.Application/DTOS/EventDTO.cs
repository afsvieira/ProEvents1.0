using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Application.DTOS
{
    public class EventDTO
    {
        public int Id { get; set; }

        [Required]
        public string Local { get; set; }

        [Required]
        public string EventDate { get; set; }

        [Required(ErrorMessage = "The {0} is required."),
        StringLength(50, MinimumLength = 3, ErrorMessage = "The {0} field requires between 3 to 50 characters.")]
        // MinLength(3, ErrorMessage = "{0} requires minimum 3 characters."),
        // MaxLength(50, ErrorMessage = "{0} requires maximum 50 characters.")]
        public string Subject { get; set; }

        [Range(1,1000), Display(Name ="Number of guests")]
        public int QtyGuests { get; set; }
        
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$")]
        public string ImageURL { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }
        
        public IEnumerable<LoteDTO> Lotes { get; set; }
        public IEnumerable<SocialMediaDTO> SocialMedias { get; set; }
        public IEnumerable<SpeakerDTO> SpeakerEvents { get; set; }
    }
}
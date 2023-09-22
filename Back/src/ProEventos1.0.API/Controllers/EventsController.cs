using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contracts;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.DTOS;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace ProEventos1._0.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        public readonly IEventService _eventService;
        private readonly IWebHostEnvironment _hostEnvironment;
        
        public EventsController(IEventService eventService, IWebHostEnvironment hostEnvironment)
        {            
            this._hostEnvironment = hostEnvironment;
            this._eventService = eventService;
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {         
            try
            {
                var events = await _eventService.GetAllEventAsync(true);
                if(events == null) return NoContent();
                return Ok(events);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error when trying to retrieve events. Error: {ex.Message}");
            }         
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {         
            try
            {
                var evento  = await _eventService.GetEventByIdAsync(id, true);
                if(evento == null) return NoContent();
                return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error when trying to retrieve events. Error: {ex.Message}");
            }                   
        }

        [HttpGet("{subject}/subject")]
        public async Task<IActionResult> GetBySubject(string subject)
        {         
            try
            {
                var events  = await _eventService.GetAllEventBySubjectAsync(subject, true);
                if(events == null) return NoContent();
                return Ok(events);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error when trying to retrieve events. Error: {ex.Message}");
            }                   
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventDTO model)
        {         
            try
            {
                var evento  = await _eventService.AddEvent(model);
                if(evento == null) return NoContent();;
                return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error when trying to add event. Error: {ex.Message}");
            }                   
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventDTO model)
        {
           try
            {
                var evento  = await _eventService.UpdateEvent(id, model);
                if(evento == null) return NoContent();;
                return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error when trying to update event. Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           try
            {
                return await _eventService.DeleteEvent(id) 
                        ? Ok(new {message = "Deleted"}) 
                        : BadRequest("Event not deleted.");
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error when trying to delete event. Error: {ex.Message}");
            }
        }

        [HttpPost("upload-image/{eventId}")]
        public async Task<IActionResult> UploadImage(int eventId) {
            try 
            {
                var evento = await _eventService.GetEventByIdAsync(eventId, true);
                if(evento == null) return NoContent();

                var file = Request.Form.Files[0];
                if(file.Length > 0)
                {
                    DeleteImage(evento.ImageURL);
                    evento.ImageURL = await SaveImage(file);

                }
                var eventReturn = await _eventService.UpdateEvent(eventId, evento);

                return Ok(eventReturn);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error to loading image. Error: {ex.Message}");
            }
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images");
            if(System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images", imageName);
            using(var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
    }
}
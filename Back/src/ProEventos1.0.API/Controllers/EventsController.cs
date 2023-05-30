using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace ProEventos1._0.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        public IEventService _eventService { get; }
        
        public EventsController(IEventService eventService)
        {            
            this._eventService = eventService;
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {         
            try
            {
                var events = await _eventService.GetAllEventAsync(true);
                if(events == null) return NotFound("Any events found");
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
                if(evento == null) return NotFound("Event not found");
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
                if(events == null) return NotFound("No events found");
                return Ok(events);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error when trying to retrieve events. Error: {ex.Message}");
            }                   
        }

        [HttpPost]
        public async Task<IActionResult> Post(Event model)
        {         
            try
            {
                var evento  = await _eventService.AddEvent(model);
                if(evento == null) return BadRequest("Error to add event.");
                return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error when trying to add event. Error: {ex.Message}");
            }                   
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Event model)
        {
           try
            {
                var evento  = await _eventService.UpdateEvent(id, model);
                if(evento == null) return BadRequest("Error to update event.");
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
                return await _eventService.DeleteEvent(id) ?
                        Ok("Deleted") :
                        BadRequest("Event not found.");
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error when trying to delete event. Error: {ex.Message}");
            }
        }
    }
}
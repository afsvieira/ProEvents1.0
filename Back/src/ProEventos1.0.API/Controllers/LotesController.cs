using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contracts;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.DTOS;

namespace ProEventos1._0.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        public ILoteService _loteService { get; }
        
        public LotesController(ILoteService loteService)
        {            
            this._loteService = loteService;
            
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(int eventId)
        {         
            try
            {
                var lotes = await _loteService.GetLotesByEventIdAsync(eventId);
                if(lotes == null) return NoContent();
                return Ok(lotes);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error when trying to retrieve lotes. Error: {ex.Message}");
            }         
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> SaveLotes(int eventId, LoteDTO[] models)
        {
           try
            {
                var lotes  = await _loteService.SaveLotes(eventId, models);
                if(lotes == null) return NoContent();;
                return Ok(lotes);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error saving lotes. Error: {ex.Message}");
            }
        }

        [HttpDelete("{eventId}/{loteId}")]
        public async Task<IActionResult> Delete(int eventId, int loteId)
        {
           try
            {
                var lote = await _loteService.GetLoteByIdsAsync(eventId, loteId);
                if(lote == null) return NoContent();

                return await _loteService.DeleteLote(lote.EventId, loteId) 
                        ? Ok(new {message = "Lote Deleted"}) 
                        : BadRequest("Lote not deleted.");
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error deleting lote.. Error: {ex.Message}");
            }
        }

        
    }
}
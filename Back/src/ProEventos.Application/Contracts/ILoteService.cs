using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.DTOS;
using ProEventos.Domain;

namespace ProEventos.Application.Contracts
{
    public interface ILoteService
    {
        
        Task<LoteDTO[]> SaveLotes(int eventId, LoteDTO[] models);
        Task<bool> DeleteLote(int eventId, int loteId);
        Task<LoteDTO[]> GetLotesByEventIdAsync(int eventId);        
        Task<LoteDTO> GetLoteByIdsAsync(int eventId, int loteId);


    
    }
}
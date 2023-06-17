using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contracts
{
    public interface ILotePersist
    {   
        /// <summary>
        /// Method get to return list of lotes by event id.
        /// </summary>
        /// <param name="eventId">Id of event.</param>
        /// <returns>List of lotes.</returns>
        Task<Lote[]> GetLotesByEventIdAsync(int eventId);

        /// <summary>
        /// Method get. Returns only one lote.
        /// </summary>
        /// <param name="eventId">Id of event</param>
        /// <param name="id">Id code of Lote</param>
        /// <returns>Only one lote.</returns>
        Task<Lote> GetLoteByIdsAsync(int eventId, int id);
        
    }
}
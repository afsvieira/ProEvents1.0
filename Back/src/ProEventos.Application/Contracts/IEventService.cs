using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.DTOS;
using ProEventos.Domain;

namespace ProEventos.Application.Contracts
{
    public interface IEventService
    {
        Task<EventDTO> AddEvent(EventDTO model);
        Task<EventDTO> UpdateEvent(int eventId, EventDTO model);
        Task<bool> DeleteEvent(int eventId);

        Task<EventDTO[]> GetAllEventAsync(bool includeSpeakers = false);
        Task<EventDTO[]> GetAllEventBySubjectAsync(string subject, bool includeSpeakers = false);
        Task<EventDTO> GetEventByIdAsync(int eventId, bool includeSpeakers = false);


    
    }
}
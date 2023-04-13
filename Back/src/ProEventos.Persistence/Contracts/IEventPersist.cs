using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contracts
{
    public interface IEventPersist
    {        
        Task<Event[]> GetAllEventBySubjectAsync(string subject, bool includeSpeakers = false);
        Task<Event[]> GetAllEventAsync(bool includeSpeakers = false);
        Task<Event> GetEventByIdAsync(int eventId, bool includeSpeakers = false);
        
    }
}
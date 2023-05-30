using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Persistence
{
    public class EventPersist : IEventPersist
    {
        private readonly ProEventosContext _context;
        public EventPersist(ProEventosContext context)
        {
            this._context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            
        }

        public async Task<Event[]> GetAllEventAsync(bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(e => e.Lotes)
                .Include(e => e.SocialMedias);
            
            if(includeSpeakers)
            {
                query = query.Include(e => e.SpeakerEvents)
                            .ThenInclude(se => se.Speaker);
            }
            query = query.OrderBy(e => e.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Event[]> GetAllEventBySubjectAsync(string subject, bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(e => e.Lotes)
                .Include(e => e.SocialMedias);
            
            if(includeSpeakers)
            {
                query = query.Include(e => e.SpeakerEvents)
                            .ThenInclude(se => se.Speaker);
            }
            query = query.OrderBy(e => e.Id)
                        .Where(e=> e.Subject.ToLower().Contains(subject.ToLower()));
            return await query.ToArrayAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId, bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(e => e.Lotes)
                .Include(e => e.SocialMedias);
            
            if(includeSpeakers)
            {
                query = query.Include(e => e.SpeakerEvents)
                            .ThenInclude(se => se.Speaker);
            }
            query = query.Where(e=> e.Id == eventId);
            return await query.FirstOrDefaultAsync();
        }
        
    }
}
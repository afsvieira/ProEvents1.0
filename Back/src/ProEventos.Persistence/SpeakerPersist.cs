using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Persistence
{
    public class SpeakerPersist : ISpeakerPersist
    {
        private readonly ProEventosContext _context;
        public SpeakerPersist(ProEventosContext context)
        {
            this._context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;            
            
        }

        public async Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(s => s.SocialMedias);
            
            if(includeEvents)
            {
                query = query.Include(e => e.SpeakerEvents)
                            .ThenInclude(se => se.Event);
            }
            query = query.OrderBy(s => s.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvents = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(s => s.SocialMedias);
            
            if(includeEvents)
            {
                query = query.Include(e => e.SpeakerEvents)
                            .ThenInclude(se => se.Event);
            }
            query = query.OrderBy(s => s.Id)
                        .Where(s => s.Name.ToLower().Contains(name.ToLower()));
            return await query.ToArrayAsync();
        }        

        public async Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(s => s.SocialMedias);
            
            if(includeEvents)
            {
                query = query.Include(e => e.SpeakerEvents)
                            .ThenInclude(se => se.Event);
            }
            query = query.Where(s => s.Id == speakerId);
            return await query.FirstOrDefaultAsync();
        }

        
    }
}
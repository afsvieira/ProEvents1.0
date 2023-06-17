using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Persistence
{
    public class LotePersist : ILotePersist
    {
        private readonly ProEventosContext _context;
        public LotePersist(ProEventosContext context)
        {
            this._context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventId, int id)
        {
            IQueryable<Lote> query = _context.Lotes;
            query = query.AsNoTracking()
                        .Where(lote => lote.EventId == eventId && lote.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> GetLotesByEventIdAsync(int eventId)
        {
            IQueryable<Lote> query = _context.Lotes;
            query = query.AsNoTracking()
                        .Where(lote => lote.EventId == eventId);
            return await query.ToArrayAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contracts;
using ProEventos.Application.DTOS;
using ProEventos.Domain;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Application
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly ILotePersist _lotePersist;
        private readonly IMapper _mapper;
        public LoteService(IGeralPersist geralPersist, ILotePersist lotePersist, IMapper mapper)
        {
            this._lotePersist = lotePersist;
            this._geralPersist = geralPersist;
            this._mapper = mapper;
            
        }
        public async Task AddLote(int eventId, LoteDTO model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);
                lote.EventId = eventId;
                _geralPersist.Add<Lote>(lote);
                await _geralPersist.SaveChangeAsync();
                
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
        public async Task<LoteDTO[]> SaveLotes(int eventId, LoteDTO[] models)
        {
            try
            {              
                var lotes = await _lotePersist.GetLotesByEventIdAsync(eventId);
                if(lotes == null) return null;

                foreach(var model in models)
                {
                    if(model.Id == 0)
                    {
                        await AddLote(eventId, model);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);
                        model.EventId = eventId;
                        _mapper.Map(model, lote);
                        _geralPersist.Update<Lote>(lote);
                        await _geralPersist.SaveChangeAsync();
                    }
                }
                var loteReturn = await _lotePersist.GetLotesByEventIdAsync(eventId);
                return _mapper.Map<LoteDTO[]>(loteReturn);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLote(int eventId, int loteId)
        {
             try
            {
                var lote = await _lotePersist.GetLoteByIdsAsync(eventId, loteId);
                if(lote == null) throw new Exception("Event for delete not found.");

                _geralPersist.Delete<Lote>(lote);
                return await _geralPersist.SaveChangeAsync();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }        

        public async Task<LoteDTO[]> GetLotesByEventIdAsync(int eventId)
        {
            try
            {
                var lotes = await _lotePersist.GetLotesByEventIdAsync(eventId);
                if(lotes == null) return null;
                var result = _mapper.Map<LoteDTO[]>(lotes);

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDTO> GetLoteByIdsAsync(int eventId, int loteId)
        {
             try
            {
                var lote = await _lotePersist.GetLoteByIdsAsync(eventId, loteId);
                if(lote == null) return null;

                var result = _mapper.Map<LoteDTO>(lote);

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
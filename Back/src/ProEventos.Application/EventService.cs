using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.Contracts;
using ProEventos.Domain;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Application
{
    public class EventService : IEventService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventPersist _eventPersist;
        public EventService(IGeralPersist geralPersist, IEventPersist eventPersist)
        {
            this._eventPersist = eventPersist;
            this._geralPersist = geralPersist;
            
        }
        public async Task<Event> AddEvent(Event model)
        {
            try
            {
                _geralPersist.Add<Event>(model);
                if(await _geralPersist.SaveChangeAsync())
                {
                    return await _eventPersist.GetEventByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
        public async Task<Event> UpdateEvent(int eventId, Event model)
        {
            try
            {
                var evento = await _eventPersist.GetEventByIdAsync(eventId, false);
                if(evento == null) return null;

                model.Id = evento.Id;

                _geralPersist.Update(model);
                if(await _geralPersist.SaveChangeAsync())
                {
                    return await _eventPersist.GetEventByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvent(int eventId)
        {
             try
            {
                var evento = await _eventPersist.GetEventByIdAsync(eventId, false);
                if(evento == null) throw new Exception("Event for delete not found.");               
                
                _geralPersist.Delete<Event>(evento);
                return await _geralPersist.SaveChangeAsync();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public Task<Event[]> GetAllEventAsync(bool includeSpeakers = false)
        {
            throw new NotImplementedException();
        }

        public Task<Event[]> GetAllEventBySubjectAsync(string subject, bool includeSpeakers = false)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEventByIdAsync(int eventId, bool includeSpeakers = false)
        {
            throw new NotImplementedException();
        }

        
    }
}
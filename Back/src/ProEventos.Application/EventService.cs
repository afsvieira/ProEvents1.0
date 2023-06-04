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
    public class EventService : IEventService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventPersist _eventPersist;
        private readonly IMapper _mapper;
        public EventService(IGeralPersist geralPersist, IEventPersist eventPersist, IMapper mapper)
        {
            this._eventPersist = eventPersist;
            this._geralPersist = geralPersist;
            this._mapper = mapper;
            
        }
        public async Task<EventDTO> AddEvent(EventDTO model)
        {
            try
            {
                var evento = _mapper.Map<Event>(model);
                _geralPersist.Add<Event>(evento);
                
                if(await _geralPersist.SaveChangeAsync())
                {
                    var result = await _eventPersist.GetEventByIdAsync(evento.Id, false);
                    return _mapper.Map<EventDTO>(result);
                }
                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
        public async Task<EventDTO> UpdateEvent(int eventId, EventDTO model)
        {
            try
            {              
                var evento = await _eventPersist.GetEventByIdAsync(eventId, false);
                if(evento == null) return null;

                model.Id = evento.Id;

                _mapper.Map(model, evento);

                _geralPersist.Update<Event>(evento);
                if(await _geralPersist.SaveChangeAsync())
                {
                    var result = await _eventPersist.GetEventByIdAsync(evento.Id, false);
                    return _mapper.Map<EventDTO>(result);
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

        public async Task<EventDTO[]> GetAllEventAsync(bool includeSpeakers = false)
        {
            try
            {
                var events = await _eventPersist.GetAllEventAsync(includeSpeakers);
                if(events == null) return null;
                var result = _mapper.Map<EventDTO[]>(events);

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<EventDTO[]> GetAllEventBySubjectAsync(string subject, bool includeSpeakers = false)
        {
            try
            {
                var events = await _eventPersist.GetAllEventBySubjectAsync(subject, includeSpeakers);
                if(events == null) return null;
                var result = _mapper.Map<EventDTO[]>(events);

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventDTO> GetEventByIdAsync(int eventId, bool includeSpeakers = false)
        {
             try
            {
                var events = await _eventPersist.GetEventByIdAsync(eventId, includeSpeakers);
                if(events == null) return null;

                var result = _mapper.Map<EventDTO>(events);

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
using System;
using System.Threading.Tasks;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application.Contratos
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        public EventoService(IEventoPersist eventoPersist, IGeralPersist geralPersist)
        {
            _eventoPersist = eventoPersist;
            _geralPersist = geralPersist;
        }

        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersist.Add<Evento>(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetAllEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<Evento> UpdateEventos(int eventoId, Evento model)
        {

            try
            {
                var evento = await _eventoPersist.GetAllEventoByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.Id = eventoId;

                _geralPersist.Update<Evento>(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetAllEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEventos(int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetAllEventoByIdAsync(eventoId);
                if (evento == null) throw new Exception("Evento para delete não encontrado.");

                _geralPersist.Delete<Evento>(evento);
                return await _geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = _eventoPersist.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                return await eventos;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                return await eventos;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<Evento> GetAllEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                 var eventos = _eventoPersist.GetAllEventoByIdAsync(eventoId, includePalestrantes);
                 if (eventos == null) return null;

                 return await eventos;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

    }
}
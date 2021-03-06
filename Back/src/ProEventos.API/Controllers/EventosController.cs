using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence;
using Microsoft.AspNetCore.Http;


namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum Evento Econtrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetAllEventoByIdAsync(id, true);
                if (evento == null) return NotFound("Eventos por Id não Econtrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro {ex.Message}");
            }
        }
        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if (evento == null) return NotFound("Eventos por Tema não Econtrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await _eventoService.AddEventos(model);
                if (evento == null) return BadRequest("Erro ao adicionar o Evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar um envento ao servidor Error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _eventoService.UpdateEventos(id, model);
                if (evento == null) return BadRequest("Erro ao atualizar o Evento solicitado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Atualizar um envento ao servidor Error: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _eventoService.DeleteEventos(id) ? 
                                            Ok("Deletado com Sucesso") : 
                                            BadRequest("Evento não deletado");

            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar um envento ao servidor Error: {ex.Message}");
            }
        }

    }
}


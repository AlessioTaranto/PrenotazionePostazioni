using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Exceptions;
using Microsoft.AspNetCore.Cors;
using log4net;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/room")]
    public class RoomController : ControllerBase
    {
        private RoomService _roomService;
        private readonly ILog _logger = LogManager.GetLogger(typeof(RoomController));

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }
        /// <summary>
        /// Restituisce tutte le stanze
        /// </summary>
        /// <returns>Lista di tutte le stanze</returns>
        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                _logger.Info("Trovando tutte le stanze...");
                List<Room> rooms = _roomService.GetAll();
                _logger.Info("Stanze trovate con successo!");
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        
        /// <summary>
        /// Restituisce la stanza mediante l'id associato
        /// </summary>
        /// <param name="id">L'id della stanza</param>
        /// <returns>La stanza trovata con 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getById")]
        public IActionResult GetById(int id)
        {
            try
            {
                _logger.Info("Id stanza: " + id);
                _logger.Info("Trovando la stanza mediante il suo id: " + id + "...");
                Room stanza = _roomService.GetById(id);
                _logger.Info("Stanza trovata con successo!");
                return Ok(stanza);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Stanza non trovata: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return BadRequest();
            }
            
        }

        /// <summary>
        /// Restituisce la stanza mediante il suo nome
        /// </summary>
        /// <param name="name">Il nome della stanza da trovare</param>
        /// <returns>La stanza trovata e 200, 404 altrimenti </returns>
        [HttpGet]
        [Route("getByName")]
        public IActionResult GetByName(string name)
        {
            try
            {
                _logger.Info("Nome della stanza: " + name);
                _logger.Info("Trovando la stanza mediante il suo nome: " + name + "...");
                Room room = _roomService.GetByName(name);
                _logger.Info("Stanza trovata con successo!");
                return Ok(room);
            }
            catch (PrenotazionePostazioniApiException ex)
            {

                _logger.Warn("stanza non trovata: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Aggiunge una nuova stanza al database
        /// </summary>
        /// <param name="roomDto">L'oggetto stanza da aggiungere al database</param>
        /// <returns>httpstatus 200</returns>
        [HttpPost]
        [Route("add")]
        public IActionResult Add(RoomDto roomDto)
        {
            try
            {
                _logger.Info("Nome della stanza: " + roomDto.Name);
                _logger.Info("Salvando una stanzaDto nel database...");
                _roomService.Add(roomDto);
                _logger.Info("StanzaDto salvato nel database con successo!");
                return Ok();
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("updateCapacity")]
        public IActionResult UpdateCapacity(int capacity, string name)
        {
            try
            {
                Room room = _roomService.GetByName(name);
                _roomService.UpdateCapacity(capacity, room.Id);
                return Ok("Posti massimi cambiati");
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("updateCapacityEmergency")]
        public IActionResult UpdateCapacityEmergency(int capacity, string name)
        {
            try
            {
                Room room = _roomService.GetByName(name);
                _roomService.UpdateCapacityEmergency(capacity, room.Id);
                return Ok("Posti massimi cambiati");
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

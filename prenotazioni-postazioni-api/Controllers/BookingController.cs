
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private BookingService _bookingService;
        private readonly ILog _logger = LogManager.GetLogger(typeof(BookingController));

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
        /// <summary>
        /// Restituisce la Prenotazione trovata mediante il suo ID
        /// </summary>
        /// <param name="idBooking">Id della Prenotazione</param>
        /// <returns>Prenotazione e status 200, status 404 altrimenti</returns>
        [HttpGet]
        [Route("getById")]
        public IActionResult GetById(int idBooking)
        {
            try
            {
                _logger.Info("Id Prenotazione: " + idBooking);
                _logger.Info("Trovando una prenotazione mediante l'id...");
                _logger.Info("Id: " + idBooking);
                Prenotazione booking = _bookingService.GetById(idBooking);
                _logger.Info("Trovato una prenotazione con id: " + booking.IdPrenotazioni + " con successo");
                return Ok(booking);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Prenotazione non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce tutte le prenotazioni presenti nel Database
        /// </summary>
        /// <returns>Lista di Prenotazioni e status 200</returns>
        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                _logger.Info("Trovando tutte le prenotazioni...");
                List<Prenotazione> bookings = _bookingService.GetAll();
                _logger.Info("Prenotazioni trovate con successo!");
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce la Prenotazione associata alla sua stanza.
        /// </summary>
        /// <param name="idRoom">L'Id della stanza associata alla Prenotazione</param>
        /// <returns>Lista di Prenotazione e status 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getByRoom")]
        public IActionResult GetByroom(int idRoom)
        {
            try
            {
                _logger.Info("Id stanza: " + idRoom);
                _logger.Info("Trovando tutte le prenotazioni di una stanza...");
                List<Prenotazione> bookings = _bookingService.GetByRoom(idRoom);
                _logger.Info("Prenotazioni della stanza ID: " + idRoom + " trovate!");
                return Ok(bookings);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }

        }
        
        /// <summary>
        /// Restituisce tutte le Prenotazioni dall'Id utente associato
        /// </summary>
        /// <param name="idUser">L'id utente associata alla Prenotazione</param>
        /// <returns>Lista di Prenotazione e status 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getByUser")]
        public IActionResult GetByUser(int idUser)
        {
            try
            {
                _logger.Info("Id utente: " + idUser);
                _logger.Info("Trovando tutte le prenotazioni di un utente");
                List<Prenotazione> bookings = _bookingService.GetByUser(idUser);
                _logger.Info("Prenotazioni dell'id utente: " + idUser + " trovate con successo!");
                return Ok(bookings);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce tutte le Prenotazioni effettuate in una Stanza
        /// </summary>
        /// <param name="idRoom"></param>
        /// <param name="date"></param>
        /// <returns>Lista di Prenotazioni e status 200, altrimenti 404</returns>
        [HttpGet]
        [Route("getByDate")]
        public IActionResult getByDate(int idRoom, int startDateYear, int startDateMonth, int startDateDay, int startDateHour, int startDateMinute, int endDateYear, int endDateMonth, int endDateDay, int endDateHour, int endDateMinute)
        {

            DateTime startDate = new DateTime(startDateYear, startDateMonth, startDateDay, startDateHour, startDateMinute, 0);
            DateTime endDate = new DateTime(endDateYear, endDateMonth, endDateDay, endDateHour, endDateMinute, 59);

            try
            {
                _logger.Info("Giorno inserite: ");
                _logger.Info("Id Stanza: " + idRoom);
                _logger.Info("StartDate: " + startDate.ToString());
                _logger.Info("EndDate: " + endDate.ToString());
                _logger.Info("Trovando tutte le prenotazioni di una data...");
                List<Prenotazione> bookings = _bookingService.GetAllByRoomDate(idRoom, startDate, endDate);
                _logger.Info("Prenotazioni della stanza trovate con successo");
                return Ok(bookings);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }



        /// <summary>
        /// Salva una prenotazione nel database
        /// </summary>
        /// <param name="bookingDto">L'oggetto dto da mappare e poi salvare</param>
        /// <returns>status 200</returns>
        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] PrenotazioneDto bookingDto)
        {
            try
            {
                _logger.Info("Utente: " + bookingDto.IdUtente);
                _logger.Info("Inizio data: " + bookingDto.StartDate.ToString());
                _logger.Info("Fine data: " + bookingDto.EndDate.ToString());
                _logger.Info("Stanza: " + bookingDto.IdStanza);
                _logger.Info("Aggiungendo una prenotazioneDto nel database...");
                _bookingService.Add(bookingDto);
                _logger.Info("PrenotazioneDto aggiunto con successo!");
                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.Error("Errore inserimento del parametro: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Error("Errore: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Cancella una prenotazione se presente nel database
        /// </summary>
        /// <param name="idBooking">Id associato alla prenotazione da cancellare</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int idBooking)
        {
            try
            {
                _logger.Info("Id Prenotazione: " + idBooking);
                _bookingService.Delete(idBooking);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.Error("Errore inserimento del parametro: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Error("Errore: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

    }
}

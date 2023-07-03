using prenotazione_postazioni_libs.Dto;
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        private UserService _UserService;
        private readonly ILog _logger = LogManager.GetLogger(typeof(UserController));

        public UserController( UserService userService)
        {
            _UserService = userService;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult getAll()
        {
            try
            {
                _logger.Info("Prelevando tutti gli utenti...");
                List<User> users = _UserService.GetAll();
                _logger.Info("Prelevato tutti gli utenti con successo!");

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce un utente mediante il suo id
        /// </summary>
        /// <param name="idUser">Id dell'utente da trovare</param>
        /// <returns>L'utente trovato e 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getById")]
        public IActionResult GetById(int idUser)
        {
            try
            {
                _logger.Info("Id utente: " + idUser);
                _logger.Info("Prelevando un utente mediante il suo id: " + idUser + "...");
                User user = _UserService.GetById(idUser);
                _logger.Info("Utente trovato con successo!");
                return Ok(user);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Utente non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("getByName")]
        public IActionResult GetByName(string name, string surname)
        {
            try
            {
                _logger.Info("Nome: " + name);
                _logger.Info("Cognome: " + surname);
                _logger.Info($"Trovando l'utente mediante il suo nome {name} {surname}...");
                User user = _UserService.GetByName(name, surname);
                _logger.Info($"utente trovato mediante il suo nome con successo!");
                return Ok(user);
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Utente non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce un utente mediante il suo email
        /// </summary>
        /// <param name="email">L'email dell'utente da trovare</param>
        /// <returns>L'utente trovato e 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getByEmail")]
        public IActionResult GetByEmail(string email)
        {
            try
            {
                _logger.Info("Email: " + email);
                _logger.Info($"Trovando l'utente mediante la sua email:{email}...");
                User user = _UserService.GetByEmail(email);
                _logger.Info("Utente trovato mediante il suo email con successo!");
                return Ok(user);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Utente non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message); 
            }
        }

        /// <summary>
        /// Dato un giorno trova l'elenco di persone che hanno effettuato una prenotazione in tale giorno
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Lista di utenti</returns>
        [HttpGet]
        [Route("getByDate")]
        public IActionResult GetByDate(int year, int month, int day)
        {
            DateTime date = new DateTime(year, month, day);
            try
            {
                List<User> users = _UserService.GetByDate(date);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno. " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Aggiunge un utente al database
        /// </summary>
        /// <param name="userDto">L'utente da inserire nel database</param>
        /// <returns>httpstatus 200 se salvataggio corretto, httpstatus 400 altrimenti</returns>
        [HttpPost]
        [Route("add")]
        public IActionResult Add(UtenteDto userDto)
        {
            try
            {
                _logger.Info("Nome utente: " + userDto.Nome);
                _logger.Info("Cognome utente: " + userDto.Cognome);
                _logger.Info("Salvando un utente nel database...");
                _UserService.Add(userDto);
                _logger.Info("Utente salvato nel database con successo!");
                return Ok();
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Error("Bad request: " + ex.Message);
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

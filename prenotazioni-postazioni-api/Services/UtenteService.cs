using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;

namespace prenotazioni_postazioni_api.Services
{
    public class UtenteService
    {
        UtenteRepository _utenteRepository = new UtenteRepository();

        /// <summary>
        /// Resituisce l'utente mediante il suo id
        /// </summary>
        /// <param name="id">L'id dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal Utente GetUtenteById(int id)
        {
            Utente utente = _utenteRepository.FindById(id);
            if (utente == null) throw new PrenotazionePostazioniApiException("Utente non trovato");
            else return utente;
        }

        /// <summary>
        /// Restituisce l'utente mediante la sua email
        /// </summary>
        /// <param name="email">L'email dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal Utente GetUtenteByEmail(string email)
        {
            Utente utente = _utenteRepository.FindByEmail(email);
            if (utente == null) throw new PrenotazionePostazioniApiException("Utente non trovato");
            else return utente;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="utenteDto"></param>
        internal void Save(UtenteDto utenteDto)
        {
            string nome = utenteDto.Nome, cognome = utenteDto.Cognome;
            string image = utenteDto.Image, email = utenteDto.Email;
            int idRuolo = utenteDto.IdRuolo;

            if (utenteDto.IsValid)
            {
                Utente utente = new Utente(nome, cognome, image, email, idRuolo);//aggiungere costruttore
                _utenteRepository.Save(utente);
            }
            else throw new PrenotazionePostazioniApiException("Utente da salvare non valido");
        }

    }
}

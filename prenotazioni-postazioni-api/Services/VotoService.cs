using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using prenotazioni_postazioni_api.Utilities;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class VotoService
    {
        private VotoRepository _votoRepository;
        private readonly ILog logger = LogManager.GetLogger(typeof(VotoService));
        public VotoService(VotoRepository votoRepository)
        {
            _votoRepository = votoRepository;
        }



        /// <summary>
        /// Serve a ottenere tutti i voti fatti da un utente
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> GetVotiFromUtente(int idUtente)
        {
            logger.LogInformation("Trovando tutti i voti effettuati da un utente...");
            return _votoRepository.FindAllByIdUtenteFrom(idUtente);
        }

        /// <summary>
        /// Serve a ottenere tutti i voti che sono stati fatti verso un determinato utente
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> GetVotiToUtente(int idUtente)
        {
            logger.LogInformation("Trovando tutti  i voti effettuati verso un determinato utente...");
            return _votoRepository.FindAllByIdUtenteTo(idUtente);
        }

        /// <summary>
        /// Aggiunge un voto al database 
        /// </summary>
        /// <param name="votoDto"></param>
        internal void MakeVotoToUtente(VotoDto votoDto)
        {
            logger.LogInformation("Trovando il voto mediante l'id dell'utente " + votoDto.Utente.IdUtente + " che ha votato e l'id dell'utente " + votoDto.UtenteVotato.IdUtente + " che ha ricevuto il voto");
            Voto voto = _votoRepository.FindByIdUtenteToAndIdUtenteFrom(votoDto.Utente.IdUtente, votoDto.UtenteVotato.IdUtente);
            logger.LogInformation("Controllando se il voto e' null..");
            if (voto == null)
            {
                logger.LogInformation("Il voto e' null, e' valido");
                logger.LogInformation("Convertendo il votoDto in Voto...");
                logger.LogInformation("Procedo con il salvataggio nel database");
                _votoRepository.Save(new Voto(votoDto.Utente.IdUtente, votoDto.UtenteVotato.IdUtente, votoDto.VotoEffettuato));
                return;
            }
            logger.LogInformation("Il voto non e' null, il voto dunque e' gia stato effettuato in precedenza");
            logger.LogInformation("Procedo con il cambiare il voto effettuato...");
            logger.LogInformation("Controllo se il voto effettutato e' uguale al voto nel votoDto...");
            if(voto.VotoEffettuato == votoDto.VotoEffettuato)
            {
                logger.LogCritical("ERRORE: Il voto effettutato e' uguale a quello nel votoDto...");
                throw new PrenotazionePostazioniApiException("Il voto e' uguale");
            }
            logger.LogInformation("Aggiorno il voto!");
            //switch il valore del voto
            _votoRepository.UpdateVoto(voto);
        }
    }
}

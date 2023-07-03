using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class VoteService
    {
        private VoteRepository _voteRepository;
        private UserService _userService;
        private readonly ILog logger = LogManager.GetLogger(typeof(VoteService));
        public VoteService(VoteRepository voteRepository, UserService userService)
        {
            _voteRepository = voteRepository;
            _userService = userService;
        }



        /// <summary>
        /// Serve a ottenere tutti i voti fatti da un utente
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> GetUserVotes(int idUser)
        {
            logger.Info("Trovando tutti i voti effettuati da un utente...");
            logger.Info($"Verifico che l'idUser {idUser} sia associato a un utente valido");
            Utente user = _userService.GetById(idUser);
            return _voteRepository.GetUserVotes(idUser);
        }

        /// <summary>
        /// Serve a ottenere tutti i voti che sono stati fatti verso un determinato utente
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> GetVictimVotes(int idUser)
        {
            logger.Info("Trovando tutti  i voti effettuati verso un determinato utente...");
            logger.Info($"Verifico che l'idUser {idUser} sia associato a un utente valido");
            Utente user = _userService.GetById(idUser);
            return _voteRepository.GetVictimVotes(idUser);
        }

        /// <summary>
        /// Aggiunge un voto al database 
        /// </summary>
        /// <param name="voteDto"></param>
        internal void UpdateVote(VotoDto voteDto)
        {
            logger.Info("Trovando il voto mediante l'id dell'utente " + voteDto.IdUtente.IdUtente + " che ha votato e l'id dell'utente " + voteDto.IdUtenteVotato.IdUtente + " che ha ricevuto il voto");
            Voto? vote = _voteRepository.GetUserVictimVote(voteDto.IdUtente.IdUtente, voteDto.IdUtenteVotato.IdUtente);
            logger.Info("Controllando se il voto e' null..");
            if (vote == null)
            {
                logger.Info("Il voto e' null, e' valido");
                logger.Info("Convertendo il votoDto in Voto...");
                logger.Info("Procedo con il salvataggio nel database");
                _voteRepository.Add(new Voto(voteDto.IdUtente.IdUtente, voteDto.IdUtenteVotato.IdUtente, voteDto.VotoEffettuato));
                return;
            }
            logger.Info("Il voto non e' null, il voto dunque e' gia stato effettuato in precedenza");
            logger.Info("Procedo con il cambiare il voto effettuato...");
            logger.Info("Controllo se il voto effettutato e' uguale al voto nel votoDto...");
            if(vote.VotoEffettuato == voteDto.VotoEffettuato)
            {
                logger.Fatal("ERRORE: Il voto effettutato e' uguale a quello nel votoDto...");
                throw new PrenotazionePostazioniApiException("Il voto e' uguale");
            }
            logger.Info("Aggiorno il voto!");
            //switch il valore del voto
            _voteRepository.Set(vote);
        }

        internal void DeleteVoto(int idUtente, int idUtenteVotato)
        {
            bool ok = false;
            int id = 0;
            logger.Info($"Eliminazione voto di {idUtente} verso {idUtenteVotato}");
            logger.Info($"Ricerco esistenza voto");
            List<Voto> votiApp = GetUserVotes(idUtente);
            foreach(Voto voto in votiApp) if (!ok)
            {
                if(voto.IdUtenteVotato == idUtenteVotato)
                {
                    ok = true;
                    id = voto.IdVoto;
                }
            }
            if(ok == true)
            {
                logger.Info("Voto da eliminare trovato!");
                logger.Info("Elimino il voto...");
                _voteRepository.Delete(id);
            }else
            {
                logger.Fatal("ERRORE: Voto da elimare non valido!");
                throw new PrenotazionePostazioniApiException("Voto da eliminare non esistente");
            }
        }
    }
}

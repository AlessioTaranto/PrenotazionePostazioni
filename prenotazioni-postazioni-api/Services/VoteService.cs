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
        internal List<Vote> GetUserVotes(int idUser)
        {
            logger.Info("Trovando tutti i voti effettuati da un utente...");
            logger.Info($"Verifico che l'idUser {idUser} sia associato a un utente valido");
            User user = _userService.GetById(idUser);
            return _voteRepository.GetUserVotes(idUser);
        }

        /// <summary>
        /// Serve a ottenere tutti i voti che sono stati fatti verso un determinato utente
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns>Lista di voti</returns>
        internal List<Vote> GetVictimVotes(int idUser)
        {
            logger.Info("Trovando tutti  i voti effettuati verso un determinato utente...");
            logger.Info($"Verifico che l'idUser {idUser} sia associato a un utente valido");
            User user = _userService.GetById(idUser);
            return _voteRepository.GetVictimVotes(idUser);
        }

        /// <summary>
        /// Aggiunge un voto al database 
        /// </summary>
        /// <param name="voteDto"></param>
        internal void UpdateVote(VoteDto voteDto)
        {
            logger.Info("Trovando il voto mediante l'id dell'utente " + voteDto.IdUser + " che ha votato e l'id dell'utente " + voteDto.IdVictim + " che ha ricevuto il voto");
            Vote? vote = _voteRepository.GetUserVictimVote(voteDto.IdUser, voteDto.IdVictim);
            logger.Info("Controllando se il voto e' null..");
            if (vote == null)
            {
                logger.Info("Il voto e' null, e' valido");
                logger.Info("Convertendo il votoDto in Voto...");
                logger.Info("Procedo con il salvataggio nel database");
                _voteRepository.Add(new Vote(voteDto.IdUser, voteDto.IdVictim, voteDto.VoteResults));
                return;
            }
            logger.Info("Il voto non e' null, il voto dunque e' gia stato effettuato in precedenza");
            logger.Info("Procedo con il cambiare il voto effettuato...");
            logger.Info("Controllo se il voto effettutato e' uguale al voto nel votoDto...");
            if(vote.VoteResults == voteDto.VoteResults)
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
            List<Vote> votiApp = GetUserVotes(idUtente);
            foreach(Vote voto in votiApp) if (!ok)
            {
                if(voto.IdVictim == idUtenteVotato)
                {
                    ok = true;
                    id = voto.Id;
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

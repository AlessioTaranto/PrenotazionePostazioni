using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using prenotazioni_postazioni_api.Utilities;
using log4net;

namespace prenotazioni_postazioni_api.Repositories
{
    public class UtenteRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(UtenteRepository));

        public UtenteRepository()
        {
        }



        /// <summary>
        /// Serve per ottenere una lista completa di tutti gli utenti
        /// </summary>
        /// <returns>Lista di Utente trovati, null altrimenti</returns>
        internal List<Utente> FindAll()
        {
            string query = "SELECT * FROM Utenti;";
            return DatabaseManager<List<Utente>>.GetInstance().MakeQueryMoreResults(query);
        }
        

        /// <summary>
        /// Query al db, restituisce un utente mediante il suo id
        /// </summary>
        /// <param name="id">L'id dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal Utente FindById(int idUtente)
        {
            string query = $"SELECT * FROM Utenti WHERE idUtente = {idUtente};";
            return DatabaseManager<Utente>.GetInstance().MakeQueryOneResult(query);
        }

        /// <summary>
        /// Query al db, restituisce un utente mediante la sua email
        /// </summary>
        /// <param name="email">L'email dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal Utente FindByEmail(string email)
        {
            string query = $"SELECT * FROM Utenti WHERE email = '{email}';";
            return DatabaseManager<Utente>.GetInstance().MakeQueryOneResult(query);
        }


        /// <summary>
        /// Query al db, salva un utente al database
        /// </summary>
        /// <param name="utente">L'utente che verra salvato nel database (tabella Utenti)</param>
        internal void Save(Utente utente)
        {
            string query = $"INSERT INTO Utenti (nome, cognome, immagine, email, idRuolo) VALUES ('{utente.Nome}', '{utente.Cognome}', '{utente.Image}', '{utente.Email}', {utente.IdRuolo});";
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(query);
        }
    }
}
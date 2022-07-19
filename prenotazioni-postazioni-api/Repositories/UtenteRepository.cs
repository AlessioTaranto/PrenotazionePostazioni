using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;

namespace prenotazioni_postazioni_api.Repositories
{
    public class UtenteRepository
    {
        private readonly ILogger<UtenteRepository> logger;

        public UtenteRepository(ILogger<UtenteRepository> logger)
        {
            this.logger = logger;
        }



        /// <summary>
        /// Serve per ottenere una lista completa di tutti gli utenti
        /// </summary>
        /// <returns>Lista di Utente trovati, null altrimenti</returns>
        internal List<Utente> FindAll()
        {
            string query = "SELECT * FROM Utenti;";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Utente> utenti = JsonConvert.DeserializeObject<List<Utente>>(_databaseManager.GetAllResults(query));
            _databaseManager.DeleteConnection();
            return utenti;
        }
        

        /// <summary>
        /// Query al db, restituisce un utente mediante il suo id
        /// </summary>
        /// <param name="id">L'id dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal Utente FindById(int idUtente)
        {
            string query = $"SELECT * FROM Utenti WHERE idUtente = {idUtente};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            Utente utente = JsonConvert.DeserializeObject<Utente>(_databaseManager.GetOneResult(query));
            _databaseManager.DeleteConnection();
            return utente;
        }

        /// <summary>
        /// Query al db, restituisce un utente mediante la sua email
        /// </summary>
        /// <param name="email">L'email dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal Utente FindByEmail(string email)
        {
            string query = $"SELECT * FROM Utenti WHERE email = '{email}';";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            Utente utente = JsonConvert.DeserializeObject<Utente>(_databaseManager.GetOneResult(query));
            _databaseManager.DeleteConnection();
            return utente;
        }


        /// <summary>
        /// Query al db, salva un utente al database
        /// </summary>
        /// <param name="utente">L'utente che verra salvato nel database (tabella Utenti)</param>
        internal void Save(Utente utente)
        {
            string query = $"INSERT INTO Utenti (nome, cognome, immagine, email, idRuolo) VALUES ('{utente.Nome}', '{utente.Cognome}', '{utente.Image}', '{utente.Email}', {utente.IdRuolo});";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            _databaseManager.GetNoneResult(query);
            _databaseManager.DeleteConnection();
        }
    }
}
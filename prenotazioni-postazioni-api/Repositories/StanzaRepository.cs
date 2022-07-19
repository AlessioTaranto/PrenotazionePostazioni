using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;

namespace prenotazioni_postazioni_api.Repositories
{
    public class StanzaRepository
    {
        private readonly ILogger<StanzaRepository> logger;

        public StanzaRepository(ILogger<StanzaRepository> logger)
        {
            this.logger = logger;
        }


        /// <summary>
        /// Query al db, restituisce tutte le stanze presente nel database
        /// </summary>
        /// <returns>Lista di Stanza</returns>
        internal List<Stanza> FindAll()
        {
            string query = $"SELECT * FROM Stanze";
            return DatabaseManager<List<Stanza>>.GetInstance().MakeQueryMoreResults(query);
        }


        /// <summary>
        /// Query al db, restituisce una stanza mediante il suo id
        /// </summary>
        /// <param name="id">L'id della stanza</param>
        /// <returns>La stanza trovata, null altrimenti</returns>
        internal Stanza FindById(int idStanza)
        {
            string query = $"SELECT * FROM Stanze WHERE idStanza = {idStanza};";
            return DatabaseManager<Stanza>.GetInstance().MakeQueryOneResult(query);
        }

        /// <summary>
        /// Query al db, restituisce la stanza mediante il suo nome
        /// </summary>
        /// <param name="stanzaName">Il nome della stanza da trovare</param>
        /// <returns>La stanza trovata, null altrimenti</returns>
        internal Stanza FindByName(string stanzaName)
        {
            string query = $"SELECT * FROM Stanze WHERE UPPER(Stanze.nome) = UPPER('{stanzaName}');";
            return DatabaseManager<Stanza>.GetInstance().MakeQueryOneResult(query);
        }
        /// <summary>
        /// Query al db, aggiunge una nuova stanza alla tabella Stanze
        /// </summary>
        /// <param name="stanza">La stanza da aggiungere al db</param>
        internal void Save(Stanza stanza)
        {
            string query = $"INSERT INTO Stanze (nome, postiMax, postiMaxEmergenza) VALUES ('{stanza.Nome}', {stanza.PostiMax}, {stanza.PostiMaxEmergenza});";
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(query);
        }
    }
}

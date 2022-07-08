using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
﻿namespace prenotazioni_postazioni_api.Repositories
{
    public class StanzaRepository
    {
        private DatabaseManager _databaseManager = new DatabaseManager();
        /// <summary>
        /// Query al db, restituisce tutte le stanze presente nel database
        /// </summary>
        /// <returns>Lista di Stanza</returns>
        internal List<Stanza> FindAll()
        {
            string query = $"SELECT * FROM Stanze";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Stanza> stanze = (List<Stanza>) JsonConvert.DeserializeObject(_databaseManager.GetAllResults(query));
            _databaseManager.DeleteConnection();
            return stanze;
        }


        /// <summary>
        /// Query al db, restituisce una stanza mediante il suo id
        /// </summary>
        /// <param name="id">L'id della stanza</param>
        /// <returns>La stanza trovata, null altrimenti</returns>
        internal Stanza FindById(int idStanza)
        {
            string query = $"SELECT nome, postiMax, postiMaxEmergenza FROM Stanze WHERE idStanza = {idStanza};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            Stanza stanza = (Stanza)JsonConvert.DeserializeObject(_databaseManager.GetOneResult(query));
            _databaseManager.DeleteConnection();
            return stanza;
        }

        /// <summary>
        /// Query al db, restituisce la stanza mediante il suo nome
        /// </summary>
        /// <param name="stanzaName">Il nome della stanza da trovare</param>
        /// <returns>La stanza trovata, null altrimenti</returns>
        internal Stanza FindByName(string stanzaName)
        {
            string query = $"SELECT idStanza, postiMax, postiMaxEmergenza FROM Stanze WHERE nome = {stanzaName};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            Stanza stanza = (Stanza) JsonConvert.DeserializeObject(_databaseManager.GetOneResult(query));
            _databaseManager.DeleteConnection();
            return stanza;
        }
    }
}

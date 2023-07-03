using prenotazione_postazioni_libs.Dto;
using Newtonsoft.Json;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Repositories.Database;
using log4net;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace prenotazioni_postazioni_api.Repositories
{
    public class VoteRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(VoteRepository));

        public VoteRepository()
        {
        }



        /// <summary>
        /// Query al db, restituisce tutti i voti dell'utente che ha votato
        /// </summary>
        /// <param name="idUser">L'id dell'utente</param>
        /// <returns>Lista di voti</returns>
        internal List<Voto>? GetUserVotes(int idUser)
        {
            string query = $"SELECT * FROM Vote WHERE idUser = @idUser;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idUser", idUser);
            return DatabaseManager<List<Voto>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        /// <summary>
        /// Query al db, restituisce tutti i voti fatti ad un utente
        /// </summary>
        /// <param name="idVictim">L'id dell'utente</param>
        /// <returns>Lista di voti</returns>
        internal List<Voto>? GetVictimVotes(int idVictim)
        {
            string query = $"SELECT * FROM Vote WHERE idVictim = @idVictim;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idVictim", idVictim);
            return DatabaseManager<List<Voto>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        /// <summary>
        /// query al db, salva un voto al database
        /// </summary>
        /// <param name="vote">il voto che verra salvato nel database</param>
        internal void Add(Voto vote)
        {
            string query = $"INSERT INTO Vote (idUser, idVictim, vote) VALUES (@idUser, @idVictim, @vote);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idUser", vote.IdUtente);
            sqlCommand.Parameters.AddWithValue("@idVictim", vote.IdUtenteVotato);
            sqlCommand.Parameters.AddWithValue("@vote", vote.VotoEffettuato);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }
        /// <summary>
        /// query al db, restituisce il voto che un utente ha effettuato ad un altro utente
        /// </summary>
        /// <param name="idUser">L'utente che ha votato</param>
        /// <param name="idVictim">L'utente che e' stato votato</param>
        /// <returns>Il voto che trovato, null altrimenti</returns>
        internal Voto? GetUserVictimVote(int idUser, int idVictim)
        {
            string query = $"SELECT * FROM Vote WHERE idUser = @idUser AND idVictim = @idVictim;";
            SqlCommand sqlCommmand = new SqlCommand(query);
            sqlCommmand.Parameters.AddWithValue("@idUser", idUser);
            sqlCommmand.Parameters.AddWithValue("@idVictim", idVictim);
            return DatabaseManager<Voto>.GetInstance().MakeQueryOneResult(sqlCommmand);
        }

        /// <summary>
        /// query al db, aggiorna il voto al suo opposto
        /// </summary>
        /// <param name="vote">Il voto da aggiornare</param>
        internal void Set(Voto vote)
        {
            string query = $"UPDATE Vote SET vote = 1 ^ vote WHERE idUser = @idUser AND idVictim = @idVictim;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idUser", vote.IdUtente);
            sqlCommand.Parameters.AddWithValue("@idVictim", vote.IdUtenteVotato);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }

        internal void Delete(int idVote)
        {
            string query = $"DELETE FROM Vote WHERE id = @idVote;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idVote", idVote);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }
    }
}

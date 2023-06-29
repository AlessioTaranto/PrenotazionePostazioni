using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using log4net;
using System.Data.SqlClient;

namespace prenotazioni_postazioni_api.Repositories
{
    public class UserRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(UserRepository));

        public UserRepository()
        {
        }



        /// <summary>
        /// Serve per ottenere una lista completa di tutti gli utenti
        /// </summary>
        /// <returns>Lista di Utente trovati, null altrimenti</returns>
        internal List<Utente>? GetAll()
        {
            string query = "SELECT * FROM User;";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<Utente>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
        

        /// <summary>
        /// Query al db, restituisce un utente mediante il suo id
        /// </summary>
        /// <param name="id">L'id dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal Utente? GetById(int idUtente)
        {
            string query = $"SELECT * FROM User WHERE id = @id_utente;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_utente", idUtente);
            return DatabaseManager<Utente>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

        /// <summary>
        /// Query al db, restituisce un utente mediante la sua email
        /// </summary>
        /// <param name="email">L'email dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal Utente? GetByEmail(string email)
        {
            string query = $"SELECT * FROM User WHERE email = @email;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@email", email);
            return DatabaseManager<Utente>.GetInstance().MakeQueryOneResult(sqlCommand);
        }


        /// <summary>
        /// Query al db, salva un utente al database
        /// </summary>
        /// <param name="utente">L'utente che verra salvato nel database (tabella Utenti)</param>
        internal void Add(Utente utente)
        {
            string query = $"INSERT INTO User (name, surname, email, idRole) VALUES (@nome, @cognome, @email, @id_ruolo);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@nome", utente.Nome);
            sqlCommand.Parameters.AddWithValue("@cognome", utente.Cognome);
            sqlCommand.Parameters.AddWithValue("@image", utente.Image);
            sqlCommand.Parameters.AddWithValue("@email", utente.Email);
            sqlCommand.Parameters.AddWithValue("@id_ruolo", utente.IdRuolo);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }

        internal Utente? GetByName(string nome, string cognome)
        {
            string query = $"SELECT * FROM User WHERE (name = @nome AND surname = @cognome)";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@nome", nome);
            sqlCommand.Parameters.AddWithValue("@cognome", cognome);
            return DatabaseManager<Utente>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

        /// <summary>
        /// Query al Db, restituisce gli id degli utenti che hanno prenotato una postazione in un dato giorno
        /// </summary>
        /// <param name="date"></param>
        /// <returns>List di Utente contenenti solo gli Id</returns>
        internal List<Utente>? GetAllByDate(DateTime date)
        {
            string query = $"SELECT idUser FROM Booking WHERE YEAR(startDate) = @year AND MONTH(startDate) = @month AND DAY(startDate) = {date.Day};";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@year", date.Year);
            sqlCommand.Parameters.AddWithValue("@month",date.Month);
            sqlCommand.Parameters.AddWithValue("@day", date.Day);
            return DatabaseManager<List<Utente>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
    }
}
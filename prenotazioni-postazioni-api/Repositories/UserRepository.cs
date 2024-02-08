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
        internal List<User>? GetAll()
        {
            string query = "SELECT * FROM [Users];";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<User>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

         /// <summary>
        /// Query al db, restituisce gli utenti con il loro ruolo
        /// </summary>
        /// <returns>Lista di Utente con i loro ruoli trovati, null altrimenti</returns>
        internal List<UserRole>? GetAllWithRole()
        {
            string query = $"SELECT u.name AS 'username', u.surname, u.id, u.email, r.name AS 'rolename' FROM [Users] u JOIN Role r ON u.idRole = r.id;";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<UserRole>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
        

        /// <summary>
        /// Query al db, restituisce un utente mediante il suo id
        /// </summary>
        /// <param name="id">L'id dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal User? GetById(int idUser)
        {
            string query = $"SELECT * FROM [Users] WHERE id = @idUser;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idUser", idUser);
            return DatabaseManager<User>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

        /// <summary>
        /// Query al db, restituisce un utente mediante la sua email
        /// </summary>
        /// <param name="email">L'email dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal User? GetByEmail(string email)
        {
            string query = $"SELECT * FROM [Users] WHERE email = @email;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@email", email);
            return DatabaseManager<User>.GetInstance().MakeQueryOneResult(sqlCommand);
        }


        /// <summary>
        /// Query al db, salva un utente al database
        /// </summary>
        /// <param name="utente">L'utente che verra salvato nel database (tabella Utenti)</param>
        internal void Add(User utente)
        {
            string query = $"INSERT INTO [Users] (name, surname, email, idRole, image) VALUES (@name, @surname, @email, @idRole, @image);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@name", utente.Name);
            sqlCommand.Parameters.AddWithValue("@surname", utente.Surname);
            sqlCommand.Parameters.AddWithValue("@email", utente.Email);
            sqlCommand.Parameters.AddWithValue("@idRole", utente.IdRole);
            sqlCommand.Parameters.AddWithValue("@image", utente.Image);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }

        internal User? GetByName(string name, string surname)
        {
            string query = $"SELECT * FROM [Users] WHERE (name = @name AND surname = @surname)";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@name", name);
            sqlCommand.Parameters.AddWithValue("@surname", surname);
            return DatabaseManager<User>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

        /// <summary>
        /// Query al Db, restituisce gli id degli utenti che hanno prenotato una postazione in un dato giorno
        /// </summary>
        /// <param name="date"></param>
        /// <returns>List di Utente contenenti solo gli Id</returns>
        internal List<Booking>? GetAllByDate(DateTime date)
        {
            string query = $"SELECT * FROM Booking WHERE YEAR(startDate) = @year AND MONTH(startDate) = @month AND DAY(startDate) = @day;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@year", date.Year);
            sqlCommand.Parameters.AddWithValue("@month",date.Month);
            sqlCommand.Parameters.AddWithValue("@day", date.Day);
            return DatabaseManager<List<Booking>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
    }
}
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using System.Data.SqlClient;
using prenotazioni_postazioni_api.Exceptions;
using log4net;
using Microsoft.AspNetCore.Rewrite;

namespace prenotazioni_postazioni_api.Repositories
{
    public class RoleRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(RoleRepository));

        public RoleRepository()
        {
        }
        public List<Role>? GetAll()
        {
            string query = $"SELECT * FROM Roles;";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<Role>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        /// <summary>
        /// Query al db, restituisce il ruolo dell'utente associato usando il suo ID
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Ruolo trovato, null altrimenti</returns>
        public Role? GetById(int idRole)
        {
            string query = $"SELECT * FROM Roles WHERE id = {idRole};";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<Role>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

        /// <summary>
        /// Query al db, restituisce il ruolo di un utente mediante il suo id
        /// </summary>
        /// <param name="idUser">L'id dell'utente che servira per trovare il suo ruolo</param>
        /// <returns>Ruolo dell'utente, null altrimenti</returns>
        public Role? GetByUser(int idUser)
        {
            string query = $"SELECT * FROM [Users] WHERE id = @idUser;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idUser", idUser);
            User? utente = DatabaseManager<User>.GetInstance().MakeQueryOneResult(sqlCommand);
            if(utente == null)
            {
                throw new PrenotazionePostazioniApiException("IdUtente non trovato");
            }
            query = $"SELECT * FROM Roles WHERE id = @idRole;";
            sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idRole", utente.IdRole);
            return DatabaseManager<Role>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

        public Role? GetByName(string name)
        {
            string query = $"SELECT * FROM Roles WHERE name = @name;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@name", name);
            return DatabaseManager<Role>.GetInstance().MakeQueryOneResult(sqlCommand);
        }
        /// <summary>
        /// Query al db, switch il ruolo accesso impostazioni dell'utente
        /// </summary>
        /// <param name="idUser">L'id dell'utente che gli verra cambiato il ruolo</param>
        /// <param name="idRole">Il ruolo con cui verra aggiornato l'utente</param>
        internal void UpdateUserRole(int idUser, int idRole)
        {
            string query = $"UPDATE [Users] SET idRole = @idRole WHERE id = @idUser;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idRole", idRole);
            sqlCommand.Parameters.AddWithValue("@idUser", idUser);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }
    }
}

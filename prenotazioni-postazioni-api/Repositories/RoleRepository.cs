using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using System.Data.SqlClient;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Repositories
{
    public class RoleRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(RoleRepository));

        public RoleRepository()
        {
        }

        /// <summary>
        /// Query al db, restituisce il ruolo dell'utente associato usando il suo ID
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Ruolo trovato, null altrimenti</returns>
        public Ruolo? GetById(int idRole)
        {
            string query = $"SELECT * FROM Role WHERE id = {idRole};";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<Ruolo>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

        /// <summary>
        /// Query al db, restituisce il ruolo di un utente mediante il suo id
        /// </summary>
        /// <param name="idUser">L'id dell'utente che servira per trovare il suo ruolo</param>
        /// <returns>Ruolo dell'utente, null altrimenti</returns>
        public Ruolo? GetByUser(int idUser)
        {
            string query = $"SELECT * FROM User WHERE id = @idUser;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idUser", idUser);
            Utente? utente = DatabaseManager<Utente>.GetInstance().MakeQueryOneResult(sqlCommand);
            if(utente == null)
            {
                throw new PrenotazionePostazioniApiException("IdUtente non trovato");
            }
            query = $"SELECT * FROM Role WHERE id = @idRole;";
            sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idRole", utente.IdRuolo);
            return DatabaseManager<Ruolo>.GetInstance().MakeQueryOneResult(sqlCommand);
        }
        /// <summary>
        /// Query al db, switch il ruolo accesso impostazioni dell'utente
        /// </summary>
        /// <param name="idUser">L'id dell'utente che gli verra cambiato il ruolo</param>
        /// <param name="idRole">Il ruolo con cui verra aggiornato l'utente</param>
        internal void UpdateUserRole(int idUser, int idRole)
        {
            string query = $"UPDATE User SET idRole = @idRole WHERE id = @idUser;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idRole", idRole);
            sqlCommand.Parameters.AddWithValue("@idUser", idUser);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }
    }
}

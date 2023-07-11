using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using log4net;
using System.Data.SqlClient;
namespace prenotazioni_postazioni_api.Repositories
{
    public class MenuChoicesRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(MenuChoicesRepository));

        public MenuChoicesRepository() {}
        
        internal List<MenuChoices>? GetAll()
        {
            string query = "SELECT * FROM MenuChoices;";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<MenuChoices>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        internal MenuChoices? GetById(int id)
        {
            string query = "SELECT * FROM MenuChoices WHERE id = @id;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id", id);
            return DatabaseManager<MenuChoices>.GetInstance().MakeQueryOneResult(sqlCommand);
        }
        internal MenuChoices? GetByIdMenu(int idMenu)
        {
            string query = "SELECT * FROM MenuChoices WHERE idMenu = @idMenu;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idMenu", idMenu);
            return DatabaseManager<MenuChoices>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
        internal MenuChoices? GetByUserAndIdMenu(int idMenu, int idUser)
        {
            string query = "SELECT * FROM MenuChoices WHERE ((idMenu = @idMenu)AND(idUser = @idUser));";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idMenu", idMenu);
            sqlCommand.Parameters.AddWithValue("@idUser", idUser);
            return DatabaseManager<MenuChoices>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

        internal void Add(MenuChoices menuChoices)
        {
            string query = $"INSERT INTO MenuChoices (idMenu, choice, idUser) VALUES (@idMenu, @choice, @idUser);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idMenu", menuChoices.IdMenu);
            sqlCommand.Parameters.AddWithValue("@choice", menuChoices.Choice);
            sqlCommand.Parameters.AddWithValue("@idUser", menuChoices.IdUser);
            DatabaseManager<object>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        internal void DeleteByUserAndIdMenu(int idMenu, int idUser)
        {
            string query = $"DELETE FROM MenuChoices WHERE ((idMenu = @idMenu)AND(idUser = @idUser));";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idMenu", idMenu);
            sqlCommand.Parameters.AddWithValue("@idUser", idUser);
            DatabaseManager<object>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }



    }
}

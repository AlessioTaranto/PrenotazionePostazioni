using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using log4net;
using System.Data.SqlClient;
namespace prenotazioni_postazioni_api.Repositories
{
    public class MenuRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(MenuRepository));

        public MenuRepository(){}

        internal List<Menu>? GetAll() 
        {
            string query = "SELECT * FROM Menu;";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<Menu>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
        
        internal Menu? GetByDate(DateOnly day) {
            string query = "SELECT * FROM Menu WHERE day = @day;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@day", day.ToString("yyyy-MM-dd"));
            return DatabaseManager<Menu>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
        internal Menu? GetById(int id)
        {
            string query = "SELECT * FROM Menu WHERE id = @id;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id", id.ToString("yyyy-MM-dd"));
            return DatabaseManager<Menu>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
        internal void Add(Menu menu)
        {
            string query = $"INSERT INTO Menu (day, image) VALUES (@day, @image);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@day", menu.Day.ToString("yyyy-MM-dd"));
            sqlCommand.Parameters.AddWithValue("@image", menu.Image.ToString());
            DatabaseManager<object>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        internal void Delete(DateOnly day) 
        {
            string query = $"DELETE FROM Menu WHERE day = @day;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@day", day.ToString("yyyy-MM-dd"));
            DatabaseManager<object>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

    }
}

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
            string query = $"SELECT * FROM Menu;";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<Menu>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
        
        internal Menu? GetByDate(DateTime date) {
            string query = $"SELECT * FROM Menu WHERE date = @date;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-ddTHH:mm:ss"));
            return DatabaseManager<Menu>.GetInstance().MakeQueryOneResult(sqlCommand);
        }
        internal Menu? GetById(int id)
        {
            string query = $"SELECT * FROM Menu WHERE id = @id;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id", id);
            return DatabaseManager<Menu>.GetInstance().MakeQueryOneResult(sqlCommand);
        }
        internal void Add(Menu menu)
        {
            string query = $"INSERT INTO Menu (date, image) VALUES (@date, @image);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@date", menu.Date.ToString("yyyy-MM-ddTHH:mm:ss"));
            sqlCommand.Parameters.AddWithValue("@image", menu.Image);
            DatabaseManager<object>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        internal void Delete(DateTime date) 
        {
            string query = $"DELETE FROM Menu WHERE date = @date;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-ddTHH:mm:ss"));
            DatabaseManager<object>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

    }
}

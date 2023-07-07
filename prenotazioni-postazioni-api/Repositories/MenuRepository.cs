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

        internal List<Menu> GetAll() 
        {
            string query = "SELECT * FROM Menu;";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<Menu>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
        
        internal Menu? GetByDate(DateOnly day) {
            string query = "SELECT * FROM Menu WHERE giorno = @giorno;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@giorno", day.ToString("yyyy-MM-dd"));
            return DatabaseManager<Menu>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

    }
}

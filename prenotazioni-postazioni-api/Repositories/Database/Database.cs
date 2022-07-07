
using System;
using System.Text;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace prenotazioni_postazioni_api.Repositories.Database
{
    public class Database
    {

        public static string DatabaseName { get; } = "[prenotazioni-impostazioni].dbo";
        public static string DefaultInitialCatalog { get; } = "prenotazioni-impostazioni";
        public static string DefaultDataSource { get; } = "LTP040";

        private SqlConnection? _conn;

        /// <summary>
        /// Instaura una connesione al database
        /// </summary>
        /// <param name="initialCatalog">Nome del database</param>
        /// <param name="datasource">Nome del server sql</param>
        /// <param name="integratedSecurity">integrated security</param>
        public void CreateConnectionToDatabase(string initialCatalog, string datasource, bool integratedSecurity)
        {
            SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder();
            connBuilder.InitialCatalog = initialCatalog ?? DefaultInitialCatalog;
            connBuilder.DataSource = datasource ?? DefaultDataSource;
            connBuilder.IntegratedSecurity = integratedSecurity;
            //Insert example: INSEùRT INTO "+db+ ".Ruoli (idRuolo, descRuolo, accessoImpostazioni) VALUES ("+0+",'Amministratore',"+0+")
            //using (SqlConnection conn = new SqlConnection(connBuilder.ToString()))
            //{
            //    SqlCommand cmd = new SqlCommand(query, conn);

            //    conn.Open();

            //    var obj = cmd.ExecuteScalar();

            //    conn.Close();
            //}


            _conn = new SqlConnection(connBuilder.ToString());
        }

        /// <summary>
        /// Viene usato per restituire la prima colonna trovata
        /// </summary>
        /// <param name="query">la query al db</param>
        /// <returns>Json con il dato trovato</returns>
        public string? GetOneResult(string query)
        {
            if (!checkConnectionDatabase())
            {
                return null;
            }
            using (var conn = _conn)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                string jsonResult = JsonConvert.SerializeObject(cmd.ExecuteScalar());
                conn.Close();
                return jsonResult;
            }
        }


        /// <summary>
        /// Viene usato per restituire tutte le colonne trovate
        /// </summary>
        /// <param name="query">La query al db</param>
        /// <returns></returns>
        public string? GetAllResults(string query)
        {
            if (!checkConnectionDatabase())
            {
                return null;
            }
            string jsonResults;
            using (var conn = _conn)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                var values = new List<Dictionary<string, object>>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    do
                    {
                        while (reader.Read())
                        {
                            var fieldValues = new Dictionary<string, object>();              
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                fieldValues.Add(reader.GetName(i), reader[i]);
                            }
                            values.Add(fieldValues);
                        }
                    } while (reader.NextResult());
                }
                conn.Close();
                jsonResults = JsonConvert.SerializeObject(values);
                return jsonResults;
            }
        }

        /// <summary>
        /// Viene usato quando la query non prevede nessun dato in ritorno
        /// </summary>
        /// <param name="query">La query al db</param>
        public void GetNoneResult(string query)
        {
            if (!checkConnectionDatabase())
            {
                return;
            }
            using (var conn = _conn)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        private bool checkConnectionDatabase()
        {
            return _conn != null;
        }
    }
}

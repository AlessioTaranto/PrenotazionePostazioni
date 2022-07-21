
using System;
using System.Text;
using log4net;
using System.Data.SqlClient;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Exceptions;

namespace prenotazioni_postazioni_api.Repositories.Database
{
    public class DatabaseManager<T>
    {
        public static string DatabaseName { get; } = "[prenotazioni-impostazioni].dbo";
        public static string DefaultInitialCatalog { get; } = "prenotazioni-impostazioni";
        public static string DefaultDataSource { get; } = "LTP040";
        public readonly static string DEFAULT_DATABASE_NAME_STRING = "[prenotazioni - impostazioni].dbo";

        private SqlConnection? _conn;
        private ILog logger = LogManager.GetLogger(typeof(DatabaseManager<T>));

        /// <summary>
        /// Costruttore vuoto per creare istanze
        /// </summary>
        private DatabaseManager()
        {

        }


        public T? MakeQueryOneResult(SqlCommand sqlCommand)
        {
            logger.Info("Mi connetto al database...");
            CreateConnectionToDatabase(null, null, true);
            logger.Info("faccio una query al db");
            T? value = JsonConvert.DeserializeObject<T>(GetOneResult(sqlCommand));
            logger.Info("Ho prelevato tutte le informazioni dal db con successo!");
            logger.Info("mi disconnetto dal db");
            DeleteConnection();
            return value;
        }

        public T? MakeQueryMoreResults(SqlCommand sqlCommand)
        {
            logger.Info("Mi connetto al database...");
            CreateConnectionToDatabase(null, null, true);
            logger.Info("faccio una query al db");
            T? value = JsonConvert.DeserializeObject<T>(GetAllResults(sqlCommand));
            logger.Info("Ho prelevato tutte le informazioni dal db con successo!");
            logger.Info("mi disconnetto dal db");
            DeleteConnection();
            return value;
        }

        public void MakeQueryNoResult(SqlCommand sqlCommand)
        {
            logger.Info("Mi connetto al database...");
            CreateConnectionToDatabase(null, null, true);
            logger.Info("faccio una query al db");
            GetNoneResult(sqlCommand);
            logger.Info("mi disconnetto dal db");
            DeleteConnection();
        }


        public static DatabaseManager<T> GetInstance()
        {
            return new DatabaseManager<T>();
        }

        /// <summary>
        /// Instaura una connesione al database
        /// </summary>
        /// <param name="initialCatalog">Nome del database</param>
        /// <param name="datasource">Nome del server sql</param>
        /// <param name="integratedSecurity">integrated security</param>
        private void CreateConnectionToDatabase(string? initialCatalog, string? datasource, bool integratedSecurity)
        {
            //significa che _conn ha gia un'istanza di SqlConnection
            logger.Info("Controllo se la connessione e' gia esistente...");
            if (checkConnectionDatabase())
            {
                logger.Fatal("Connessione gia esistente. [Forse ti sei dimenticato di chiuderla?]");
                throw new Exception("Errore interno. Connessione gia aperta.");
            }
            logger.Info("Connessione non esistente");
            SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder();
            logger.Info("Inizializzo la connection string...");
            connBuilder.ConnectionString = DatabaseInfo.DefaultConnectionString;
            logger.Info("Creo una nuova connessione...");
            _conn = new SqlConnection(connBuilder.ToString());
            logger.Info("Creazione della connessione fatta con successo!");
        }

        private void DeleteConnection()
        {
            logger.Info("Elimino la connessione");
            _conn = null;
        }

        /// <summary>
        /// Viene usato per restituire la prima colonna trovata
        /// </summary>
        /// <param name="query">la query al db</param>
        /// <returns>Json con il dato trovato</returns>

        private string GetOneResult(SqlCommand cmd)
        {
            logger.Info("Controllo se la connessione con il database e' stata stabilita...");
            if (!checkConnectionDatabase())
            {
                logger.Fatal("La connessione con il database NON e' stata stabilita!");
                throw new DatabaseException("Database connection not set");
            }
            logger.Info("La connessione con il database e' stabilita con successo");
            using (var conn = _conn)
            {
                cmd.Connection = conn;
                logger.Info("Apro una connessione");
                conn.Open();
                var reader = cmd.ExecuteReader();
                logger.Info("Eseguisco la query al db...");
                IEnumerable<Dictionary<string, object>> result = Serialize(reader);
                logger.Info("Serializzo il risultato in json...");
                string jsonResult = JsonConvert.SerializeObject(result);
                logger.Info("Serializzazione in json completata con successo!");
                logger.Info("Disconnessione dal database...");
                conn.Close();
                logger.Info("Disconnessione dal database fatto con successo!");
                jsonResult = jsonResult.Replace("[", "").Replace("]", "");
                return jsonResult;
            }
        }


        
        protected IEnumerable<Dictionary<string, object>> Serialize(SqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
                cols.Add(reader.GetName(i));

            while (reader.Read())
                results.Add(SerializeRow(cols, reader));

            return results;
        }
        protected Dictionary<string, object> SerializeRow(IEnumerable<string> cols,
                                                        SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }

        /// <summary>
        /// Viene usato per restituire tutte le colonne trovate
        /// </summary>
        /// <param name="query">La query al db</param>
        /// <returns></returns>
        private string GetAllResults(SqlCommand cmd)
        {
            logger.Info("Controllo se la connessione e' gia esistente...");
            if (!checkConnectionDatabase())
            {
                logger.Fatal("La connessione e' gia esistente. [Forse ti sei dimenticato di chiuderla?]");
                throw new DatabaseException("Database connection not set");
            }
            using (var conn = _conn)
            {
                cmd.Connection = conn;
                logger.Info("Apro una connessione al database...");
                conn.Open();
                logger.Info("Connessione aperta.");
                var values = new List<Dictionary<string, object>>();
                logger.Info("Eseguisco la query...");
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
                logger.Info("Query eseguita con successo!");
                logger.Info("Mi disconnetto dal database...");
                conn.Close();
                logger.Info("Disconnessione dal database effettuata con successo!");
                logger.Info("Conversione il risultato della query in json...");
                var json = JsonConvert.SerializeObject(values);
                logger.Info("Conversione in json effettuata con successo!");
                return json;

            }
        }

        /// <summary>
        /// Viene usato quando la query non prevede nessun dato in ritorno
        /// </summary>
        /// <param name="query">La query al db</param>
        private void GetNoneResult(SqlCommand cmd)
        {
            logger.Info("Controllo se la connessione e' gia esistente...");
            if (!checkConnectionDatabase())
            {
                logger.Fatal("La connessione e' gia esistente. [Forse ti sei dimenticato di chiuderla?]");
                throw new DatabaseException("Database connection not set");
            }
            logger.Info("Connessione non esistente");
            using (var conn = _conn)
            {
                cmd.Connection = conn;
                logger.Info("Stabilisco una connessione al database...");
                conn.Open();
                logger.Info("Eseguisco la query...");
                cmd.ExecuteNonQuery();
                logger.Info("Query eseguita con successo!");
                logger.Info("Chiudo la connessione...");
                conn.Close();
                logger.Info("Connessione chiusa con successo!");
            }
        }
        private bool checkConnectionDatabase()
        {
            if (_conn == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
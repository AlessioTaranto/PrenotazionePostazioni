using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using prenotazioni_postazioni_api.Utilities;
using log4net;

namespace prenotazioni_postazioni_api.Repositories
{
    public class PrenotazioneRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(PrenotazioneRepository));

        public PrenotazioneRepository() { }

        /// <summary>
        /// Query al db per restitire una Prenotazione in base al suo Id
        /// </summary>
        /// <param name="idPrenotazione">Id della Prenotazione</param>
        /// <returns>Prenotazione</returns>
        internal Prenotazione FindById(int idPrenotazione)
        {

            string query = $"SELECT * FROM Prenotazioni WHERE idPrenotazione = {idPrenotazione};";
            return DatabaseManager<Prenotazione>.GetInstance().MakeQueryOneResult(query);
        }
        /// <summary>
        /// Query al db per restituire una Prenotazione in base all'id della stanza associata
        /// </summary>
        /// <param name="idStanza">L'Id della stanza associata alla Prenotazione</param>
        /// <returns>Lista di Prenotazione</returns>
        internal List<Prenotazione> FindByStanza(int idStanza)
        {
            string query = $"SELECT * FROM Prenotazioni WHERE idStanza = {idStanza};";
            return DatabaseManager<List<Prenotazione>>.GetInstance().MakeQueryMoreResults(query);
        }

        /// <summary>
        /// Query al db, per trovare tutte le prenotazioni esistenti al database
        /// </summary>
        /// <returns>Lista di Prenotazione</returns>
        internal List<Prenotazione> FindAll()
        {
            string query = "SELECT * FROM Prenotazioni;";
            return DatabaseManager<List<Prenotazione>>.GetInstance().MakeQueryMoreResults(query);
        }
        /// <summary>
        /// Query al db per restituire una Prenotazione in base all'id dell'utente
        /// </summary>
        /// <param name="idUtente">L'id dell'utente associata alla Prenotazione</param>
        /// <returns>Prenotazione</returns>
        internal List<Prenotazione> FindByUtente(int idUtente)
        {
            string query = $"SELECT * FROM Prenotazioni WHERE idUtente = {idUtente};";
            return DatabaseManager<List<Prenotazione>>.GetInstance().MakeQueryMoreResults(query);

        }

        /// <summary>
        /// Query al db, aggiunge una nuova prenotazione nella tabella Prenotazioni
        /// </summary>
        /// <param name="prenotazione">La prenotazione da aggiungere al database</param>
        internal void Save(Prenotazione prenotazione)
        {
            string query = $"INSERT INTO Prenotazioni (startDate, endDate, idStanza, idUtente) VALUES ('{prenotazione.StartDate.ToString("yyyy-MM-ddTHH:mm:ss")}', '{prenotazione.EndDate.ToString("yyyy-MM-ddTHH:mm:ss")}', {prenotazione.IdStanza}, {prenotazione.IdUtente});";
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(query);
        }


        /// <summary>
        /// Query al db, restituisce una lista di prenotazione mediante la stanza e l'intervallo di tempo
        /// </summary>
        /// <param name="idStanza">L'id della stanza dove sono effettuate delle prenotazioni</param>
        /// <param name="date">La data delle prenotazioni</param>
        /// <returns></returns>
        internal List<Prenotazione> FindAllByIdStanzaAndDate(int idStanza, DateTime startDate, DateTime endDate)
        {
            string start = startDate.ToString("yyyy-MM-ddTHH:mm:ss");
            string end = endDate.ToString("yyyy-MM-ddTHH:mm:ss");
            string query = $"SELECT * FROM dbo.Prenotazioni WHERE((idStanza = {idStanza})AND((startDate BETWEEN '{start}' AND '{end}') OR(endDate BETWEEN '{start}' AND '{end}') OR (DATEDIFF(HH, startDate, '{start}') >= 0 AND DATEDIFF(HH, endDate, '{end}') <= 0)))";
            return DatabaseManager<List<Prenotazione>>.GetInstance().MakeQueryMoreResults(query);
        }





    }
}

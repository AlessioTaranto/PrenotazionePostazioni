
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Prenotazione
    {

        public int IdPrenotazioni { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Stanza Stanza { get; set; }
        public Utente Utente { get; set; }
        private Exception ModelException { get; set; }
        public bool IsValid { get; set; } = false;

        public Prenotazione(int idPrenotazioni, DateTime startDate, DateTime endDate, Stanza stanza, Utente utente)
        {
            IdPrenotazioni = idPrenotazioni;
            StartDate = startDate;
            EndDate = endDate;
            Stanza = stanza;
            Utente = utente;
        }

        public Prenotazione(DateTime startDate, DateTime endDate, Stanza stanza, Utente utente)
        {
            StartDate = startDate;
            EndDate = endDate;
            Stanza = stanza;
            Utente = utente;
        }

        public Prenotazione()
        {
        }

        public void Validate()
        {
            try
            {
                if (StartDate.CompareTo(EndDate) > 0) throw new Exception("La data di inizio non può essere più grande di quella di fine");
                this.IsValid = true;
            }
            catch (Exception ex)
            {
                this.ModelException = ex;
                this.IsValid = false;
            }
        }
    }
}

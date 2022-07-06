using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    internal class Prenotazione
    {

        private int idPrenotazioni { get; set; }
        private DateOnly date { get; set; }
        private int idStanza { get; set; }
        private int idUtente { get; set; }

        public Prenotazione(int idPrenotazioni, DateOnly date, int idStanza, int idUtente)
        {
            this.idPrenotazioni = idPrenotazioni;
            this.date = date;
            this.idStanza = idStanza;
            this.idUtente = idUtente;
        }

        public Prenotazione()
        {
        }
    }
}

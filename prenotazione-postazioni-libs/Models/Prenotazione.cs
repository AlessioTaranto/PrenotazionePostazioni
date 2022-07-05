using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    internal class Prenotazione
    {

        private int idPrenotazioni;
        private DateOnly date;
        private int idStanza;
        private int idUtente;

        public int IdPrenotazioni { get => idPrenotazioni; set => idPrenotazioni = value; }
        public DateOnly Date { get => date; set => date = value; }
        public int IdStanza { get => idStanza; set => idStanza = value; }
        public int IdUtente { get => idUtente; set => idUtente = value; }
    }
}

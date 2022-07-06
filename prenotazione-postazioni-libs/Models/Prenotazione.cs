
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class PrenotazioneDto
    {

        public int IdPrenotazioni { get; set; }
        public DateOnly Date { get; set; }
        public int IdStanza { get; set; }
        public int IdUtente { get; set; }

        public PrenotazioneDto(int idPrenotazioni, DateOnly date, int idStanza, int idUtente)
        {
            this.IdPrenotazioni = idPrenotazioni;
            this.Date = date;
            this.IdStanza = idStanza;
            this.IdUtente = idUtente;
        }

        public PrenotazioneDto()
        {
        }
    }
}

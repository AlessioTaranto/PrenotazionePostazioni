
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class PrenotazioneDto
    {
        public DateOnly Date { get; set; }
        public int IdStanza { get; set; }
        public int IdUtente { get; set; }

        public PrenotazioneDto(DateOnly date, int idStanza, int idUtente)
        {
            Date = date;
            IdStanza = idStanza;
            IdUtente = idUtente;
        }

        public PrenotazioneDto()
        {
        }
    }
}

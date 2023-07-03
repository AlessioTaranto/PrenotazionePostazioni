using prenotazione_postazioni_libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class VotoDto
    {
        public int IdUtente { get; set; }
        public int IdUtenteVotato { get; set; }
        public bool VotoEffettuato { get; set; }

        public VotoDto(int idUtente, int idUutenteVotato, bool votoEffettuato)
        {
            this.IdUtente = idUtente;
            this.IdUtenteVotato = idUutenteVotato;
            this.VotoEffettuato = votoEffettuato;
        }

     
    }
}

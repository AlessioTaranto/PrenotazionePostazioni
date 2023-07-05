using prenotazione_postazioni_libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class VoteDto
    {
        public int IdUser { get; set; }
        public int IdVictim { get; set; }
        public bool VoteResults { get; set; }

        public VoteDto(int idUtente, int idUutenteVotato, bool votoEffettuato)
        {
            this.IdUser = idUtente;
            this.IdVictim = idUutenteVotato;
            this.VoteResults = votoEffettuato;
        }

     
    }
}

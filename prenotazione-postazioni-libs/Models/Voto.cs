using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    internal class Voto
    {

        private int idVoto;
        private int idUtente;
        private int idUtenteVotato;
        private bool votoEffettuato;

        public int IdVoto { get => idVoto; set => idVoto = value; }
        public int IdUtente { get => idUtente; set => idUtente = value; }
        public int IdUtenteVotato { get => idUtenteVotato; set => idUtenteVotato = value; }
        public bool VotoEffettuato { get => votoEffettuato; set => votoEffettuato = value; }
    }
}

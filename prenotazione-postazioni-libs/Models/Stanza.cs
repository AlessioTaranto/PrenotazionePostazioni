using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    internal class Stanza
    {

        private int idStanza;
        private string nome;
        private int postiMax;
        private int postiMaxEmergenza;

        public int IdStanza { get => idStanza; set => idStanza = value; }
        public string Nome { get => nome; set => nome = value; }
        public int PostiMax { get => postiMax; set => postiMax = value; }
        public int PostiMaxEmergenza { get => postiMaxEmergenza; set => postiMaxEmergenza = value; }
    }
}

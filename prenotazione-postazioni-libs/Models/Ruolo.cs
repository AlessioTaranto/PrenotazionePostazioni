using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    internal class Ruolo
    {

        private int idRuolo;
        private string descRuolo;
        private bool accessoImpostazioni;

        public int IdRuolo { get => idRuolo; set => idRuolo = value; }
        public string DescRuolo { get => descRuolo; set => descRuolo = value; }
        public bool AccessoImpostazioni { get => accessoImpostazioni; set => accessoImpostazioni = value; }
    }
}

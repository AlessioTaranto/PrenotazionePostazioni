using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Festa
    {
        public int IdFesta { get; set; }
        public DateTime Giorno { get; set; }
        public string? Descrizione { get; set; }

        public Exception ModelException = new Exception();
        public bool IsValid { get; set; } = true;

        public Festa(DateTime date, string? desc)
        {
            this.Giorno = date;
            this.Descrizione = desc;
        }
        public Festa(int idFestivita, DateTime date, string? desc)
        {
            IdFesta = idFestivita;
            Giorno = date;
            Descrizione = desc;
        }
        public Festa(DateTime date)
        {
            this.Giorno = date;
        }

        public Festa()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Festivita
    {
        public int IdFestivita { get; set; }
        public DateOnly Date { get; set; }
        public string? Desc { get; set; }

        public Exception ModelException;
        public bool IsValid { get; set; } = false;

        public Festivita()
        {

        }
        public Festivita(DateOnly date, string? desc)
        {
            this.Date = date;
            this.Desc = desc;
            this.Validate();
        }
        public Festivita(int idFestivita, DateOnly date, string? desc)
        {
            IdFestivita = idFestivita;
            Date = date;
            Desc = desc;
            this.Validate();
        }
        public Festivita(DateOnly date)
        {
            this.Date = date;
            this.Validate();
        }


        private void Validate()
        {
            try
            {
                if (this.Date == null)
                    throw new Exception("Date non puo essere null");
                this.IsValid = true;
            }
            catch (Exception ex)
            {
                this.IsValid = false;
                this.ModelException = ex;
            }

        }
    }
}

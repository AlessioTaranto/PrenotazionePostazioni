
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public enum RuoloType
    {
        Admin = 1,
        Utente = 2,
    }
    public class Ruolo
    {

        public int IdRuolo { get; set; }
        public string? DescRuolo { get; set; }
        public bool AccessoImpostazioni { get; set; }

        private Exception ModelException { get; set; } = new Exception();
        public bool IsValid { get; set; } = false;

        public Ruolo() { }
        public Ruolo(int idRuolo, string descRuolo, bool accessoImpostazioni)
        {
            this.IdRuolo = idRuolo;
            this.DescRuolo = descRuolo;
            this.AccessoImpostazioni = accessoImpostazioni;

            this.Validate();
        }

        public Ruolo(string descRuolo, bool accessoImpostazioni)
        {
            DescRuolo = descRuolo;
            AccessoImpostazioni = accessoImpostazioni;

            this.Validate();
        }

        public void Validate()
        {
            try
            {
                if (this.DescRuolo == null)
                    throw new Exception("La descrizione del ruolo non può essere nulla");

                this.IsValid = true;
            }
            catch (Exception e)
            {
                this.ModelException = e;
                this.IsValid = false;
            }
        }

    }
}
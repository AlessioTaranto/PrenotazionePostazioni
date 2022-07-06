using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    internal class Ruolo
    {

        private int idRuolo { get; set; }
        private string descRuolo { get; set; }
        private bool accessoImpostazioni { get; set; }

        private Exception modelExceprion { get; set; }
        private bool isValid { get; set; } = false;

        public Ruolo(int idRuolo, string descRuolo, bool accessoImpostazioni)
        {
            this.idRuolo = idRuolo;
            this.descRuolo = descRuolo;
            this.accessoImpostazioni = accessoImpostazioni;

            this.Validate();
        }

        public Ruolo()
        {
        }

        public void Validate()
        {
            try
            {
                if (this.descRuolo == null)
                    throw new Exception("La descrizione del ruolo non può essere nulla");

                this.isValid = true;
            }
            catch (Exception e)
            {
                this.modelExceprion = e;
                this.isValid = false;
            }
        }

    }
}

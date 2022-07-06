using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    internal class Stanza
    {

        private int idStanza { get; set; }
        private string nome { get; set; }
        private int postiMax { get; set; }
        private int postiMaxEmergenza { get; set; }


        private Exception modelExceprion { get; set; }
        private bool isValid { get; set; } = false;

        public Stanza(int idStanza, string nome, int postiMax, int postiMaxEmergenza)
        {
            this.idStanza = idStanza;
            this.nome = nome;
            this.postiMax = postiMax;
            this.postiMaxEmergenza = postiMaxEmergenza;

            this.Validate();
        }

        public Stanza()
        {
        }

        public void Validate()
        {
            try
            {
                if (this.postiMax <= 0)
                    throw new Exception("I posti devono essere un numero maggiore di 0");

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    internal class Voto
    {

        private int idVoto { get; set; }
        private int idUtente { get; set; }
        private int idUtenteVotato { get; set; }
        private bool votoEffettuato { get; set; }

        private Exception modelExceprion { get; set; }
        private bool isValid { get; set; } = false;

        public Voto(int idVoto, int idUtente, int idUtenteVotato, bool votoEffettuato)
        {
            this.idVoto = idVoto;
            this.idUtente = idUtente;
            this.idUtenteVotato = idUtenteVotato;
            this.votoEffettuato = votoEffettuato;

            this.Validate();
        }

        public Voto()
        {
        }

        public void Validate()
        {
            try
            {
                if (this.idUtente == this.idUtenteVotato)
                    throw new Exception("L'id dell'utente votato non può essere lo stesso dell'utente che vota");

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

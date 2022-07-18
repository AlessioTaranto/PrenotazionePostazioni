using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Voto
    {

        public int IdVoto { get; set; }
        public Utente Utente { get; set; }
        public Utente UtenteVotato { get; set; }
        public bool VotoEffettuato { get; set; }

        private Exception ModelException { get; set; }
        public bool IsValid { get; set; } = false;

        public Voto(int idVoto, Utente utente, Utente utenteVotato, bool votoEffettuato)
        {
            this.IdVoto = idVoto;
            this.Utente = utente;
            this.UtenteVotato = utenteVotato;
            this.VotoEffettuato = votoEffettuato;

            this.Validate();
        }

        public Voto(Utente utente, Utente utenteVotato, bool votoEffettuato)
        {
            Utente = utente;
            UtenteVotato = utenteVotato;
            VotoEffettuato = votoEffettuato;
            this.Validate();
        }

        public Voto()
        {
        }

        public void Validate()
        {
            try
            {
                if (this.Utente == this.UtenteVotato)
                    throw new Exception("L'id dell'utente votato non può essere lo stesso dell'utente che vota");

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

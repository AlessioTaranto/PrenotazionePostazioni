using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class VotoDto
    {
        public int IdUtente { get; set; }
        public int IdUtenteVotato { get; set; }
        public bool VotoEffettuato { get; set; }

        private Exception ModelException { get; set; }
        public bool IsValid { get; set; } = false;

        public VotoDto(int idUtente, int idUtenteVotato, bool votoEffettuato)
        {
            this.IdUtente = idUtente;
            this.IdUtenteVotato = idUtenteVotato;
            this.VotoEffettuato = votoEffettuato;

            this.Validate();
        }

        public VotoDto()
        {
        }

        public void Validate()
        {
            try
            {
                if (this.IdUtente == this.IdUtenteVotato)
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

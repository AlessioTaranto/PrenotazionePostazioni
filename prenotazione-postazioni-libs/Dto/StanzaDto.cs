
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class StanzaDto
    {
        public string Nome { get; set; }
        public int PostiMax { get; set; }
        public int PostiMaxEmergenza { get; set; }


        private Exception ModelException { get; set; }
        public bool IsValid { get; set; } = false;

        public StanzaDto(string nome, int postiMax, int postiMaxEmergenza)
        {
            this.Nome = nome;
            this.PostiMax = postiMax;
            this.PostiMaxEmergenza = postiMaxEmergenza;

            this.Validate();
        }

        public StanzaDto()
        {
        }

        public void Validate()
        {
            try
            {
                if (this.PostiMax <= 0)
                    throw new Exception("I posti devono essere un numero maggiore di 0");

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{

    public class Vote
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdVictim { get; set; }
        public float? VoteResults { get; set; }

        private Exception ModelException { get; set; } = new Exception();
        public bool IsValid { get; set; } = false;

        public Vote(int id, int idUser, int idVictim, float voteResults)
        {
            this.Id = id;
            this.IdUser = idUser;
            this.IdVictim = idVictim;
            this.VoteResults = voteResults;

            this.Validate();
        }

        public Vote(int idUser, int idVictim, float voteResults)
        {
            IdUser = idUser;
            IdVictim = idVictim;
            VoteResults = voteResults;
            this.Validate();
        }

        public Vote() { }


        public void Validate()
        {
            try
            {
                if (this.IdUser == this.IdVictim)
                    throw new Exception("L'id dell'idVictim votato non può essere lo stesso dell'idUser che vota");

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

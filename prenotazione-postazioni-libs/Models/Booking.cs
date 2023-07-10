using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Booking
    {

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int IdRoom { get; set; }
        public int IdUser { get; set; }
        private Exception ModelException { get; set; } = new Exception();
        public bool IsValid { get; set; } = false;

        public Booking(int id, DateTime startDate, DateTime endDate, int idRoom, int idUser)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            IdRoom = idRoom;
            this.IdUser = idUser;
        }

        public Booking(DateTime startDate, DateTime endDate, int idRoom, int idUser)
        {
            StartDate = startDate;
            EndDate = endDate;
            IdRoom = idRoom;
            IdUser = idUser;
        }

        public Booking()
        {
        }

        public void Validate()
        {
            try
            {
                if (StartDate.CompareTo(EndDate) > 0) throw new Exception("La data di inizio non può essere più grande di quella di fine");
                this.IsValid = true;
            }
            catch (Exception ex)
            {
                this.ModelException = ex;
                this.IsValid = false;
            }
        }
    }
}
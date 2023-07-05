
using prenotazione_postazioni_libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class BookingDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int IdRoom { get; set; }
        public int IdUser { get; set; }

        public BookingDto(DateTime startDate, DateTime endDate, int idRoom, int idUser)
        {
            StartDate = startDate;
            EndDate = endDate;
            IdRoom = idRoom;
            IdUser = idUser;
        }

    
    }
}

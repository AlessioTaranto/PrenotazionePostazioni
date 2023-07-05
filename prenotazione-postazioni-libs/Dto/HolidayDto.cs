using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class HolidayDto
    {
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        
        public HolidayDto()
        {

        }
        public HolidayDto(DateTime date, string? description)
        {
            Date = date;
            Description = description;
        }
        public HolidayDto(DateTime date)
        {
            this.Date = date;   
        }
    }
}

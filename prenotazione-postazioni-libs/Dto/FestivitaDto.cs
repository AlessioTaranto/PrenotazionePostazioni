using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class FestivitaDto
    {
        public DateOnly Date { get; set; }
        public string? Desc { get; set; }
        
        public FestivitaDto()
        {

        }
        public FestivitaDto(DateOnly date, string? desc)
        {
            Date = date;
            Desc = desc;
        }
        public FestivitaDto(DateOnly date)
        {
            this.Date = date;
        }
    }
}

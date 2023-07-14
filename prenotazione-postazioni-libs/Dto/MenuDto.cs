using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class MenuDto
    {
        public DateTime Date { get; set; }
        public string? Image { get; set; }
        //Base 64x converter (https://stackoverflow.com/questions/69303512/converting-an-sql-Image-to-base64-in-c-sharp)

        public MenuDto(DateTime date, string image)
        {
            this.Date = date;
            this.Image = image;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public byte[]? MenuImage { get; set; }
        //Base 64x converter (https://stackoverflow.com/questions/69303512/converting-an-sql-image-to-base64-in-c-sharp)


        public Menu(int Id, DateTime Date, byte[]? MenuImage) {
            this.Id = Id;
            this.Date = Date;
            this.MenuImage = MenuImage;
        }
        public Menu(DateTime Date, byte[]? MenuImage)
        {
            this.Date = Date;
            this.MenuImage = MenuImage;
        }
        public Menu(DateTime Date)
        {
            this.Date = Date;
        }
        public Menu() { }
    }
}

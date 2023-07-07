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
        public DateOnly Day { get; set; }
        public string? Image { get; set; }
        //Base 64x converter (https://stackoverflow.com/questions/69303512/converting-an-sql-Image-to-base64-in-c-sharp)


        public Menu(int Id, DateOnly Day, string? Image) {
            this.Id = Id;
            this.Day = Day;
            this.Image = Image;
        }
        public Menu(DateOnly Day, string? Image)
        {
            this.Day = Day;
            this.Image = Image;
        }
        public Menu(DateOnly Day)
        {
            this.Day = Day;
        }
        public Menu() { }
    }
}

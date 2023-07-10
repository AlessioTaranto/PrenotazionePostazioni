using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class MenuChoicesDto
    {
        public int IdMenu { get; set;}
        public string? Choice { get; set;}
        public int IdUser { get; set;}
        
        public MenuChoicesDto(int idMenu, string choice, int idUser)
        {
            this.IdMenu = idMenu;
            this.Choice = choice;
            this.IdUser = idUser;
        }
    }
}

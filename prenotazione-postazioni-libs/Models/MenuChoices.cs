using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class MenuChoices
    {
        public int Id;
        public int IdMenu;
        public string? Choice;
        public int IdUser;

        public MenuChoices(int id, int idMenu, string? choice, int idUser) {
            Id = id;
            IdMenu = idMenu;
            Choice = choice;
            IdUser = idUser;
        }

        public MenuChoices(int idMenu, string? choice, int idUser)
        {
            IdMenu = idMenu;
            Choice = choice;
            IdUser = idUser;
        }


    }
}

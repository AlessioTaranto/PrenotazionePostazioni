using prenotazione_postazioni_libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int IdRole { get; set; }
        public UserDto(string name, string surname, string email, int idRole)
        {
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.IdRole = idRole;
        }

   
    }




}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Rolename { get; set; }


        private Exception ModelException { get; set; } = new Exception();
        public bool IsValid { get; set; } = false;

        public UserRole(int id, string name, string surname, string email, string Rolename)
        {
            this.Id = id;
            this.Username = name;
            this.Surname = surname;
            this.Email = email;
            this.Rolename = Rolename;

            this.Validate();
        }



        public UserRole(string name, string surname, string email, string Rolename)
        {
            Username = name;
            Surname = surname;
            Email = email;
            Rolename = Rolename;
            this.Validate();
        }

        public UserRole() { }
        public void Validate()
        {
            try
            {
                if (this.Username == null)
                    throw new Exception("Il Nome non può essere nullo");
                else if (this.Username.Length < 3)
                    throw new Exception("Il Nome deve contenere almeno 3 caratteri");

                if (this.Surname == null)
                    throw new Exception("Il Cognome non può essere nullo");
                else if (this.Surname.Length < 3)
                    throw new Exception("Il Cognome deve contenere almeno 3 caratteri");

                if (this.Email == null)
                    throw new Exception("L'indirizzo Email non può essere nullo");
                else if (!this.Email.Contains("@") && !this.Email.Contains("."))
                    throw new Exception("L'indirizzo Email non è valido");


                this.IsValid = true;
            }
            catch (Exception e)
            {
                this.ModelException = e;
                this.IsValid = false;
            }
        }

        

    }




}

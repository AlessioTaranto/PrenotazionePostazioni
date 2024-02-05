using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int IdRole { get; set; }
        public string Image { get; set; }
        public string GoogleId { get; set; }


        private Exception ModelException { get; set; } = new Exception();
        public bool IsValid { get; set; } = false;

        public User(int id, string name, string surname, string email, int idRole, string googleId)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.IdRole = idRole;

            this.Validate();
            GoogleId = googleId;
        }

        public User(int id, string name, string surname, string email, int idRole, string googleId, string Image)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.IdRole = idRole;
            this.GoogleId = googleId;
            this.Image = Image;

            this.Validate();
        }

        public User(string name, string surname, string email, int idRole, string googleId)
        {
            Name = name;
            Surname = surname;
            Email = email;
            IdRole = idRole;
            GoogleId = googleId;
            this.Validate();
        }

        public User(string name, string surname, string email, int idRole, string googleId, string Image)
        {
            Name = name;
            Surname = surname;
            Email = email;
            IdRole = idRole;
            this.Image = Image;
            GoogleId = googleId;

            this.Validate();
        }

        public User() { }
        public void Validate()
        {
            try
            {
                if (this.Name == null)
                    throw new Exception("Il Nome non può essere nullo");
                else if (this.Name.Length < 3)
                    throw new Exception("Il Nome deve contenere almeno 3 caratteri");

                if (this.Surname == null)
                    throw new Exception("Il Cognome non può essere nullo");
                else if (this.Surname.Length < 3)
                    throw new Exception("Il Cognome deve contenere almeno 3 caratteri");

                if (this.Email == null)
                    throw new Exception("L'indirizzo Email non può essere nullo");
                else if (!this.Email.Contains("@") && !this.Email.Contains("."))
                    throw new Exception("L'indirizzo Email non è valido");

                if (this.IdRole <= 0)
                    throw new Exception("Il ruolo non è valido");


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

<<<<<<< HEAD:prenotazione-postazioni-libs/Models/User.cs
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


        private Exception ModelException { get; set; } = new Exception();
        public bool IsValid { get; set; } = false;

        public User(int id, string name, string surname, string email, int idRole)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.IdRole = idRole;

            this.Validate();
        }



        public User(string name, string surname, string email, int idRole)
        {
            Name = name;
            Surname = surname;
            Email = email;
            IdRole = idRole;
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
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Utente
    {
        public int IdUtente { get; set; }
        public string Nome { get; set; } = "";
        public string Cognome { get; set; } = "";
        public string? Image { get; set; }
        //Base 64x converter (https://stackoverflow.com/questions/69303512/converting-an-sql-Image-to-base64-in-c-sharp)
        public string Email { get; set; } = "";
        public int IdRuolo { get; set; }


        private Exception ModelException { get; set; } = new Exception();
        public bool IsValid { get; set; } = false;

        public Utente(int idUtente, string nome, string cognome, string image, string email, int idRuolo)
        {
            this.IdUtente = idUtente;
            this.Nome = nome;
            this.Cognome = cognome;
            this.Image = image;
            this.Email = email;
            this.IdRuolo = idRuolo;
            this.Validate();
        }



        public Utente(string nome, string cognome, string image, string email, int idRuolo)
        {
            Nome = nome;
            Cognome = cognome;
            Image = image;
            Email = email;
            IdRuolo = idRuolo;
            this.Validate();
        }

        public Utente() { }
        public void Validate()
        {
            try
            {
                if (this.Nome == null)
                    throw new Exception("Il Nome non può essere nullo");
                else if (this.Nome.Length < 3)
                    throw new Exception("Il Nome deve contenere almeno 3 caratteri");

                if (this.Cognome == null)
                    throw new Exception("Il Cognome non può essere nullo");
                else if (this.Cognome.Length < 3)
                    throw new Exception("Il Cognome deve contenere almeno 3 caratteri");

                if (this.Email == null)
                    throw new Exception("L'indirizzo Email non può essere nullo");
                else if (!this.Email.Contains("@"))
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
>>>>>>> 290623_lorenzo:prenotazione-postazioni-libs/Models/Utente.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    internal class Utente
    {
        private int idUtente { get; set; }
        private string nome { get; set; }
        private string cognome { get; set; }
        private string image { get; set; }
        //Base 64x converter (https://stackoverflow.com/questions/69303512/converting-an-sql-image-to-base64-in-c-sharp)
        private string email { get; set; }
        private int idRuolo { get; set; }

        
        private Exception modelExceprion { get; set; }
        private bool isValid { get; set; } = false;

        public Utente(int idUtente, string nome, string cognome, string image, string email, int idRuolo)
        {
            this.idUtente = idUtente;
            this.nome = nome;
            this.cognome = cognome;
            this.image = image;
            this.email = email;
            this.idRuolo = idRuolo;

            this.Validate();
        }

        public Utente()
        {
        }

        public void Validate()
        {
            try
            {
                if (this.nome == null)
                    throw new Exception("Il nome non può essere nullo");
                else if (this.nome.Length < 3)
                    throw new Exception("Il nome deve contenere almeno 3 caratteri");

                if (this.cognome == null)
                    throw new Exception("Il cognome non può essere nullo");
                else if (this.cognome.Length < 3)
                    throw new Exception("Il cognome deve contenere almeno 3 caratteri");

                if (this.email == null)
                    throw new Exception("L'indirizzo email non può essere nullo");
                else if (!this.email.Contains("@"))
                    throw new Exception("L'indirizzo email non è valido");


                this.isValid = true;
            }
            catch (Exception e)
            {
                this.modelExceprion = e;
                this.isValid = false;
            }
        }

    }




}

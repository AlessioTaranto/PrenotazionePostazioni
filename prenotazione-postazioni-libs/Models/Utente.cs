using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    internal class Utente
    {
        private int idUtente;
        private string nome;
        private string cognome;
        private string image; //Base 64x converter (https://stackoverflow.com/questions/69303512/converting-an-sql-image-to-base64-in-c-sharp)
        private string email;
        private int idRuolo;

        public int IdUtente { get => idUtente; set => idUtente = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cognome { get => cognome; set => cognome = value; }
        public string Image { get => image; set => image = value; }
        public string Email { get => email; set => email = value; }
        public int IdRuolo { get => idRuolo; set => idRuolo = value; }
    }
}

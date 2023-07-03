
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Role
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool AccessToSettings { get; set; }

        private Exception ModelException { get; set; } = new Exception();
        public bool IsValid { get; set; } = false;

        public Role() { }
        public Role(int id, string name,  string description, bool accessToSettings)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.AccessToSettings = accessToSettings;

            this.Validate();
        }

        public Role(string name, bool accessToSettings)
        {
            Name = name;
            AccessToSettings = accessToSettings;

            this.Validate();
        }

        public void Validate()
        {
            try
            {
                if (this.Name == null)
                    throw new Exception("Il nome del ruolo non può essere nullo");

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
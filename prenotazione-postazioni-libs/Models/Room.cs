
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Room
    {

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Capacity { get; set; } = 0;
        public int CapacityEmergency { get; set; } = 0;


        private Exception ModelException { get; set; } = new Exception();
        public bool IsValid { get; set; } = false;

        public Room(int id, string name, int capacity, int capacityEmergency)
        {
            this.Id = id;
            this.Name = name;
            this.Capacity = capacity;
            this.CapacityEmergency = capacityEmergency;

            this.Validate();
        }

        public Room(string name, int capacity, int capacityEmergency)
        {
            Name = name;
            Capacity = capacity;
            this.CapacityEmergency = capacityEmergency;
            this.Validate();
        }

        public Room() { }
        public void Validate()
        {
            try
            {
                if (this.Capacity <= 0 && this.CapacityEmergency <= 0)
                    throw new Exception("I posti devono essere un numero maggiore di 0");

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

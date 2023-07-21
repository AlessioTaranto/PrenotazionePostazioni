
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class RoomDto
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int CapacityEmergency { get; set; }

        public RoomDto(string name, int capacity, int capacityEmergency)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.CapacityEmergency = capacityEmergency;
        }

 
    }
}

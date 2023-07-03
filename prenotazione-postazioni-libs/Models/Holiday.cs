using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Holiday
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }

        public Exception ModelException = new Exception();
        public bool IsValid { get; set; } = true;

        public Holiday(DateTime date, string? description)
        {
            this.Date = date;
            this.Description = description;
        }
        public Holiday(int id, DateTime date, string? description)
        {
            Id = id;
            Date = date;
            Description = description;
        }
        public Holiday(DateTime date)
        {
            this.Date = date;
        }

        public Holiday()
        {

        }
    }
}

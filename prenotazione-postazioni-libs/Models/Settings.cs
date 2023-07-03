
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Settings
    {
        private static Settings? _instance;
        private Settings()
        {

        }

        public static Settings GetInstance()
        {
            if(_instance == null)
            {
                _instance = new Settings();
            }
            return _instance;
        }
    
        public bool ModEmergency { get; set; }

    }
}
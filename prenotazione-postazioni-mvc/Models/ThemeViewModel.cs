using prenotazione_postazioni_mvc.HttpServices;

namespace prenotazione_postazioni_mvc.Models
{
    public class ThemeViewModel
    {

        public string ThemeURL { get; set; }

        public ThemeViewModel()
        {
           this.ThemeURL = "/img/piantina.png";
        }

        public string GetTheme() { 
            return this.ThemeURL;
        }

        public Object SetTheme()
        {
            if (ThemeURL.Equals("/img/piantina.png")) {
                ThemeURL = "/img/piantinaWhite.png";
            }else{
                ThemeURL = "/img/piantina.png";
            }

            return ThemeURL;
        }
    }
}

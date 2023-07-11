using prenotazione_postazioni_mvc.HttpServices;

namespace prenotazione_postazioni_mvc.Models
{
    public class MenuViewModel
    {
        public DateTime Date { get; set; }
        public string Scelta { get; set; } 
        public string UrlMenu { get; set; }

        public MenuViewModel()
        {
            UrlMenu = "https://marketplace.canva.com/EAD0UMPtOv4/1/0/1236w/canva-oro-tenue-elegante-matrimonio-menu-FQIyCfgp1aY.jpg";
            Date = DateTime.Now;
        }
        public MenuViewModel(string scelta) { Scelta = scelta; }
       
       
        public string GetDate() { return Date.ToString("dd/MM"); }


        public string GetUrlMenu()
        {
            //richiesta url menu
            return UrlMenu;
        }

    }
}

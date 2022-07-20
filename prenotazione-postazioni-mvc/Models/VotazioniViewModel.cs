namespace prenotazione_postazioni_mvc.Models
{
    public class VotazioniViewModel
    {
        public List<int> Votazioni { get; set; }

        public VotazioniViewModel(List<int> votazioni)
        {
            Votazioni = votazioni;
        }

        public VotazioniViewModel()
        {
            Votazioni = new List<int>();
        }

        public string VotazioniJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(Votazioni);
        }

    }
}

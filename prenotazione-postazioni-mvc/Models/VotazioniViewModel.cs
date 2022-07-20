namespace prenotazione_postazioni_mvc.Models
{
    public class VotazioniViewModel
    {
        public List<bool?> Votazioni { get; set; }

        public VotazioniViewModel(List<bool?> votazioni)
        {
            Votazioni = votazioni;
        }

        public VotazioniViewModel()
        {
            Votazioni = new List<bool?>();
        }

        public string VotazioniJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(Votazioni);
        }

    }
}

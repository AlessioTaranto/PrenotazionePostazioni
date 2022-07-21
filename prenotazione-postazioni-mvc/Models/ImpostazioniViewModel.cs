namespace prenotazione_postazioni_mvc.Models
{
    public class ImpostazioniViewModel
    {

        // Modello del Tab 0 (Covid)
        public CapienzaImpostazioniViewModel CapienzaViewModel { get; set; }
        // Modello del Tab 1 (Festività)
        public FestivitaImpostazioniViewModel FestivitaViewModel { get; set; }
        // Modello del Tab 2 (Presenze)
        public PresenzeImpostazioniViewModel PresenzeViewModel { get; set; }

        // Stato del Tab
        public int StateTab { get; set; } = 0;

        public ImpostazioniViewModel(CapienzaImpostazioniViewModel capienzaViewModel, FestivitaImpostazioniViewModel festivitaViewModel, PresenzeImpostazioniViewModel presenzeViewModel)
        {
            CapienzaViewModel = capienzaViewModel;
            FestivitaViewModel = festivitaViewModel;
            PresenzeViewModel = presenzeViewModel;
        }
    }
}

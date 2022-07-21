using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_mvc.Models;

namespace prenotazione_postazioni_mvc.Controllers
{
    public class VotazioniController : Controller
    {

        public static VotazioniViewModel ViewModel { get; set; }
        public readonly VotoHttpService _votoHttpService;

        public VotazioniController(VotoHttpService votoHttpService)
        {
            _votoHttpService = votoHttpService;
        }

        public IActionResult Index()
        {
            if(ViewModel == null)
                ViewModel = new VotazioniViewModel();
            return View(ViewModel);
        }

        [HttpPost]
        [ActionName("VoteUser")]
        public IActionResult VoteUser(int voto, Utente UtenteVotato, Utente Utente)
        {
            /*try
            {
                ViewModel.Votazioni[i] = voto;
            } catch(Exception)
            {
                ///controllo se sono presenti elementi prima della posizione in cui si vuole inserire il voto
                if (idUtenteVotato != ViewModel.Votazioni.Count)
                {
                    ///il numero di cicli rappresenta il numero di posizioni da riempire (con un voto nullo)
                    int cicli = idUtenteVotato - ViewModel.Votazioni.Count;
                    for (int j = 0; j < cicli; j++)
                    {
                        ViewModel.Votazioni.Add(0);
                    }
                }
                ViewModel.Votazioni.Add(voto);
            }*/
            if(voto == 0)
            {

            }else if(voto == 1)
            {
                _votoHttpService.OnMakeVoto(new VotoDto(Utente, UtenteVotato, true));
            }else if(voto == -1)
            {
                _votoHttpService.OnMakeVoto(new VotoDto(Utente, UtenteVotato, false));
            }

            return Ok("Votazionr effettuata");
        }
    }
}

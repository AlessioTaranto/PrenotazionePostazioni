using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_mvc.Models;
using System.Net;

namespace prenotazione_postazioni_mvc.Controllers
{
    public class VotazioniController : Controller
    {

        public static VotazioniViewModel ViewModel { get; set; }
        public readonly VotoHttpService _votoHttpService;
        public readonly UtenteHttpService _utenteHttpService;

        public VotazioniController(VotoHttpService votoHttpService, UtenteHttpService utenteHttpService)
        {
            _votoHttpService = votoHttpService;
            _utenteHttpService = utenteHttpService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("VoteUser")]
        public async Task<IActionResult> VoteUser(int voto, int idUtente, int idUtenteVotato)
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
            HttpResponseMessage? response = null;
            //if (utenteResponse.StatusCode == HttpStatusCode.OK && utenteVotatoResponse.StatusCode == HttpStatusCode.OK)
            //{

            Utente utente = new Utente(idUtente, null, null, null, null, 0);//await utenteResponse.Content.ReadFromJsonAsync<Utente>();
            Utente utenteVotato = new Utente(idUtente, null, null, null, null, 0);//await utenteVotatoResponse.Content.ReadFromJsonAsync<Utente>();
            if (voto == 0)
            {
                  response = await _votoHttpService.OnDeleteVoto(idUtente, idUtenteVotato);
            }
            else if (voto == 1)
            {
                response = await _votoHttpService.OnMakeVoto(new VotoDto(utente, utenteVotato, true));
            }
            else if (voto == -1)
            {
                response = await _votoHttpService.OnMakeVoto(new VotoDto(utente, utenteVotato, false));
            }
            return Ok("Votazione effettuata");
            //}
            //else return NotFound("Utente non trovato");
        }
    }

    /*
     * 
     *  CALCOLO RUMOROSITA':
     *  
     *  Mediana dei velori ottenuti su ciascun utente:
     *  
     *      - Ordina array:
     *          - Se count % 2 == 0: 
     *              ret array[round((n/2+(n+1)/2)/2)]
     *          - Se count % 2 != 0:
     *              ret array[(n+1)/2]
     *  
    */

}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_mvc.Models;
using System.Net;

namespace prenotazione_postazioni_mvc.Controllers
{
    public class VoteController : Controller
    {

        public static VoteViewModel ViewModel { get; set; }
        public readonly VoteHttpService _voteHttpService;
        public readonly UserHttpService _userHttpService;

        public VoteController(VoteHttpService voteHttpService, UserHttpService userHttpService)
        {
            _voteHttpService = voteHttpService;
            _userHttpService = userHttpService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(int voteRusults, int idUser, int idVictim)
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

            if (voteRusults == 0)
            {
                  response = await _voteHttpService.Delete(idUser, idVictim);
            }
            else if (voteRusults == 1)
            {
                response = await _voteHttpService.Add(new VoteDto(idUser, idVictim, 0));
            }
            else if (voteRusults == -1)
            {
                response = await _voteHttpService.Add(new VoteDto(idUser, idVictim, 1));
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

using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using System.Net;
namespace prenotazione_postazioni_mvc.Models
{
    public class VoteViewModel
    {
        public Vote vote { get; set; }

        public readonly VoteHttpService _voteHttpService;
        public readonly UserHttpService _userHttpService;


        public VoteViewModel(VoteHttpService _voteHttpService,  UserHttpService _userHttpService)
        {
            _voteHttpService = _voteHttpService;
            _userHttpService = _userHttpService;

        }


        
    }
}

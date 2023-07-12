using log4net;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories.Database;
using System.Data.SqlClient;

namespace prenotazioni_postazioni_api.Services
{
    public class MenuChoicesService
    {
        private MenuChoicesRepository _menuChoicesRepository;
        private readonly ILog logger = LogManager.GetLogger(typeof(MenuChoicesService));

        public MenuChoicesService(MenuChoicesRepository menuChoicesRepository)
        {
            _menuChoicesRepository = menuChoicesRepository;
        }

        internal List<MenuChoices>? GetAll()
        {
            logger.Info("Ricerca di tutte le scelte");
            return _menuChoicesRepository.GetAll();

        }

        internal MenuChoices? GetById(int id)
        {
            logger.Info("Trovando la scelta mediante il suo id: " + id);
            MenuChoices menuChoices = _menuChoicesRepository.GetById(id);
            if (menuChoices == null)
            {
                logger.Error("La scelta trovata non e' valida");
                throw new PrenotazionePostazioniApiException("Non e' presente nessuna scelta con questo id");
            }
            else
            {
                logger.Info("La scelta trovata e' valida");
                return menuChoices;
            }
        }

        internal List<MenuChoices>? GetByIdMenu(int idMenu)
        {
            logger.Info("Trovando le scelte mediante un certo id di un menu: " + idMenu);
            List<MenuChoices> menuChoices = _menuChoicesRepository.GetByIdMenu(idMenu);
            if (menuChoices == null)
            {
                logger.Error("La scelta trovata non e' valida");
                throw new PrenotazionePostazioniApiException("Nessuno ha scelto qualcosa da questo menu");
            }
            else
            {
                logger.Info("La scelta trovata e' valida");
                return menuChoices;
            }

        }

        internal MenuChoices? GetByUserAndIdMenu(int idMenu, int idUser)
        {
            logger.Info("Tovando tutte le scelte di un un utente su un certo menu: " + idMenu + " " + idUser);
            MenuChoices menuChoices = _menuChoicesRepository.GetByUserAndIdMenu(idMenu, idUser);
            if (menuChoices == null)
            {
                logger.Error("La scelta trovata non e' valida");
                throw new PrenotazionePostazioniApiException("Id scelta e/o Id menu non valido");
            }
            else
            {
                logger.Info("La scelta trovata e' valida");
                return menuChoices;
            }

        }

        internal void Add(MenuChoicesDto menuChoicesDto)
        {
            if(_menuChoicesRepository.GetByUserAndIdMenu(menuChoicesDto.IdUser, menuChoicesDto.IdMenu) != null)
            {
                logger.Warn("MenuDto non e' valido, ho lanciato una prenotazionePostazioniApiException!");
                throw new PrenotazionePostazioniApiException("Il seguente utente ha gia' fatto una scelta per il menu corrente!!!");
            }
            logger.Info("MenuChoicesDto e' valido. Cercando di salvare la scelta nel database...");
            _menuChoicesRepository.Add(new MenuChoices(menuChoicesDto.IdMenu, menuChoicesDto.Choice, menuChoicesDto.IdUser));
        }

        internal void DeleteByUserAndIdMenu(int idMenu, int idUser)
        {
            logger.Info("Rimozione della scelta dal database...");
            _menuChoicesRepository.DeleteByUserAndIdMenu(idMenu, idUser);
            logger.Info("Scelta rimossa dal database");
        }


    }
}

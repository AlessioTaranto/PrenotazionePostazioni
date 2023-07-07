using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Repositories;
using prenotazioni_postazioni_api.Exceptions;
using prenotazione_postazioni_libs.Dto;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class MenuService
    {
        private MenuRepository _menuRepository;
        private readonly ILog logger = LogManager.GetLogger(typeof(MenuService));

        public MenuService (MenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        internal Menu? GetByDate(DateOnly date)
        {
            logger.Info("Ricerca menu mediante una data...");
            return _menuRepository.GetByDate(date);
        }
        internal List<Menu>? GetAll()
        {
            logger.Info("Ricerca di tutti i menu");
            return _menuRepository.GetAll();
        }
        internal Menu? GetById(int id)
        {
            logger.Info("Trovando il menu mediante il suo id: " + id);
            Menu menu = _menuRepository.GetById(id);
            logger.Info("Controllando se il menu trovato e' valido...");
            if(menu == null)
            {
                logger.Error("Il menu trovato NON e' valido");
                throw new PrenotazionePostazioniApiException("Id Utente non trovato");
            }
            else
            {
                logger.Info("Il menu trovato e' valido");
                return menu;
            }
        }

        internal void Save(MenuDto menuDto)
        {
            logger.Info("Controllando se menuDto e' valido...");
            if(_menuRepository.GetByDate(menuDto.Day) != null)
            {
                logger.Warn("MenuDto non e' valido, ho lanciato una prenotazionePostazioniApiException!");
                throw new PrenotazionePostazioniApiException("data gia occupata da un altro menu!!!");
            }
            logger.Info("MenuDto e' valido. Cercando di salvare Menu nel database...");
            _menuRepository.Add(new Menu(menuDto.Day, menuDto.Image));
        }
        internal void Delete(DateOnly day) 
        {
            logger.Info("Rimoazione del menu dal database...");
            _menuRepository.Delete(day);
            logger.Info("Menu rimosso dal database");

        }

    }
}

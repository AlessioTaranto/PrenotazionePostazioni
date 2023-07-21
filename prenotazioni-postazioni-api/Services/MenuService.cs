using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class MenuService
    {
        private MenuRepository _menuRepository;
        private readonly ILog logger = LogManager.GetLogger(typeof(MenuService));

        public MenuService (MenuRepository _menuRepository)
        {
            this._menuRepository = _menuRepository;
        }
        internal List<Menu> GetAll()
        {
            logger.Info("Ricerca di tutti i menu");
            return _menuRepository.GetAll();
        }

        internal Menu GetByDate(DateTime date)
        {
            logger.Info("Ricerca menu mediante una data...");
            return _menuRepository.GetByDate(date);
        }
       
        internal Menu GetById(int id)
        {
            logger.Info("Trovando il menu mediante il suo id: " + id);
            Menu menu = _menuRepository.GetById(id);
            logger.Info("Controllando se il menu trovato e' valido...");
            if(menu == null)
            {
                logger.Error("Il menu trovato NON e' valido");
                throw new PrenotazionePostazioniApiException("Nessun menu trovato con questo id");
            }
            else
            {
                logger.Info("Il menu trovato e' valido");
                return menu;
            }
        }

        internal void Add(MenuDto menuDto)
        {
            logger.Info("Controllando se menuDto e' valido...");
            if(_menuRepository.GetByDate(menuDto.Date) != null)
            {
                Console.WriteLine("UPDATE");
                _menuRepository.Update(new Menu(menuDto.Date, menuDto.MenuImage));
            }
            else 
            {
                Console.WriteLine("ADD");
                logger.Info("MenuDto e' valido. Cercando di salvare Menu nel database...");
                _menuRepository.Add(new Menu(menuDto.Date, menuDto.MenuImage));
            }
        }
        internal void Delete(DateTime day) 
        {
            logger.Info("Rimoazione del menu dal database...");
            _menuRepository.Delete(day);
            logger.Info("Menu rimosso dal database");

        }

    }
}

using prenotazione_postazioni_mvc.HttpServices;
using System.Net;

namespace prenotazione_postazioni_mvc.Models
{
    public class SettingsViewModel
    {

        // Modello del Tab 0 (Covid)
        public CapacitySettingsViewModel CapacitySettingsViewModel { get; set; }

        // Modello del Tab 2 (Presenze)
        public AttendanceSettingsViewModel AttendanceSettingsViewModel { get; set; }

        // Stato del Tab
        public int StateTab { get; set; } = 0;

        //Festività selezionata
        public DateTime HolidaySelected { get; set; }

        //Stringa JSON con lista delle feste da leggere in javascript
        public string HolidayJSON { get; set; } = "{}";

        // Http Festa service 
        public HolidayHttpService _HolidayHttpService { get; set; }

        public SettingsViewModel(CapacitySettingsViewModel capacitySettingsViewModel, AttendanceSettingsViewModel attendanceSettingsViewModel, HolidayHttpService holidayHttpService)
        {
            CapacitySettingsViewModel = capacitySettingsViewModel;
            AttendanceSettingsViewModel = attendanceSettingsViewModel;
            _HolidayHttpService = holidayHttpService;
        }

        public string GetHolidaySelected()
        {
            if (HolidaySelected.Date.Year == 1)
                return "Seleziona un giorno";

            return "Giorno selezionato: " + HolidaySelected.Date.Day + "/" + HolidaySelected.Date.Month + "/" + HolidaySelected.Date.Year;
        }

        public void SetHolidaySelected(int year, int month, int day)
        {
            try
            {
                HolidaySelected = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Giorno non valido");
            }
        }

        public async void AddHoliday(int year, int month, int day, string description)
        {
            DateTime date;
         
            try
            {
                date = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Giorno non valido");
            }

            HttpResponseMessage addHoliday = await _HolidayHttpService.Add(date, description);
            if (addHoliday == null || addHoliday.StatusCode != HttpStatusCode.OK)
                return;
        }

        public async Task DeleteHoliday(int year, int month, int day)
        {
            DateTime date;

            try
            {
                date = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Giorno non valido");
            }

            HttpResponseMessage delete = await _HolidayHttpService.Delete(year, month, day);
            if (delete == null || delete.StatusCode != HttpStatusCode.OK)
                return;
        }

        public async Task ReloadHoliday()
        {
            HttpResponseMessage getAll = await _HolidayHttpService.GetAll();
            if (getAll == null || getAll.StatusCode != HttpStatusCode.OK)
                return;

            Task<String> ctxString = getAll.Content.ReadAsStringAsync();
            ctxString.Wait();
            HolidayJSON = ctxString.Result;
        }


    }
}

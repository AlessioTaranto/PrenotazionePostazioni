using prenotazione_postazioni_libs.Models;

namespace prenotazioni_postazioni_api.Utilities
{
    public class UtenteEqualityComparer : IEqualityComparer<Utente>
    {
        public bool Equals(Utente? x, Utente? y)
        {
            if(x == null || y == null) return false;
            return x.IdUtente == y.IdUtente;
        }

        public int GetHashCode(Utente obj)
        {
            return obj.IdUtente.GetHashCode();
        }

    }
}

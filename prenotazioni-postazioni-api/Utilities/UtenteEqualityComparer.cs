using prenotazione_postazioni_libs.Models;

namespace prenotazioni_postazioni_api.Utilities
{
    public class UtenteEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User? x, User? y)
        {
            if(x == null || y == null)
            {
                return false;
            }
            return x.Id == y.Id;
        }

        public int GetHashCode(User obj)
        {
            return obj.Id.GetHashCode();
        }

    }
}

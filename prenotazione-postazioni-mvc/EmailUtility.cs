using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Hangfire;

namespace prenotazione_postazioni_mvc
{
    public class EmailUtility
    {
        public static async Task InviaEmail(string destinatario, string oggetto, string corpo)
        {
            // Configura le informazioni del server SMTP
            string host = "smtp.gmail.com";
            int port = 587; // Porta del server SMTP
            string username = "lorenzorisaliti763@gmail.com";
            string password = "ncxnvdvqidhexsvt";

            // Crea l'oggetto MailMessage con mittente, destinatario, oggetto e corpo dell'email
            MailMessage message = new MailMessage(username, destinatario, oggetto, corpo);
            message.IsBodyHtml = true; // Se il corpo contiene HTML

            // Configura il client SMTP
            SmtpClient smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(username, password);

            try
            {
                // Invia l'email in modo asincrono
                await smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                // Lascia che l'eccezione venga catturata e gestita nel contesto in cui stai chiamando questo metodo.
                throw;
            }
        }
    }
}

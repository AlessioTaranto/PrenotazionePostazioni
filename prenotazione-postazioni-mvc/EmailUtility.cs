using System.Net.Mail;
using System.Net;

namespace prenotazione_postazioni_mvc
{
    public class EmailUtility
    {
        public static void InviaEmail(string destinatario, string oggetto, string corpo)
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

            // Invia l'email
            smtpClient.Send(message);
        }
    }
}

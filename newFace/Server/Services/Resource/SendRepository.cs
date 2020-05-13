using System.Net.Mail;
using System.Threading.Tasks;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Resource;
using Kavenegar;

namespace newFace.Server.Services.Resource
{
    public class SendRepository : ISendRepository
    {

        public SendRepository()
        {

        }

        //-----------------------------------------------------------

        public bool Sms(string phoneNumber, string text)
        {
            KavenegarApi api = new KavenegarApi("4D79564E434D37413658756D6361327A707067724363464C7853765568615551");
            string msg = text;

            try
            {
                api.Send("100065995", phoneNumber, msg);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void SendEmail(IdentityMessage message)
        {
            // Credentials:
            string credentialUserName = "masoud.afarin.moghaddam@gmail.com";
            string sentFrom = "masoud.afarin.moghaddam@gmail.com";
            string pwd = "77zH26nbqT";

            // Configure the client:
            System.Net.Mail.SmtpClient client =
                new System.Net.Mail.SmtpClient("smtp.gmail.com")
                {
                    Port = 25,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false
                };

            // Creatte the credentials:
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            MailMessage mail =
                new System.Net.Mail.MailMessage(sentFrom, message.Destination)
                {
                    Subject = message.Subject,
                    Body = message.Body
                };

            // Send:
            mail.IsBodyHtml = true;
         client.Send(mail);
           
        }
    }
}
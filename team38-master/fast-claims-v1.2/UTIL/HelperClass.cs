using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
//System.Web.Security.Membership.GeneratePassword

namespace fast_claims_v1._2.UTIL
{
    public class HelperClass
    {
        private bool IsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string sendEmail(string recipient, string Subject, string messageBody)
        {

            MailMessage message;
            SmtpClient client;
            try
            {
                message = new MailMessage("iamxteecy@gmail.com", "iamxteecy@gmail.com", Subject, messageBody);
                message.IsBodyHtml = true;
            }
            catch (Exception e)
            {
                return e.StackTrace;
            }

            try
            {
                ///message.Priority = MailPriority.Normal;
                using (client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential("iamxteecy@gmail.com", "0762619926");
                    client.EnableSsl = true;
                    client.Send(message);
                }


            }
            catch (Exception e)
            {
                string err = e.Message + e.Source + e.InnerException;
                return err;
            }



            return "";
        }

        public int GenerateRand(int max)
        {
            if (max < 1)
            {
                return 0;
            }

            if (max == 1)
            {
                return 1;
            }

            var rand = new Random().Next(max);
            return rand;
        }

        public string GenerateUniqueString(int size) {
            Guid g = Guid.NewGuid();
            System.Diagnostics.Debug.Write(g);
            string GuidString =g.ToString();
            GuidString = GuidString.Replace("-", "");
            System.Diagnostics.Debug.Write(GuidString);
            return "xolani";
        }

    }
}
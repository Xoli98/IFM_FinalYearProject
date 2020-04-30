using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using fast_claims_v1._2.UTIL;

namespace fast_claims_v1._2
{
   
    public class WebApiApplication : System.Web.HttpApplication
    {
       
        public void printGuid() {
            Guid guid = Guid.NewGuid();
            
            display(guid.ToString().Substring(4,4));
        }
        private void display(string message)
        {
            



            System.Diagnostics.Debug.Write(message +"\n");
        }
        private bool sendEmail()
        {
            bool isSent = false;
            try
            {
                var smtpclient = new SmtpClient("smtp.gmail.com", 587);
                smtpclient.EnableSsl = true;
                smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new NetworkCredential("iamxteecy@gmail.com", "0762619926");
                var mailmessage = new MailMessage("iamxteecy@gmail.com", "iamxteecy@gmail.com", "no-reply", "Password");
                mailmessage.IsBodyHtml = false;
                mailmessage.BodyEncoding = UTF8Encoding.UTF8;
                smtpclient.Send(mailmessage);
                isSent = true;
            }
            catch (Exception e)
            {

                display(e.Message + e.HResult + "\n" +e.InnerException+"\n" );
                return false;
            }

            return isSent;
        }
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //var gmail = new Gmail();
            //if (sendEmail())
            //{
            //    display("email sent.");
            //}
            //else { display("email not sent."); } ;

            this.printGuid();
        }
    }
}

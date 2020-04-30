using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using fast_claims_v1._2.Models;
using fast_claims_v1._2.UTIL;



namespace fast_claims_v1._2.Controllers
{
    public class ClaimantController : Controller
    {
        //

        private DatabaseDataContext _database = new DatabaseDataContext();
        public GenericUnitOfWork _unit = new GenericUnitOfWork();
       

        private void display(string message) {
            System.Diagnostics.Debug.Write(message+"\n");
        }

        private bool SubmitChanges() {
            try
            {
                _database.SubmitChanges();
            }
            catch (Exception e)
            {
                display(e.Message + e.InnerException);
                return false;
            }
            return true;

        }

        private bool sendEmail(string from, string to, string subject, string body) {
            bool isSent = false;
            try
            {
                var smtpclient = new SmtpClient("smtp@gmail.com",25);
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

                display(e.InnerException + e.Message);
            }

            return isSent;
        }

        // GET: Claimant
        public ActionResult Process()
        {
            return View();
        }

        public ActionResult UploadID() {
            
            return View();
        }


        private tbl_file saveFileToDatabase(HttpPostedFileBase postedfile, string filename) {

            Stream stream = postedfile.InputStream;
            BinaryReader br = new BinaryReader(stream);
            byte[] bytes = br.ReadBytes((int)stream.Length);
            var newFile = new tbl_file
            {
                name = filename,
                file = bytes,
            };

            //save file to database
            try
            {
                _unit.GetRepositoryInstance<tbl_file>().Add(newFile);
                return _unit.GetRepositoryInstance<tbl_file>().GetFirstorDefaultByParameter(e => e.file.Equals(bytes) && e.name.Equals(filename));
            }
            catch (Exception)
            {

                return null;
            }
            
        }


        [HttpPost]
        public ActionResult VerifyCustomer()
        {
            var application = new ClaimApplicationModel();
            TryUpdateModel(application);

            
            if (ModelState.IsValid)
            {
                
                var rec = _unit.GetRepositoryInstance<tbl_policyholder>().GetAllRecordsIQueryable().Where(e => e.id_number.Equals(application.idnumber)).FirstOrDefault();
                
                if (rec != null)// a record of policy holder exists, there polic holder is deceased

                {
                    
                    application.OTP = "2hoH34a";
                   
                    //add a login to table
                    var newLogin = new tbl_login
                    {
                        username = application.email,
                        password = application.OTP
                    };


                   
                    if (_unit.GetRepositoryInstance<tbl_login>().Add(newLogin))
                    {
                        
                        var LoginRec = _unit.GetRepositoryInstance<tbl_login>().GetFirstorDefaultByParameter(e => e.username.Equals(application.email) && e.password.Equals(application.OTP));
                        var newClaimant = new tbl_claimant
                        {
                            email = application.email,
                            login_id = LoginRec.login_id,
                        };

                       
                        if (_unit.GetRepositoryInstance<tbl_claimant>().Add(newClaimant))
                        {
                            display("claimant added to table");
                            var newApplication = new tbl_application {
                                date_created = DateTime.Today,
                                isActive = true,
                                admin_id = 1,
                            };
                            display("add application to table");
                            if (_unit.GetRepositoryInstance<tbl_application>().Add(newApplication))
                            {
                                display("application added to table");
                                var newClaimApplication = new tbl_claimapplication {
                                    appl_id = 1,
                                    claim_number = 10,
                                    deceased_id = ""+application.idnumber,
                                    claimant_id = 1,
                                };
                                display("add claim application to table");
                                if (_unit.GetRepositoryInstance<tbl_claimapplication>().Add(newClaimApplication)) {
                                    var model = application;
                                    display("claim application added to table");
                                    if (_unit.SaveChanges())
                                    {
                                        display("Changes saved. \nNow redirecting to Give pin view");
                                        //return page that gives pin to user 
                                        return RedirectToAction("GivePin", model);
                                    }
                                    else { display("couldnt save changes"); }


                                    display("claim added");
                                } else { display("could add claim application"); }
                            }
                            else { display("couldnt add application"); };  
                            
                        }
                        else
                        {
                            display("couldnt add claimant");
                        }
                    }
                    else {
                        ViewBag.Error = "Adding New login failed.";
                    }
                       
                }
                else {
                    ViewBag.Error = "Some information you provided was invalid. \n";
                }
            }
            return View("Process");
        }

        public ActionResult TrackProgress() {

            return View();
        }

        public ActionResult VerifyAccount() {

            var model = new ClaimApplicationModel();
            TryUpdateModel(model);
            if (ModelState.IsValid)
            {
                var data = _unit.GetRepositoryInstance<tbl_login>();
                var rec = data.GetFirstorDefaultByParameter(e => e.username.Equals(model.email) && e.password.Equals(model.OTP));
                if (rec != null)
                {
                    return View("Index");
                }
                else {
                    model.ErrorMessage = "incorrect credentials";
                }
            } 
            return View("TrackProgress", model);
        }


        public ActionResult GivePin() {
            return View();
        }


        public ActionResult Index() {

            return View();
        }

        public ActionResult CompleteForm() {
            return View();
        }

        private bool isValidContentType(string contectType) {

            return contectType.Equals(".pdf");
        }

        [HttpPost]
        public ActionResult ProcessId() {

            var iddoucument = Request.Files["iddocument"];
            var deathcertificate = Request.Files["deathcertificate"];

            //save documents to database

            var filerec = saveFileToDatabase(iddoucument, "iddoucument");
            var certrec = saveFileToDatabase(deathcertificate, "deathcertificate");

            //link file t respective tables and claim application
            if (filerec != null && certrec != null)
            {
                display("name: " + filerec.name+", ID: "+filerec.fileId+"\n"+"name: "+certrec.name+", ID: "+certrec.fileId);
                _unit.GetRepositoryInstance<tbl_claimant>().GetFirstorDefaultByParameter(e => e.claimaint_id.Equals(1)).iddocument = filerec.fileId;
                _unit.GetRepositoryInstance<tbl_claimapplication>().GetFirstorDefaultByParameter(e => e.claimant_id.Equals(1)).deceaced_deathcert = certrec.fileId;
                if (_unit.SaveChanges( )) {

                    display("changes saved");
                    return View("Index"); } else { display("couldnt save changes."); }
            }
            else
            {
                ViewBag.Error = "something went wrong";

            }

            return View("UploadID");
        }
       
        public ActionResult DownloadForm(string filename) {

            display("inside function");
            Response.Clear();
            Response.ContentType = "application/octect-stream";
            Response.AppendHeader("content-disposition", "filename="+filename);
            Response.TransmitFile(Server.MapPath("~/Content/forms/"+filename ));
            
            Response.End();
            return View("CompleteForm");
        }


        public ActionResult UploadForm() {
            return View();
        }
    }
}
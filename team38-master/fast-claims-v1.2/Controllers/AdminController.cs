using fast_claims_v1._2.Models.shared;
using fast_claims_v1._2.UTIL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fast_claims_v1._2.Models.admin;
using System.IO;

namespace fast_claims_v1._2.Controllers
{
    
    public class AdminController : Controller
    {
        private GenericUnitOfWork _unit;
        private DatabaseDataContext _database = new DatabaseDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [Route("admin/login")]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Autherize()
        {
            var loginmodel = new LoginModel();
            TryUpdateModel(loginmodel);

            if (ModelState.IsValid)
            {
                var query = (from login in _database.tbl_logins
                             join admin in _database.tbl_admins on login.login_id equals admin.login_id
                             where login.username.Equals(loginmodel.username) && login.password.Equals(loginmodel.password)
                             select new
                             {
                                 adminid = admin.admin_id

                            }).FirstOrDefault();

                if (query != null) {
                    // set session variables. and return index view. set roles 
                    Session["AdminId"] = query.adminid;
                    Session["Role"] = "admin";
                    return View("Index");
                }else {
                    ViewBag.Error = "username or password incorrect.";
                }
            }
            return View("Login");
        }


        [Route("admin/reset")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

       

        //gives you password without logging in
        [HttpPost]
        public ActionResult ResetPassword()
        {
            var passwordModel = new ForgotPasswordModel();
            TryUpdateModel(passwordModel);
            

            if (ModelState.IsValid)
            {
                var query = (from log in _database.tbl_logins
                             join adm in _database.tbl_admins on
                             log.login_id equals adm.login_id
                             where log.username.Equals(passwordModel.email)
                             select log).FirstOrDefault();

                if (query != null)
                {
                    return JavaScript("alert('Here are are your credentials: \n\nusername: " + query.username + ", password: " + query.password + "')");
                }
                else {
                    ViewBag.Error = "Incorrect email.";
                    
                    return JavaScript("alert('incorrect email')");
                }
               
            }
            return View("ForgotPassword");
        }



        public ActionResult ViewClaimApplications(bool ShowNew)
        {
            var modelList = new List<AdminClaimApplicationModel>();

            if (ShowNew)
            {
                
            }
            //read all querries regardless

            var query = (from application in _database.tbl_applications
                         join claimapplication in _database.tbl_claimapplications
                         on application.appl_id equals claimapplication.appl_id
                         where application.admin_id.Equals((int)Session["adminId"])
                         join claimant in _database.tbl_claimants
                         on claimapplication.claimant_id equals claimant.claimaint_id
                         select new
                         {
                             applicationid = application.appl_id,
                             date = application.date_created,
                             claimant_email = claimant.email,
                             status = application.isActive,
                             claim_nmber = claimapplication.claim_number
                         }).ToList();
            if (query != null)
            {
                foreach (var item in query)
                {
                    var model = new AdminClaimApplicationModel()
                    {
                        applicationid = item.applicationid,
                        datecreated = item.date,
                        claimnumber = item.claim_nmber,
                        email = item.claimant_email,
                        status = item.status.ToString()
                    };
                    modelList.Add(model);
                }

            }

            return View(modelList);
        }


        //[HttpPost]
        public ActionResult ViewUserRegistrations()
        {
            var results = _database.tbl_claimapplications.ToList();
            var modelList = new List<AdminClaimApplicationModel>();
            foreach (var d in results) {
                var rec = _database.tbl_applications.FirstOrDefault(e => e.appl_id.Equals(d.appl_id) && e.isActive.Equals(true));
                if (rec != null) {
                    var model = new AdminClaimApplicationModel() {
                        applicationid = rec.appl_id,
                        datecreated = rec.date_created,
                        claimnumber = d.claim_number,
                        email = _database.tbl_claimants.FirstOrDefault(cl => cl.claimaint_id.Equals(d.claimant_id)).email
                    };
                    modelList.Add(model);
                }
            }
            return View(modelList);
        }


        public ActionResult Logout()
        {
            return View("Login");
        }

        public ActionResult ChangePassword()
        {

            return View();
        }


        //changing password of a logged in user and is called by  
        [HttpPost]
        public ActionResult UpdatePassword()
        {

            var model = new ChangePasswordModel();
            TryUpdateModel(model);

            if (ModelState.IsValid)
            {
                var id = (int)Session["AdminId"];
                var result = _database.tbl_admins.FirstOrDefault(e => e.login_id.Equals(id));

                if (result != null)
                {
                    var query = (from m in _database.tbl_logins
                                 where m.login_id.Equals(result.login_id)
                                 select m).FirstOrDefault();

                    if (query != null)
                    {
                        query.password = model.newpassword;
                        _database.SubmitChanges();

                        //add script adds a pop notifying user that password changed
                        //return JavaScript(alert("Hello this is an alert"));
                        //return View("Index");

                        return Content("<script language='javascript' type='text/javascript'>alert('Thanks for Feedback!');</script>");
                    }
                    else
                    {
                        ViewBag.Error = "For some reason, the login id does not exist";
                    }
                }
                else { ViewBag.Error = "We could get id from session"; }
                
                
                if (result != null) {
                    
                }
                var LoginRec = _unit.GetRepositoryInstance<tbl_login>().GetFirstorDefaultByParameter(e => e.username.Equals("iamxteecy@gmail.com") && e.password.Equals(model.oldpassword));

                if (LoginRec != null)
                {
                    LoginRec.password = model.oldpassword;
                    _unit.SaveChanges();
                    return View(model);
                }
            }
            return View("ChangePassword", model);
        }

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Mail()
        {
            
            return View();
        }

        public ActionResult ViewApplication(int claimnumber) {


           
            var rec = _database.tbl_claimapplications.FirstOrDefault(e => e.claim_number.Equals(claimnumber));

            if (rec != null) {
               
                var query = (from claim in _database.tbl_claimants
                             join application in _database.tbl_claimapplications on
                             claim.claimaint_id equals application.claimant_id
                             select new {
                                 id = claim.iddocument,
                                 deathcert = application.deceaced_deathcert
                             }).FirstOrDefault();

                if (query != null) {
                    //save to model
                    var model = new ViewClaimApplicationModel() {
                        deathcertificate = query.deathcert,
                        iddocument = query.id
                    };
                    return View(model);
                } else {

                }
            }

            //returm model
            return JavaScript("alert('error 404')");
        }

        
        public ActionResult DisplayPDF(int? fileId)
        {
            if (fileId != null) {
                var file = _database.tbl_files.FirstOrDefault(e => e.fileId.Equals(fileId)).file.ToArray();

                if (file != null)
                {
                    MemoryStream pdfStream = new MemoryStream();
                    pdfStream.Write(file, 0, file.Length);
                    pdfStream.Position = 0;
                    return new FileStreamResult(pdfStream, "application/pdf");
                }
            }
            


            return JavaScript("alert('something went wrong!')");
        }

    }
}
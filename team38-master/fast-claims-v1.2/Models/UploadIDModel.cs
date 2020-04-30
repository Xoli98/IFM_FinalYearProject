using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace fast_claims_v1._2.Models
{
    public class UploadIDModel
    {
        [Required(ErrorMessage = "Death certificate required")]
        public HttpPostedFile deathcertificate { get; set; }

        [Required(ErrorMessage = "Your ID document is required")]
        public HttpPostedFile iddocument { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace fast_claims_v1._2.Models
{
    public class ClaimApplicationModel
    {
        [Required(ErrorMessage ="Please enter your email")]
        [RegularExpression("^.+\\@.+\\..+", ErrorMessage = "Please use a correct email syntax")]
        public string email { get; set; }

        

      
        [Required(ErrorMessage = "Please enter the deceaced id.")]
        
       // [RegularExpression("9|0[0-9]{2}\\0[1-9]|1[012]{2}\\[12][0-9]|3[01]{2}\\[0-9]{7}", ErrorMessage = "Please use correct id number syntax.")]
        public HttpPostedFileBase idnumber { get; set; }

        public string ErrorMessage { get; set; } = "";
        public string OTP { get; set; }
        public int claim_number { get; set; }

    }
}
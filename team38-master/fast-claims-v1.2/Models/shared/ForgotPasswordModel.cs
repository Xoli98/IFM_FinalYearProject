using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace fast_claims_v1._2.Models.shared
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "your email is required")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        

        public string errormessage { get; set; } = "rrrr";
    }
}
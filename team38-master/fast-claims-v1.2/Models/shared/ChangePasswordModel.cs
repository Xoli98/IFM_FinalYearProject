using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace fast_claims_v1._2.Models.shared
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "your old password is required")]
        [DataType(DataType.Password)]
        public string oldpassword { get; set; }

        [Required(ErrorMessage = "your new password is required")]
        [DataType(DataType.Password)]
        public string newpassword { get; set; }
        public string errormessage { get; set; }
    }
}
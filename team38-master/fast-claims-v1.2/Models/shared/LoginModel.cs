using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace fast_claims_v1._2.Models.shared
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Please enter your username.")]
        [DataType(DataType.EmailAddress)]
        public string username { get; set; }


        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string password { get; set; }

      
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fast_claims_v1._2.Models.admin
{
    public class AdminClaimApplicationModel
    {

        public int applicationid { get; set; }
        public DateTime datecreated { get; set; }
        public int claimnumber { get; set; }
        public string status { get; set; }

        public string email { get; set; }

    }
}
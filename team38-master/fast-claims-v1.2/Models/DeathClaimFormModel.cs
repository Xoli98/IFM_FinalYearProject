using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fast_claims_v1._2.Models
{
    public class DeathClaimFormModel
    {

        public int policynumber { get; set; }
        public int DateOfDeath { get; set; }
        public string CauseOfDeath { get; set; }

        public string Doctor { get; set; }
        public string DoctorsNumber { get; set; }
        public string FaxNumber { get; set; }
        public string PlaceOfDeath { get; set; }

        //Details of the undertaker

        public int UndertakersCompanyName { get; set; }
        public int UndertakerCompanyNumber { get; set; }
        public string ContactPerson { get; set; }
        public string PostalAddress { get; set; }
        public int UnderTakerTelNumber { get; set; }
        public int UndertakerFaxNumber { get; set; }
        public string PlaceOfBurial { get; set; }
        public DateTime DateOfBurial { get; set; }


        //who must sanlam communication with
        public string RelationWithDeceased { get; set; }
        public string ClaimantsLanguage { get; set; }
        public string ClaimantFullName { get; set; }
        public string ClaimantSurname { get; set; }
        public int ClaimantIDNumber { get; set; }
        public  string PostalAdress { get; set; }

    }

    

    


}
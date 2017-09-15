using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHRWEBAPI.Models
{
    public class DemographicsDetails
    {
        public int PERSONID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public DateTime? Birthday { get; set; }

    }
}
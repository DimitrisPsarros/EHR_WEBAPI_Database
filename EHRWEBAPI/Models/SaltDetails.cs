using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHRWEBAPI.Models
{
    public class SaltDetails
    {
        public string salt { get; set; }
        public string SaltedPassword { get; set; }
    }
}
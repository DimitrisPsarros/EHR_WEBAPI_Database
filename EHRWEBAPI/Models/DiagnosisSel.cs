using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHRWEBAPI.Models
{
    public class DiagnosisSel
    {
        public Nullable<int> VisitID { get; set; }
        public string Description { get; set; }
        public string ICD_Code { get; set; }
        public string ICD_Chapter { get; set; }
    }
}
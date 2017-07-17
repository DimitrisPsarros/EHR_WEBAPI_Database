using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHRWEBAPI.Models
{
    public class VisitDetails
    {
        public Nullable<System.DateTime> Date { get; set; }
        public int DoctorPersonID { get; set; }
        public Nullable<int> PersonID { get; set; }
        public int NumberOfVisit { get; set; }
        public string Description { get; set; }

        public virtual ICollection<DIagnosi> DIagnosis { get; set; }
        public virtual ICollection<Treat_Medicines> Treat_Medicines { get; set; }
    }
}
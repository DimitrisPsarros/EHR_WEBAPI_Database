//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EHRWEBAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DIagnosi
    {
        public int DiagnosisID { get; set; }
        public Nullable<int> VisitID { get; set; }
        public string Description { get; set; }
        public string ICD_CODE { get; set; }
    
        public virtual Visit Visit {internal get; set; }
    }
}

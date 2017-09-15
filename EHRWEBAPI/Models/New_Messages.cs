using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHRWEBAPI.Models
{
    public class New_Messages
    {
        public int DataSenderID { get; set; }
        public Nullable<int> PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Text { get; set; }
        public DateTime? Date { get; set; }
        public Nullable<bool> Seen { get; set; }
        
    }
}
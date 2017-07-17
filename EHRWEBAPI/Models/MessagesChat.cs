using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHRWEBAPI.Models
{
    public class MessagesChat
    {
       
        public Nullable<int> PersonID { get; set; }
        public Nullable<int> ReseiverID { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool IsMe { get; set; }
        
    }
}
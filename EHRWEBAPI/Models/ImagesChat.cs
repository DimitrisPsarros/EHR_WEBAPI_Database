using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHRWEBAPI.Models
{
    public class ImagesChat
    {
        public byte[] Picture { get; set; }
        public DateTime? Date { get; set; }
        public string Text { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientWeb.Models
{
    public class Part9DetailView
    {
        public int Id { get; set; }
        public string FileURI { get; set; }
        public string FileRemarks { get; set; }
        public DateTime? FileDateStamp { get; set; }
        public string FileUserStamp { get; set; }
        public HttpPostedFileBase UpLdFile { get; set; }  // byte[]
        //public byte[] UpLdFile { get; set; }
    }
}
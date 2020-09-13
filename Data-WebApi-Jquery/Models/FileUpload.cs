using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data_WebApi_Jquery.Models
{
    public class FileUpload
    {
        public int Id { get; set; }
        public string FileURI { get; set; }
        public string FileRemarks { get; set; }
        public DateTime? FileDateStamp { get; set; }
        public string FileUserStamp { get; set; }
    }
}
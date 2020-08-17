using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data_WebApi_Jquery.Models
{
    public class AccessToken
    {
        public int Id { get; set; }
        public string TokenString { get; set; }
        public System.DateTime Created { get; set; }
    }
}
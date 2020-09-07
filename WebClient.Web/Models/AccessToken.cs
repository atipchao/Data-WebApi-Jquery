using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientWeb.Models
{
    public class AccessToken
    {
        public int Id { get; set; }
        public string TokenString { get; set; }
        public DateTime? Created { get; set; }
    }
}
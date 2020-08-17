using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientWeb.Models
{
    public class Part7DetailView
    {

        public string userName { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public DateTime issued { get; set; }
        public DateTime expires { get; set; }


    }
}
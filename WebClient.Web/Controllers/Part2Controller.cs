using Data_WebApi_Jquery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Formatting;
using Data_WebApi_Jquery.Models;

namespace ClientWeb.Controllers
{
    public class Part2Controller : Controller
    {
        // GET: Part2
        public ActionResult Index() 
        {
            List<Employee> list = new List<Employee>();
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:44385/api/Example").Result;
            if (result.IsSuccessStatusCode)
            {
                list =  result.Content.ReadAsAsync<List<Employee>>().Result;                    
            }
            return View(list);
        }
    }
}
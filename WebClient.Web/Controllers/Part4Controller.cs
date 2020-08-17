using Data_WebApi_Jquery;
using Data_WebApi_Jquery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ClientWeb.Controllers
{
    public class Part4Controller : Controller
    {
        // GET: Part4
        public ActionResult Part4()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Part4(Employee emp)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var result = client.PostAsJsonAsync("http://localhost:44385/api/Example", emp).Result;
                if (result.IsSuccessStatusCode)
                {
                    emp = result.Content.ReadAsAsync<Employee>().Result;
                    ViewBag.Result = "Successfully saved!";
                    ModelState.Clear();
                    return View(new Employee());
                }
                else
                {
                    ViewBag.Result = "Error! Please try with valid data.";
                }
            }
            return View(emp);
        }
    }
}
using Data_WebApi_Jquery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Data_WebApi_Jquery.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }


        [HttpPost]
        public HttpResponseMessage Post(Employee emp)
        {
            string testString = Session["SelectedProductName"].ToString();
            HttpRequestMessage request = new HttpRequestMessage();
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                using (EmpDBEntities dc = new EmpDBEntities())
                {
                    dc.Employees.Add(emp);
                    dc.SaveChanges();
                }
                response = request.CreateResponse(HttpStatusCode.Created, emp);                
            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest, "Error! Please try again with valid data.");
            }
            return response;
        }

    }
}

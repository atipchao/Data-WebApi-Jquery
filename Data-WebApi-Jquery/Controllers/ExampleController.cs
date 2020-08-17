using Data_WebApi_Jquery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Data_WebApi_Jquery.Controllers
{
    public class ExampleController : ApiController
    {

        //adds GET action for fetching Employee records, and returns to Client over Http
        public HttpResponseMessage Get()
        {

            List<Employee> employees = new List<Employee>();
            EmpDBEntities _db = new EmpDBEntities();
            using (_db)
            {
                employees = _db.Employees.OrderBy(s => s.Name).ToList();
                HttpResponseMessage data;
                data = Request.CreateResponse(HttpStatusCode.OK, employees);
                return (data);
            }
            
        }

        [HttpPost]
        public HttpResponseMessage Post(Employee emp)
        {
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

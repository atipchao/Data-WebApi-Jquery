using Data_WebApi_Jquery.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientWeb.Models;
using System.Net.Http;
//using WebClient.Utilities.TokenStuff;

namespace WebClient.Web.Controllers
{
    public class Part8Controller : Controller
    {
        // database connection to App local Db
        // we use this connection to Fetch our API access-token - kept locally.
        EmpDBEntities _db = new EmpDBEntities(); 

        // GET: Part8
        public ActionResult Index()
        {
            //call web-api from TRM data manager @ localhost:56473
            // Get the last Token - order desc by  Id
            Data_WebApi_Jquery.Models.AccessToken _accessToken = _db.AccessTokens
                                                                    .OrderByDescending(p => p.Id)
                                                                    .FirstOrDefault();
            string accessToken = _accessToken.TokenString;


            List<Part8IndexView> list = new List<Part8IndexView>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var result = client.GetAsync("http://localhost:56473/api/EmpAPI").Result;
            if (result.IsSuccessStatusCode)
            {
                list = result.Content.ReadAsAsync<List<Part8IndexView>>().Result;
                return View(list);
            }
            else
            {
                //refreshTToken();
                string newToken = WebClient.Utilities.TokenStuff.refreshTToken();
                List<Part8IndexView> list2 = new List<Part8IndexView>();
                HttpClient client2 = new HttpClient();
                client2.DefaultRequestHeaders.Add("Authorization", "Bearer " + newToken);
                var result2 = client2.GetAsync("http://localhost:56473/api/EmpAPI").Result;
                if (result2.IsSuccessStatusCode)
                {
                    list2 = result2.Content.ReadAsAsync<List<Part8IndexView>>().Result;
                    return View(list2);
                }
            }


            return View(list);

            
        }
    }
}
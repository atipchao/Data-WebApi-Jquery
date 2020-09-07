using Data_WebApi_Jquery;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using ClientWeb.Models;
//using WebClient.Web.Models;

using Data_WebApi_Jquery.Models;
using Data_WebApi_Jquery.Controllers;

namespace ClientWeb.Controllers
{
    public class Part7Controller : Controller
    {
        EmpDBEntities _db = new EmpDBEntities();

        public string Test_String;
        // GET: Part7
        // Passing in the userName & password, and Just to show the Key-Token
        public ActionResult Index()
        {

            if (Session["SelectedProductName"] != null)
            {
                Test_String = Session["SelectedProductName"].ToString();
            }
            //await GetTokenDetails("atip.chao1@gmail.com", "123456");
            return View();
        }

        [HttpPost]
        public ActionResult Index(Part7IndexView part7IndexView)
        {
            if (Session["SelectedProductName"] != null)
            {
               Test_String = Session["SelectedProductName"].ToString();
            }

        //await GetTokenDetails("atip.chao1@gmail.com", "123456");

            string userName = part7IndexView.UserName.Trim();
            //string passWord = "1234567";
            string passWord = part7IndexView.Password.Trim();
            string BaseAddress = "http://localhost:56473/Token";

            using (var client = new HttpClient { BaseAddress = new Uri(BaseAddress) })
            {
                var token = client.PostAsync("Token",
                    new FormUrlEncodedContent(new[]
                    {
            new KeyValuePair<string,string>("grant_type","password"),
            new KeyValuePair<string,string>("username", userName),
            new KeyValuePair<string,string>("password", passWord)
                    })).Result.Content.ReadAsAsync<AuthenticationToken>().Result;

                Console.WriteLine("...");

                if (token != null)
                {
                    part7IndexView.access_token = token.access_token.ToString().Trim();

                    client.DefaultRequestHeaders.Authorization =
                           new AuthenticationHeaderValue(token.token_type, token.access_token);

                    // insert accessToken to AccessToken table
                    // create new AccessToken record
                    Data_WebApi_Jquery.Models.AccessToken accessToken = new Data_WebApi_Jquery.Models.AccessToken();
                    accessToken.Created = DateTime.Now;
                    accessToken.TokenString = token.access_token;
                    Session["SelectedProductName"] = token.access_token;

                    
                    try
                    {
                        using (EmpDBEntities dc = new EmpDBEntities())
                        {

                            dc.AccessTokens.Add(accessToken);
                            dc.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }




                }
                // actual requests from your api follow here . . .
            }
            
            return View(part7IndexView);
        }




    }
}
using ClientWeb.Models;
using Data_WebApi_Jquery.Controllers;
using Data_WebApi_Jquery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WebClient.Utilities
{
    public static class TokenStuff
    {
        public static string refreshTToken()
        {
            string _token = "";

            string userName = "atip1.chao@gmail.com";
            string passWord = "1234567";

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
                    _token = token.access_token.ToString().Trim();
                    // insert accessToken to AccessToken table
                    // create new AccessToken record
                    Data_WebApi_Jquery.Models.AccessToken accessToken = new Data_WebApi_Jquery.Models.AccessToken();
                    accessToken.Created = DateTime.Now;
                    accessToken.TokenString = token.access_token;



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

                    return _token;
                }
            }
            return _token;
        }
    }
}
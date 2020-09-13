using ClientWeb.Models;
using Data_WebApi_Jquery.Controllers;
using Data_WebApi_Jquery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//using System.Web.UI.WebControls;

namespace WebClient.Web.Controllers
{
    public class Part9FileUploadController : Controller
    {
        // database connection to App local Db
        // we use this connection to Fetch our API access-token - kept locally.
        EmpDBEntities _db = new EmpDBEntities();
        
        // GET: Part9FileUpload
        public ActionResult Index()
        {
            //call web-api from TRM data manager @ localhost:56473
            // Get the last Token - order desc by  Id
            Data_WebApi_Jquery.Models.AccessToken _accessToken = _db.AccessTokens
                                                                    .OrderByDescending(p => p.Id)
                                                                    .FirstOrDefault();
            string accessToken = _accessToken.TokenString;

            //get list of FileUpload            
            var fileUploads = _db.FileUploads.ToList();
            // Map fileUploads to ViewModel Part9InexView
            List<Part9InexView> part9InexViews = new List<Part9InexView>();
            foreach (FileUpload rec in fileUploads)
            {
                Part9InexView part9InexView = new Part9InexView();
                part9InexView.Id = rec.Id;
                part9InexView.FileURI = rec.FileURI;
                part9InexView.FileRemarks = rec.FileRemarks;
                part9InexView.FileDateStamp = rec.FileDateStamp;
                part9InexView.FileUserStamp = rec.FileUserStamp;
                part9InexViews.Add(part9InexView);
            }

            return View(part9InexViews);
        }

        [HttpGet]
        public ActionResult Detail(int Id)
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult Detail(Part9DetailView part9DetailView)
        {
            // do something with model.SomeFile

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Part9DetailView part9DetailView)
        {
            Data_WebApi_Jquery.Models.AccessToken _accessToken = _db.AccessTokens
                                                                    .OrderByDescending(p => p.Id)
                                                                    .FirstOrDefault();
            string accessToken = _accessToken.TokenString;
            //*************************************
            // First TEST token - if still valid
            //*************************************
            HttpClient TestClient = new HttpClient();
            TestClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var resultToken = TestClient.GetAsync("http://localhost:56473/api/values").Result;
            if (!resultToken.IsSuccessStatusCode)
            {
                //Refresh Token
                string newToken = WebClient.Utilities.TokenStuff.refreshTToken();
                accessToken = newToken;
            }
            TestClient.Dispose();
            // After getting the token, all you have to do is below.. - passing token to API - see line#
            if (ModelState.IsValid)
            {
                var file = part9DetailView.UpLdFile; // this is where we pass in the upload file
                                                     // File will be passed in as header attachment    
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken); // passing token to the api
                    using (var content = new MultipartFormDataContent())
                    {                        
                        byte[] Bytes = new byte[file.InputStream.Length + 1];
                        file.InputStream.Read(Bytes, 0, Bytes.Length);
                        var fileContent = new ByteArrayContent(Bytes);
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = file.FileName };
                        content.Add(fileContent);
                        var requestUri = "http://localhost:56473/api/Upload?userStmp=BX";
                        var result = client.PostAsync(requestUri, content ).Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            List<string> m = result.Content.ReadAsAsync<List<string>>().Result;
                            ViewBag.Success = m.FirstOrDefault();//m[0] => URI, m[1] => scaned id

                            // Save into your app file Upload table

                        }
                        else
                        {
                            ViewBag.Failed = "Failed !" + result.Content.ToString();
                        }
                    }
                }

            }
            return View();
        }

    }
}
using EquinitiSampleProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EquinitiSampleProject.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<Application> lstApplication = new List<Application>();

            string token = string.Empty;
            using (var apiclient = new Apiclient())
            {
                apiclient.BaseAddress = new Uri("http://localhost:49451/Models/appJson.json");
                HttpResponseMessage Res = await apiclient.GetAsync("appJson.json");

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list   
                    lstApplication = JsonConvert.DeserializeObject<List<Application>>(EmpResponse);
                }
            }

            Session["Database"] = lstApplication;
            ViewBag.Title = "Home Page";
            return View(lstApplication);
        }

        [HttpGet]
        public ActionResult edit(Guid? id)
        {
            Guid appId = id ?? Guid.Empty;
            ViewBag.Title = "Edit Application";
            List<Application> lstApplication = Session["Database"] as List<Application>;
            Application application = lstApplication.SingleOrDefault(a => a.Id == appId);
            return View(application);
        }

        [HttpPost]
        public async Task<ActionResult> edit(Application application)
        {

            using (var apiclient = new Apiclient())
            {
                string jsonString = JsonConvert.SerializeObject(application);
                var buffer = Encoding.UTF8.GetBytes(jsonString);
                var byteContent = new ByteArrayContent(buffer);

                HttpResponseMessage Res = await apiclient.PostAsync("api/EditApplication", byteContent);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list   
                }
            }

            return RedirectToAction("Index");
        }
    }
}

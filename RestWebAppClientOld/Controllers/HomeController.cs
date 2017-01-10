using RestWebAppClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace RestWebAppClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var client = new HttpClient();
            Employees model = null;
            var task = client.GetAsync("http://localhost:50775/Employees")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var readtask = response.Content.ReadAsAsync<Employees>();
                    readtask.Wait();
                    return model.Result;
                }};
            task.Wait();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
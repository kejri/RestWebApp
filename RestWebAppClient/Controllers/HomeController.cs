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
            List<Employee> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("http://localhost:50775/api/Employees")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var readtask = response.Content.ReadAsAsync<List<Employee>>();
                    readtask.Wait();
                    model = readtask.Result;
                });
            task.Wait();
            return View(model);
        }
    }
}

using RestWebAppClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace RestWebAppClient.Controllers
{
    public class EmployeesController : Controller
    {
        static string urlService = "http://localhost:50775/api/Employees";
        public ActionResult Index()
        {
            var model = new EmployeesModel();
            var client = new HttpClient();
            var task = client.GetAsync(urlService)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var readtask = response.Content.ReadAsAsync<List<Employee>>();
                    readtask.Wait();
                    model.Employees = readtask.Result;
                });
            task.Wait();
            return View(model.Employees);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = new EmployeesModel();
            var client = new HttpClient();
            string url = String.Format("{0}/{1}", urlService, id);
            var task = client.GetAsync(url)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var readtask = response.Content.ReadAsAsync<Employee>();
                    readtask.Wait();
                    model.Employee = readtask.Result;
                });
            task.Wait();
            return View(model.Employee);
        }

        public ActionResult Create()
        {
            var model = new EmployeesModel();
            model.Employee = new Employee() { D_Narozeni = DateTime.Today, EmployeeType = EmployeeType.Ostatni };
            return View(model.Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Prijmeni,Jmeno,D_Narozeni,EmployeeType,Vyska")] Employee data)
        {
            if (ModelState.IsValid)
            {
                var client = new HttpClient();
                string url = String.Format(urlService);
                var task = client.PostAsJsonAsync<Employee>(url, data)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    response.EnsureSuccessStatusCode();
                }).ContinueWith((err) =>
                {
                    if (err.Exception != null)
                    {

                    }
                });
                task.Wait();
            }
            return View(data);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = new EmployeesModel();
            var client = new HttpClient();
            string url = String.Format("{0}/{1}", urlService, id);
            var task = client.GetAsync(url)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var readtask = response.Content.ReadAsAsync<Employee>();
                    readtask.Wait();
                    model.Employee = readtask.Result;
                });
            task.Wait();
            return View(model.Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Prijmeni,Jmeno,D_Narozeni,EmployeeType,Vyska")] Employee data)
        {
            if (ModelState.IsValid)
            {
                var client = new HttpClient();
                string url = String.Format("{0}/{1}", urlService, data.Id);
                var task = client.PutAsJsonAsync<Employee>(url, data)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    response.EnsureSuccessStatusCode();
                }).ContinueWith((err) =>
                {
                    if (err.Exception != null)
                    {

                    }
                });
                task.Wait();
            }
            return View(data);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = new EmployeesModel();
            var client = new HttpClient();
            string url = String.Format("{0}/{1}", urlService, id);
            var task = client.GetAsync(url)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var readtask = response.Content.ReadAsAsync<Employee>();
                    readtask.Wait();
                    model.Employee = readtask.Result;
                });
            task.Wait();
            return View(model.Employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            var client = new HttpClient();
            string url = String.Format("{0}/{1}", urlService, id);
            var task = client.DeleteAsync(url)
            .ContinueWith((taskwithresponse) =>
            {
                var response = taskwithresponse.Result;
                response.EnsureSuccessStatusCode();
            }).ContinueWith((err) =>
            {
                if (err.Exception != null)
                {

                }
            });
            task.Wait();
            return RedirectToAction("Index");
        }
    }
}

using RestWebAppClient.Helpers;
using RestWebAppClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RestWebAppClient.Controllers
{
    public class EmployeesController : Controller
    {
        static async Task<HttpResponseMessage> SendDataToApi(AuthenticationToken token, Employee employee)
        {
            using (var client = HttpClientHelper.GetClient(token))
            {
                string url = String.Format(Const.UrlServiceEmployee);
                HttpResponseMessage response = await client.PostAsJsonAsync<Employee>(url, employee);
                return response;
            }
        }
        public ActionResult Create()
        {
            var employee = new Employee() { D_Narozeni = DateTime.Today, EmployeeType = EmployeeType.Ostatni };
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Prijmeni,Jmeno,D_Narozeni,EmployeeType,Vyska")] Employee employee)
        {
            var sessionHelper = new SessionHelper(HttpContext.Session);
            var token = SecurityHelper.GetToken(sessionHelper);
            var response = await SendDataToApi(token, employee);
            return RedirectToAction("Create", "Employees", response.StatusCode == HttpStatusCode.Created ? new { message = "Zaznam byl ulozen uspesne." } : new { message = "Zaznam nebyl ulozen." });
        }
    }
}
using RestWebAppClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using RestWebAppClient.Helpers;
using System.Net.Http.Headers;

namespace RestWebAppClient.Controllers
{
    public class EmployeesController : Controller
    {
        static string urlService = "http://localhost:50775";
        static string urlServiceEmployee = urlService + "/api/Employees";
        static string adminUser = "admin@email.com";
        static string adminPassword = "Admin@123";

        public AuthenticationToken GetToken(string username, string password)
        {
            var sessionHelper = new SessionHelper(HttpContext.Session);
            var  token = sessionHelper.Token;
            if (token == null)
            {
                using (var client = new HttpClient { BaseAddress = new Uri(urlService) })
                {
                    token = client.PostAsync("Token",
                        new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string,string>("grant_type","password"),
                        new KeyValuePair<string,string>("username", username),
                        new KeyValuePair<string,string>("password",password)
                        })).Result.Content.ReadAsAsync<AuthenticationToken>().Result;

                    if (String.IsNullOrEmpty(token.access_token))
                    {
                        throw new Exception("Chybné uživatelské jméno nebo heslo.");
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(token.token_type, token.access_token);

                }
                sessionHelper.Token = token;
            }
            return token;
        }
        public ActionResult Index()
        {
            var token = GetToken(adminUser, adminPassword);

            var sessionHelper = new SessionHelper(HttpContext.Session);
            sessionHelper.Employees = null;
            using (var client = HttpClientHelper.GetClient(token))
            {
                var task = client.GetAsync(urlServiceEmployee)
                    .ContinueWith((taskwithresponse) =>
                    {
                        var response = taskwithresponse.Result;
                        response.EnsureSuccessStatusCode();

                        var readtask = response.Content.ReadAsAsync<List<Employee>>();
                        readtask.Wait();
                        sessionHelper.Employees = readtask.Result;

                    }).ContinueWith((err) =>
                    {
                        if (err.Exception != null)
                        {
                            throw err.Exception;
                        }
                    });
                task.Wait();
                return View(sessionHelper.Employees);
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = null;
            var sessionHelper = new SessionHelper(HttpContext.Session);
            if (sessionHelper.Employees != null)
            {
                employee = sessionHelper.Employees.FirstOrDefault<Employee>(item => item.Id == (int)id);
                if (employee == null)
                {
                    var token = GetToken(adminUser, adminPassword);
                    using (var client = HttpClientHelper.GetClient(token))
                    {
                        string url = String.Format("{0}/{1}", urlServiceEmployee, id);
                        var task = client.GetAsync(url)
                            .ContinueWith((taskwithresponse) =>
                            {
                                var response = taskwithresponse.Result;
                                response.EnsureSuccessStatusCode();

                                var readtask = response.Content.ReadAsAsync<Employee>();
                                readtask.Wait();
                                employee = readtask.Result;
                            }).ContinueWith((err) =>
                            {
                                if (err.Exception != null)
                                {
                                    throw err.Exception;
                                }
                            });
                        task.Wait();
                    }
                }
            }
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        public ActionResult Create()
        {

            var employee = new Employee() { D_Narozeni = DateTime.Today, EmployeeType = EmployeeType.Ostatni };
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Prijmeni,Jmeno,D_Narozeni,EmployeeType,Vyska")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var token = GetToken(adminUser, adminPassword);
                using (var client = HttpClientHelper.GetClient(token))
                {
                    string url = String.Format(urlServiceEmployee);
                    var task = client.PostAsJsonAsync<Employee>(url, employee)
                    .ContinueWith((taskwithresponse) =>
                    {
                        var response = taskwithresponse.Result;
                        response.EnsureSuccessStatusCode();
                    }).ContinueWith((err) =>
                    {
                        if (err.Exception != null)
                        {
                            throw err.Exception;
                        }
                    });
                    task.Wait();
                }
            }
            return View(employee);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = null;
            var sessionHelper = new SessionHelper(HttpContext.Session);
            if (sessionHelper.Employees != null)
            {
                employee = sessionHelper.Employees.FirstOrDefault<Employee>(item => item.Id == (int)id);
                if (employee == null)
                {
                    var token = GetToken(adminUser, adminPassword);
                    using (var client = HttpClientHelper.GetClient(token))
                    {
                        string url = String.Format("{0}/{1}", urlServiceEmployee, id);
                        var task = client.GetAsync(url)
                            .ContinueWith((taskwithresponse) =>
                            {
                                var response = taskwithresponse.Result;
                                response.EnsureSuccessStatusCode();

                                var readtask = response.Content.ReadAsAsync<Employee>();
                                readtask.Wait();
                                employee = readtask.Result;
                            }).ContinueWith((err) =>
                            {
                                if (err.Exception != null)
                                {
                                    throw err.Exception;
                                }
                            });
                        task.Wait();
                    }
                }
            }
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Prijmeni,Jmeno,D_Narozeni,EmployeeType,Vyska")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var token = GetToken(adminUser, adminPassword);
                using (var client = HttpClientHelper.GetClient(token))
                {
                    string url = String.Format("{0}/{1}", urlServiceEmployee, employee.Id);
                    var task = client.PutAsJsonAsync<Employee>(url, employee)
                    .ContinueWith((taskwithresponse) =>
                    {
                        var response = taskwithresponse.Result;
                        response.EnsureSuccessStatusCode();
                    }).ContinueWith((err) =>
                    {
                        if (err.Exception != null)
                        {
                            throw err.Exception;
                        }
                    });
                    task.Wait();
                }
            }
            return View(employee);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = null;
            var sessionHelper = new SessionHelper(HttpContext.Session);
            if (sessionHelper.Employees != null)
            {
                employee = sessionHelper.Employees.FirstOrDefault<Employee>(item => item.Id == (int)id);
                if (employee == null)
                {
                    var token = GetToken(adminUser, adminPassword);
                    using (var client = HttpClientHelper.GetClient(token))
                    {
                        string url = String.Format("{0}/{1}", urlServiceEmployee, id);
                        var task = client.GetAsync(url)
                            .ContinueWith((taskwithresponse) =>
                            {
                                var response = taskwithresponse.Result;
                                response.EnsureSuccessStatusCode();

                                var readtask = response.Content.ReadAsAsync<Employee>();
                                readtask.Wait();
                                employee = readtask.Result;
                            }).ContinueWith((err) =>
                            {
                                if (err.Exception != null)
                                {
                                    throw err.Exception;
                                }
                            });
                        task.Wait();
                    }
                }
            }
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            var token = GetToken(adminUser, adminPassword);
            using (var client = HttpClientHelper.GetClient(token))
            {
                string url = String.Format("{0}/{1}", urlServiceEmployee, id);
                var task = client.DeleteAsync(url)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    response.EnsureSuccessStatusCode();
                }).ContinueWith((err) =>
                {
                    if (err.Exception != null)
                    {
                        throw err.Exception;
                    }
                });
                task.Wait();
                return RedirectToAction("Index");
            }
        }
    }
}

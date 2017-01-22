using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestWebAppService.Models;

namespace RestWebAppService.Controllers
{
    [Authorize (Roles = "Administrators")]
    public class EmployeesController : Controller
    {
        private RestWebDbEntities db = new RestWebDbEntities();

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            var employees = await db.Employees.ToListAsync();
            var employeesVM = new List<RestWebAppService.ViewModels.Employee>();
            foreach (var employee in employees)
            {
                var employeeVM = new RestWebAppService.ViewModels.Employee();
                employeeVM.Read(employee);
                employeesVM.Add(employeeVM);
            }
            return View(employeesVM);
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            var employeeVM = new RestWebAppService.ViewModels.Employee();
            employeeVM.Read(employee);
            return View(employeeVM);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            var employeeVM = new RestWebAppService.ViewModels.Employee() { D_Narozeni = DateTime.Today, EmployeeType = RestWebAppService.ViewModels.EmployeeType.Ostatni };
            return View(employeeVM);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Prijmeni,Jmeno,D_Narozeni,EmployeeType,Vyska")] RestWebAppService.ViewModels.Employee employeeVM)
        {
            var employee = new Employee();
            employeeVM.Write(employee);
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(employeeVM);
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            var employeeVM = new RestWebAppService.ViewModels.Employee();
            employeeVM.Read(employee);
            return View(employeeVM);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Prijmeni,Jmeno,D_Narozeni,EmployeeType,Vyska")] RestWebAppService.ViewModels.Employee employeeVM)
        {
            var employee = new Employee();
            employeeVM.Write(employee);
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(employeeVM);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            var employeeVM = new RestWebAppService.ViewModels.Employee();
            employeeVM.Read(employee);
            return View(employeeVM);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

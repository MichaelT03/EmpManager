using EmpManager.Data;
using EmpManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmpManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        // Constructor
        public HomeController(ApplicationDbContext db)
        {
            // Ensures that data is only being read
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewEmployees()
        {
            // Put data from table and put into a list to be passed to the view
            List<Employee> objEmployeeList = _db.Employees.ToList();

            return View(objEmployeeList);
        }

        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee (Employee obj)
        {

            if (obj.Shift == null)
            {
                obj.Shift = 2;
            }

            if (ModelState.IsValid)
            {
                _db.Employees.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Employee added successfully";
                return RedirectToAction("ViewEmployees", "Home");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // .Find() only works with primary keys. For others use .FirstOrDefault(u => u.Id == id)
            Employee? employeeFromDb = _db.Employees.Find(id);

            if (employeeFromDb == null)
            {
                return NotFound();
            }

            return View(employeeFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Employee obj)
        {
            if (ModelState.IsValid)
            {
                // Uses update instead of add
                _db.Employees.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Employee updated successfully";
                return RedirectToAction("ViewEmployees", "Home");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Employee? employeeFromDd = _db.Employees.Find(id);

            if (employeeFromDd == null)
            {
                return NotFound();
            }

            return View(employeeFromDd);
        }

        // The mehtods name cannot be Delete since there is another method with that name and the same parameters.
        // The ActionName tag alleviates this issue
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Employee? obj = _db.Employees.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Employees.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Employee deleted successfully";
            return RedirectToAction("ViewEmployees", "Home");
        }

        public IActionResult Punch(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Employee? employeeFromDb = _db.Employees.Find(id);

            return View(employeeFromDb);
        }

        [HttpPost, ActionName("Punch")]
        public IActionResult PunchPOST(int ?id)
        {
            var Emp = _db.Employees.Find(id);

            string Confirmation = string.Empty;

            if (Emp != null)
            {
                if (!Emp.IsClockedIn)
                {
                    Emp.IsClockedIn = true;
                    Confirmation = Emp.FirstName + " Was Clocked In!";
                }
                else
                {
                    Emp.IsClockedIn = false;
                    Confirmation = Emp.FirstName + " Was Clocked Out!";
                }

                _db.Employees.Update(Emp);
                _db.SaveChanges();
                TempData["success"] = Confirmation;

                return RedirectToAction("ViewEmployees", "Home");

            }
            return View();
        }

        public IActionResult Working()
        {
            List<Employee> objEmployeeList = _db.Employees.ToList();

            return View(objEmployeeList);
        }
    }
}

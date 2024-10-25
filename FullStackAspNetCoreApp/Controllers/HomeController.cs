using FullStackAspNetCoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FullStackAspNetCoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly List<Employee> _employees;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Department = "IT", Email = "john.doe@gmail.com" },
                new Employee { Id = 2, Name = "Jane Doe", Department = "HR", Email = "jane.doe@gmail.com" },
                new Employee { Id = 3, Name = "Sammy Doe", Department = "IT", Email = "sammy.doe@gmail.com" }
            };
        }

        public IActionResult Delete(int Id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == Id);
            return View(employee);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int Id)
        {
            try
            {
                var employee = _employees.FirstOrDefault(e => e.Id == Id);
                if (employee != null)
                {
                    _employees.Remove(employee);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in Delete: {Message}", ex.Message);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        public IActionResult Index()
        {
            try
            {
                return View(_employees);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in Index: {Message}", ex.Message);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client, NoStore = false)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

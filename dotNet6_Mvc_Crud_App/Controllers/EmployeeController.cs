using dotNet6_Mvc_Crud_App.Data;
using dotNet6_Mvc_Crud_App.Models;
using dotNet6_Mvc_Crud_App.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace dotNet6_Mvc_Crud_App.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext appDbContext;

        public EmployeeController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
   

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var newEmployee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary
            };

            await appDbContext.Employees.AddAsync(newEmployee);
            await appDbContext.SaveChangesAsync();

            return RedirectToAction("Add");
        }


    }
}

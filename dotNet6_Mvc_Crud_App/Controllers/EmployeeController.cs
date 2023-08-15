using dotNet6_Mvc_Crud_App.Data;
using dotNet6_Mvc_Crud_App.Models;
using dotNet6_Mvc_Crud_App.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
           var employees = await appDbContext.Employees.ToListAsync();
            return View(employees);
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

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if(employee != null)
            {
                var viewModelEmployee = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                    Email = employee.Email,
                    Salary = employee.Salary
                };
                return await Task.Run(() => View("View", viewModelEmployee));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel updateModelEmployee)
        {
            var existEmployee = await appDbContext.Employees.FindAsync(updateModelEmployee.Id);

            if(existEmployee != null)
            {
                existEmployee.Name = updateModelEmployee.Name;
                existEmployee.DateOfBirth = updateModelEmployee.DateOfBirth;
                existEmployee.Email = updateModelEmployee.Email;
                existEmployee.Salary = updateModelEmployee.Salary;
                existEmployee.Department = updateModelEmployee.Department;
             
                await appDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel deleteModelEmployee)
        {
            var existEmployee = await appDbContext.Employees.FindAsync(deleteModelEmployee.Id);

            if (existEmployee != null)
            {
                appDbContext.Employees.Remove(existEmployee);
                await appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}

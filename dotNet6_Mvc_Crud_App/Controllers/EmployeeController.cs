using Microsoft.AspNetCore.Mvc;

namespace dotNet6_Mvc_Crud_App.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


    }
}

using Employee.MVC.Models;
using Employee.MVC.Services.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Employee.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IBusinessService _businessService;

        public EmployeeController(IBusinessService businessService)
        {
            _businessService = businessService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Search(EmployeeModel employeeInfo)
        {
            if (employeeInfo.Id == 0)
                return RedirectToAction("SearchEmployees");

            var employee = await _businessService.GetEmployeesById(employeeInfo.Id);

            if (employee == null)
                return Error(new ErrorViewModel() { RequestId = Guid.NewGuid().ToString() });

            return View(employee);
        }


        [HttpGet]
        public async Task<IActionResult> SearchEmployees()
        {
            IEnumerable<EmployeeModel> employees = await _businessService.GetEmployees().ConfigureAwait(true);
            return View(employees);
        }


        public  IActionResult Error(ErrorViewModel error)
        {
            return View(error);
        }

    }
}

using Employee.MVC.Models;

namespace Employee.MVC.Services.Business.Contracts;

public interface IBusinessService
{
    Int32 CalculateEmployeesAnnualSalary(int emplyID);
    Task<EmployeeModel> GetEmployeesById(int emplyID);
    Task<List<EmployeeModel>> GetEmployees();
}

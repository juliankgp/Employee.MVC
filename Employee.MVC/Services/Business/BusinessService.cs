using Employee.MVC.Extensions.Constants;
using Employee.MVC.Extensions.Exceptions;
using Employee.MVC.Models;
using Employee.MVC.Services.Business.Contracts;
using Employee.MVC.Services.SharedKernel.Interfaces;

namespace Employee.MVC.Services.Business;

public class BusinessService : IBusinessService
{
    private readonly IRestService _restClient;
    private readonly IConfiguration _configuration;

    public BusinessService(IRestService restClient, IConfiguration configuration)
    {
        _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<List<EmployeeModel>> GetEmployees()
    {
        var urlBase = $"{_configuration.GetValue<string>("Services:EmployeeService:UrlBase")}s";
        var response = await _restClient.GetRestService<ModelResponse<List<EmployeeModel>>>(urlBase, new Dictionary<string, string>());
        foreach (var employee in response.Data)
            employee.Employee_anual_salary = CalculateEmployeesAnnualSalary(employee.Employee_salary);

        return response.Data;
    }

    public async Task<EmployeeModel> GetEmployeesById(int emplyID)
    {
        var urlBase = $"{_configuration.GetValue<string>("Services:EmployeeService:UrlBase")}/{emplyID}";
        var response = await _restClient.GetRestService<ModelResponse<EmployeeModel>>(urlBase, new Dictionary<string, string>());
        if (response != null && response.Data != null)
            response.Data.Employee_anual_salary = CalculateEmployeesAnnualSalary(response.Data.Employee_salary);
        else
            return null;
        return response.Data;
    }

    public Int32 CalculateEmployeesAnnualSalary(int salary )
    {
        if (salary != 0)
            return salary * 12;

        throw new ApiException(Constants.MSG_FAIL_API);
    }
}

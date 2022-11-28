using Employee.MVC.Extensions.Exceptions;
using Employee.MVC.Models;
using Employee.MVC.Services.Business;
using Employee.MVC.Services.SharedKernel;
using Employee.MVC.Services.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Employee.MVC.Test.Services.Business;

public class BusinessServiceTest
{
    [Fact]
    public void BusinessService_GetEmployees_Success()
    {
        #region Arrange
        var restServiceMock = new Mock<IRestService>();
        restServiceMock.Setup(m => m.GetRestService<ModelResponse<List<EmployeeModel>>>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
            .ReturnsAsync(new ModelResponse<List<EmployeeModel>>() { Data = new List<EmployeeModel>() { new EmployeeModel() { Employee_salary = 12222} } } );

        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c.GetSection(It.IsAny<string>()))
            .Returns(new Mock<IConfigurationSection>().Object);

        var businessService = new BusinessService(restServiceMock.Object, configMock.Object);
        #endregion

        #region Act
        var response = businessService.GetEmployees();
        #endregion

        #region Assert
        Assert.IsType<List<EmployeeModel>>(response.Result);
        #endregion
    }

    [Fact]
    public void BusinessService_GetEmployeesById_Success()
    {
        #region Arrange
        var restServiceMock = new Mock<IRestService>();
        restServiceMock.Setup(m => m.GetRestService<ModelResponse<EmployeeModel>>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
            .ReturnsAsync(new ModelResponse<EmployeeModel>() { Data = new EmployeeModel() { Employee_salary = 12222}  } );

        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c.GetSection(It.IsAny<string>()))
            .Returns(new Mock<IConfigurationSection>().Object);

        var businessService = new BusinessService(restServiceMock.Object, configMock.Object);
        #endregion

        #region Act
        var response = businessService.GetEmployeesById(2);
        #endregion

        #region Assert
        Assert.IsType<EmployeeModel>(response.Result);
        #endregion
    }


    [Fact]
    public void BusinessService_CalculateEmployeesAnnualSalary_Success()
    {
        #region Arrange
        var restServiceMock = new Mock<IRestService>();
        restServiceMock.Setup(m => m.GetRestService<ModelResponse<EmployeeModel>>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
            .ReturnsAsync(new ModelResponse<EmployeeModel>() { Data = new EmployeeModel() { Employee_salary = 12222 } });

        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c.GetSection(It.IsAny<string>()))
            .Returns(new Mock<IConfigurationSection>().Object);

        var businessService = new BusinessService(restServiceMock.Object, configMock.Object);
        #endregion

        #region Act
        var response = businessService.CalculateEmployeesAnnualSalary(2);
        #endregion

        #region Assert
        Assert.IsType<Int32>(response);
        #endregion
    }

    [Fact]
    public void BusinessService_CalculateEmployeesAnnualSalary_Catch()
    {
        #region Arrange
        var restServiceMock = new Mock<IRestService>();
        restServiceMock.Setup(m => m.GetRestService<ModelResponse<EmployeeModel>>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
            .ReturnsAsync(new ModelResponse<EmployeeModel>());

        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c.GetSection(It.IsAny<string>()))
            .Returns(new Mock<IConfigurationSection>().Object);

        var businessService = new BusinessService(restServiceMock.Object, configMock.Object);
        #endregion

        #region Act
        #endregion

        #region Assert
        Assert.Throws<ApiException>(() => businessService.CalculateEmployeesAnnualSalary(2) );
        #endregion
    }
}

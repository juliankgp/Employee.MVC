using Employee.MVC.Models;
using Employee.MVC.Services.SharedKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;

namespace Employee.MVC.Test.Services.SharedKernel;

public class RestServiceTest
{
    readonly IConfigurationRoot build;
    readonly string url = "";

    public RestServiceTest()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        build = builder.Build();
        url = build.GetValue<string>("Services:EmployeeService:UrlBase");
    }

    [Fact]
    public void RestServiceTest_PostServiceAsync_Cath()
    {
        #region Arrange
        //var loggerMock = Mock.Of<ILogger<RestService>>();
        var httpClientFactMock = new Mock<IHttpClientFactory>();
        httpClientFactMock.Setup(h => h.CreateClient(It.IsAny<string>()))
                    .Returns(new HttpClient());

        var restService = new RestService(httpClientFactMock.Object);
        #endregion

        #region Act
        #endregion

        #region Assert
        Assert.ThrowsAsync<Exception>(() => restService.PostRestService<object>(string.Empty, new Dictionary<string, string> { }, new object { }));
        #endregion
    }

    [Fact]
    public void RestServiceTest_PostServiceAsync_Success()
    {
        #region Arrange
        string newUrl = url;
        //var loggerMock = Mock.Of<ILogger<RestService>>();
        var httpClientFactMock = new Mock<IHttpClientFactory>();

        var headers = new Dictionary<string, string> { { "Authorization", "ABCDeFG" } };

        var httpMock = new MockHttpMessageHandler();
        httpMock.When(newUrl)
            .Respond("application/json", JsonConvert.SerializeObject(new object { }));

        var client = new HttpClient(httpMock);  
        httpClientFactMock.Setup(h => h.CreateClient(It.IsAny<string>()))
                    .Returns(client);

        var restService = new RestService(httpClientFactMock.Object);
        #endregion

        #region Act
        var response = restService.PostRestService<ModelResponse<string>>(url, headers, new object { });
        #endregion

        #region Assert
        Assert.IsType<ModelResponse<string>>(response.Result);
        #endregion
    }

    [Fact]
    public void RestServiceTest_GetServiceAsync_Success()
    {
        #region Arrange
        string newUrl = url;
        //var loggerMock = Mock.Of<ILogger<RestService>>();
        var httpClientFactMock = new Mock<IHttpClientFactory>();

        var headers = new Dictionary<string, string> { { "Authorization", "ABCDeFG" } };

        var httpMock = new MockHttpMessageHandler();
        httpMock.When(newUrl)
            .Respond("application/json", JsonConvert.SerializeObject(new object { }));

        var client = new HttpClient(httpMock);
        httpClientFactMock.Setup(h => h.CreateClient(It.IsAny<string>()))
                    .Returns(client);

        var restService = new RestService(httpClientFactMock.Object);
        #endregion

        #region Act
        var response = restService.GetRestService<ModelResponse<string>>(url, headers);
        #endregion

        #region Assert
        Assert.IsType<ModelResponse<string>>(response.Result);
        #endregion
    }

    [Fact]
    public void RestServiceTest_GetServiceAsync_Catch()
    {
        #region Arrange
        //var loggerMock = Mock.Of<ILogger<RestService>>();
        var httpClientFactMock = new Mock<IHttpClientFactory>();
        httpClientFactMock.Setup(h => h.CreateClient(It.IsAny<string>()))
                    .Returns(new HttpClient());

        var restService = new RestService(httpClientFactMock.Object);
        #endregion

        #region Act
        #endregion

        #region Assert
        Assert.ThrowsAsync<Exception>(() => restService.GetRestService<object>(string.Empty, new Dictionary<string, string> { }));
        #endregion
    }
}

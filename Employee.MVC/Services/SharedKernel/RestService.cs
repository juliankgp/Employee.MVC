using Employee.MVC.Services.SharedKernel.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Employee.MVC.Services.SharedKernel;

public class RestService : IRestService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public RestService(IHttpClientFactory httpClientFactory) =>
                        _httpClientFactory = httpClientFactory;


    public async Task<T> GetRestService<T>(string url, IDictionary<string, string> headers)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data) ?? throw new Exception("Error deserealizando el objeto"); ;

            }

            throw new Exception("Error trayendo la información");

        }
        catch (Exception ex)
        {
            throw;
        }

    }
    public async Task<T> PostRestService<T>(string url, IDictionary<string, string> headers, object body)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            HttpContent bodyJSON = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, bodyJSON);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data) ?? throw new Exception("Error deserealizando el objeto"); 

            }

            throw new Exception("Error trayendo la información");

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}

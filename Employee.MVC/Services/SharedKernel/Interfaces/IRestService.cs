using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employee.MVC.Services.SharedKernel.Interfaces;

public interface IRestService
{
    Task<T> GetRestService<T>(string url, IDictionary<string, string> headers);
    Task<T> PostRestService<T>(string url, IDictionary<string, string> headers, object body);
}

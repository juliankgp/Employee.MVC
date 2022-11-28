using Employee.MVC.Services.Business;
using Employee.MVC.Services.Business.Contracts;
using Employee.MVC.Services.SharedKernel;
using Employee.MVC.Services.SharedKernel.Interfaces;

namespace Api.Pagos.Handlers
{
    public static class DependencyInjectionHandler
    {
        public static void DependencyInjectionConfig(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IBusinessService, BusinessService>();
            services.AddSingleton<IRestService, RestService>();

        }

    }
}

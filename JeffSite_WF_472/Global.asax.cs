using JeffSite_WF_472.App_Start;
using JeffSite_WF_472.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JeffSite_WF_472
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IocConfig.ConfigurarDependencias();

        }

        private void ConfigureServices(IServiceCollection services)
        {
            var connection = @"server=a2nlmysql45plsk.secureserver.net;userid=jeffUserBD;password=89&Owf2j;database=jeffdb";
            services.AddDbContext<JeffContext>(options => options.UseMySql(connection));
        }
    }
}

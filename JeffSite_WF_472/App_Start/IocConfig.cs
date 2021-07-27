using JeffSite_WF_472.Data;
using JeffSite_WF_472.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using System.Web.Mvc;

namespace JeffSite_WF_472.App_Start
{
    public class IocConfig
    {
        public static void ConfigurarDependencias()
        {
            //Cria o Container
            IKernel kernel = new StandardKernel();

            var connection = @"server=a2nlmysql45plsk.secureserver.net;userid=jeffUserBD;password=89&Owf2j;database=jeffdb";
            kernel.Bind<JeffContext>().ToSelf().WithConstructorArgument("options", new DbContextOptionsBuilder<JeffContext>().UseMySql(connection).Options);

            kernel.Bind<UserService>().ToSelf().InSingletonScope();
            kernel.Bind<ConfiguracaoService>().ToSelf().InSingletonScope();
            kernel.Bind<SocialMidiaService>().ToSelf().InSingletonScope();
            kernel.Bind<CarouselService>().ToSelf().InSingletonScope();
            kernel.Bind<MallingService>().ToSelf().InSingletonScope();
            kernel.Bind<LeitorService>().ToSelf().InSingletonScope();

            //Registra o container no ASP.NET
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
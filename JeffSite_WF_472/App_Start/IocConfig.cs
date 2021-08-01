using JeffSite_WF_472.Data;
using JeffSite_WF_472.Models;
using JeffSite_WF_472.Services;
using Microsoft.EntityFrameworkCore;
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

            kernel.Bind<UserService>().ToSelf().InTransientScope();
            kernel.Bind<ConfiguracaoService>().ToSelf().InTransientScope();
            kernel.Bind<SocialMidiaService>().ToSelf().InTransientScope();
            kernel.Bind<CarouselService>().ToSelf().InTransientScope();
            kernel.Bind<MallingService>().ToSelf().InTransientScope();
            kernel.Bind<LeitorService>().ToSelf().InTransientScope();
            kernel.Bind<LojaService>().ToSelf().InTransientScope();
            kernel.Bind<UserLogged>().ToSelf().InSingletonScope();

            //Registra o container no ASP.NET
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
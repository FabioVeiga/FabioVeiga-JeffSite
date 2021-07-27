using JeffSite_WF_472.Services;
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
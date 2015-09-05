using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace SamsWebsiteShop.Web
{
    public class MvcApplication : HttpApplication
    {
        private static IWindsorContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BootstrapContainer();
        }

        protected void Application_End()
        {
            container.Dispose();
        }

        private static void BootstrapContainer()
        {
            container = new WindsorContainer()
                .Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}

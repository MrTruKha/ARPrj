using ARPrj.Web.Framework.Dependency;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ARPrj.WebManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterAutoFac();
        }
        private static void RegisterAutoFac()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());    
            // Register your MVC controllers.
            var callingAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(callingAssembly);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
        }
    }
}

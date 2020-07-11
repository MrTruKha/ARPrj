using System.Reflection;
using Autofac;
using Module = Autofac.Module;
namespace ARPrj.Web.Framework.Dependency
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
          
            builder.RegisterAssemblyTypes(Assembly.Load("ARPrj.DataAccess"))
                     .Where(t => t.Name.EndsWith("Repository"))
                     .AsImplementedInterfaces()
                     .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.Load("ARPrj.Services"))
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
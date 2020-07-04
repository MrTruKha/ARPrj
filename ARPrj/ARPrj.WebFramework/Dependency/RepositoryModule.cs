using System.Reflection;
using Autofac;

namespace PAS.Web.Framework.Dependency
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
          
            builder.RegisterAssemblyTypes(Assembly.Load("PAS.DataAccess"))
                     .Where(t => t.Name.EndsWith("Repository"))
                     .AsImplementedInterfaces()
                     .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.Load("PAS.Services"))
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
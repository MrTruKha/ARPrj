using System.Reflection;
using Autofac;
using AutoMapper;
using PAS.Web.Framework.Automap;
using Module = Autofac.Module;

namespace PAS.Web.Framework.Dependency
{
    public class AutomapModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperBootstrap());
            });
            builder.RegisterInstance<IMapper>(config.CreateMapper());
        }
    }
}
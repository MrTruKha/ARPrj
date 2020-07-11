using ARPrj.DataAccess;
using ARPrj.DataAccess.Common;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.Web.Framework.Dependency
{
    public class EFModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());           

            builder.RegisterType(typeof(ARPrjEntities)).InstancePerRequest();
            builder.RegisterType(typeof(ARPrjContext)).InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
        }
    }
}

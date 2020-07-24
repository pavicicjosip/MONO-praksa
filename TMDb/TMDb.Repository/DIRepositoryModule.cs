using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using TMDb.Repository.Common;


namespace TMDb.Repository
{
    public class DIRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
        }

    }
}

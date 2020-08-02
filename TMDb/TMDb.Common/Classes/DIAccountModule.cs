using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace TMDb.Common.Account
{
    public class DIAccountModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountAccountID>().As<IAccountAccountID>();
            builder.RegisterType<AccountUserName>().As<IAccountUserName>();
            builder.RegisterType<AccountUserPassword>().As<IAccountUserPassword>();
            builder.RegisterType<AccountFacade>().As<IAccountFacade>();
        }

    }
}
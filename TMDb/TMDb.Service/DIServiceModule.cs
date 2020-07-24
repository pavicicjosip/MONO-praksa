using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using TMDb.Service.Common;

namespace TMDb.Service
{
    public class DIServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<ReviewService>().As<IReviewService>();
        }
    }
}

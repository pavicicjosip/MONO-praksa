using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace TMDb.Common.Review
{
    public class DIReviewModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReviewMovieID>().As<IReviewMovieID>();
            builder.RegisterType<ReviewAccountID>().As<IReviewAccountID>();
            builder.RegisterType<ReviewFacade>().As<IReviewFacade>();
        }
    }
}

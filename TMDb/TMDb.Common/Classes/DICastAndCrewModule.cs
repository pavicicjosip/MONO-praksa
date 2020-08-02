using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.CastAndCrew
{
    public class DICastAndCrewModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CACFirstName>().As<ICACFirstName>();
            builder.RegisterType<CACLastName>().As<ICACLastName>();
            builder.RegisterType<CACDateOfBirth>().As<ICACDateOfBirth>();
            builder.RegisterType<CACMovieID>().As<ICACMovieID>();
            builder.RegisterType<CACRole>().As<ICACRole>();
            builder.RegisterType<CastAndCrewFacade>().As<ICastAndCrewFacade>();
        }
    }
}

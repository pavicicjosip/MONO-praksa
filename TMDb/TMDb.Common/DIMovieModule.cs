using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using TMDb.Common;

namespace TMDb.Repository
{
    public class DIMovieModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MovieTitle>().As<IMovieTitle>();
            builder.RegisterType<MovieYearOfProduction>().As<IMovieYearOfProduction>();
            builder.RegisterType<MovieGenre>().As<IMovieGenre>();
            builder.RegisterType<MovieFacade>().As<IMovieFacade>();
        }

    }
}

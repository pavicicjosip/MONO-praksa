using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace TMDb.Common.MovieLists
{
    public class DIMovieListsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MovieListsAccountID>().As<IMovieListsAccountID>();
            builder.RegisterType<MovieListsMovieID>().As<IMovieListsMovieID>();
            builder.RegisterType<MovieListsListName>().As<IMovieListsListName>();
            builder.RegisterType<MovieListsFacade>().As<IMovieListsFacade>();
        }
    }
}

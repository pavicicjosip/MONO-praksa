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
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>();
            builder.RegisterType<MovieRepository>().As<IMovieRepository>();
            builder.RegisterType<CastAndCrewRepository>().As<ICastAndCrewRepository>();
            builder.RegisterType<GenreRepository>.As<IGenreRepository>();
            builder.RegisterType<FileStorageRepository>.As<IFileStorageRepository>();
        }

    }
}

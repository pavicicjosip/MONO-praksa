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
            builder.RegisterType<MovieService>().As<IMovieService>();
            builder.RegisterType<CastAndCrewService>().As<ICastAndCrewService>();
            builder.RegisterType<GenreService>().As<IGenreService>();
            builder.RegisterType<FileStorageService>().As<IFileStorageService>();
            builder.RegisterType<GenreMovieService>().As<IGenreMovieService>();
            builder.RegisterType<UserGenreService>().As<IUserGenreService>();
            builder.RegisterType<CCMovieService>().As<ICCMovieService>();
        }
    }
}

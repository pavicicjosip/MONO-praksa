using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Service.Common;
using TMDb.Repository.Common;
using TMDb.Common;

namespace TMDb.Service
{
    public class MovieService : IMovieService
    {
        protected IMovieRepository movieRepository
        { get; private set; }

        public MovieService(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<List<Movie>> SelectMovieByTitleAsync(PagedResponse pagedResponse, IMovieFacade imovieFacade)
        {
            int pageNumberStart = (pagedResponse.PageNumber - 1) * pagedResponse.PageSize;

            string whereStatement = imovieFacade.WhereStatement();
            string stripedWhereStatement = "";

            if ( !(whereStatement == "" )  && whereStatement[0] == '€' )
                stripedWhereStatement = whereStatement.Remove(0, 1);

            if ( whereStatement != stripedWhereStatement)
                return await movieRepository.SelectMovieByTitleAsyncWith(pageNumberStart, pageNumberStart + pagedResponse.PageSize, stripedWhereStatement);
            else
                return await movieRepository.SelectMovieByTitleAsync(pageNumberStart, pageNumberStart + pagedResponse.PageSize, stripedWhereStatement);



        }

        public async Task<List<Movie>> SelectMovieByYearAsync(int yearOfProduction)
        {
            return await movieRepository.SelectMovieByYearAsync(yearOfProduction);
        }

        public async Task<List<Movie>> GetMoviesByGenreAsync(string genreTitle)
        {
            return await movieRepository.GetMoviesByGenreAsync(genreTitle);
        }

        public async Task<List<Movie>> GetMovieCastAndCrewAsync(string title)
        {
            return await movieRepository.GetMovieCastAndCrewAsync(title);
        }


    }
}

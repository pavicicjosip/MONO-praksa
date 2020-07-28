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

        public async Task<List<Movie>> SelectMovieAsync(PagedResponse pagedResponse, IMovieFacade imovieFacade, ISorting sort)
        {
            int pageNumberStart = (pagedResponse.PageNumber - 1) * pagedResponse.PageSize;
            if (sort.Column == "default")
            {
                sort.Column = "m.Title";
                sort.Order = true;
            }

            string whereStatement = imovieFacade.WhereStatement();
            string stripedWhereStatement = "";
            string joinTables = "";
            if (!(whereStatement == "") && whereStatement[0] == '€')
                stripedWhereStatement = whereStatement.Remove(0, 1);

            if (stripedWhereStatement != "")
            {
                joinTables = ", GenreMovie gm, Genre g ";
                return await movieRepository.SelectMovieAsync(pageNumberStart, pageNumberStart + pagedResponse.PageSize, stripedWhereStatement, joinTables, sort);
            }
            else
            {
                return await movieRepository.SelectMovieAsync(pageNumberStart, pageNumberStart + pagedResponse.PageSize, whereStatement, joinTables, sort);
            }


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

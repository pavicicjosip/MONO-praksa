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
        protected IMovieRepository MovieRepository
        { get; private set; }

        public MovieService(IMovieRepository movieRepository)
        {
            this.MovieRepository = movieRepository;
        }

        public async Task<Tuple<int, List<Movie>>> SelectMovieAsync(PagedResponse pagedResponse, IMovieFacade imovieFacade, Sorting sort)
        {
            int pageNumberStart = (pagedResponse.PageNumber - 1) * pagedResponse.PageSize;
            string whereStatement = imovieFacade.WhereStatement();
            string joinTables = "";
            string extraColumn = "";
            int numberOfResults;
            bool flag = false;

            if (sort.Column == "default")
            {
                sort.Column = "m.Title";
            }

            if (sort.Column == "COUNT(ReviewID)")
            {
                flag = true;
                extraColumn = " , COUNT(ReviewID) AS broj ";
            }

            if (sort.Column == "NumberOfStars")
            {
                sort.Column = "AVG(CAST(NumberOfStars AS FLOAT))";
                flag = true;
                extraColumn = " , AVG(CAST(NumberOfStars AS FLOAT)) AS prosjek ";
            }

            if (!imovieFacade.AccountReviewNull() || flag)
            {
                joinTables += ", Review r ";
            }

            if (flag)
            {

                if (whereStatement == "")
                {
                    whereStatement = " WHERE r.MovieID = m.MovieID ";
                }
                else
                {
                    whereStatement += " AND r.MovieID = m.MovieID ";
                }
                imovieFacade.GroupByBool = true;
            }

            if (!imovieFacade.GenreNull())
            {
                joinTables += ", GenreMovie gm, Genre g ";
                numberOfResults = await MovieRepository.SelectNumberOfResultsAsync(whereStatement, joinTables);
                return new Tuple<int, List<Movie>>(numberOfResults, await MovieRepository.SelectMovieAsync(pageNumberStart, pageNumberStart + pagedResponse.PageSize, whereStatement, joinTables, extraColumn, imovieFacade.GroupBy(), sort));
            }

            numberOfResults = await MovieRepository.SelectNumberOfResultsAsync(whereStatement, joinTables);
            return new Tuple<int, List<Movie>>(numberOfResults, await MovieRepository.SelectMovieAsync(pageNumberStart, pageNumberStart + pagedResponse.PageSize, whereStatement, joinTables, extraColumn, imovieFacade.GroupBy(), sort));
        }

        public async Task<Movie> SelectMovieByIdAsync(Guid movieID)
        {
            var movie = await MovieRepository.SelectMovieByIdAsync(movieID);
            return movie;
        }

        public async Task CreateMovieAsync(Movie movie)
        {
            await MovieRepository.InsertMovieAsync(movie);
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            await MovieRepository.UpdateMovieAsync(movie);
        }

        public async Task RemoveMovieAsync(Guid movieID)
        {
            await MovieRepository.DeleteMovieAsync(movieID);
        }
    }
}

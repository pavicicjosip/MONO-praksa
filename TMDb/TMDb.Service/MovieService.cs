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

        public async Task<Tuple<int, List<Movie>>> SelectMovieAsync(PagedResponse pagedResponse, IMovieFacade imovieFacade, Sorting sort)
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
            int numberOfResults;

            if (!(whereStatement == "") && whereStatement[0] == '€')
                stripedWhereStatement = whereStatement.Remove(0, 1);

            if (stripedWhereStatement != "")
            {
                joinTables = ", GenreMovie gm, Genre g ";
                numberOfResults = await movieRepository.SelectNumberOfResultsAsync(stripedWhereStatement, joinTables);
                return new Tuple<int, List<Movie>>(numberOfResults, await movieRepository.SelectMovieAsync(pageNumberStart, pageNumberStart + pagedResponse.PageSize, stripedWhereStatement, joinTables, sort));
            }
            else
            {
                numberOfResults = await movieRepository.SelectNumberOfResultsAsync(whereStatement, joinTables);
                return new Tuple<int, List<Movie>>(numberOfResults, await movieRepository.SelectMovieAsync(pageNumberStart, pageNumberStart + pagedResponse.PageSize, whereStatement, joinTables, sort));
            }
        }

        public async Task CreateMovieAsync(Movie movie)
        {
            await movieRepository.InsertMovieAsync(movie);
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            await movieRepository.UpdateMovieAsync(movie);
        }

        public async Task RemoveMovieAsync(Guid movieID)
        {
            await movieRepository.DeleteMovieAsync(movieID);
        }
    }
}

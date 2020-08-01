using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Service.Common;
using TMDb.Repository.Common;
using TMDb.Common.MovieLists;
using TMDb.Common;

namespace TMDb.Service
{
    public class MovieListsService : IMovieListsService
    {
        protected IMovieListsRepository MovieListsRepository
        { get; private set; }
        protected IMovieRepository MovieRepository
        { get; private set; }

        public MovieListsService(IMovieListsRepository movieListsRepository, IMovieRepository movieRepository)
        {
            this.MovieListsRepository = movieListsRepository;
            this.MovieRepository = movieRepository;
        }
        public async Task<Tuple<int, List<MovieLists>>> SelectMovieListsAsync(Guid accountID, PagedResponse pagedResponse)
        {
            int pageNumberStart = (pagedResponse.PageNumber - 1) * pagedResponse.PageSize;
            int numberOfResults;

            numberOfResults = await MovieListsRepository.SelectNumberOfResultsAsync(accountID);
            return new Tuple<int, List<MovieLists>>(numberOfResults, await MovieListsRepository.SelectMovieListsAsync(pageNumberStart, pageNumberStart + pagedResponse.PageSize, accountID));
        }
        public async Task<Tuple<int, List<Movie>>> SelectMoviesFromListAsync(IMovieListsFacade movieListsFacade, PagedResponse pagedResponse, Sorting sort)
        {
            int pageNumberStart = (pagedResponse.PageNumber - 1) * pagedResponse.PageSize;
            string whereStatement = " WHERE m.MovieID = ml.MovieID AND " + movieListsFacade.WhereStatement();
            int numberOfResults;
            string joinTables = ", MovieLists ml ";

            if (sort.Column == "default")
            {
                sort.Column = "m.Title";
                sort.Order = true;
            }

            numberOfResults = await MovieRepository.SelectNumberOfResultsAsync(whereStatement, joinTables);
            var movieList = await MovieRepository.SelectMovieAsync(pageNumberStart, pageNumberStart + pagedResponse.PageSize, whereStatement, joinTables, "", "", sort);
            return new Tuple<int, List<Movie>>(numberOfResults, movieList);
        }
        public async Task CreateMovieListAsync(MovieLists movieList)
        {
            await MovieListsRepository.InsertMovieListAsync(movieList);
        }
        public async Task RemoveMovieListsAsync(IMovieListsFacade MovieListsFacade)
        {
            string whereStatment = MovieListsFacade.WhereStatement();
            await MovieListsRepository.DeleteMovieListsAsync(whereStatment);
        }
    }
}

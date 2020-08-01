using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Common;
using TMDb.Common.MovieLists;

namespace TMDb.Service.Common
{
    public interface IMovieListsService
    {
        Task<Tuple<int, List<MovieLists>>> SelectMovieListsAsync(Guid accountID, PagedResponse pagedResponse);
        Task<Tuple<int, List<Movie>>> SelectMoviesFromListAsync(IMovieListsFacade movieListsFacade, PagedResponse pagedResponse, Sorting sort);
        Task CreateMovieListAsync(MovieLists movieList);
        Task RemoveMovieListsAsync(IMovieListsFacade MovieListsFacade);
    }
}

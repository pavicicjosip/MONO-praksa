using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Common;

namespace TMDb.Service.Common
{
    public interface IMovieService
    {
        Task<Tuple<int, List<Movie>>> SelectMovieAsync(PagedResponse pagedResponse, IMovieFacade imovieFacade, Sorting sort);
        Task CreateMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task RemoveMovieAsync(Guid movieID);
    }
}

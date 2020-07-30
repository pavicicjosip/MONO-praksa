using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Common;

namespace TMDb.Service.Common
{
    public interface IUserGenreService
    {
        Task InsertUserGenreAsync(UserGenre userGenre);
        Task RemoveUserGenreAsync(Guid accountID, Guid genreID);
        Task<List<Genre>> SelectFavouriteGenreAsync(Guid accountID);
        Task<List<Movie>> SelectMoviesFromGenreAsync(PagedResponse pagedResponse, Guid accountID);
    }
}

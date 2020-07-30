using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Common;

namespace TMDb.Repository.Common
{
    public interface IUserGenreRepository
    {
        Task InsertUserGenreAsync(UserGenre userGenre);
        Task RemoveUserGenreAsync(Guid accountID, Guid genreID);
        Task<List<Genre>> SelectFavouriteGenreAsync(Guid accountID);
        Task<List<Movie>> SelectMoviesFromGenreAsync(PagedResponse pagedResponse, Guid accountID);
    }
}

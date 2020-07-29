using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;

namespace TMDb.Repository.Common
{
    public interface IUserGenreRepository
    {
        Task InsertUserGenreAsync(UserGenre userGenre);
        Task RemoveUserGenreAsync(Guid accountID, Guid genreID);
        Task<List<Genre>> SelectFavouriteGenreAsync(Guid accountID);
    }
}

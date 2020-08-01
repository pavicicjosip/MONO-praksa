using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;

namespace TMDb.Repository.Common
{
    public interface IMovieListsRepository
    {
        Task<List<MovieLists>> SelectMovieListsAsync(int pageNumberStart, int pageNumberEnd, Guid accountID);
        Task<int> SelectNumberOfResultsAsync(Guid accountID);
        Task InsertMovieListAsync(MovieLists movieList);
        Task DeleteMovieListsAsync(string whereStatment);
    }
}

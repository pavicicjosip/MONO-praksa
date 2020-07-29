using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Common;

namespace TMDb.Repository.Common
{
    public interface IMovieRepository
    {
        Task<List<Movie>> SelectMovieAsync(int pageNumberStart, int pageNumberEnd, string whereStatement, string joinTables, string extraColumn, string groupBy, Sorting sort);
        Task<int> SelectNumberOfResultsAsync(string whereStatement, string joinTables);
        Task InsertMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(Guid movieID);
    }
}
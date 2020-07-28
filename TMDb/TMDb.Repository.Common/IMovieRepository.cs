using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Common;

namespace TMDb.Repository.Common
{
    public interface IMovieRepository
    {
        Task<List<Movie>> SelectMovieAsync(int pageNumberStart, int pageNumberEnd, string whereStatement, string joinTables, ISorting sort);
        Task<List<Movie>> SelectMovieByYearAsync(int yearOfProduction);
        Task<List<Movie>> GetMoviesByGenreAsync(string genreTitle);
        Task<List<Movie>> GetMovieCastAndCrewAsync(string title);
    }
}
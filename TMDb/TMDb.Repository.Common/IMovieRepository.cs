using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;

namespace TMDb.Repository.Common
{
    public interface IMovieRepository
    {
        Task<List<Movie>> SelectMovieByTitleAsync(string title);
        Task<List<Movie>> SelectMovieByYearAsync(int yearOfProduction);
    }
}

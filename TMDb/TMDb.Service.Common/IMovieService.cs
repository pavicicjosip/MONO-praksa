using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Common;

namespace TMDb.Service.Common
{
    public interface IMovieService
    {
        Task<List<Movie>> SelectMovieByTitleAsync(PagedResponse pagedResponse, IMovieFacade imovieFacade);
        Task<List<Movie>> SelectMovieByYearAsync(int yearOfProduction);
        Task<List<Movie>> GetMoviesByGenreAsync(string genreTitle);
        Task<List<Movie>> GetMovieCastAndCrewAsync(string title);

    }
}

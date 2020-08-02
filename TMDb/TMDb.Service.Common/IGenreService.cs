using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Common;

namespace TMDb.Service.Common
{
    public interface IGenreService
    {
        Task<List<Genre>> ReturnAllGenresAsync();
        Task<Genre> ReturnGenreByTitleAsync(string title);
        Task InsertGenreAsync(string title);
        Task UpdateGenreAsync(Genre genre);
    }
}

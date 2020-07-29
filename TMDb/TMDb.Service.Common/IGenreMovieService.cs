using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Model;
using TMDb.Common.Account;
using TMDb.Common;

namespace TMDb.Service.Common
{
    public interface IGenreMovieService
    {
        Task InsertGenreMovieAsync(GenreMovie genreMovie);
        Task<List<string>> GetGenreOfMovieAsync(Guid movieID);




    }
}

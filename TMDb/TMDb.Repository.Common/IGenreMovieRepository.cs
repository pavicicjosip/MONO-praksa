using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;


namespace TMDb.Repository.Common
{
    public interface IGenreMovieRepository
    {
        Task InsertGenreMovieAsync(GenreMovie genreMovie);
        Task<List<string>> GetGenreOfMovieAsync(Guid movieID);
        Task RemoveGenreMovieAsync(Guid movieID, Guid genreID);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Service.Common;
using TMDb.Model;

namespace TMDb.Service
{
    public class GenreMovieService : IGenreMovieService
    {
        protected IGenreMovieRepository GenreMovieRepository
        { get; private set; }

        public GenreMovieService(IGenreMovieRepository genreMovieRepository)
        {
            this.GenreMovieRepository = genreMovieRepository;
        }
        public async Task InsertGenreMovieAsync(GenreMovie genreMovie)
        {
            await GenreMovieRepository.InsertGenreMovieAsync(genreMovie);
        }

        public async Task<List<string>> GetGenreOfMovieAsync(Guid movieID)
        {
            return await GenreMovieRepository.GetGenreOfMovieAsync(movieID);
        }
        public async Task RemoveGenreMovieAsync(Guid movieID, Guid genreID)
        {
            await GenreMovieRepository.RemoveGenreMovieAsync(movieID, genreID);
        }
    }
}

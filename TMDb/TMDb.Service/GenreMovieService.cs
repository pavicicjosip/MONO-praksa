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
        protected IGenreMovieRepository genreMovieRepository
        { get; private set; }

        public GenreMovieService(IGenreMovieRepository genreMovieRepository)
        {
            this.genreMovieRepository = genreMovieRepository;
        }
        public async Task InsertGenreMovieAsync(GenreMovie genreMovie)
        {
            await genreMovieRepository.InsertGenreMovieAsync(genreMovie);
        }

        public async Task<List<string>> GetGenreOfMovieAsync(Guid movieID)
        {
            return await genreMovieRepository.GetGenreOfMovieAsync(movieID);
        }
        public async Task RemoveGenreMovieAsync(Guid movieID, Guid genreID)
        {
            await genreMovieRepository.RemoveGenreMovieAsync(movieID, genreID);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Service.Common;
using TMDb.Repository.Common;

namespace TMDb.Service
{
    public class GenreService : IGenreService
    {
        protected IGenreRepository genreRepository
        { get; private set; }

        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }
        public async Task<List<Genre>> ReturnAllGenresAsync()
        {            
            return await genreRepository.ReturnAllGenresAsync();
        }
        public async Task<Genre> ReturnGenreByTitleAsync(string title)
        {
            return await genreRepository.ReturnGenreByTitleAsync(title);
        }
        public async Task InsertGenreAsync(string title)
        {
            await genreRepository.InsertGenreAsync(title);
        }
        public async Task UpdateGenreAsync(Genre genre)
        {
            await genreRepository.UpdateGenreAsync(genre);
        }
    }
}

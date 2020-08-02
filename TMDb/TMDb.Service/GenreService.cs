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
        protected IGenreRepository GenreRepository
        { get; private set; }

        public GenreService(IGenreRepository genreRepository)
        {
            this.GenreRepository = genreRepository;
        }
        public async Task<List<Genre>> ReturnAllGenresAsync()
        {            
            return await GenreRepository.ReturnAllGenresAsync();
        }
        public async Task<Genre> ReturnGenreByTitleAsync(string title)
        {
            return await GenreRepository.ReturnGenreByTitleAsync(title);
        }
        public async Task InsertGenreAsync(string title)
        {
            await GenreRepository.InsertGenreAsync(title);
        }
        public async Task UpdateGenreAsync(Genre genre)
        {
            await GenreRepository.UpdateGenreAsync(genre);
        }
    }
}

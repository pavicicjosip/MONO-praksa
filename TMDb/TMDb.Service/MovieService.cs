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
    public class MovieService : IMovieService
    {
        protected IMovieRepository movieRepository
        { get; private set; }

        public MovieService(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<List<Movie>> SelectMovieByTitleAsync(string title)
        {
            return await movieRepository.SelectMovieByTitleAsync(title);
        }

        public async Task<List<Movie>> SelectMovieByYearAsync(int yearOfProduction)
        {
            return await movieRepository.SelectMovieByYearAsync(yearOfProduction);
        }

        public async Task<List<Movie>> GetMoviesByGenreAsync(string genreTitle)
        {
            return await movieRepository.GetMoviesByGenreAsync(genreTitle);
        }

        public async Task<List<Movie>> GetMovieCastAndCrewAsync(string title)
        {
            return await movieRepository.GetMovieCastAndCrewAsync(title);
        }


    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TMDb.Model;
using TMDb.Service.Common;

namespace TMDb.WebAPI.Controllers
{
    public class MovieController : ApiController
    {
        protected IMovieService movieService { get; private set; }
        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<Movie, RestMovie>());
        public MovieController()
        {
        }
        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        [Route("api/Movie/Title/{title}")]
        public async Task<HttpResponseMessage> SelectMovieByTitleAsync([FromUri] string title)
        {
            var mapper = Mapper.CreateMapper();
            List<RestMovie> restMovieList = mapper.Map<List<RestMovie>>(await movieService.SelectMovieByTitleAsync(title));
            return Request.CreateResponse(HttpStatusCode.OK, restMovieList);
        }

        [HttpGet]
        [Route("api/Movie/Year/{yearOfProduction}")]
        public async Task<HttpResponseMessage> SelectMovieByYearAsync([FromUri] int yearOfProduction)
        {
            var mapper = Mapper.CreateMapper();
            List<RestMovie> restMovieList = mapper.Map<List<RestMovie>>(await movieService.SelectMovieByYearAsync(yearOfProduction));
           return Request.CreateResponse(HttpStatusCode.OK, restMovieList);
        }

        [HttpGet]
        [Route("api/Movie/Genre/{genreTitle}")]
        public async Task<HttpResponseMessage> GetMovieByGenreAsync([FromUri] string genreTitle)
        {
            var mapper = Mapper.CreateMapper();
            List<RestMovie> restMovieList = mapper.Map<List<RestMovie>>(await movieService.GetMoviesByGenreAsync(genreTitle));
            return Request.CreateResponse(HttpStatusCode.OK, restMovieList);
        }

        [HttpGet]
        [Route("api/Movie/CastAndCrew/{title}")]
        public async Task<HttpResponseMessage> GetMovieCastAndCrewAsync([FromUri] string title)
        {
            var mapper = Mapper.CreateMapper();
            List<RestMovie> restMovieList = mapper.Map<List<RestMovie>>(await movieService.GetMovieCastAndCrewAsync(title));
            return Request.CreateResponse(HttpStatusCode.OK, restMovieList);
        }
        public class RestMovie
        {
            public Guid MovieID
            { get; set; }
            public string Title
            { get; set; }
            public int YearOfProduction
            { get; set; }
            public string CountryOfOrigin
            { get; set; }
            public string Duration
            { get; set; }
            public string PlotOutline
            { get; set; }
            public Guid FileID
            { get; set; }

        }
    }
}
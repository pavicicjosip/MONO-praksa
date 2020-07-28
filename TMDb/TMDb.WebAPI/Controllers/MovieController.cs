using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TMDb.Model;
using TMDb.Service.Common;
using TMDb.Common;

namespace TMDb.WebAPI.Controllers
{
    public class MovieController : ApiController
    {
        protected IMovieService movieService { get; private set; }
        protected IMovieFacade movieFacade { get; private set; }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<Movie, RestMovie>());
        public MovieController()
        {
        }
        public MovieController(IMovieService movieService, IMovieFacade movieFacade)
        {
            this.movieService = movieService;
            this.movieFacade = movieFacade;
        }

        [HttpGet]
        [Route("api/Movie/Title/{pageNumber}/{pageSize}")]
        public async Task<HttpResponseMessage> SelectMovieAsync([FromUri] int pageNumber, [FromUri] int pageSize, string yearOfProduction = "default"
            , string genre = "default", string title = "default", string column = "default", bool order = true)
        {
            var mapper = Mapper.CreateMapper();

            PagedResponse pagedResponse = new PagedResponse { PageNumber = pageNumber, PageSize = pageSize };
            Sorting sort = new Sorting { Column = column, Order = order };

            movieFacade.movieYearOfProduction.YearOfProduction = yearOfProduction;
            movieFacade.movieTitle.Title = title;
            movieFacade.movieGenre.Genre = genre;

            List<RestMovie> restMovieList = mapper.Map<List<RestMovie>>(await movieService.SelectMovieAsync(pagedResponse, movieFacade, sort));
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
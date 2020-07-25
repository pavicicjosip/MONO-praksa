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
        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        [Route("api/Movie/Title/{title}")]
        public async Task<HttpResponseMessage> SelectMovieByTitleAsync(string title)
        {
            var mapper = Mapper.CreateMapper();
            List<RestMovie> restMovieList = mapper.Map<List<RestMovie>>(await movieService.SelectMovieByTitleAsync(title));
            return Request.CreateResponse(HttpStatusCode.OK, restMovieList);
        }

        /*[HttpGet]
        [Route("api/Movie/Year/{year}")]
        public async Task<HttpResponseMessage> SelectMovieByYearAsync(int yearOfProduction)
        {
            var mapper = Mapper.CreateMapper();
            List<RestMovie> restMovieList = mapper.Map<List<RestMovie>>(await movieService.SelectMovieByTitleAsync(yearOfProduction));
            return Request.CreateResponse(HttpStatusCode.OK, restMovieList);
        }*/

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

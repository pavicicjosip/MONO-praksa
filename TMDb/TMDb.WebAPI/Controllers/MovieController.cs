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
        protected IMovieService MovieService { get; private set; }
        protected IMovieFacade MovieFacade { get; private set; }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<Movie, RestMovie>().ReverseMap());
        public MovieController()
        {
        }
        public MovieController(IMovieService movieService, IMovieFacade movieFacade)
        {
            this.MovieService = movieService;
            this.MovieFacade = movieFacade;
        }

        [HttpGet]
        [Route("api/Movie")]
        public async Task<HttpResponseMessage> SelectMovieAsync(Guid? accountID = null, int pageNumber = 1, int pageSize = 10, string yearOfProduction = "default"
            , string genre = "default", string title = "default", string column = "default", bool order = true)
        {
            var mapper = Mapper.CreateMapper();

            PagedResponse pagedResponse = new PagedResponse { PageNumber = pageNumber, PageSize = pageSize };
            Sorting sort = new Sorting { Column = column, Order = order };

            MovieFacade.movieYearOfProduction.YearOfProduction = yearOfProduction;
            MovieFacade.movieTitle.Title = title;
            MovieFacade.movieGenre.Genre = genre;
            if (accountID.HasValue)
            {
                MovieFacade.movieAccountReview.AccountID = accountID.Value;
            }
            else
            {
                MovieFacade.movieAccountReview.AccountID = Guid.Empty;
            }

            var movieTuple = await MovieService.SelectMovieAsync(pagedResponse, MovieFacade, sort);
            List<RestMovie> restMovieList = mapper.Map<List<RestMovie>>(movieTuple.Item2);
            var restMovieTuple = new Tuple<int, List<RestMovie>>(movieTuple.Item1, restMovieList);
            return Request.CreateResponse(HttpStatusCode.OK, restMovieTuple);
        }

        [HttpPost]
        [Route("api/Movie")]
        public async Task<HttpResponseMessage> PostMovieAsync(RestMovie restMovie)
        {
            var mapper = Mapper.CreateMapper();
            Movie movie = mapper.Map<Movie>(restMovie);
            await MovieService.CreateMovieAsync(movie);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/Movie")]
        public async Task<HttpResponseMessage> PutMovieAsync(Guid movieID, RestMovie restMovie)
        {
            var mapper = Mapper.CreateMapper();
            Movie movie = mapper.Map<Movie>(restMovie);
            movie.MovieID = movieID;
            await MovieService.UpdateMovieAsync(movie);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/Movie")]
        public async Task<HttpResponseMessage> DeleteMoviewAsync(Guid movieID)
        {
            await MovieService.RemoveMovieAsync(movieID);
            return Request.CreateResponse(HttpStatusCode.OK);
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
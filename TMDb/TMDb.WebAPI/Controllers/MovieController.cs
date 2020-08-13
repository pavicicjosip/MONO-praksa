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
using System.Security.Claims;
using System.Linq;
using System.Web.Http.Cors;

namespace TMDb.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        public async Task<HttpResponseMessage> SelectMovieAsync(bool account = false, int pageNumber = 1, int pageSize = 10, string yearOfProduction = "default"
            , string genre = "default", string title = "default", string column = "default", bool order = true)
        {
            var mapper = Mapper.CreateMapper();

            PagedResponse pagedResponse = new PagedResponse { PageNumber = pageNumber, PageSize = pageSize };
            Sorting sort = new Sorting { Column = column, Order = order };

            MovieFacade.MovieYearOfProduction.YearOfProduction = yearOfProduction;
            MovieFacade.MovieTitle.Title = title;
            MovieFacade.MovieGenre.Genre = genre;
            if (account && User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                var claims = identity.Claims;
                MovieFacade.MovieAccountReview.AccountID = Guid.Parse(claims.Where(p => p.Type == "guid").FirstOrDefault()?.Value);
            }
            else
            {
                MovieFacade.MovieAccountReview.AccountID = Guid.Empty;
            }

            var movieTuple = await MovieService.SelectMovieAsync(pagedResponse, MovieFacade, sort);
            List<RestMovie> restMovieList = mapper.Map<List<RestMovie>>(movieTuple.Item2);
            var restMovieTuple = new Tuple<int, List<RestMovie>>(movieTuple.Item1, restMovieList);
            return Request.CreateResponse(HttpStatusCode.OK, restMovieTuple);
        }
        [HttpGet]
        [Route("api/Movie/{MovieID}")]
        public async Task<HttpResponseMessage> SelectMovieByIdAsync(Guid movieID)
        {
            var mapper = Mapper.CreateMapper();
            var movie = mapper.Map<RestMovie>( await MovieService.SelectMovieByIdAsync(movieID));
            if(movie == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, movie);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("api/Movie")]
        public async Task<HttpResponseMessage> PostMovieAsync(RestMovie restMovie)
        {
            var mapper = Mapper.CreateMapper();
            Movie movie = mapper.Map<Movie>(restMovie);
            await MovieService.CreateMovieAsync(movie);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
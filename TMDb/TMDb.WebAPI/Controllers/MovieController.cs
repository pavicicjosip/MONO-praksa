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

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<Movie, RestMovie>().ReverseMap());
        public MovieController()
        {
        }
        public MovieController(IMovieService movieService, IMovieFacade movieFacade)
        {
            this.movieService = movieService;
            this.movieFacade = movieFacade;
        }

        [HttpGet]
        [Route("api/Movie")]
        public async Task<HttpResponseMessage> SelectMovieAsync(int pageNumber = 1, [FromUri] int pageSize = 10, string yearOfProduction = "default"
            , string genre = "default", string title = "default", string column = "default", bool order = true)
        {
            var mapper = Mapper.CreateMapper();

            PagedResponse pagedResponse = new PagedResponse { PageNumber = pageNumber, PageSize = pageSize };
            Sorting sort = new Sorting { Column = column, Order = order };

            movieFacade.movieYearOfProduction.YearOfProduction = yearOfProduction;
            movieFacade.movieTitle.Title = title;
            movieFacade.movieGenre.Genre = genre;
            var movieTuple = await movieService.SelectMovieAsync(pagedResponse, movieFacade, sort);
            List<RestMovie> restMovieList = mapper.Map<List<RestMovie>>(movieTuple.Item2);
            var restMovieTuple = new Tuple<int, List<Movie>>(movieTuple.Item1, movieTuple.Item2);
            return Request.CreateResponse(HttpStatusCode.OK, restMovieTuple);
        }

        [HttpPost]
        [Route("api/Movie")]
        public async Task<HttpResponseMessage> PostMovieAsync(RestMovie restMovie)
        {
            var mapper = Mapper.CreateMapper();
            Movie movie = mapper.Map<Movie>(restMovie);
            await movieService.CreateMovieAsync(movie);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/Movie")]
        public async Task<HttpResponseMessage> PutMovieAsync(Guid movieID, RestMovie restMovie)
        {
            var mapper = Mapper.CreateMapper();
            Movie movie = mapper.Map<Movie>(restMovie);
            movie.MovieID = movieID;
            await movieService.UpdateMovieAsync(movie);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/Movie")]
        public async Task<HttpResponseMessage> DeleteMoviewAsync(Guid movieID)
        {
            await movieService.RemoveMovieAsync(movieID);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /*
        - get sve filmove koje je ocjenio određeni user
        SELECT m.MovieID, m.Title, m.YearOfProduction, m.CountryOfOrigin, m.Duration, m.PlotOutline, m.FileID
        FROM Movie m, Review r
        WHERE r.MovieID = m.MovieID AND r.AccountID = '8EAA8D25-5014-4108-9486-33592DBF56D8'
        GROUP BY m.MovieID, m.Title, m.YearOfProduction, m.CountryOfOrigin, m.Duration, m.PlotOutline, m.FileID
        
        OVO GORE MOZDA MOZE U GORNJI GET
        --------------------------------------------------------------------------------------------------------
        - get filmove po broju komentara, sortiranje 
        podupit:
        SELECT COUNT(ReviewID), MovieID
        FROM Review
        GROUP BY MovieID
        ORDER BY COUNT(ReviewID) ASC

        --------------------------------------------------------------------------------------------------------
        - get za najolje ocjenje filmove, sortiranje
        podupit:
        SELECT AVG(NumberOfStars), MovieID
        FROM Review
        GROUP BY MovieID
        ORDER BY AVG(NumberOfStars) ASC


        CAST(NumberOfStars AS FLOAT) gore u AVG umjesto NumberOfStars? (ili promijeniti NumberOfStars u float u tablici?)
        */

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
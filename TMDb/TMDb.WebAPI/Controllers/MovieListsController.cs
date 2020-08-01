﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TMDb.Common;
using TMDb.Common.MovieLists;
using TMDb.Model;
using TMDb.Service.Common;

namespace TMDb.WebAPI.Controllers
{
    public class MovieListsController : ApiController
    {
        protected IMovieListsService MovieListsService
        { get; private set; }

        protected IMovieListsFacade MovieListsFacade { get; private set; }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<MovieLists, RestMovieLists>().ReverseMap());

        public MovieListsController(IMovieListsService movieListsService, IMovieListsFacade movieListsFacade)
        {
            this.MovieListsService = movieListsService;
            this.MovieListsFacade = movieListsFacade;
        }

        [HttpGet]
        [Route("api/MovieLists/{AccountID}")]
        public async Task<HttpResponseMessage> GetMovieListsAsync(Guid accountID, int pageNumber = 1, int pageSize = 10)
        {
            var mapper = Mapper.CreateMapper();

            var pagedResponse = new PagedResponse { PageNumber = pageNumber, PageSize = pageSize };
            var movieListsTuple = await MovieListsService.SelectMovieListsAsync(accountID, pagedResponse);
            List<RestMovieLists> restMovieLists = mapper.Map<List<RestMovieLists>>(movieListsTuple.Item2);
            var restMovieListsTuple = new Tuple<int, List<RestMovieLists>>(movieListsTuple.Item1, restMovieLists);
            return Request.CreateResponse(HttpStatusCode.OK, restMovieListsTuple);
        }

        [HttpGet]
        [Route("api/MovieLists/{AccountID}/{ListName}")]
        public async Task<HttpResponseMessage> GetMoviesFromListAsync(Guid accountID, string listName, string column = "default", bool order = true, int pageNumber = 1, int pageSize = 10)
        {
            PagedResponse pagedResponse = new PagedResponse { PageNumber = pageNumber, PageSize = pageSize };
            Sorting sort = new Sorting { Column = column, Order = order };
            MovieListsFacade.MovieListsAccountID.AccountID = accountID;
            MovieListsFacade.MovieListsListName.ListName = listName;
            MovieListsFacade.MovieListsMovieID.MovieID = Guid.Empty;

            var moviesFromListTuple = await MovieListsService.SelectMoviesFromListAsync(MovieListsFacade, pagedResponse, sort);
            return Request.CreateResponse(HttpStatusCode.OK, moviesFromListTuple);
        }

        [HttpPost]
        [Route("api/MovieLists")]
        public async Task<HttpResponseMessage> PostMovieListsAsync(Guid accountID, RestMovieLists restMovieList)
        {
            var mapper = Mapper.CreateMapper();
            MovieLists movieList = mapper.Map<MovieLists>(restMovieList);
            movieList.AccountID = accountID;
            await MovieListsService.CreateMovieListAsync(movieList);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/MovieLists")]
        public async Task<HttpResponseMessage> DeleteMovieListsAsync(string listName = "default", Guid? accountID = null, Guid? movieID = null)
        {
            if (accountID.HasValue)
            {
                MovieListsFacade.MovieListsAccountID.AccountID = accountID.Value;
            }
            else
            {
                MovieListsFacade.MovieListsAccountID.AccountID = Guid.Empty;
            }

            if (movieID.HasValue)
            {
                MovieListsFacade.MovieListsMovieID.MovieID = movieID.Value;
            }
            else
            {
                MovieListsFacade.MovieListsMovieID.MovieID = Guid.Empty;
            }

            MovieListsFacade.MovieListsListName = new MovieListsListName(listName);

            await MovieListsService.RemoveMovieListsAsync(MovieListsFacade);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
    public class RestMovieLists
    {
        public string ListName { get; set; }
        public Guid MovieID { get; set; }
    }
}

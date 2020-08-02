using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMDb.Model;
using TMDb.Service.Common;
using AutoMapper;
using System.Threading.Tasks;

namespace TMDb.WebAPI.Controllers
{
    public class GenreMovieController : ApiController
    {
        protected IGenreMovieService genreMovieService { get; private set; }
        public GenreMovieController(IGenreMovieService genreMovieService) 
        { 
            this.genreMovieService = genreMovieService; 
        }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<GenreMovie, RestGenreMovie>().ReverseMap());

        [HttpPost]
        [Route("api/GenreMovie/insertGenreMovieAsync")]
        public async Task<HttpResponseMessage> InsertGenreMovieAsync([FromBody] RestGenreMovie restGenreMovie)
        {
            var mapper = Mapper.CreateMapper();
            GenreMovie genreMovie = mapper.Map<GenreMovie>(restGenreMovie);
            await genreMovieService.InsertGenreMovieAsync(genreMovie);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpGet]
        [Route("api/GenreMovie/getGenreOfMovie")]
        public async Task<HttpResponseMessage> GetGenreOfMovieAsync(Guid movieID)
        {
            List<string> list = await genreMovieService.GetGenreOfMovieAsync(movieID);
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        [HttpDelete]
        [Route("api/GenreMovie/deleteGenreMovie")]
        public async Task<HttpResponseMessage> RemoveGenreMovieAsync(Guid movieID, Guid genreID)
        {
            await genreMovieService.RemoveGenreMovieAsync(movieID, genreID);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        public class RestGenreMovie
        {
            public Guid MovieID { get; set; }
            public Guid GenreID { get; set; }
        }

    }
}

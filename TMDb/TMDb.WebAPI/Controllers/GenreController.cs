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
using TMDb.Service;

namespace TMDb.WebAPI.Controllers
{
    public class GenreController : ApiController
    {
        ///<summary>
        ///GetAll
        ///GetByTitle
        ///Order po abecedi
        ///</summary>
        protected IGenreService genreService { get; private set; }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<Genre, RestGenre>());
        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;

        }


        [Route("api/Genre/getAllGenres")]
        public async Task<HttpResponseMessage> ReturnAllGenresAsync()
        {
            var mapper = Mapper.CreateMapper();
            List<RestGenre> restGenreList = mapper.Map<List<RestGenre>>(await genreService.ReturnAllGenresAsync());

            return Request.CreateResponse(HttpStatusCode.OK, restGenreList);
        }

        public class RestGenre
        {
            public string Title { get; set; }
        }

    }
}

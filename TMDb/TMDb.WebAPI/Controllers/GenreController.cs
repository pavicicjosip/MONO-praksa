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
        protected IGenreService GenreService { get; private set; }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<Genre, RestGenre>().ReverseMap());
        public GenreController(IGenreService genreService)
        {
            this.GenreService = genreService;

        }

        [HttpGet]
        [Route("api/Genre/getAllGenres")]
        public async Task<HttpResponseMessage> ReturnAllGenresAsync()
        {
            var mapper = Mapper.CreateMapper();
            List<RestGenre> restGenreList = mapper.Map<List<RestGenre>>(await GenreService.ReturnAllGenresAsync());

            return Request.CreateResponse(HttpStatusCode.OK, restGenreList);
        }

        [HttpGet]
        [Route("api/Genre/getGenreByTitle")]
        public async Task<HttpResponseMessage> ReturnGenreByTitleAsync(string title)
        {
            var mapper = Mapper.CreateMapper();
            RestGenre file = mapper.Map<RestGenre>(await GenreService.ReturnGenreByTitleAsync(title));
            if (file == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no genre with title: {0}", title));
            }
            return Request.CreateResponse(HttpStatusCode.OK, file);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("api/Genre")]
        public async Task<HttpResponseMessage> InsertGenreAsync(string title)
        {
            await GenreService.InsertGenreAsync(title);
            return Request.CreateResponse(HttpStatusCode.OK, String.Format("{0} inserted in the database", title));
        }

        [HttpPut]
        [Route("api/Genre/{GenreID}")]
        public async Task<HttpResponseMessage> UpdateGenreAsync(Guid genreID, RestGenre restGenre)
        {
            var mapper = Mapper.CreateMapper();
            Genre genre = mapper.Map<Genre>(restGenre);
            genre.GenreID = genreID;
            await GenreService.UpdateGenreAsync(genre);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public class RestGenre
        {
            public Guid GenreID { get; set; }
            public string Title { get; set; }
            public RestGenre(Guid genreID, string title)
            {
                this.GenreID = genreID;
                this.Title = title;
            }
        }

    }
}

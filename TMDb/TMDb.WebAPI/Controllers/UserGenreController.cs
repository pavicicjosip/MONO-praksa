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
    public class UserGenreController : ApiController
    {
        ///<summary>
        ///get po accountID - vraća 10 najbolje ocjenjenih filmova od jednog, drugog, trećeg ... žanra
        ///get da  vrati najdraže žanrove od usera
        ///</summary>

        protected IUserGenreService userGenreService { get; private set; }
        public UserGenreController(IUserGenreService userGenreService)
        {
            this.userGenreService = userGenreService;
        }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserGenre, RestUserGenre>().ReverseMap());

        [HttpPost]
        [Route("api/UserGenre/insertUserGenreAsync")]
        public async Task<HttpResponseMessage> InsertUserGenreAsync([FromBody] RestUserGenre restUserGenre)
        {
            var mapper = Mapper.CreateMapper();
            UserGenre userGenre = mapper.Map<UserGenre>(restUserGenre);
            await userGenreService.InsertUserGenreAsync(userGenre);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpDelete]
        [Route("api/UserGenre/deleteUserGenre")]
        public async Task<HttpResponseMessage> RemoveUserGenreAsync(Guid accountID, Guid genreID)
        {
            await userGenreService.RemoveUserGenreAsync(accountID, genreID);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/UserGenre/{accountID}")]
        public async Task<HttpResponseMessage> SelectFavouriteGenreAsync(Guid accountID)
        {
            List<Genre> list = await userGenreService.SelectFavouriteGenreAsync(accountID);
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public class RestUserGenre
        {
            public Guid AccountID { get; set; }
            public Guid GenreID { get; set; }
        }
    }
}

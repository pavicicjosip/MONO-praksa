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
using System.Security.Claims;
using System.Linq;

namespace TMDb.WebAPI.Controllers
{
    public class UserGenreController : ApiController
    {
        protected IUserGenreService UserGenreService { get; private set; }
        public UserGenreController(IUserGenreService userGenreService)
        {
            this.UserGenreService = userGenreService;
        }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserGenre, RestUserGenre>().ReverseMap());

        [HttpPost]
        [Authorize]
        [Route("api/UserGenre/insertUserGenreAsync")]
        public async Task<HttpResponseMessage> InsertUserGenreAsync([FromBody] RestUserGenre restUserGenre)
        {
            var mapper = Mapper.CreateMapper();

            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            var claims = identity.Claims;

            UserGenre userGenre = mapper.Map<UserGenre>(restUserGenre);
            userGenre.AccountID = Guid.Parse(claims.Where(p => p.Type == "guid").FirstOrDefault()?.Value);
            await UserGenreService.InsertUserGenreAsync(userGenre);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpDelete]
        [Authorize]
        [Route("api/UserGenre/deleteUserGenre")]
        public async Task<HttpResponseMessage> RemoveUserGenreAsync(Guid genreID)
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            var claims = identity.Claims;

            var accountID = Guid.Parse(claims.Where(p => p.Type == "guid").FirstOrDefault()?.Value);
            await UserGenreService.RemoveUserGenreAsync(accountID, genreID);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Authorize]
        [Route("api/UserGenre")]
        public async Task<HttpResponseMessage> SelectFavouriteGenreAsync()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var accountID = Guid.Parse(claims.Where(p => p.Type == "guid").FirstOrDefault()?.Value);
            
            List<Genre> list = await UserGenreService.SelectFavouriteGenreAsync(accountID);
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/UserGenre/selectMoviesFromGenreAsync")]
        public async Task<HttpResponseMessage> SelectMoviesFromGenreAsync(Guid accountID, int pageNumber = 1, int pageSize = 10)
        {
            PagedResponse pagedResponse = new PagedResponse { PageNumber = pageNumber, PageSize = pageSize };
            List<Movie> list = await UserGenreService.SelectMoviesFromGenreAsync(pagedResponse, accountID);
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

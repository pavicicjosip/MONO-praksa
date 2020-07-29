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
    public class CCMovieController : ApiController
    {
        ///<summary>
        ///Get po movie_id
        ///Get po CastID
        ///get po uloga i movieID
        ///paging, sorting, filtering
        ///</summary>
        protected ICCMovieService ccMovieService { get; private set; }
        public CCMovieController(ICCMovieService ccMovieService)
        {
            this.ccMovieService = ccMovieService;
        }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<CCMovie, RestCCMovie>().ReverseMap());

        [HttpPost]
        [Route("api/CCmovie/insertCCMovieAsync")]
        public async Task<HttpResponseMessage> InsertCCMovieAsync([FromBody] RestCCMovie restCCMovie)
        {
            var mapper = Mapper.CreateMapper();
            CCMovie ccMovie = mapper.Map<CCMovie>(restCCMovie);
            await ccMovieService.InsertCCMovieAsync(ccMovie);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public class RestCCMovie
        {
            public Guid MovieID { get; set; }
            public Guid CastID { get; set; }
            public string RoleInMovie { get; set; }
        }
    }
}

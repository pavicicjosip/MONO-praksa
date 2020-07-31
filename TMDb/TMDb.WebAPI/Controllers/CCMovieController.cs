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
        ///Get po CastID, vrati filmove
        ///get po uloga i movieID, vrati castove
        ///paging, sorting, filtering
        ///</summary>
        protected ICCMovieService ccMovieService { get; private set; }
        public CCMovieController(ICCMovieService ccMovieService)
        {
            this.ccMovieService = ccMovieService;
        }


        [HttpPost]
        [Route("api/CCmovie/InsertAsync")]
        public async Task<HttpResponseMessage> InsertAsync([FromBody] CCMovie ccMovie)
        {
            await ccMovieService.InsertAsync(ccMovie);
            return Request.CreateResponse(HttpStatusCode.OK, "Insert successful");
        }

        [HttpDelete]
        [Route("api/CCmovie/DeleteAsync")]
        public async Task<HttpResponseMessage> DeleteAsync(Guid castID, Guid movieID, string roleInMovie)
        {
            await ccMovieService.DeleteAsync(castID, movieID, roleInMovie);
            return Request.CreateResponse(HttpStatusCode.OK, "Delete successful");
        }

        [HttpGet]
        [Route("api/CCmovie/SelectAsync")]
        public async Task<HttpResponseMessage> SelectAsync(Guid castID, int pageNumber = 1, int pageSize = 10)
        {
            PagedResponse pagedResponse = new PagedResponse { PageNumber = pageNumber, PageSize = pageSize };
            Tuple<int, List<Movie>> tuple = await ccMovieService.SelectAsync(pagedResponse, castID);

            return Request.CreateResponse(HttpStatusCode.OK, tuple.Item2);
        }

        
    }
}

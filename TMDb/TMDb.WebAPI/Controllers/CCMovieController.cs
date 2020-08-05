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
        protected ICCMovieService CCMovieService { get; private set; }
        public CCMovieController(ICCMovieService ccMovieService)
        {
            this.CCMovieService = ccMovieService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("api/CCmovie/InsertAsync")]
        public async Task<HttpResponseMessage> InsertAsync([FromBody] CCMovie ccMovie)
        {
            await CCMovieService.InsertAsync(ccMovie);
            return Request.CreateResponse(HttpStatusCode.OK, "Insert successful");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("api/CCmovie/DeleteAsync")]
        public async Task<HttpResponseMessage> DeleteAsync(Guid castID, Guid movieID, string roleInMovie)
        {
            await CCMovieService.DeleteAsync(castID, movieID, roleInMovie);
            return Request.CreateResponse(HttpStatusCode.OK, "Delete successful");
        }

        [HttpGet]
        [Route("api/CCmovie/SelectAsync")]
        public async Task<HttpResponseMessage> SelectAsync(Guid castID, int pageNumber = 1, int pageSize = 10)
        {
            PagedResponse pagedResponse = new PagedResponse { PageNumber = pageNumber, PageSize = pageSize };
            Tuple<int, List<Movie>> tuple = await CCMovieService.SelectAsync(pagedResponse, castID);

            return Request.CreateResponse(HttpStatusCode.OK, tuple.Item2);
        }

        
    }
}

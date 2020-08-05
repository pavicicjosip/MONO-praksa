using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMDb.Model;
using TMDb.Service.Common;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using System.Web;
using TMDb.Common.CastAndCrew;
using TMDb.Common;

namespace TMDb.WebAPI.Controllers
{
    public class CastAndCrewController : ApiController
    {
        protected ICastAndCrewService CastAndCrewService { get; private set; }
        protected ICastAndCrewFacade CastAndCrewFacade { get; private set; }

        static MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<RestCastAndCrew, CastAndCrew>(); });
        
        public CastAndCrewController() { }
        public CastAndCrewController(ICastAndCrewService iCastAndCrewService, ICastAndCrewFacade iCastAndCrewFacade)
        {
            this.CastAndCrewService = iCastAndCrewService;
            this.CastAndCrewFacade = iCastAndCrewFacade;
        }

        

        [HttpGet]
        [Route("api/CastAndCrew/SelectAsync")]
        public async Task<HttpResponseMessage> SelectAsync(int pageNumber = 1, int pageSize = 10, string firstName = default(String), string lastName = default(String), string dateOfBirth = default(String), Guid? movieID = null, string role = default(String))
        {
            PagedResponse pagedResponse = new PagedResponse { PageNumber = pageNumber, PageSize = pageSize };

            CastAndCrewFacade.FirstName.FirstName = firstName;
            CastAndCrewFacade.LastName.LastName = lastName;
            CastAndCrewFacade.DateOfBirth.DateOfBirth = dateOfBirth;
            CastAndCrewFacade.MovieID.MovieID = movieID;
            CastAndCrewFacade.Role.Role = role;


            Tuple<int, List<CastAndCrew>> tuple =  await CastAndCrewService.SelectAsync(pagedResponse, CastAndCrewFacade);

            return Request.CreateResponse(HttpStatusCode.OK, tuple.Item2);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("api/CastAndCrew/InsertAsync")]
        public async Task<HttpResponseMessage> InsertAsync([FromBody] RestCastAndCrew restCastAndCrew)
        {
            IMapper iMapper = config.CreateMapper();
            CastAndCrew castAndCrew = iMapper.Map<RestCastAndCrew, CastAndCrew>(restCastAndCrew);

            await CastAndCrewService.InsertAsync(castAndCrew);

            return Request.CreateResponse(HttpStatusCode.OK, "Insert successful");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("api/CastAndCrew/UpdateAsync/{castID}")]
        public async Task<HttpResponseMessage> UpdateAsync([FromBody] RestCastAndCrew restCastAndCrew, [FromUri] Guid castID)
        {
            IMapper iMapper = config.CreateMapper();
            CastAndCrew castAndCrew = iMapper.Map<RestCastAndCrew, CastAndCrew>(restCastAndCrew);

            await CastAndCrewService.UpdateAsync(castID, castAndCrew);

            return Request.CreateResponse(HttpStatusCode.OK, "Update successful");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("api/CastAndCrew/DeleteAsync/{castID}")]
        public async Task<HttpResponseMessage> DeleteAsync([FromUri] Guid castID)
        {
            await CastAndCrewService.DeleteAsync(castID);

            return Request.CreateResponse(HttpStatusCode.OK, "Delete successful");
        }
    }

    public class RestCastAndCrew
    {
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public Guid FileID { get; set; }

    }

}

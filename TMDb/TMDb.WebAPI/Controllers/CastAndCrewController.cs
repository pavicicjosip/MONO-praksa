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
        protected ICastAndCrewService _ICastAndCrewService { get; private set; }
        protected ICastAndCrewFacade castAndCrewFacade { get; private set; }

        static MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<RestCastAndCrew, CastAndCrew>(); });
        
        public CastAndCrewController() { }
        public CastAndCrewController(ICastAndCrewService iCastAndCrewService, ICastAndCrewFacade iCastAndCrewFacade)
        {
            this._ICastAndCrewService = iCastAndCrewService;
            this.castAndCrewFacade = iCastAndCrewFacade;
        }

        

        [HttpGet]
        [Route("api/CastAndCrew/SelectAsync")]
        public async Task<HttpResponseMessage> SelectAsync(int pageNumber = 1, int pageSize = 10, string firstName = default(String), string lastName = default(String), string dateOfBirth = default(String), Guid? movieID = null)
        {
            PagedResponse pagedResponse = new PagedResponse { PageNumber = pageNumber, PageSize = pageSize };

            castAndCrewFacade.FirstName.FirstName = firstName;
            castAndCrewFacade.LastName.LastName = lastName;
            castAndCrewFacade.DateOfBirth.DateOfBirth = dateOfBirth;
            castAndCrewFacade.MovieID.MovieID = movieID;


            Tuple<int, List<CastAndCrew>> tuple =  await _ICastAndCrewService.SelectAsync(pagedResponse, castAndCrewFacade);

            return Request.CreateResponse(HttpStatusCode.OK, tuple.Item2);
        }

        [HttpPost]
        [Route("api/CastAndCrew/InsertAsync")]
        public async Task<HttpResponseMessage> InsertAsync([FromBody] RestCastAndCrew restCastAndCrew)
        {
            IMapper iMapper = config.CreateMapper();
            CastAndCrew castAndCrew = iMapper.Map<RestCastAndCrew, CastAndCrew>(restCastAndCrew);

            await _ICastAndCrewService.InsertAsync(castAndCrew);

            return Request.CreateResponse(HttpStatusCode.OK, "Insert successful");
        }

        [HttpPut]
        [Route("api/CastAndCrew/UpdateAsync/{castID}")]
        public async Task<HttpResponseMessage> UpdateAsync([FromBody] RestCastAndCrew restCastAndCrew, [FromUri] Guid castID)
        {
            IMapper iMapper = config.CreateMapper();
            CastAndCrew castAndCrew = iMapper.Map<RestCastAndCrew, CastAndCrew>(restCastAndCrew);

            await _ICastAndCrewService.UpdateAsync(castID, castAndCrew);

            return Request.CreateResponse(HttpStatusCode.OK, "Update successful");
        }

        [HttpDelete]
        [Route("api/CastAndCrew/DeleteAsync/{castID}")]
        public async Task<HttpResponseMessage> DeleteAsync([FromUri] Guid castID)
        {
            await _ICastAndCrewService.DeleteAsync(castID);

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

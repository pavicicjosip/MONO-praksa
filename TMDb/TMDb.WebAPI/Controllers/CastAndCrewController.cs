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

namespace TMDb.WebAPI.Controllers
{
    /// <summary>
    /// dodati paging, sorting
    /// </summary>
    public class CastAndCrewController : ApiController
    {
        protected ICastAndCrewService _ICastAndCrewService { get; set; }
        protected ICastAndCrewFacade castAndCrewFacade { get; set; }
        public CastAndCrewController(ICastAndCrewService iCastAndCrewService, ICastAndCrewFacade iCastAndCrewFacade)
        {
            this._ICastAndCrewService = iCastAndCrewService;
            this.castAndCrewFacade = iCastAndCrewFacade;
        }

        [HttpGet]
        [Route("api/CastAndCrew/SelectAsync")]
        public async Task<HttpResponseMessage> SelectAsync(string firstName = default(String), string lastName = default(String), string dateOfBirth = default(String), Guid? movieID = null)
        {
            castAndCrewFacade.FirstName.FirstName = firstName;
            castAndCrewFacade.LastName.LastName = lastName;
            castAndCrewFacade.DateOfBirth.DateOfBirth = dateOfBirth;
            castAndCrewFacade.MovieID.MovieID = movieID;


            List<CastAndCrew> _out = await _ICastAndCrewService.SelectAsync(castAndCrewFacade);
            return Request.CreateResponse(HttpStatusCode.OK, _out);
        }

    }

}

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

namespace TMDb.WebAPI.Controllers
{
    /// <summary>
    /// dodati get svi koji sudjeluju na određenom filmu
    /// dodati paging, sorting, filtering
    /// </summary>
    public class CastAndCrewController : ApiController
    {
        protected ICastAndCrewService _ICastAndCrewService { get; set; }
        public CastAndCrewController(ICastAndCrewService iCastAndCrewService)
        {
            this._ICastAndCrewService = iCastAndCrewService;
        }

        [HttpGet]
        [Route("api/CastAndCrew/SelectByFirstNameAsync")]
        public async Task<HttpResponseMessage> SelectByFirstNameAsync([FromBody] CastAndCrewFirstName firstName)
        {
            List<CastAndCrew> _out = await _ICastAndCrewService.SelectByFirstNameAsync(firstName.FirstName);
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _out);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/CastAndCrew/SelectByLastNameAsync")]
        public async Task<HttpResponseMessage> SelectByLastNameAsync([FromBody] CastAndCrewlastName lastName)
        {
            List<CastAndCrew> _out = await _ICastAndCrewService.SelectByLastNameAsync(lastName.LastName);
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _out);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/CastAndCrew/SelectByDateOfBirthAsync")]
        public async Task<HttpResponseMessage> SelectByDateOfBirthAsync([FromBody] CastAndCrewDateOfBirth cacDate)
        {
            List<CastAndCrew> _out = await _ICastAndCrewService.SelectByDateOfBirthAsync(cacDate.Date);
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _out);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }


    public class CastAndCrewFirstName
    {
        public string FirstName { get; set; }
    }
    public class CastAndCrewlastName
    {
        public string LastName { get; set; }
    }
    public class CastAndCrewDateOfBirth
    {
        public string Date { get; set; }
    }
}

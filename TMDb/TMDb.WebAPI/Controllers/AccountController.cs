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
    /// hash lozinka i kada napravimo tokenizaciju bit će promjena 
    /// </summary>
    public class AccountController : ApiController
    {
        protected IAccountService _IAccountService { get; set; }
        public AccountController(IAccountService iAccountService)
        {
            this._IAccountService = iAccountService;
        }

        [HttpGet]
        [Route("api/Account/SelectAccountAsync")]
        public async Task<HttpResponseMessage> SelectAccountAsync([FromBody] Account acc)
        {
            Account account = await _IAccountService.SelectAccountAsync(acc);
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, account);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/Account/SelectAccountAsync/{accountID}")]
        public async Task<HttpResponseMessage> SelectAccountAsync([FromUri] Guid accountID)
        {
            Account account = await _IAccountService.SelectAccountAsync(accountID);
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, account);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [Route("api/Account/DeleteAccountAsync/{accountID}")]
        public async Task<HttpResponseMessage> DeleteAccountAsync([FromUri] Guid accountID)
        {
            await _IAccountService.DeleteAccountAsync(accountID);

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Delete successful");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [Route("api/Account/UpdateAccountAsync/{accountID}")]
        public async Task<HttpResponseMessage> UpdateAccountAsync([FromUri] Guid accountID, [FromBody] RestAccount restAcc)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RestAccount, Account>(); });
            IMapper iMapper = config.CreateMapper();

            Account acc = iMapper.Map<RestAccount, Account>(restAcc);

            acc.AccountID = accountID;

            await _IAccountService.UpdateAccountAsync(acc);

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Update successful");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }

    public class RestAccount
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public Guid FileID { get; set; }
    }


}
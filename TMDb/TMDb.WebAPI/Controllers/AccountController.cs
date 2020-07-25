
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

namespace TMDb.WebAPI.Controllers
{
    public class AccountController : ApiController
    {
        protected IAccountService _IAccountService { get; set; }
        public AccountController(IAccountService iAccountService)
        {
            this._IAccountService = iAccountService;
        }

        [HttpGet]
        [Route("api/Acc/SelectAccountAsync")]
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

    }

    public class RestAccount
    {
        public string Username { get; set; }
        public string UserPassword { get; set; }
    }
}
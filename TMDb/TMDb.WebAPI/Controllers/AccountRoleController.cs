using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMDb.Model;
using TMDb.Service.Common;
using AutoMapper;
using System.Threading.Tasks;

namespace TMDb.WebAPI.Controllers
{
    public class AccountRoleController : ApiController
    {
        protected IAccountRoleService accountRoleService { get; private set; }
        public AccountRoleController(IAccountRoleService accountRoleService)
        {
            this.accountRoleService = accountRoleService;
        }
        [HttpDelete]
        [Route("api/AccountRole")]
        public async Task<HttpResponseMessage> DeleteAccountAsync(Guid accountID, string role)
        {
            await accountRoleService.DeleteAccountAsync(accountID, role);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

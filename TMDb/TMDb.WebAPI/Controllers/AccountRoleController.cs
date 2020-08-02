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
        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<AccountRole, RestAccountRole>().ReverseMap());

        [HttpDelete]
        [Route("api/AccountRole")]
        public async Task<HttpResponseMessage> DeleteAccountAsync(Guid accountID, string role)
        {
            await accountRoleService.DeleteAccountAsync(accountID, role);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/AccountRole")]
        public async Task<HttpResponseMessage> GetRoleByAccountIdAsync(Guid accountID)
        {
            List<string> list = await accountRoleService.GetRoleByAccountIdAsync(accountID);
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [Route("api/AccountRole")]
        public async Task<HttpResponseMessage> UpdateAccountRoleAsync(Guid accountID, [FromBody]RestAccountRole restAccountRole)
        {
            var mapper = Mapper.CreateMapper();
            AccountRole accountRole = mapper.Map<AccountRole>(restAccountRole);
            accountRole.AccountID = accountID;
            await accountRoleService.UpdateAccountRoleAsync(accountRole);
            return Request.CreateResponse(HttpStatusCode.OK, "Update successful!");
        }

        public class RestAccountRole
        {
            public Guid AccountID { get; set; }
            public string Role { get; set; }
        }
    }
}
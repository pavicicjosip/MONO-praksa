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
        protected IAccountRoleService AccountRoleService { get; private set; }
        public AccountRoleController(IAccountRoleService accountRoleService)
        {
            this.AccountRoleService = accountRoleService;
        }
        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<AccountRole, RestAccountRole>().ReverseMap());

        [HttpDelete]
        [Route("api/AccountRole")]
        public async Task<HttpResponseMessage> DeleteAccountAsync(Guid accountID, string role)
        {
            await AccountRoleService.DeleteAccountAsync(accountID, role);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/AccountRole")]
        public async Task<HttpResponseMessage> GetRoleByAccountIdAsync(Guid? accountID)
        {
            string list = await AccountRoleService.GetRoleByAccountIdAsync(accountID);
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
        [Authorize(Roles = "Admin")]
        [Route("api/AccountRole")]
        public async Task<HttpResponseMessage> UpdateAccountRoleAsync(Guid accountID, [FromBody]RestAccountRole restAccountRole)
        {
            var mapper = Mapper.CreateMapper();
            AccountRole accountRole = mapper.Map<AccountRole>(restAccountRole);
            accountRole.AccountID = accountID;
            await AccountRoleService.UpdateAccountRoleAsync(accountRole);
            return Request.CreateResponse(HttpStatusCode.OK, "Update successful!");
        }
        [HttpPost]
        [Route("api/AccountRole")]
        public async Task<HttpResponseMessage> InsertAccountRoleAsync([FromBody] RestAccountRole restAccountRole)
        {
            var mapper = Mapper.CreateMapper();
            AccountRole accountRole = mapper.Map<AccountRole>(restAccountRole);
            await AccountRoleService.UpdateAccountRoleAsync(accountRole);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public class RestAccountRole
        {
            public Guid AccountID { get; set; }
            public string Role { get; set; }
        }
    }
}
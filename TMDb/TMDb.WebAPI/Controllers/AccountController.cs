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
using TMDb.Common.Account;
using TMDb.Common;
using System.Security.Claims;
using TMDb.Repository;
using TMDb.Model.Common;
using System.Web.Http.Cors;

namespace TMDb.WebAPI.Controllers
{
    /// <summary>
    /// kada napravimo tokenizaciju bit ce promjena 
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        protected IAccountService AccountService { get; set; }
        protected TokenGenerator TokenGenerator { get; set; }
        protected IAccountRoleService AccountRoleService { get; private set; }

        protected IAccountFacade AccountFacade { get; set; }
        public AccountController(IAccountService iAccountService, IAccountFacade accountFacade, IAccountRoleService accountRoleService)
        {
            this.AccountService = iAccountService;
            this.AccountFacade = accountFacade;
            this.AccountRoleService = accountRoleService;
            this.TokenGenerator = new TokenGenerator();
        }

        [HttpGet]
        [Route("api/Account/SelectAccountAsync")]
        public async Task<HttpResponseMessage> SelectAccountAsync(Guid? accountID = null, string userName = "", string userPassword = "")
        {
            AccountFacade.AccountID.AccountID = accountID;
            AccountFacade.UserName.UserName = userName;
            AccountFacade.UserPassword.UserPassword = userPassword;
            string token = "";
            
            Account account = await AccountService.SelectAccountAsync(AccountFacade);

            if (account.UserName != null)
            {
                token = TokenGenerator.GenerateToken(account.AccountID, await AccountRoleService.GetRoleByAccountIdAsync(account.AccountID)); 
            }
            return Request.CreateResponse(HttpStatusCode.OK, token);
            /*var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var id = claims.Where(p => p.Type == "guid").FirstOrDefault()?.Value;
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            else  
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }*/
        }


        [HttpDelete]
        [Authorize]
        [Route("api/Account/DeleteAccountAsync")]
        public async Task<HttpResponseMessage> DeleteAccountAsync()
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            var claims = identity.Claims;
            await AccountService.DeleteAccountAsync(Guid.Parse(claims.Where(p => p.Type == "guid").FirstOrDefault()?.Value));

            return Request.CreateResponse(HttpStatusCode.OK, "Delete successful");
        }

        [HttpPut]
        [Authorize]
        [Route("api/Account/UpdateAccountAsync/{accountID}")]
        public async Task<HttpResponseMessage> UpdateAccountAsync([FromBody] RestAccount restAcc)
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            var claims = identity.Claims;

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RestAccount, Account>(); });
            IMapper iMapper = config.CreateMapper();

            Account acc = iMapper.Map<RestAccount, Account>(restAcc);

            acc.AccountID = Guid.Parse(claims.Where(p => p.Type == "guid").FirstOrDefault()?.Value);

            await AccountService.UpdateAccountAsync(acc);

            return Request.CreateResponse(HttpStatusCode.OK, "Update successful");
        }

        [HttpPost]
        [Route("api/Account/InsertAccountAsync")]
        public async Task<HttpResponseMessage> InsertAccountAsync([FromBody] RestAccount restAcc)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RestAccount, Account>(); });
            IMapper iMapper = config.CreateMapper();

            Account acc = iMapper.Map<RestAccount, Account>(restAcc);

            await AccountService.InsertAccountAsync(acc);

            return Request.CreateResponse(HttpStatusCode.OK, "Insert successful");
        }
    }

    public class RestAccount
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public Guid FileID { get; set; }
    }


}

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
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using TMDb.Repository;

namespace TMDb.WebAPI.Controllers
{
    /// <summary>
    /// kada napravimo tokenizaciju bit ce promjena 
    /// </summary>
    public class AccountController : ApiController
    {
        protected IAccountService AccountService { get; set; }
        protected TokenGenerator TokenGenerator { get; set; }

        protected IAccountFacade AccountFacade { get; set; }
        public AccountController(IAccountService iAccountService, IAccountFacade accountFacade)
        {
            this.AccountService = iAccountService;
            this.AccountFacade = accountFacade;
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

            if (account != null)
            {
                token = TokenGenerator.GenerateToken(account.AccountID, "User"); //dohvatiti ulogu iz tablice AccountRole
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
        [Route("api/Account/DeleteAccountAsync/{accountID}")]
        public async Task<HttpResponseMessage> DeleteAccountAsync([FromUri] Guid accountID)
        {
            await AccountService.DeleteAccountAsync(accountID);

            return Request.CreateResponse(HttpStatusCode.OK, "Delete successful");
        }

        [HttpPut]
        [Route("api/Account/UpdateAccountAsync/{accountID}")]
        public async Task<HttpResponseMessage> UpdateAccountAsync([FromUri] Guid accountID, [FromBody] RestAccount restAcc)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RestAccount, Account>(); });
            IMapper iMapper = config.CreateMapper();

            Account acc = iMapper.Map<RestAccount, Account>(restAcc);

            acc.AccountID = accountID;

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
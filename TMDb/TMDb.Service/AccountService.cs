using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Model;
using TMDb.Service.Common;

namespace TMDb.Service
{
    public class AccountService : IAccountService
    {
        protected IAccountRepository _IAccountRepository { get; set; }

        public AccountService(IAccountRepository _IAccountRepository)
        {
            this._IAccountRepository = _IAccountRepository;
        }

        public async Task<Account> SelectAccountAsync(string username, string userPassword)
        {
            return await _IAccountRepository.SelectAccountAsync(username, userPassword);
        }
    }
}

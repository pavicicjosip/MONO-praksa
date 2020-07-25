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

        public async Task<Account> SelectAccountAsync(Guid accountID)
        {
            return await _IAccountRepository.SelectAccountAsync(accountID);
        }

        public async Task<Account> SelectAccountAsync(Account acc)
        {
            return await _IAccountRepository.SelectAccountAsync(acc);
        }

        public async Task DeleteAccountAsync(Guid accountID)
        {
            await _IAccountRepository.DeleteAccountAsync(accountID);
        }

        public async Task UpdateAccountAsync(Account acc)
        {
            Account account = await _IAccountRepository.SelectAccountAsync(acc.AccountID);


            if (acc.Email != "")
                account.Email = acc.Email;
            if (acc.Username != "")
                account.Username = acc.Username;
            if (acc.UserPassword != "")
                account.UserPassword = acc.UserPassword;
            if (acc.FileID.ToString() != "")
                account.FileID = acc.FileID;

            await _IAccountRepository.UpdateAccountAsync(account);
        }
    }
}
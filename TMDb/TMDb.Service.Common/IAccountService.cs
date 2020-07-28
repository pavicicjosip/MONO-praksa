using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Model;
using TMDb.Common.Account;
using TMDb.Common;

namespace TMDb.Service.Common
{
    public interface IAccountService
    {
        Task<Account> SelectAccountAsync(IAccountFacade iAccountFacade);
        Task DeleteAccountAsync(Guid accountID);
        Task UpdateAccountAsync(Account acc);
        Task InsertAccountAsync(Account acc);
    }
}
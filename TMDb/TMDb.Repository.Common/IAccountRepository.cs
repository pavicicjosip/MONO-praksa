using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;

namespace TMDb.Repository.Common
{
    public interface IAccountRepository
    {
        Task<Account> SelectAccountAsync(string whereStatement);
        Task DeleteAccountAsync(Guid accountID);
        Task UpdateAccountAsync(Account acc);
        Task InsertAccountAsync(Account acc);


    }
}
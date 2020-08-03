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
    public class AccountRoleService : IAccountRoleService
    {
        protected IAccountRoleRepository AccountRoleRepository
        { get; private set; }

        public AccountRoleService(IAccountRoleRepository accountRoleRepository)
        {
            this.AccountRoleRepository = accountRoleRepository;
        }
        public async Task DeleteAccountAsync(Guid accountID, string role)
        {
            await AccountRoleRepository.DeleteAccountAsync(accountID, role);
        }
        public async Task<List<string>> GetRoleByAccountIdAsync(Guid accountID)
        {
            return await AccountRoleRepository.GetRoleByAccountIdAsync(accountID);
        }
        public async Task UpdateAccountRoleAsync(AccountRole accountRole)
        {
            await AccountRoleRepository.UpdateAccountRoleAsync(accountRole);
        }
        public async Task InsertAccountRoleAsync(AccountRole accountRole)
        {
            await AccountRoleRepository.InsertAccountRoleAsync(accountRole);
        }
    }
}

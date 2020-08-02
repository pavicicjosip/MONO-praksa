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
        protected IAccountRoleRepository accountRoleRepository
        { get; private set; }

        public AccountRoleService(IAccountRoleRepository accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
        }
        public async Task DeleteAccountAsync(Guid accountID, string role)
        {
            await accountRoleRepository.DeleteAccountAsync(accountID, role);
        }
    }
}

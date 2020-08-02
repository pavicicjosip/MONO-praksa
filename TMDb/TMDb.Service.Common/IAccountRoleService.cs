using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Model;

namespace TMDb.Service.Common
{
    public interface IAccountRoleService
    {
        Task DeleteAccountAsync(Guid accountID, string role);
        Task<List<string>> GetRoleByAccountIdAsync(Guid accountID);
        Task UpdateAccountRoleAsync(AccountRole accountRole);
    }
}

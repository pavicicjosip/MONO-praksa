﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;

namespace TMDb.Repository.Common
{
    public interface IAccountRoleRepository
    {
        Task DeleteAccountAsync(Guid accountID, string role);
        Task<string> GetRoleByAccountIdAsync(Guid? accountID);
        Task UpdateAccountRoleAsync(AccountRole accountRole);
        Task InsertAccountRoleAsync(AccountRole accountRole);
    }
}

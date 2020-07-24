using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Model;

namespace TMDb.Service.Common
{
    public interface IAccountService
    {
        Task<Account> SelectAccountAsync(string username, string userPassword);
    }
}

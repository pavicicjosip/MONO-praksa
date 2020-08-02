using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Common.Account;

namespace TMDb.Common
{
    public interface IAccountFacade
    {
        IAccountAccountID AccountID { get; set; }
        IAccountUserName UserName { get; set; }
        IAccountUserPassword UserPassword { get; set; }

        string WhereStatement();
    }
}
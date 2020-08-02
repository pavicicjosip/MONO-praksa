using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.Account
{
    public interface IAccountUserPassword
    {
        string UserPassword { get; set; }
        bool Default();
        string WhereStatement();
    }
}

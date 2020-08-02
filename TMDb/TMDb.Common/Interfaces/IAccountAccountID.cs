using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.Account
{
    public interface IAccountAccountID
    {
        Guid? AccountID { set; get; }
        string WhereStatement();
    }
}
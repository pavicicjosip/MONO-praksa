using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.Account
{
    class AccountAccountID : IAccountAccountID
    {
        public Guid? AccountID { get; set; }

        public AccountAccountID() { }
        public AccountAccountID(Guid accountID)
        {
            this.AccountID = accountID;
        }

        public string WhereStatement()
        {
            return " AccountID = " + "'" + AccountID + "' ";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Common.Account;

namespace TMDb.Common
{
    public class AccountFacade : IAccountFacade
    {
        public IAccountAccountID AccountID { get; set; }
        public IAccountUserName UserName { get; set; }
        public IAccountUserPassword UserPassword { get; set; }


        public AccountFacade(IAccountAccountID accountID, IAccountUserName userName, IAccountUserPassword userPassword)
        {
            this.AccountID = accountID;
            this.UserName = userName;
            this.UserPassword = userPassword;
        }

        public string WhereStatement()
        {
            string _out = default(string);


            if (UserName.Default() && UserPassword.Default())
            {
                _out = " WHERE " + AccountID.WhereStatement();
            }
            //else if (!UserName.Default() && !UserPassword.Default())
            else
            {
                _out = " WHERE " + UserName.WhereStatement() + " AND " + UserPassword.WhereStatement();
            }

            return _out;
        }
    }
}
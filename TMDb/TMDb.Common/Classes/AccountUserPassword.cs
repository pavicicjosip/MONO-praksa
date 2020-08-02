using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.Account
{
    public class AccountUserPassword : IAccountUserPassword
    {
        public string UserPassword { set; get; }

        public AccountUserPassword() { }
        public AccountUserPassword(string userPassowrd)
        {
            this.UserPassword = userPassowrd;
        }

        public bool Default()
        {
            return UserPassword == "";
        }

        public string WhereStatement()
        {
            return " UserPassword = " + "'" + UserPassword + "' ";
        }
    }
}
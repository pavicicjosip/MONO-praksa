using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.Account
{
    public class AccountUserName : IAccountUserName
    {
        public string UserName { get; set; }

        public AccountUserName() { }
        public AccountUserName(string userName)
        {
            this.UserName = userName;
        }

        public bool Default()
        {
            return UserName == "";
        }

        public string WhereStatement()
        {
            return " UserName = " + "'" + UserName + "' ";
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model.Common;


namespace TMDb.Model
{
    public class AccountRole :IAccountRole
    {
        public Guid AccountID { get; set; }
        public string Role { get; set; }
        public AccountRole(Guid accountID, string role)
        {
            this.AccountID = accountID;
            this.Role = role;
        }
    }
}

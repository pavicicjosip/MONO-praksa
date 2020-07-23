using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model.Common;


namespace TMDb.Model
{
    public class Account : IAccount
    {
        public Guid AccountID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public Guid FileID { get; set; }

        public Account(Guid accountID, string email, string username, Guid fileID)
        {
            this.AccountID = accountID;
            this.Email = email;
            this.Username = username;
            this.FileID = fileID;
        }
    }
}

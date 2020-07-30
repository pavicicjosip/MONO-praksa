using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.Review
{
    public class ReviewAccountID : IReviewAccountID
    {
        public Guid AccountID { get; set; }

        public ReviewAccountID() { }
        public ReviewAccountID(Guid accountID)
        {
            AccountID = accountID;
        }

        public string WhereStatement()
        {
            return String.Format(" r.AccountID = '{0}' ", AccountID);
        }
        public bool Default()
        {
            return AccountID == Guid.Empty;
        }
    }
}

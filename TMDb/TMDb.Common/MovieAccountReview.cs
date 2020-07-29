using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common
{
    public class MovieAccountReview : IMovieAccountReview
    {
        public Guid AccountID { get; set; }

        public MovieAccountReview() { }

        public MovieAccountReview(Guid AccountID)
        {
            this.AccountID = AccountID;
        }

        public bool Default()
        {
            return AccountID == default(Guid);
        }

        public string WhereStatement()
        {
            return String.Format(" r.MovieID = m.MovieID AND r.AccountID = '{0}' ", AccountID);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.MovieLists
{
    public class MovieListsAccountID : IMovieListsAccountID
    {
        public Guid AccountID { get; set; }

        public MovieListsAccountID() { }
        public MovieListsAccountID(Guid accountID)
        {
            AccountID = accountID;
        }

        public string WhereStatement()
        {
            return String.Format(" AccountID = '{0}' ", AccountID);
        }
        public bool Default()
        {
            return AccountID == Guid.Empty;
        }
    }
}

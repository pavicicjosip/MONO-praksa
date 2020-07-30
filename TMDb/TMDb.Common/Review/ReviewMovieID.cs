using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.Review
{
    public class ReviewMovieID : IReviewMovieID
    {
        public Guid MovieID { get; set; }

        public ReviewMovieID() { }
        public ReviewMovieID(Guid movieID)
        {
            MovieID = movieID;
        }

        public string WhereStatement()
        {
            return String.Format(" r.MovieID = '{0}' ", MovieID);
        }
        public bool Default()
        {
            return MovieID == Guid.Empty;
        }

    }
}

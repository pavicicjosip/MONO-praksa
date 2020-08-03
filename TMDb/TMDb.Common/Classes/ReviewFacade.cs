using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.Review
{
    public class ReviewFacade : IReviewFacade
    {
        public IReviewAccountID ReviewAccountID { get; set; }
        public IReviewMovieID ReviewMovieID { get; set; }
        public ReviewFacade(IReviewAccountID reviewAccountID, IReviewMovieID reviewMovieID)
        {
            this.ReviewAccountID = reviewAccountID;
            this.ReviewMovieID = reviewMovieID;
        }
        public string WhereStatement()
        {
            bool accountBool = ReviewAccountID.Default();
            bool movieBool = ReviewMovieID.Default();

            string _out = "";

            switch (movieBool)
            {
                case true when accountBool: 
                    break;
                case true when !accountBool:
                    _out += " AND " + ReviewAccountID.WhereStatement();
                    break;
                case false when accountBool:
                    _out += " AND " + ReviewMovieID.WhereStatement();
                    break;
                case false when !accountBool:
                    _out += " AND " + ReviewAccountID.WhereStatement() +" AND " + ReviewMovieID.WhereStatement();
                    break;
            }

            return _out;
        }
    }
}

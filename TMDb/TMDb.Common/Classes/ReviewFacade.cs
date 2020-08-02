using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.Review
{
    public class ReviewFacade : IReviewFacade
    {
        public IReviewAccountID reviewAccountID { get; set; }
        public IReviewMovieID reviewMovieID { get; set; }
        public ReviewFacade(IReviewAccountID reviewAccountID, IReviewMovieID reviewMovieID)
        {
            this.reviewAccountID = reviewAccountID;
            this.reviewMovieID = reviewMovieID;
        }
        public string WhereStatement()
        {
            bool accountBool = reviewAccountID.Default();
            bool movieBool = reviewMovieID.Default();

            string _out = "";

            switch (movieBool)
            {
                case true when accountBool: 
                    break;
                case true when !accountBool:
                    _out += " AND " + reviewAccountID.WhereStatement();
                    break;
                case false when accountBool:
                    _out += " AND " + reviewMovieID.WhereStatement();
                    break;
                case false when !accountBool:
                    _out += " AND " + reviewAccountID.WhereStatement() +" AND " + reviewMovieID.WhereStatement();
                    break;
            }

            return _out;
        }
    }
}

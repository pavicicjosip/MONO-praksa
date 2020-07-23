using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model.Common;

namespace TMDb.Model
{
    public class Review : IReview
    {
        public Guid ReviewID { get; set; }
        public int NumberOfStars { get; set; }
        public string Comment { get; set; }
        public Guid AccountID { get; set; }
        public Guid MovieID { get; set; }

        public Review(Guid reviewID, int numberOfStars, string comment, Guid accountID, Guid movieID)
        {
            this.ReviewID = reviewID;
            this.NumberOfStars = numberOfStars;
            this.Comment = comment;
            this.AccountID = accountID;
            this.MovieID = movieID;
        }
    }
}

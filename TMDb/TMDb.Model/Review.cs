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
        public DateTime DateAndTime { get; set; }
        public string Username { get; set; }
        public Guid MovieID { get; set; }

        public Review() { }
        public Review(Guid reviewID, int numberOfStars, string comment, DateTime dateAntTime, string username, Guid movieID)
        {
            this.ReviewID = reviewID;
            this.NumberOfStars = numberOfStars;
            this.Comment = comment;
            this.DateAndTime = dateAntTime;
            this.Username = username;
            this.MovieID = movieID;
        }
    }
}

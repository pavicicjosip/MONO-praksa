using System;

namespace TMDb.Common.Review
{
    public interface IReviewMovieID
    {
        Guid MovieID { get; set; }

        bool Default();
        string WhereStatement();
    }
}
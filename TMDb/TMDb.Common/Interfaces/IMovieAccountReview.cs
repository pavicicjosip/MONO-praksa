using System;

namespace TMDb.Common
{
    public interface IMovieAccountReview
    {
        Guid AccountID { get; set; }

        bool Default();
        string WhereStatement();
    }
}
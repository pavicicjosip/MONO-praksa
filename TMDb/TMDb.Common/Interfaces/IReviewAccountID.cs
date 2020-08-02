using System;

namespace TMDb.Common.Review
{
    public interface IReviewAccountID
    {
        Guid AccountID { get; set; }

        bool Default();
        string WhereStatement();
    }
}
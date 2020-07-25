using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;

namespace TMDb.Service.Common
{
    public interface IReviewService
    {
        Task<List<Review>> ReturnMovieReviewsAsync(Guid movieID);
        Task<List<Review>> ReturnMovieReviewsOrderedAsync(Guid movieID, string column, bool order);
        Task<List<Review>> ReturnUserReviewsOrderedAsync(Guid accountID, string column, bool order);
        Task RemoveReviewAsync(Guid reviewID);
    }
}
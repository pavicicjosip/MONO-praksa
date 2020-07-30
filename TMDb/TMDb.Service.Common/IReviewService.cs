using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Common;
using TMDb.Common.Review;
using TMDb.Model;

namespace TMDb.Service.Common
{
    public interface IReviewService
    {
        Task<Tuple<int, List<Review>>> SelectReviewsAsync(PagedResponse pagedResponse, IReviewFacade reviewFacade, Sorting sort);
        Task CreateReviewAsync(Review review, Guid accountID);
        Task UpdateReviewAsync(Review review);
        Task RemoveReviewAsync(Guid reviewID);
    }
}
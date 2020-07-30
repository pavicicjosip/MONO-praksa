using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Service.Common;
using TMDb.Repository.Common;
using TMDb.Common;
using TMDb.Common.Review;

namespace TMDb.Service
{
    public class ReviewService : IReviewService
    {
        protected IReviewRepository reviewRepository
        { get; private set; }

        public ReviewService(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public async Task<Tuple<int, List<Review>>> SelectReviewsAsync(PagedResponse pagedResponse, IReviewFacade reviewFacade, Sorting sort)
        {
            int pageNumberStart = (pagedResponse.PageNumber - 1) * pagedResponse.PageSize;
            string whereStatement = reviewFacade.WhereStatement();
            int numberOfResults;

            if (sort.Column == "default")
            {
                sort.Column = "r.DateAndTime";
                sort.Order = true;
            }
            numberOfResults = await reviewRepository.SelectNumberOfResultsAsync(whereStatement);
            return new Tuple<int, List<Review>>(numberOfResults, await reviewRepository.SelectReviewsAsync(pageNumberStart, pageNumberStart + pagedResponse.PageSize, whereStatement, sort));
        }

        public async Task CreateReviewAsync(Review review, Guid accountID)
        {
            await reviewRepository.InsertReviewAsync(review, accountID);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            await reviewRepository.UpdateReviewAsync(review);
        }

        public async Task RemoveReviewAsync(Guid reviewID)
        {
            await reviewRepository.DeleteReviewAsync(reviewID);
        }
    }
}

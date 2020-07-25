using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Service.Common;
using TMDb.Repository.Common;


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

        public async Task<List<Review>> ReturnMovieReviewsAsync(Guid movieID)
        {
            return await reviewRepository.SelectMovieReviewsAsync(movieID);
        }

        public async Task<List<Review>> ReturnMovieReviewsOrderedAsync(Guid movieID, string column, bool order)
        {
            if (order)
            {
                return await reviewRepository.SelectMovieReviewsOrderedAsync(movieID, column, "ASC");
            }
            else
            {
                return await reviewRepository.SelectMovieReviewsOrderedAsync(movieID, column, "DESC");
            }
        }

        public async Task<List<Review>> ReturnUserReviewsOrderedAsync(Guid accountID, string column, bool order)
        {
            if (order)
            {
                return await reviewRepository.SelectUserReviewsOrderedAsync(accountID, column, "ASC");
            }
            else
            {
                return await reviewRepository.SelectUserReviewsOrderedAsync(accountID, column, "DESC");
            }
        }

        public async Task RemoveReviewAsync(Guid reviewID)
        {
            await reviewRepository.DeleteReviewAsync(reviewID);
        }
    }
}

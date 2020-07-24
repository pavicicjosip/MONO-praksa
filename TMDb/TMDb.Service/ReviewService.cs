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
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;

namespace TMDb.Repository.Common
{
    public interface IReviewRepository
    {
        Task<List<Review>> SelectMovieReviewsAsync(Guid movieID);
        Task<List<Review>> SelectMovieReviewsOrderedAsync(Guid movieID, string column, string order);
        Task<List<Review>> SelectUserReviewsOrderedAsync(Guid accountID, string column, string order);
        Task InsertReviewAsync(Review review, Guid accountID);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(Guid reviewID);
    }
}
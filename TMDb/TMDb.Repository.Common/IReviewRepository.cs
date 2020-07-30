using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Common;
using TMDb.Model;

namespace TMDb.Repository.Common
{
    public interface IReviewRepository
    {
        Task<List<Review>> SelectReviewsAsync(int pageNumberStart, int pageNumberEnd, string whereStatement, Sorting sort);
        Task<int> SelectNumberOfResultsAsync(string whereStatement);
        Task InsertReviewAsync(Review review, Guid accountID);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(Guid reviewID);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;

namespace TMDb.Repository.Common
{
    public interface IReviewRepository
    {
        Task<List<Review>> SelectMovieReviewsAsync(Guid movieID);
    }
}
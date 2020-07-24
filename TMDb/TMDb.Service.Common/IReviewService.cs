using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;

namespace TMDb.Service.Common
{
    public interface IReviewService
    {
        Task<List<Review>> ReturnMovieReviewsAsync(Guid movieID);
    }
}
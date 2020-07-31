using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;

namespace TMDb.Repository.Common
{
    public interface ICCMovieRepository
    {
        Task InsertAsync(CCMovie ccMovie);
        Task DeleteAsync(Guid castID, Guid movieID, string roleInMovie);
        Task<List<Movie>> SelectAsync(int pageStart, int pageEnd, Guid castID);
        Task<int> HowMany(Guid castID);
    }
}

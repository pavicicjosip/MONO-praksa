using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Common;


namespace TMDb.Service.Common
{
    public interface ICCMovieService
    {
        Task InsertAsync(CCMovie ccMovie);
        Task DeleteAsync(Guid castID, Guid movieID, string roleInMovie);
        Task<Tuple<int, List<Movie>>> SelectAsync(PagedResponse pagedResponse, Guid castID);
    }
}

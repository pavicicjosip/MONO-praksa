using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Common.CastAndCrew;
using TMDb.Common;

namespace TMDb.Service.Common
{
    public interface ICastAndCrewService
    {
        Task<Tuple<int, List<CastAndCrew>>> SelectAsync(PagedResponse pagedResponse ,ICastAndCrewFacade castAndCrewFacade);
        Task InsertAsync(CastAndCrew castAndCrew);
        Task UpdateAsync(Guid castID, CastAndCrew castAndCrew);
        Task DeleteAsync(Guid castID);
    }
}

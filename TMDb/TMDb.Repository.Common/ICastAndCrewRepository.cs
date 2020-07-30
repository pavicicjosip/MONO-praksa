using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Common.CastAndCrew;

namespace TMDb.Repository.Common
{
    public interface ICastAndCrewRepository
    {
        Task<List<CastAndCrew>> SelectAsync(int pageNumberStart, int pageNumberEnd, ICastAndCrewFacade castAndCrewFacade);

        Task<int> HowMany();
        Task InsertAsync(CastAndCrew castAndCrew);
        Task UpdateAsync(Guid castID, CastAndCrew castAndCrew);
        Task DeleteAsync(Guid castID);
    }
}

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
        Task<List<CastAndCrew>> SelectAsync(ICastAndCrewFacade castAndCrewFacade);
    }
}

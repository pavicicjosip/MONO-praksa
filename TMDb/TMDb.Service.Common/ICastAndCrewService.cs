using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Common.CastAndCrew;

namespace TMDb.Service.Common
{
    public interface ICastAndCrewService
    {
        Task<List<CastAndCrew>> SelectAsync(ICastAndCrewFacade castAndCrewFacade);
    }
}

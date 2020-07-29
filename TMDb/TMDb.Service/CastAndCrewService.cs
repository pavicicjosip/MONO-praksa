using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Service.Common;
using TMDb.Model;
using TMDb.Common.CastAndCrew;

namespace TMDb.Service
{
    class CastAndCrewService : ICastAndCrewService
    {
        protected ICastAndCrewRepository _ICastAndCrewRepository { get; set; }

        public CastAndCrewService(ICastAndCrewRepository _ICastAndCrewRepository)
        {
            this._ICastAndCrewRepository = _ICastAndCrewRepository;
        }
        public async Task<List<CastAndCrew>> SelectAsync(ICastAndCrewFacade castAndCrewFacade)
        {
            return await _ICastAndCrewRepository.SelectAsync(castAndCrewFacade);
        }
    }
}

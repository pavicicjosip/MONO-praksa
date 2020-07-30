using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Service.Common;
using TMDb.Model;
using TMDb.Common.CastAndCrew;
using TMDb.Common;

namespace TMDb.Service
{
    class CastAndCrewService : ICastAndCrewService
    {
        protected ICastAndCrewRepository _ICastAndCrewRepository { get; set; }

        public CastAndCrewService(ICastAndCrewRepository _ICastAndCrewRepository)
        {
            this._ICastAndCrewRepository = _ICastAndCrewRepository;
        }
        public async Task<Tuple<int, List<CastAndCrew>>> SelectAsync(PagedResponse pagedResponse, ICastAndCrewFacade castAndCrewFacade)
        {
            int pageNumberStart = (pagedResponse.PageNumber - 1) * pagedResponse.PageSize;
            int howMany = await _ICastAndCrewRepository.HowMany();

            List<CastAndCrew> list = await _ICastAndCrewRepository.SelectAsync(pageNumberStart, (pageNumberStart + pagedResponse.PageSize), castAndCrewFacade);

            Tuple<int, List<CastAndCrew>> tuple = new Tuple<int, List<CastAndCrew>>(howMany, list);

            return tuple;
        }

        public async Task InsertAsync(CastAndCrew castAndCrew)
        {
            await _ICastAndCrewRepository.InsertAsync(castAndCrew);
        }

        public async Task UpdateAsync(Guid castID, CastAndCrew castAndCrew)
        {
            await _ICastAndCrewRepository.UpdateAsync(castID, castAndCrew);
        }

        public async Task DeleteAsync(Guid castID)
        {
            await _ICastAndCrewRepository.DeleteAsync(castID);
        }
    }
}

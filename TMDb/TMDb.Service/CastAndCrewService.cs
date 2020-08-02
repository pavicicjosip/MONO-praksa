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
        protected ICastAndCrewRepository CastAndCrewRepository { get; set; }

        public CastAndCrewService(ICastAndCrewRepository castAndCrewRepository)
        {
            this.CastAndCrewRepository = castAndCrewRepository;
        }
        public async Task<Tuple<int, List<CastAndCrew>>> SelectAsync(PagedResponse pagedResponse, ICastAndCrewFacade castAndCrewFacade)
        {
            int pageNumberStart = (pagedResponse.PageNumber - 1) * pagedResponse.PageSize;
            int howMany = await CastAndCrewRepository.HowMany();

            List<CastAndCrew> list = await CastAndCrewRepository.SelectAsync(pageNumberStart, (pageNumberStart + pagedResponse.PageSize), castAndCrewFacade);

            Tuple<int, List<CastAndCrew>> tuple = new Tuple<int, List<CastAndCrew>>(howMany, list);

            return tuple;
        }

        public async Task InsertAsync(CastAndCrew castAndCrew)
        {
            await CastAndCrewRepository.InsertAsync(castAndCrew);
        }

        public async Task UpdateAsync(Guid castID, CastAndCrew castAndCrew)
        {
            await CastAndCrewRepository.UpdateAsync(castID, castAndCrew);
        }

        public async Task DeleteAsync(Guid castID)
        {
            await CastAndCrewRepository.DeleteAsync(castID);
        }
    }
}

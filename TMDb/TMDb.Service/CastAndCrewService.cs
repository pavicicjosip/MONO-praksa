using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Service.Common;
using TMDb.Model;

namespace TMDb.Service
{
    class CastAndCrewService : ICastAndCrewService
    {
        protected ICastAndCrewRepository _ICastAndCrewRepository { get; set; }

        public CastAndCrewService(ICastAndCrewRepository _ICastAndCrewRepository)
        {
            this._ICastAndCrewRepository = _ICastAndCrewRepository;
        }
        public async Task<List<CastAndCrew>> SelectByFirstNameAsync(string firstName)
        {
            return await _ICastAndCrewRepository.SelectByFirstNameAsync(firstName);
        }

        public async Task<List<CastAndCrew>> SelectByLastNameAsync(string lastName)
        {
            return await _ICastAndCrewRepository.SelectByLastNameAsync(lastName);
        }

        public async Task<List<CastAndCrew>> SelectByDateOfBirthAsync(string date)
        {
            return await _ICastAndCrewRepository.SelectByDateOfBirthAsync(date);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;

namespace TMDb.Repository.Common
{
    public interface ICastAndCrewRepository
    {
        Task<List<CastAndCrew>> SelectByFirstNameAsync(string firstName);
        Task<List<CastAndCrew>> SelectByLastNameAsync(string lastName);

        Task<List<CastAndCrew>> SelectByDateOfBirthAsync(string date);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.CastAndCrew
{
    public interface ICastAndCrewFacade
    {
        ICACFirstName FirstName { get; set; }
        ICACLastName LastName { get; set; }
        ICACDateOfBirth DateOfBirth { get; set; }
        ICACMovieID MovieID { get; set; }

        string SQLStatement();
    }
}

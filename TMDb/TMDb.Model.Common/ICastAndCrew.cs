using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Model.Common
{
    public interface ICastAndCrew
    {
        Guid CastID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime DateOfBirth { get; set; }
        string Gender { get; set; }
        Guid FileID { get; set; }
    }
}

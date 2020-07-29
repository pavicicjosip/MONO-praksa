using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.CastAndCrew
{
    public interface ICACFirstName
    {
        string FirstName { set; get; }
        bool Default();
        string WhereStatement();

    }
}

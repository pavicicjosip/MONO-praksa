using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.CastAndCrew
{
    public interface ICACLastName
    {
        string LastName { get; set; }
        bool Default();
        string WhereStatement();
    }
}

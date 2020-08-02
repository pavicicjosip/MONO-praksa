using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.CastAndCrew
{
    public interface ICACMovieID
    {
        Guid? MovieID { set; get; }

        bool Default();
        string WhereStatement();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.CastAndCrew
{
    public interface ICACDateOfBirth
    {
        string DateOfBirth { get; set; }
        bool Default();
        string WhereStatement();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.CastAndCrew
{
    public class CACSort : ICACSort
    {
        public CACSort() { }
        public string OrderBy()
        {
            return " ORDER BY RoleInMovie ASC, LastName ASC, FirstName ASC ";
        }
    }
}

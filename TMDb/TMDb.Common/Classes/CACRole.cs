using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.CastAndCrew
{
    public class CACRole : ICACRole
    {
        public string Role { get; set; }

        public CACRole() { }
        public CACRole(string roleInMovie)
        {
            this.Role = roleInMovie;
        }
        public bool Default()
        {
            return Role == default(String);
        }
        public string WhereStatement()
        {
            return String.Format(" ccm.RoleInMovie = '{0}' ", Role);
        }
    }
}

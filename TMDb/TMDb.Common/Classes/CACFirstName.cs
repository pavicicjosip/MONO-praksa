using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.CastAndCrew
{
    public class CACFirstName : ICACFirstName
    {
        public string FirstName { get; set; }

        public CACFirstName() { }
        public CACFirstName(string firstName)
        {
            this.FirstName = firstName;
        }
        public bool Default()
        {
            return FirstName == default(String);
        }
        public string WhereStatement()
        {
            return String.Format(" cac.FirstName = '{0}' ", FirstName);
        }
    }
}

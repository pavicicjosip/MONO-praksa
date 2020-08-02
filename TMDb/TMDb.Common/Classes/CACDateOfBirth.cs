using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.CastAndCrew
{
    public class CACDateOfBirth : ICACDateOfBirth
    {
        public string DateOfBirth { get; set; }

        public CACDateOfBirth() { }
        public CACDateOfBirth(string dateOfBirth)
        {
            this.DateOfBirth = dateOfBirth;
        }

        public bool Default()
        {
            return DateOfBirth == default(String);
        }
        public string WhereStatement()
        {
            return String.Format(" cac.DateOfBirth = '{0}' ", DateOfBirth);
        }
    }
}

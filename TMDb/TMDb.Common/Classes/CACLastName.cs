using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.CastAndCrew
{
    public class CACLastName : ICACLastName
    {
        public string LastName { get; set; }
        public CACLastName() { }
        public CACLastName(string lastName)
        {
            this.LastName = lastName;
        }
        public bool Default()
        {
            return LastName == default(String);
        }
        public string WhereStatement()
        {
            return String.Format(" cac.LastName = '{0}' ", LastName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common
{
    public class Sorting : ISorting
    {
        public string Column { get; set; }
        public bool Order { get; set; }

        public string OrderBy()
        {
            return Order ? Column + " ASC " : Column + " DESC ";
        }
    }
}

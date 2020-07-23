using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Model.Common
{
    public interface ICCMovie
    {
        Guid CastID { get; set; }
        Guid MovieID { get; set; }
    }
}

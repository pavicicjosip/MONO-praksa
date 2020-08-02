using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Model.Common
{
    public interface IAccountRole
    {
        Guid AccountID { get; set; }
        string Role { get; set; }
    }
}

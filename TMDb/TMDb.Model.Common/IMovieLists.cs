using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Model.Common
{
    public interface IMovieLists
    {
        string ListName { get; set; }
        Guid MovieID { get; set; }
        Guid AccountID { get; set; }
    }
}

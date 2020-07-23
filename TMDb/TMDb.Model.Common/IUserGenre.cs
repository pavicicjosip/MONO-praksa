using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Model.Common
{
    public interface IUserGenre
    {
        Guid AccountID { get; set; }
        Guid GenreID { get; set; }
    }
}

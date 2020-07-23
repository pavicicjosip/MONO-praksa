using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Model.Common
{
    public interface IGenreMovie
    {
        Guid MovieID { get; set; }
        Guid GenreID { get; set; }
    }
}

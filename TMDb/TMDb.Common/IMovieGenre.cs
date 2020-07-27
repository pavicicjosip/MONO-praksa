using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common
{
    public interface IMovieGenre
    {
        string Genre { get; set; }
        string WhereStatement();
        bool Default();
    }
}

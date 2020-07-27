using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common
{
    public interface IMovieTitle
    {
        string  Title { get; set; }
        bool Default();
        string WhereStatement();
    }
}

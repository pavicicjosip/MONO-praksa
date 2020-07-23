using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Model.Common
{
    public interface IGenre
    {
        Guid GenreID { get; set; }
        string Title { get; set; }
    }
}

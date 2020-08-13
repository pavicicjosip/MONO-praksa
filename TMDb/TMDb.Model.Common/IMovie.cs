using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Model.Common
{
    public interface IMovie
    {
        Guid MovieID { get; set; }
        string Title { get; set; }
        int YearOfProduction { get; set; }
        string CountryOfOrigin { get; set; }
        string Duration { get; set; }
        string PlotOutline { get; set; }
        Guid FileID { get; set; }
        double Rating { get; set; }
    }
}

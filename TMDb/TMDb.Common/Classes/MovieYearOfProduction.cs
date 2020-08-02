using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common
{
    public class MovieYearOfProduction : IMovieYearOfProduction
    {
        public string YearOfProduction { get; set; }

        public MovieYearOfProduction() { }

        public MovieYearOfProduction(string yearOfProduction)
        {
            this.YearOfProduction = yearOfProduction;
        }
        public bool Default()
        {
            return YearOfProduction == "default";
        }
        public string WhereStatement()
        {
            return " m.YearOfProduction = " + YearOfProduction + " ";
        }
    }
}

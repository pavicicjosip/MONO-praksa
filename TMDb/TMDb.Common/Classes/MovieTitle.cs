using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common
{
    class MovieTitle : IMovieTitle
    {
        public string Title { get; set; }

        public MovieTitle() { }

        public MovieTitle(string title)
        {
            this.Title = title;
        }

        public bool Default()
        {
            return Title == "default";
        }

        public string WhereStatement()
        {
            return " m.Title = " + "'" + Title + "'" + " ";
        }
    }
}

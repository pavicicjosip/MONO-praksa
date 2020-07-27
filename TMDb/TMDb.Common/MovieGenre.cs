using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common
{
    public class MovieGenre : IMovieGenre
    {
        public string Genre { get; set; }

        public MovieGenre() { }

        public MovieGenre(string genre)
        {
            this.Genre = genre;
        }

        public string WhereStatement()
        {
            return " g.Title = " + "'" + Genre + "'" + " AND gm.GenreID = g.GenreID AND m.MovieID = gm.MovieID ";
        }

        public bool Default()
        {
            return Genre == "default";    
        }
    }
}

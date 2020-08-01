using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.MovieLists
{
    public class MovieListsMovieID : IMovieListsMovieID
    {
        public Guid MovieID { get; set; }

        public MovieListsMovieID() { }
        public MovieListsMovieID(Guid movieID)
        {
            MovieID = movieID;
        }

        public string WhereStatement()
        {
            return String.Format(" MovieID = '{0}' ", MovieID);
        }
        public bool Default()
        {
            return MovieID == Guid.Empty;
        }
    }
}

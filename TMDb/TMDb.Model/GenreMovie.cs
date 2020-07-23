using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model.Common;


namespace TMDb.Model
{
    public class GenreMovie : IGenreMovie
    {
        public Guid MovieID { get; set; }
        public Guid GenreID { get; set; }

        public GenreMovie(Guid movieID, Guid genreID)
        {
            this.MovieID = movieID;
            this.GenreID = genreID;
        }
    }
}

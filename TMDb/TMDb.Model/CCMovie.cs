using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model.Common;


namespace TMDb.Model
{
    public class CCMovie : ICCMovie
    {
        public Guid CastID { get; set; }
        public Guid MovieID { get; set; }
        public string RoleInMovie { get; set; }

        public CCMovie(Guid castID, Guid movieID, string roleInMovie)
        {
            this.CastID = castID;
            this.MovieID = movieID;
            this.RoleInMovie = roleInMovie;
        }
    }
}

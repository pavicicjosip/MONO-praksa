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

        public CCMovie(Guid castID, Guid movieID)
        {
            this.CastID = castID;
            this.MovieID = movieID;
        }
    }
}

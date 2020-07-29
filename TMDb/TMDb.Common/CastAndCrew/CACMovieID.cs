using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace TMDb.Common.CastAndCrew
{
    public class CACMovieID : ICACMovieID
    {
        public Guid? MovieID { get; set; }
        public CACMovieID() { }
        public CACMovieID(Guid? movieID)
        {
            this.MovieID = movieID;
        }

        public bool Default()
        {
            return MovieID == null;
        }
        public string WhereStatement()
        {
            return String.Format(" ccm.MovieID = '{0}' ", MovieID);
        }
    }
}

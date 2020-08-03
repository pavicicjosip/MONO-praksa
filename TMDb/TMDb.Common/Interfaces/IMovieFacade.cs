using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common
{
    public interface IMovieFacade
    {
        IMovieGenre MovieGenre { get; set; }
        IMovieYearOfProduction MovieYearOfProduction { get; set; }
        IMovieTitle MovieTitle { get; set; }
        IMovieAccountReview MovieAccountReview { get; set; }
        bool GroupByBool { get; set; }
        string WhereStatement();
        bool AccountReviewNull();
        bool GenreNull();
        string GroupBy();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common
{
    public interface IMovieFacade
    {
        IMovieGenre movieGenre { get; set; }
        IMovieYearOfProduction movieYearOfProduction { get; set; }
        IMovieTitle movieTitle { get; set; }
        IMovieAccountReview movieAccountReview { get; set; }
        bool GroupByBool { get; set; }
        string WhereStatement();
        bool AccountReviewNull();
        bool GenreNull();
        string GroupBy();
    }
}

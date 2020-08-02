using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common
{
    public class MovieFacade : IMovieFacade
    {
        public IMovieGenre movieGenre { get; set; }
        public IMovieYearOfProduction movieYearOfProduction { get; set; }
        public IMovieTitle movieTitle { get; set; }
        public IMovieAccountReview movieAccountReview { get; set; }
        public bool GroupByBool { get; set; }

        public MovieFacade(IMovieGenre movieGenre, IMovieYearOfProduction movieYearOfProduction, IMovieTitle movieTitle, IMovieAccountReview movieAccountReview)
        {
            this.movieGenre = movieGenre;
            this.movieYearOfProduction = movieYearOfProduction;
            this.movieTitle = movieTitle;
            this.movieAccountReview = movieAccountReview;
        }

        public string WhereStatement()
        {
            bool genreBool = movieGenre.Default();
            bool yearOfProductionBool = movieYearOfProduction.Default();
            bool titleBool = movieTitle.Default();
            bool accountReview = movieAccountReview.Default();

            string _out = "";

            switch (genreBool)
            {
                case true when titleBool && yearOfProductionBool && accountReview:
                    _out = "";
                    break;
                case true when titleBool && !yearOfProductionBool && accountReview:
                    _out = "WHERE " + movieYearOfProduction.WhereStatement();
                    break;
                case true when !titleBool && !yearOfProductionBool && accountReview:
                    _out = "WHERE " + movieYearOfProduction.WhereStatement() + " AND " + movieTitle.WhereStatement();
                    break;
                case true when !titleBool && yearOfProductionBool && accountReview:
                    _out = "WHERE " + movieTitle.WhereStatement();
                    break;
                case false when titleBool && yearOfProductionBool && accountReview:
                    _out = "WHERE " + movieGenre.WhereStatement();
                    break;
                case false when titleBool && !yearOfProductionBool && accountReview:
                    _out = "WHERE " + movieGenre.WhereStatement() + " AND " + movieYearOfProduction.WhereStatement();
                    break;
                case false when !titleBool && yearOfProductionBool && accountReview:
                    _out = "WHERE " + movieGenre.WhereStatement() + " AND " + movieTitle.WhereStatement();
                    break;
                case false when !titleBool && !yearOfProductionBool && accountReview:
                    _out = "WHERE " + movieGenre.WhereStatement() + " AND " + movieTitle.WhereStatement() + " AND " + movieYearOfProduction.WhereStatement();
                    break;
                case true when titleBool && yearOfProductionBool && !accountReview:
                    _out = "WHERE " + movieAccountReview.WhereStatement();
                    break;
                case true when titleBool && !yearOfProductionBool && !accountReview:
                    _out = "WHERE " + movieYearOfProduction.WhereStatement() + " AND " + movieAccountReview.WhereStatement();
                    break;
                case true when !titleBool && !yearOfProductionBool && !accountReview:
                    _out = "WHERE " + movieYearOfProduction.WhereStatement() + " AND " + movieTitle.WhereStatement() + " AND " + movieAccountReview.WhereStatement();
                    break;
                case true when !titleBool && yearOfProductionBool && !accountReview:
                    _out = "WHERE " + movieTitle.WhereStatement() + " AND " + movieAccountReview.WhereStatement();
                    break;
                case false when titleBool && yearOfProductionBool && !accountReview:
                    _out = "WHERE " + movieGenre.WhereStatement() + " AND " + movieAccountReview.WhereStatement();
                    break;
                case false when titleBool && !yearOfProductionBool && !accountReview:
                    _out = "WHERE " + movieGenre.WhereStatement() + " AND " + movieYearOfProduction.WhereStatement() + " AND " + movieAccountReview.WhereStatement();
                    break;
                case false when !titleBool && yearOfProductionBool && !accountReview:
                    _out = "WHERE " + movieGenre.WhereStatement() + " AND " + movieTitle.WhereStatement() + " AND " + movieAccountReview.WhereStatement();
                    break;
                case false when !titleBool && !yearOfProductionBool && !accountReview:
                    _out = "WHERE " + movieGenre.WhereStatement() + " AND " + movieTitle.WhereStatement() + " AND " + movieYearOfProduction.WhereStatement() + " AND " + movieAccountReview.WhereStatement();
                    break;
                default:
                    break;
            }

            return _out;
        }
        public bool AccountReviewNull()
        {
            return movieAccountReview.Default();
        }

        public bool GenreNull()
        {
            return movieGenre.Default();
        }

        public string GroupBy()
        {
            if (AccountReviewNull() && !GroupByBool)
            {
                return "";
            }
            return " GROUP BY m.MovieID, m.Title, m.YearOfProduction, m.CountryOfOrigin, m.Duration, m.PlotOutline, m.FileID ";
        }
    }
}

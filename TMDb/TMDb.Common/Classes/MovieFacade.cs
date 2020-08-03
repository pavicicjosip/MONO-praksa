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
        public IMovieGenre MovieGenre { get; set; }
        public IMovieYearOfProduction MovieYearOfProduction { get; set; }
        public IMovieTitle MovieTitle { get; set; }
        public IMovieAccountReview MovieAccountReview { get; set; }
        public bool GroupByBool { get; set; }

        public MovieFacade(IMovieGenre movieGenre, IMovieYearOfProduction movieYearOfProduction, IMovieTitle movieTitle, IMovieAccountReview movieAccountReview)
        {
            this.MovieGenre = movieGenre;
            this.MovieYearOfProduction = movieYearOfProduction;
            this.MovieTitle = movieTitle;
            this.MovieAccountReview = movieAccountReview;
        }

        public string WhereStatement()
        {
            bool genreBool = MovieGenre.Default();
            bool yearOfProductionBool = MovieYearOfProduction.Default();
            bool titleBool = MovieTitle.Default();
            bool accountReview = MovieAccountReview.Default();

            string _out = "";

            switch (genreBool)
            {
                case true when titleBool && yearOfProductionBool && accountReview:
                    _out = "";
                    break;
                case true when titleBool && !yearOfProductionBool && accountReview:
                    _out = "WHERE " + MovieYearOfProduction.WhereStatement();
                    break;
                case true when !titleBool && !yearOfProductionBool && accountReview:
                    _out = "WHERE " + MovieYearOfProduction.WhereStatement() + " AND " + MovieTitle.WhereStatement();
                    break;
                case true when !titleBool && yearOfProductionBool && accountReview:
                    _out = "WHERE " + MovieTitle.WhereStatement();
                    break;
                case false when titleBool && yearOfProductionBool && accountReview:
                    _out = "WHERE " + MovieGenre.WhereStatement();
                    break;
                case false when titleBool && !yearOfProductionBool && accountReview:
                    _out = "WHERE " + MovieGenre.WhereStatement() + " AND " + MovieYearOfProduction.WhereStatement();
                    break;
                case false when !titleBool && yearOfProductionBool && accountReview:
                    _out = "WHERE " + MovieGenre.WhereStatement() + " AND " + MovieTitle.WhereStatement();
                    break;
                case false when !titleBool && !yearOfProductionBool && accountReview:
                    _out = "WHERE " + MovieGenre.WhereStatement() + " AND " + MovieTitle.WhereStatement() + " AND " + MovieYearOfProduction.WhereStatement();
                    break;
                case true when titleBool && yearOfProductionBool && !accountReview:
                    _out = "WHERE " + MovieAccountReview.WhereStatement();
                    break;
                case true when titleBool && !yearOfProductionBool && !accountReview:
                    _out = "WHERE " + MovieYearOfProduction.WhereStatement() + " AND " + MovieAccountReview.WhereStatement();
                    break;
                case true when !titleBool && !yearOfProductionBool && !accountReview:
                    _out = "WHERE " + MovieYearOfProduction.WhereStatement() + " AND " + MovieTitle.WhereStatement() + " AND " + MovieAccountReview.WhereStatement();
                    break;
                case true when !titleBool && yearOfProductionBool && !accountReview:
                    _out = "WHERE " + MovieTitle.WhereStatement() + " AND " + MovieAccountReview.WhereStatement();
                    break;
                case false when titleBool && yearOfProductionBool && !accountReview:
                    _out = "WHERE " + MovieGenre.WhereStatement() + " AND " + MovieAccountReview.WhereStatement();
                    break;
                case false when titleBool && !yearOfProductionBool && !accountReview:
                    _out = "WHERE " + MovieGenre.WhereStatement() + " AND " + MovieYearOfProduction.WhereStatement() + " AND " + MovieAccountReview.WhereStatement();
                    break;
                case false when !titleBool && yearOfProductionBool && !accountReview:
                    _out = "WHERE " + MovieGenre.WhereStatement() + " AND " + MovieTitle.WhereStatement() + " AND " + MovieAccountReview.WhereStatement();
                    break;
                case false when !titleBool && !yearOfProductionBool && !accountReview:
                    _out = "WHERE " + MovieGenre.WhereStatement() + " AND " + MovieTitle.WhereStatement() + " AND " + MovieYearOfProduction.WhereStatement() + " AND " + MovieAccountReview.WhereStatement();
                    break;
                default:
                    break;
            }

            return _out;
        }
        public bool AccountReviewNull()
        {
            return MovieAccountReview.Default();
        }

        public bool GenreNull()
        {
            return MovieGenre.Default();
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

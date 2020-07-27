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
        public IMovieGenre movieGenre { get;  set; }
        public IMovieYearOfProduction movieYearOfProduction { get;  set; }
        public IMovieTitle movieTitle { get;  set; }


        public MovieFacade(IMovieGenre movieGenre, IMovieYearOfProduction movieYearOfProduction, IMovieTitle movieTitle)
        {
            this.movieGenre = movieGenre;
            this.movieYearOfProduction = movieYearOfProduction;
            this.movieTitle = movieTitle;
        }

        public string WhereStatement()
        {
            bool genreBool = movieGenre.Default();
            bool yearOfProductionBool = movieYearOfProduction.Default();
            bool titleBool = movieTitle.Default();

            string _out = "";

            switch (genreBool)
            {
                case true when titleBool && yearOfProductionBool:
                    _out = "";
                    break;
                case true when titleBool && !yearOfProductionBool:
                    _out = "WHERE " + movieYearOfProduction.WhereStatement();
                    break;
                case true when !titleBool && !yearOfProductionBool:
                    _out = "WHERE " + movieYearOfProduction.WhereStatement() + " AND " + movieTitle.WhereStatement();
                    break;
                case true when !titleBool && yearOfProductionBool:
                    _out = "WHERE "  + movieTitle.WhereStatement();
                    break;
                case false when titleBool && yearOfProductionBool:
                    _out = "€" + "WHERE " + movieGenre.WhereStatement();
                    break;
                case false when titleBool && !yearOfProductionBool:
                    _out = "€" + "WHERE " + movieGenre.WhereStatement() + " AND " + movieYearOfProduction.WhereStatement();
                    break;
                case false when !titleBool && yearOfProductionBool:
                    _out = "€" + "WHERE " + movieGenre.WhereStatement() + " AND " + movieTitle.WhereStatement();
                    break;
                case false when !titleBool && !yearOfProductionBool:
                    _out = "€" + "WHERE " + movieGenre.WhereStatement() + " AND " + movieTitle.WhereStatement() + " AND " + movieYearOfProduction.WhereStatement();
                    break;
                default:
                    break; 
            }

            return _out;
        }
    }
}

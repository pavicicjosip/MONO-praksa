using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.MovieLists
{
    public class MovieListsFacade : IMovieListsFacade
    {
        public IMovieListsAccountID MovieListsAccountID { get; set; }
        public IMovieListsMovieID MovieListsMovieID { get; set; }
        public IMovieListsListName MovieListsListName { get; set; }
        public MovieListsFacade(IMovieListsAccountID movieListsAccountID, IMovieListsMovieID movieListsMovieID, IMovieListsListName movieListsListName)
        {
            this.MovieListsAccountID = movieListsAccountID;
            this.MovieListsMovieID = movieListsMovieID;
            this.MovieListsListName = movieListsListName;
        }
        public string WhereStatement()
        {
            bool accountBool = MovieListsAccountID.Default();
            bool movieBool = MovieListsMovieID.Default();
            bool listNameBool = MovieListsListName.Default();

            string _out = "";

            switch (movieBool)
            {
                case true when accountBool && listNameBool:
                    break;
                case true when !accountBool && listNameBool:
                    _out += MovieListsAccountID.WhereStatement();
                    break;
                case false when accountBool && listNameBool:
                    _out += MovieListsMovieID.WhereStatement();
                    break;
                case false when !accountBool && listNameBool:
                    _out += MovieListsMovieID.WhereStatement() + " AND " + MovieListsAccountID.WhereStatement();
                    break;
                case true when accountBool && !listNameBool:
                    _out += MovieListsListName.WhereStatement();
                    break;
                case true when !accountBool && !listNameBool:
                    _out += MovieListsListName.WhereStatement() + " AND " + MovieListsAccountID.WhereStatement();
                    break;
                case false when accountBool && !listNameBool:
                    _out += MovieListsListName.WhereStatement() + " AND " + MovieListsMovieID.WhereStatement();
                    break;
                case false when !accountBool && !listNameBool:
                    _out += MovieListsListName.WhereStatement() + " AND " + MovieListsAccountID.WhereStatement() + " AND " + MovieListsMovieID.WhereStatement();
                    break;
            }

            return _out;
        }
    }
}

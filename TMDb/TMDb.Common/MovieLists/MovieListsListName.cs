using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.MovieLists
{
    public class MovieListsListName : IMovieListsListName
    {
        public string ListName { get; set; }

        public MovieListsListName() { }
        public MovieListsListName(string listName)
        {
            ListName = listName;
        }

        public string WhereStatement()
        {
            return String.Format(" ListName = '{0}' ", ListName);
        }
        public bool Default()
        {
            return ListName == "default";
        }
    }
}

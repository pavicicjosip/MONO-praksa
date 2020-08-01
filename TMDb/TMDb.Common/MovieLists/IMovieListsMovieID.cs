using System;

namespace TMDb.Common.MovieLists
{
    public interface IMovieListsMovieID
    {
        Guid MovieID { get; set; }

        bool Default();
        string WhereStatement();
    }
}
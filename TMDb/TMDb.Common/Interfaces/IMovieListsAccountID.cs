using System;

namespace TMDb.Common.MovieLists
{
    public interface IMovieListsAccountID
    {
        Guid AccountID { get; set; }

        bool Default();
        string WhereStatement();
    }
}
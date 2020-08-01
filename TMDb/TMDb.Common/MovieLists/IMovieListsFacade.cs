namespace TMDb.Common.MovieLists
{
    public interface IMovieListsFacade
    {
        IMovieListsAccountID MovieListsAccountID { get; set; }
        IMovieListsListName MovieListsListName { get; set; }
        IMovieListsMovieID MovieListsMovieID { get; set; }
        string WhereStatement();

    }
}
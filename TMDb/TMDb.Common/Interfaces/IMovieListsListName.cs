namespace TMDb.Common.MovieLists
{
    public interface IMovieListsListName
    {
        string ListName { get; set; }

        bool Default();
        string WhereStatement();
    }
}
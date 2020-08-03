namespace TMDb.Common.Review
{
    public interface IReviewFacade
    {
        IReviewAccountID ReviewAccountID { get; set; }
        IReviewMovieID ReviewMovieID { get; set; }

        string WhereStatement();
    }
}
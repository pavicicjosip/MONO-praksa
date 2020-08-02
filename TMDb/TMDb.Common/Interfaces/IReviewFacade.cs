namespace TMDb.Common.Review
{
    public interface IReviewFacade
    {
        IReviewAccountID reviewAccountID { get; set; }
        IReviewMovieID reviewMovieID { get; set; }

        string WhereStatement();
    }
}
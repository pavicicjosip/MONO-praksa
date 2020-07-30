using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMDb.Model;
using TMDb.Service.Common;
using AutoMapper;
using System.Threading.Tasks;
using TMDb.Common;
using TMDb.Common.Review;

namespace TMDb.WebAPI.Controllers
{
    public class ReviewController : ApiController
    {
        protected IReviewService reviewService
        { get; private set; }
        protected IReviewFacade reviewFacade { get; private set; }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<Review, RestReview>().ReverseMap());

        public ReviewController(IReviewService reviewService, IReviewFacade reviewFacade)
        {
            this.reviewService = reviewService;
            this.reviewFacade = reviewFacade;
        }

        [HttpGet]
        [Route("api/Review")]
        public async Task<HttpResponseMessage> GetReviewsAsync(Guid? movieID = null, Guid? accountID = null, string column = "default", bool order = true,int pageNumber = 1, int pageSize = 10)
        {
            var mapper = Mapper.CreateMapper();

            PagedResponse pagedResponse = new PagedResponse { PageNumber = pageNumber, PageSize = pageSize };
            Sorting sort = new Sorting { Column = column, Order = order };
            if (accountID.HasValue)
            {
                reviewFacade.reviewAccountID.AccountID = accountID.Value;
            }
            else
            {
                reviewFacade.reviewAccountID.AccountID = Guid.Empty;
            }

            if (movieID.HasValue)
            {
                reviewFacade.reviewMovieID.MovieID = movieID.Value;
            }
            else
            {
                reviewFacade.reviewMovieID.MovieID = Guid.Empty;
            }

            var reviewTuple = await reviewService.SelectReviewsAsync(pagedResponse, reviewFacade, sort);

            List<RestReview> restReviewList = mapper.Map<List<RestReview>>(reviewTuple.Item2);
            var restReviewTuple = new Tuple<int, List<RestReview>>(reviewTuple.Item1, restReviewList);
            return Request.CreateResponse(HttpStatusCode.OK, restReviewTuple);
        }

        [HttpPost]
        [Route("api/Review/{MovieID}/{AccountID}")]
        public async Task<HttpResponseMessage> PostReviewAsync(Guid movieID, Guid accountID, RestReview restReview)
        {
            var mapper = Mapper.CreateMapper();
            Review review = mapper.Map<Review>(restReview);
            review.MovieID = movieID;
            await reviewService.CreateReviewAsync(review, accountID);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/Review/{ReviewID}")]
        public async Task<HttpResponseMessage> PutReviewAsync(Guid reviewID, RestReview restReview)
        {
            var mapper = Mapper.CreateMapper();
            Review review = mapper.Map<Review>(restReview);
            review.ReviewID = reviewID;
            await reviewService.UpdateReviewAsync(review);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/Review/{ReviewID}")]
        public async Task<HttpResponseMessage> DeleteReviewAsync(Guid reviewID)
        {
            await reviewService.RemoveReviewAsync(reviewID);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

    public class RestReview
    {
        public Guid ReviewID
        { get; set; }

        public int NumberOfStars
        { get; set; }

        public string Comment
        { get; set; }

        public DateTime DateAndTime
        { get; set; }

        public string Username
        { get; set; }
    }
}

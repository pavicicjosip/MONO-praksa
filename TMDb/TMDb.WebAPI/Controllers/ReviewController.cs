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
using System.Security.Claims;

namespace TMDb.WebAPI.Controllers
{
    public class ReviewController : ApiController
    {
        protected IReviewService ReviewService
        { get; private set; }
        protected IReviewFacade ReviewFacade { get; private set; }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<Review, RestReview>().ReverseMap());

        public ReviewController(IReviewService reviewService, IReviewFacade reviewFacade)
        {
            this.ReviewService = reviewService;
            this.ReviewFacade = reviewFacade;
        }

        [HttpGet]
        [Route("api/Review")]
        public async Task<HttpResponseMessage> GetReviewsAsync(Guid? movieID = null, bool account = false, string column = "default", bool order = true,int pageNumber = 1, int pageSize = 10)
        {
            var mapper = Mapper.CreateMapper();

            PagedResponse pagedResponse = new PagedResponse { PageNumber = pageNumber, PageSize = pageSize };
            Sorting sort = new Sorting { Column = column, Order = order };
            if (account && User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                var claims = identity.Claims;
                ReviewFacade.ReviewAccountID.AccountID = Guid.Parse(claims.Where(p => p.Type == "guid").FirstOrDefault()?.Value);
            }
            else
            {
                ReviewFacade.ReviewAccountID.AccountID = Guid.Empty;
            }

            if (movieID.HasValue)
            {
                ReviewFacade.ReviewMovieID.MovieID = movieID.Value;
            }
            else
            {
                ReviewFacade.ReviewMovieID.MovieID = Guid.Empty;
            }

            var reviewTuple = await ReviewService.SelectReviewsAsync(pagedResponse, ReviewFacade, sort);

            List<RestReview> restReviewList = mapper.Map<List<RestReview>>(reviewTuple.Item2);
            var restReviewTuple = new Tuple<int, List<RestReview>>(reviewTuple.Item1, restReviewList);
            return Request.CreateResponse(HttpStatusCode.OK, restReviewTuple);
        }

        [HttpPost]
        [Authorize]
        [Route("api/Review/{MovieID}")]
        public async Task<HttpResponseMessage> PostReviewAsync(Guid movieID, RestReview restReview)
        {
            var mapper = Mapper.CreateMapper();

            var identity = User.Identity as ClaimsIdentity;
            var claims = identity.Claims;

            Review review = mapper.Map<Review>(restReview);
            review.MovieID = movieID;
            var accountID = Guid.Parse(claims.Where(p => p.Type == "guid").FirstOrDefault()?.Value);
            await ReviewService.CreateReviewAsync(review, accountID);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [Authorize]
        [Route("api/Review/{ReviewID}")]
        public async Task<HttpResponseMessage> PutReviewAsync(Guid reviewID, RestReview restReview)
        {
            var mapper = Mapper.CreateMapper();
            Review review = mapper.Map<Review>(restReview);
            review.ReviewID = reviewID;
            await ReviewService.UpdateReviewAsync(review);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Authorize]
        [Route("api/Review/{ReviewID}")]
        public async Task<HttpResponseMessage> DeleteReviewAsync(Guid reviewID)
        {
            await ReviewService.RemoveReviewAsync(reviewID);
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

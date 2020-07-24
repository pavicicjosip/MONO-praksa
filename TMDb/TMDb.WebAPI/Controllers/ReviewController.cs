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

namespace TMDb.WebAPI.Controllers
{
    public class ReviewController : ApiController
    {
        protected IReviewService reviewService
        { get; private set; }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<Review, RestReview>());

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        [Route("api/Review/Movie/{MovieID}")]
        public async Task<HttpResponseMessage> GetMovieReviewsAsync(Guid movieID)
        {
            var mapper = Mapper.CreateMapper();
            List<RestReview> restReviewList = mapper.Map<List<RestReview>>(await reviewService.ReturnMovieReviewsAsync(movieID));
            return Request.CreateResponse(HttpStatusCode.OK, restReviewList);
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

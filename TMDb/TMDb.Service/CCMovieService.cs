using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Service.Common;
using TMDb.Repository.Common;
using TMDb.Common;

namespace TMDb.Service
{
    public class CCMovieService : ICCMovieService
    {
        protected ICCMovieRepository ccMovieRepository
        { get; private set; }

        public CCMovieService(ICCMovieRepository ccMovieRepository)
        {
            this.ccMovieRepository = ccMovieRepository;
        }
        public async Task InsertAsync(CCMovie ccMovie)
        {
            await ccMovieRepository.InsertAsync(ccMovie);
        }
        public async Task DeleteAsync(Guid castID, Guid movieID, string roleInMovie)
        {
            await ccMovieRepository.DeleteAsync(castID, movieID, roleInMovie);
        }
        public async Task<Tuple<int, List<Movie>>> SelectAsync(PagedResponse pagedResponse, Guid castID)
        {
            int pageNumberStart = (pagedResponse.PageNumber - 1) * pagedResponse.PageSize;
            int howMany = await ccMovieRepository.HowMany(castID);

            List<Movie> list = await ccMovieRepository.SelectAsync(pageNumberStart, (pageNumberStart + pagedResponse.PageSize), castID);

            Tuple<int, List<Movie>> tuple = new Tuple<int, List<Movie>>(howMany, list);

            return tuple;
        }
    }
}

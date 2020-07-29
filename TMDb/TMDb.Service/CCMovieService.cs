using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Service.Common;
using TMDb.Repository.Common;

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
        public async Task InsertCCMovieAsync(CCMovie ccMovie)
        {
            await ccMovieRepository.InsertCCMovieAsync(ccMovie);
        }
    }
}

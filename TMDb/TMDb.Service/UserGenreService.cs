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
    public class UserGenreService : IUserGenreService
    {
        protected IUserGenreRepository userGenreRepository
        { get; private set; }

        public UserGenreService(IUserGenreRepository userGenreRepository)
        {
            this.userGenreRepository = userGenreRepository;
        }
        public async Task InsertUserGenreAsync(UserGenre userGenre)
        {
            await userGenreRepository.InsertUserGenreAsync(userGenre);
        }
        public async Task RemoveUserGenreAsync(Guid accountID, Guid genreID)
        {
            await userGenreRepository.RemoveUserGenreAsync(accountID, genreID);
        }
        public async Task<List<Genre>> SelectFavouriteGenreAsync(Guid accountID)
        {
            return await userGenreRepository.SelectFavouriteGenreAsync(accountID);
        }
        public async Task<List<Movie>> SelectMoviesFromGenreAsync(PagedResponse pagedResponse, Guid accountID)
        {
            int pageNumberStart = (pagedResponse.PageNumber - 1) * pagedResponse.PageSize;
            int pageNumberEnd = pageNumberStart + pagedResponse.PageSize;
            int maxNumberOfResults = 50;
            
            if(pageNumberEnd > maxNumberOfResults)
            {
                pageNumberEnd = maxNumberOfResults;
            }

            if(pageNumberStart > maxNumberOfResults)
            {
                pageNumberStart = maxNumberOfResults;
            }
            List<Movie> list = await userGenreRepository.SelectMoviesFromGenreAsync(pageNumberStart, pageNumberEnd, accountID);
            return list;
            
        }

    }
}

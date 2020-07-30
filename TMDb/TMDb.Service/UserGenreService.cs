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
            int limit = pageNumberStart + pagedResponse.PageSize;
            int maxNumberOfResults = 50;
            
            if(limit > maxNumberOfResults)
            {
                limit = maxNumberOfResults;
            }

            if(pageNumberStart > 50)
            {
                pageNumberStart = maxNumberOfResults;
            }
            return await userGenreRepository.SelectMoviesFromGenreAsync(pagedResponse, accountID);
        }

    }
}

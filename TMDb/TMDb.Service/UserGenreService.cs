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
        protected IUserGenreRepository UserGenreRepository
        { get; private set; }

        public UserGenreService(IUserGenreRepository userGenreRepository)
        {
            this.UserGenreRepository = userGenreRepository;
        }
        public async Task InsertUserGenreAsync(UserGenre userGenre)
        {
            await UserGenreRepository.InsertUserGenreAsync(userGenre);
        }
        public async Task RemoveUserGenreAsync(Guid accountID, Guid genreID)
        {
            await UserGenreRepository.RemoveUserGenreAsync(accountID, genreID);
        }

        public async Task<List<Genre>> SelectFavouriteGenreAsync(Guid accountID)
        {
            return await UserGenreRepository.SelectFavouriteGenreAsync(accountID);
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
            List<Movie> list = await UserGenreRepository.SelectMoviesFromGenreAsync(pageNumberStart, pageNumberEnd, accountID);
            return list;
            
        }

    }
}

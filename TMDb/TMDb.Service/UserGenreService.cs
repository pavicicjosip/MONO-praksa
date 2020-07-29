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
    }
}

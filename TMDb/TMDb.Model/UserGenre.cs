using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model.Common;

namespace TMDb.Model
{
    public class UserGenre : IUserGenre
    {
        public Guid AccountID { get; set; }
        public Guid GenreID { get; set; }

        public UserGenre(Guid accountID, Guid genreID)
        {
            this.AccountID = accountID;
            this.GenreID = genreID;
        }
    }
}

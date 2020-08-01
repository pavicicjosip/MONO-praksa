using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model.Common;


namespace TMDb.Model
{
    public class MovieLists : IMovieLists
    {
        public string ListName { get; set; }
        public Guid MovieID { get; set; }
        public Guid AccountID { get; set; }

        public MovieLists(string listName, Guid accountID, Guid movieID)
        {
            this.ListName = listName;
            this.AccountID = accountID;
            this.MovieID = movieID;
        }
    }
}

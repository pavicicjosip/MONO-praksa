using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model.Common;


namespace TMDb.Model
{
    public class Genre : IGenre
    {        
        public string Title { get; set; }
        public Guid GenreID { get; set; }


        public Genre(Guid genreID, string title)
        {
            this.GenreID = genreID;
            this.Title = title;
        }
    }
}

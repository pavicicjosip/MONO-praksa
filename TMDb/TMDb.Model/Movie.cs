using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model.Common;

namespace TMDb.Model
{
    public class Movie : IMovie
    {
        public Guid MovieID { get; set; }
        public string Title { get; set; }
        public int YearOfProduction { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Duration { get; set; }
        public string PlotOutline { get; set; }
        public Guid FileID { get; set; }
        public Movie(Guid movieID, string title, int yearOfProduction, string countryOfOrigin, string duration, string plotOutline, Guid fileID)
        {
            this.MovieID = movieID;
            this.Title = title;
            this.YearOfProduction = yearOfProduction;
            this.CountryOfOrigin = countryOfOrigin;
            this.Duration = duration;
            this.PlotOutline = plotOutline;
            this.FileID = fileID;
        }
    }
}

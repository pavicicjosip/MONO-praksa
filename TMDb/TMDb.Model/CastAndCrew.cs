using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model.Common;


namespace TMDb.Model
{
    public class CastAndCrew : ICastAndCrew
    {
        public Guid CastID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public Guid FileID { get; set; }

        public CastAndCrew(Guid castID, string firstName, string lastName, DateTime dateOfBirth, string gender, Guid fileID)
        {
            this.CastID = castID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.FileID = fileID;
        }
    }
}

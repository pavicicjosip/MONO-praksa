using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Common.CastAndCrew
{
    public class CastAndCrewFacade : ICastAndCrewFacade
    {
        public ICACFirstName FirstName { get; set; }
        public ICACLastName LastName { get; set; }
        public ICACDateOfBirth DateOfBirth { get; set; }
        public ICACMovieID MovieID { get; set; }

        public CastAndCrewFacade(ICACFirstName firstName, ICACLastName lastName, ICACDateOfBirth dateOfBirth, ICACMovieID movieID)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.MovieID = movieID;
        }

        public string SQLStatement()
        {
            bool bFirstName = FirstName.Default();
            bool bLastName = LastName.Default();
            bool bDateOfBirth = DateOfBirth.Default();
            bool bMovieID = MovieID.Default();

            string join = " SELECT cac.CastID, cac.FirstName, cac.LastName, cac.DateOfBirth, cac.Gender, cac.FileID FROM CastAndCrew cac, CCMovie ccm WHERE cac.CastID = ccm.CastID ";
            string notJoin = " SELECT * FROM CastAndCrew cac ";
            string _out = "";

            switch (bMovieID)
            {
                case true when bFirstName && bLastName && bDateOfBirth:
                    _out = notJoin;
                    break;
                case true when bFirstName && bLastName && !bDateOfBirth:
                    _out = notJoin +  " WHERE " + DateOfBirth.WhereStatement();
                    break;
                case true when bFirstName && !bLastName && bDateOfBirth:
                    _out = notJoin + " WHERE " + LastName.WhereStatement();
                    break;
                case true when bFirstName && !bLastName && !bDateOfBirth:
                    _out = notJoin + " WHERE " + LastName.WhereStatement() + " AND " + DateOfBirth.WhereStatement();
                    break;
                case true when !bFirstName && bLastName && bDateOfBirth:
                    _out = notJoin + " WHERE " + FirstName.WhereStatement();
                    break;
                case true when !bFirstName && bLastName && !bDateOfBirth:
                    _out = notJoin + " WHERE " + FirstName.WhereStatement() + " AND " + DateOfBirth.WhereStatement();
                    break;
                case true when !bFirstName && !bLastName && bDateOfBirth:
                    _out = notJoin + " WHERE " + FirstName.WhereStatement() + " AND " + LastName.WhereStatement();
                    break;
                case true when !bFirstName && !bLastName && !bDateOfBirth:
                    _out = notJoin + " WHERE " + FirstName.WhereStatement() + " AND " + LastName.WhereStatement() + " AND " + DateOfBirth.WhereStatement();
                    break;
                case false when bFirstName && bLastName && bDateOfBirth:
                    _out = join + " AND " + MovieID.WhereStatement();
                    break;
                case false when bFirstName && bLastName && !bDateOfBirth:
                    _out = join + " AND " + MovieID.WhereStatement() + " AND " + DateOfBirth.WhereStatement();
                    break;
                case false when bFirstName && !bLastName && bDateOfBirth:
                    _out = join + " AND " + MovieID.WhereStatement() + " AND " + LastName.WhereStatement();
                    break;
                case false when bFirstName && !bLastName && !bDateOfBirth:
                    _out = join + " AND " + MovieID.WhereStatement() + " AND " + LastName.WhereStatement() + " AND " + DateOfBirth.WhereStatement();
                    break;
                case false when !bFirstName && bLastName && bDateOfBirth:
                    _out = join + " AND " + MovieID.WhereStatement() + " AND " + FirstName.WhereStatement();
                    break;
                case false when !bFirstName && bLastName && !bDateOfBirth:
                    _out = join + " AND " + MovieID.WhereStatement() + " AND " + FirstName.WhereStatement() + " AND " + DateOfBirth.WhereStatement();
                    break;
                case false when !bFirstName && !bLastName && bDateOfBirth:
                    _out = join + " AND " + MovieID.WhereStatement() + " AND " + FirstName.WhereStatement() + " AND " + LastName.WhereStatement();
                    break;
                case false when !bFirstName && !bLastName && !bDateOfBirth:
                    _out = join + " AND " + MovieID.WhereStatement() + " AND " + FirstName.WhereStatement() + " AND " + LastName.WhereStatement() + " AND " + DateOfBirth.WhereStatement();
                    break;


                default:
                    break;
            }

            return _out;
        }
    }
}

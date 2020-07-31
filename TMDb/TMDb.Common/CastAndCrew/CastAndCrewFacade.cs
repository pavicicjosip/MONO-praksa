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
        public ICACRole Role { get; set; }

        public CastAndCrewFacade(ICACFirstName firstName, ICACLastName lastName, ICACDateOfBirth dateOfBirth, ICACMovieID movieID, ICACRole role)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.MovieID = movieID;
            this.Role = role;
        }


        public string SQLStatement()
        {
            bool bFirstName = FirstName.Default();
            bool bLastName = LastName.Default();
            bool bDateOfBirth = DateOfBirth.Default();
            bool bMovieID = MovieID.Default();
            bool bRole = Role.Default();
            ICACSort iCACSort = new CACSort();

            string join = String.Format(" SELECT ROW_NUMBER() OVER ( {0} ) AS RowNum, cac.CastID, cac.FirstName, cac.LastName, cac.DateOfBirth, cac.Gender, cac.FileID, ccm.RoleInMovie FROM CastAndCrew cac, CCMovie ccm WHERE cac.CastID = ccm.CastID ", iCACSort.OrderBy());
            string _out = "";

            switch (bMovieID)
            {
                case true when bFirstName && bLastName && bDateOfBirth:
                    _out = join;
                    break;
                case true when bFirstName && bLastName && !bDateOfBirth:
                    _out = join + " AND " + DateOfBirth.WhereStatement();
                    break;
                case true when bFirstName && !bLastName && bDateOfBirth:
                    _out = join + " AND " + LastName.WhereStatement();
                    break;
                case true when bFirstName && !bLastName && !bDateOfBirth:
                    _out = join + " AND " + LastName.WhereStatement() + " AND " + DateOfBirth.WhereStatement();
                    break;
                case true when !bFirstName && bLastName && bDateOfBirth && bRole:
                    _out = join + " AND " + FirstName.WhereStatement();
                    break;
                case true when !bFirstName && bLastName && !bDateOfBirth:
                    _out = join + " AND " + FirstName.WhereStatement() + " AND " + DateOfBirth.WhereStatement();
                    break;
                case true when !bFirstName && !bLastName && bDateOfBirth:
                    _out = join + " AND " + FirstName.WhereStatement() + " AND " + LastName.WhereStatement();
                    break;
                case true when !bFirstName && !bLastName && !bDateOfBirth:
                    _out = join + " AND " + FirstName.WhereStatement() + " AND " + LastName.WhereStatement() + " AND " + DateOfBirth.WhereStatement();
                    break;
                case false when bFirstName && bLastName && bDateOfBirth && bRole:
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
                case false when !bRole:
                    _out = join + " AND " + MovieID.WhereStatement() + " AND " + Role.WhereStatement();
                    break;

                default:
                    break;
            }

            return _out;
        }
    }
}

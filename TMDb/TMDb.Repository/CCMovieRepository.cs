using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TMDb.Model;
using TMDb.Repository.Common;

namespace TMDb.Repository
{
    public class CCMovieRepository : ICCMovieRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);


        public async Task InsertAsync(CCMovie ccMovie)
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand("p_InsertCCMovie", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@MovieID", ccMovie.MovieID));
            command.Parameters.Add(new SqlParameter("@CastID", ccMovie.CastID));
            command.Parameters.Add(new SqlParameter("@RoleInMovie", ccMovie.RoleInMovie));
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

        public async Task DeleteAsync(Guid castID, Guid movieID, string roleInMovie)
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand("p_DeleteCCMovie", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@CastID", castID));
            command.Parameters.Add(new SqlParameter("@MovieID", movieID));
            command.Parameters.Add(new SqlParameter("@RoleInMovie", roleInMovie));
            await command.ExecuteNonQueryAsync();

            connection.Close();
        }

        public async Task<List<Movie>> SelectAsync(int pageStart, int pageEnd, Guid castID)
        {
            await connection.OpenAsync();
            List<Movie> _out = new List<Movie>();

            SqlCommand command = new SqlCommand(
                "SELECT * FROM (SELECT ROW_NUMBER() OVER ( ORDER BY Title ASC, YearOfProduction DESC ) " +
                "AS RowNum, m.MovieID, m.Title, m.YearOfProduction, m.CountryOfOrigin, m.Duration, m.PlotOutline, m.FileID " +
                "FROM CastAndCrew cac, CCMovie ccm, Movie m " +
                String.Format("WHERE m.MovieID = ccm.MovieID AND cac.CastID = '{0}') AS RowConstrainedResult ", castID) +
                String.Format("WHERE RowNum > {0} AND RowNum <= {1}", pageStart, pageEnd),
            connection);



            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _out.Add(new Movie(reader.GetGuid(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetGuid(7)));
                }
            }

            reader.Close();
            connection.Close();

            return _out;
        }

        public async Task<int> HowMany(Guid castID)
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(
                "SELECT  COUNT(m.MovieID) " +
                "FROM CastAndCrew cac, CCMovie ccm, Movie m " +
                String.Format("WHERE m.MovieID = ccm.MovieID AND cac.CastID = '{0}'", castID),
                connection);


            SqlDataReader reader = await command.ExecuteReaderAsync();
            reader.Read();
            int howMany = reader.GetInt32(0);
            reader.Close();
            connection.Close();
            return howMany;
        }

    }
}

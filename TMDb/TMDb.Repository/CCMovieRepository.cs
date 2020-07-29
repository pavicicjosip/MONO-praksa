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
        public async Task InsertCCMovieAsync(CCMovie ccMovie)
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
    }
}

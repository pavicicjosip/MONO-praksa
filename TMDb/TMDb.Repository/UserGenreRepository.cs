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
    public class UserGenreRepository : IUserGenreRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);
        public async Task InsertUserGenreAsync(UserGenre userGenre)
        {
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand("p_InsertUserGenre", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@AccountID", userGenre.AccountID));
            command.Parameters.Add(new SqlParameter("@GenreID", userGenre.GenreID));
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }
    }
}

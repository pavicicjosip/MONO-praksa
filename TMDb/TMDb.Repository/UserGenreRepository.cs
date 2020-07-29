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

        public async Task RemoveUserGenreAsync(Guid accountID, Guid genreID)
        {
            await connection.OpenAsync();
            var command = new SqlCommand(String.Format("DELETE FROM UserGenre WHERE AccountID = '{0}' AND GenreID = '{1}'", accountID, genreID), connection);
            await command.ExecuteReaderAsync();
            connection.Close();
        }

        public async Task<List<Genre>> SelectFavouriteGenreAsync(Guid accountID)
        {
            var list = new List<Genre>();
            var command = new SqlCommand(
                "SELECT g.GenreID, g.Title " +
                "FROM Genre g, UserGenre ug " +
                "WHERE g.GenreID = g.GenreID And ug.AccountID = @AccountID", connection);
            command.Parameters.AddWithValue("@AccountID", accountID);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new Genre(reader.GetGuid(0), reader.GetString(1)));
                }
            }

            reader.Close();
            connection.Close();
            return list;

        }
    }
}

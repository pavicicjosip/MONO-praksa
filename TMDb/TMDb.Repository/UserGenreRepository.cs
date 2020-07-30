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
using TMDb.Common;

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
                "WHERE ug.GenreID = g.GenreID And ug.AccountID = @AccountID", connection);
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
        public async Task<List<Movie>> SelectMoviesFromGenreAsync(int pageNumberStart, int pageNumberEnd, Guid accountID)
        {
            var list = new List<Movie>();
            var command = new SqlCommand(
                "SELECT * FROM " +
                "SELECT ROW_NUMBER() OVER ( ORDER BY AVG(CAST(r.NumberOfStars AS FLOAT)) DESC) AS RowNum, AVG(CAST(r.NumberOfStars AS FLOAT)) AS prosjek, m.MovieID, m.Title, m.YearOfProduction, m.CountryOfOrigin, m.Duration, m.FileID, m.PlotOutline" +
                "FROM Review r, Movie m, (" +
                "SELECT m.MovieID" +
                "FROM Movie m, Genre g, GenreMovie gm, Account ac, UserGenre ug" +
                "WHERE m.MovieID = gm.MovieID AND gm.GenreID = g.GenreID AND ug.AccountID = ac.AccountID AND ug.GenreID = g.GenreID AND ac.AccountID = 'E25B6289-5157-4B55-99D8-127CCB177481'" +
                "GROUP BY m.MovieID" +
                "EXCEPT" +
                "SELECT DISTINCT m.MovieID " +
                "FROM Movie m, Review r" +
                "WHERE r.AccountID = @AccountID AND r.MovieID = m.MovieID" +
                ") AS temp" +
                "WHERE m.MovieID = r.MovieID AND temp.MovieID = m.MovieID" +
                "GROUP BY m.MovieID, m.Title, m.YearOfProduction, m.CountryOfOrigin, m.Duration, m.FileID, m.PlotOutline) AS RowConstrainedResult" +
                "WHERE   RowNum > @PageNumberStart AND RowNum <= @PageNumberEnd" +
                "ORDER BY RowNum;",
                connection);

            command.Parameters.AddWithValue("@AccountID", accountID);
            command.Parameters.AddWithValue("@PageNumberStart", pageNumberStart);
            command.Parameters.AddWithValue("@PageNumberEnd", pageNumberEnd);

            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new Movie(reader.GetGuid(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetGuid(7)));
                }
            }

            reader.Close();
            connection.Close();
            return list;
        }
    }
}

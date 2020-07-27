
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Repository.Common;
using TMDb.Common;

namespace TMDb.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);
        public async Task<List<Movie>> SelectMovieByTitleAsync( int pageNumberStart, int pageNumberEnd, string whereStatement)
        {
            var list = new List<Movie>();
            var command = new SqlCommand(
                "SELECT * FROM " +
                " (SELECT ROW_NUMBER() OVER ( ORDER BY m.Title )  AS RowNum, * " +
                " FROM Movie m" +
                whereStatement + " ) AS RowConstrainedResult " +
                "WHERE   RowNum >= @PageNumberStart " +
                "AND RowNum <= @PageNumberEnd " +
                "ORDER BY RowNum;",
                connection);

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


        public async Task<List<Movie>> SelectMovieByTitleAsyncWith(int pageNumberStart, int pageNumberEnd, string whereStatement)
        {
            var list = new List<Movie>();
            var command = new SqlCommand(
                "SELECT * FROM " +
                " (SELECT ROW_NUMBER() OVER ( ORDER BY m.Title ) AS RowNum, m.MovieID, m.Title, m.YearOfProduction, m.CountryOfOrigin, m.Duration, m.PlotOutline, m.FileID " +
                " FROM Movie m, GenreMovie gm, Genre g " +
                whereStatement + " ) AS RowConstrainedResult " +
                "WHERE   RowNum >= @PageNumberStart " +
                "AND RowNum <= @PageNumberEnd " +
                "ORDER BY RowNum;",
                connection);

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



        public async Task<List<Movie>> SelectMovieByYearAsync(int yearOfProduction)
        {
            var list = new List<Movie>();
            var command = new SqlCommand("p_GetMovieByYear", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@YearOfProduction", yearOfProduction);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new Movie(reader.GetGuid(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetGuid(6)));
                }
            }

            reader.Close();
            connection.Close();
            return list;
        }

        public async Task<List<Movie>> GetMoviesByGenreAsync(string genreTitle)
        {
            var list = new List<Movie>();
            var command = new SqlCommand("p_GetMovieByGenre", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Title", genreTitle);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while(await reader.ReadAsync())
                {
                    list.Add(new Movie(reader.GetGuid(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetGuid(6)));
                }
            }

            reader.Close();
            connection.Close();
            return list;
        }

        public async Task<List<Movie>> GetMovieCastAndCrewAsync(string title)
        {
            var list = new List<Movie>();
            var command = new SqlCommand("p_GetMovieCastAndCrew", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Title", title);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new Movie(reader.GetGuid(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetGuid(6)));
                }
            }
            reader.Close();
            connection.Close();
            return list;
        }
    }
}
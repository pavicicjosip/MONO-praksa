
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

        public async Task<List<Movie>> SelectMovieAsync(int pageNumberStart, int pageNumberEnd, string whereStatement, string joinTables, string extraColumn, string groupBy, Sorting sort)
        {
            var list = new List<Movie>();
            string orderBy = sort.OrderBy();
            var command = new SqlCommand(
                "SELECT * FROM " +
                String.Format(" (SELECT ROW_NUMBER() OVER ( ORDER BY {0}) AS RowNum, m.MovieID, m.Title, m.YearOfProduction, m.CountryOfOrigin, m.Duration, m.PlotOutline, m.FileID {1}", orderBy, extraColumn) +
                String.Format(" FROM Movie m {0}", joinTables) +
                whereStatement + groupBy + " ) AS RowConstrainedResult " +
                "WHERE   RowNum > @PageNumberStart " +
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

        public async Task<int> SelectNumberOfResultsAsync(string whereStatement, string joinTables)
        {
            int returnValue;
            var command = new SqlCommand(
                "SELECT COUNT(DISTINCT m.MovieID) " +
                String.Format(" FROM Movie m {0}", joinTables) +
                whereStatement, connection);

            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();

            returnValue = reader.GetInt32(0);
            reader.Close();
            connection.Close();
            return returnValue;
        }

        public async Task<Movie> SelectMovieByIdAsync(Guid movieID)
        {
            Movie movie = null; 
            var command = new SqlCommand(String.Format("SELECT MovieID, Title, YearOfProduction, CountryOfOrigin, Duration, PlotOutline, FileID " +
                "FROM Movie WHERE MovieID = '{0}'", movieID), connection);

            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();

            movie = new Movie(reader.GetGuid(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetGuid(6));
            reader.Close();
            connection.Close();
            return movie;
        }

        public async Task InsertMovieAsync(Movie movie)
        {
            connection.Open();
            var command = new SqlCommand(String.Format("INSERT INTO Movie(Title, YearOfProduction, CountryOfOrigin, Duration, PlotOutline, FileID)" +
                " VALUES('{0}', {1}, '{2}', '{3}', '{4}', '{5}')", movie.Title, movie.YearOfProduction, movie.CountryOfOrigin, movie.Duration, movie.PlotOutline, movie.FileID), connection);
            await command.ExecuteReaderAsync();
            connection.Close();
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            connection.Open();
            var command = new SqlCommand(String.Format("UPDATE Movie SET Title = '{0}', YearOfProduction = {1}, CountryOfOrigin = '{2}', Duration = '{3}', PlotOutline = '{4}', FileID = '{5}' " +
                "WHERE MovieID = '{6}'", movie.Title, movie.YearOfProduction, movie.CountryOfOrigin, movie.Duration, movie.PlotOutline, movie.FileID, movie.MovieID), connection);
            await command.ExecuteReaderAsync();
            connection.Close();
        }

        public async Task DeleteMovieAsync(Guid movieID)
        {
            connection.Open();
            var command = new SqlCommand(String.Format("DELETE FROM Movie WHERE MovieID = '{0}'", movieID), connection);
            await command.ExecuteReaderAsync();
            connection.Close();
        }
    }
}
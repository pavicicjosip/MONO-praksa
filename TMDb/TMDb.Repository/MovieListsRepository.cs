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
    public class MovieListsRepository : IMovieListsRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);

        public async Task<List<MovieLists>> SelectMovieListsAsync(int pageNumberStart, int pageNumberEnd, Guid accountID)
        {
            var list = new List<MovieLists>();
            var command = new SqlCommand("SELECT ListName FROM ( SELECT ROW_NUMBER() OVER(ORDER BY ListName) AS RowNum, " +
                "ListName FROM MovieLists WHERE AccountID = @AccountID GROUP BY ListName ) AS RowConstrainedResult " +
                "WHERE RowNum > @PageNumberStart AND RowNum <= @PageNumberEnd", connection);
            command.Parameters.AddWithValue("@PageNumberStart", pageNumberStart);
            command.Parameters.AddWithValue("@PageNumberEnd", pageNumberEnd);
            command.Parameters.AddWithValue("@AccountID", accountID);
            connection.Open();

            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new MovieLists { ListName = reader.GetString(0) });
                }
            }
            else
            {
                list = null;
            }

            reader.Close();
            connection.Close();
            return list;
        }
        public async Task<int> SelectNumberOfResultsAsync(Guid accountID)
        {
            int returnValue;
            connection.Open();
            var command = new SqlCommand(String.Format("SELECT COUNT(DISTINCT ListName) " +
                "FROM MovieLists " +
                "WHERE AccountID = '{0}' GROUP BY AccountID", accountID), connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();
            if (reader.HasRows)
            {
                returnValue = reader.GetInt32(0);
            }
            else
            {
                returnValue = 0;
            }
            connection.Close();
            return returnValue;
        }
        public async Task InsertMovieListAsync(MovieLists movieList)
        {
            connection.Open();
            var command = new SqlCommand(String.Format("INSERT INTO MovieLists" +
                " VALUES('{0}', '{1}', '{2}')", movieList.ListName, movieList.MovieID, movieList.AccountID), connection);
            await command.ExecuteReaderAsync();
            connection.Close();
        }
        public async Task DeleteMovieListsAsync(string whereStatment)
        {
            connection.Open();
            var commandDelete = new SqlCommand(String.Format("DELETE FROM MovieLists WHERE {0}", whereStatment), connection);
            await commandDelete.ExecuteReaderAsync();
            connection.Close();
        }
    }
}

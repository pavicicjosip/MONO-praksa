using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Common;
using TMDb.Model;
using TMDb.Repository.Common;

namespace TMDb.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);
        public async Task<List<Review>> SelectReviewsAsync(int pageNumberStart, int pageNumberEnd, string whereStatement, Sorting sort)
        {
            var list = new List<Review>();
            string orderBy = sort.OrderBy();
            var command = new SqlCommand(
                "SELECT * FROM " +
                String.Format("( SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNum", sort.OrderBy()) +
                ", r.ReviewID, r.NumberOfStars, r.Comment, r.DateAndTime, acc.Username, r.MovieID " +
                "FROM Review r, Account acc " +
                String.Format("WHERE r.AccountID = acc.AccountID {0}) AS RowConstrainedResult ", whereStatement) +
                " WHERE RowNum > @PageNumberStart AND RowNum <= @PageNumberEnd ORDER BY RowNum", connection);
            
            command.Parameters.AddWithValue("@PageNumberStart", pageNumberStart);
            command.Parameters.AddWithValue("@PageNumberEnd", pageNumberEnd);
            connection.Open();
            
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new Review(reader.GetGuid(1), reader.GetInt32(2), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetGuid(6)));
                }
            }

            reader.Close();
            connection.Close();
            return list;
        }

        public async Task<int> SelectNumberOfResultsAsync(string whereStatement)
        {
            int returnValue;
            var command = new SqlCommand(
                "SELECT COUNT(r.ReviewID) FROM Review r, Account acc WHERE r.AccountID = acc.AccountID " + whereStatement, connection);

            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();

            returnValue = reader.GetInt32(0);
            reader.Close();
            connection.Close();
            return returnValue;
        }

        public async Task InsertReviewAsync(Review review, Guid accountID)
        {
            connection.Open();
            var command = new SqlCommand(String.Format("INSERT INTO Review(NumberOfStars, Comment, DateAndTime, AccountID, MovieID)" +
                " VALUES({0}, '{1}', GETDATE(), '{2}', '{3}')", review.NumberOfStars, review.Comment, accountID, review.MovieID), connection);
            await command.ExecuteReaderAsync();
            connection.Close();
        }

        public async Task UpdateReviewAsync(Review review)
        {
            connection.Open();
            var command = new SqlCommand(String.Format("UPDATE Review SET NumberOfStars = {0}, Comment = '{1}' " +
                "WHERE ReviewID = '{2}'",review.NumberOfStars, review.Comment, review.ReviewID), connection);
            await command.ExecuteReaderAsync();
            connection.Close();
        }

        public async Task DeleteReviewAsync(Guid reviewID)
        {
            connection.Open();
            var commandDelete = new SqlCommand(String.Format("DELETE FROM Review WHERE ReviewID = '{0}'", reviewID), connection);
            await commandDelete.ExecuteReaderAsync();
            connection.Close();
        }
    }
}
